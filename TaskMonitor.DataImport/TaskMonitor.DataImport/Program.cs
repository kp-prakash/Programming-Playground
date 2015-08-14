namespace TaskMonitor.DataImport
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using SystemTask = System.Threading.Tasks.Task;

    internal class Program
    {
        private const string AnteMeridian = " AM ";
        private const string CompletedDataImport = "Completed data import!";
        private const string CsvExtension = ".csv";
        private const string DataImportInProgress = "Data import in progress...";
        private const string InvalidRootFolderPath = "Invalid root folder path!";
        private const string PostMeridian = " PM ";
        private const string PressAnyKeyToExit = "Press any key to exit...";
        private const string StartingDataImport = "Starting data import!";
        private const string TimeTakenToProcessFile = "Time taken to process file {0} : {1} ms";
        private const string TotalTimeTakenForProcessing = "Total time taken for processing : {0} ms";
        private static readonly List<SystemTask> ParallelTasks = new List<SystemTask>();

        public static void Main(string[] args)
        {
            string rootFolderPath = args.Length > 0
                ? args[0]
                : Settings.Default.RootFolderPath;
            if (Directory.Exists(rootFolderPath))
            {
                Console.WriteLine(StartingDataImport);
                var stopWatch = Stopwatch.StartNew();
                ProcessFolders(rootFolderPath);
                Console.WriteLine(DataImportInProgress);
                SystemTask.WaitAll(ParallelTasks.ToArray());
                stopWatch.Stop();
                Console.WriteLine(CompletedDataImport);
                var timeTaken = stopWatch.ElapsedMilliseconds;
                Console.WriteLine(TotalTimeTakenForProcessing, timeTaken);
            }
            else
            {
                Console.WriteLine(InvalidRootFolderPath);
            }
            Console.WriteLine(PressAnyKeyToExit);
            Console.ReadKey();
        }

        private static IEnumerable<Task> GetTasksToSave<T>(List<T> validTaskDetails, string dateTimeFormat = null)
        {
            var tasksToSave = new List<Task>();
            Task lastTask = null;
            for (int i = 0; i < validTaskDetails.Count; i++)
            {
                var newTask = new Task();

                string dateTimeFromLog = string.Empty;
                string description = string.Empty;
                if (validTaskDetails[i] is string[])
                {
                    // This is used to process the comma separated entry
                    var detail = validTaskDetails[i] as string[];
                    dateTimeFromLog = detail[0] + " " + detail[2];
                    description = detail[3];
                }
                else if (validTaskDetails[i] is string)
                {
                    // This is use to process the entry from text file
                    var detail = validTaskDetails[i] as string;
                    var isForeNoon = detail.Contains(AnteMeridian);
                    var isAfterNoon = detail.Contains(PostMeridian);
                    if (isForeNoon)
                    {
                        dateTimeFromLog = detail.Substring(0, detail.IndexOf(AnteMeridian, StringComparison.Ordinal) + 4).Trim();
                        description = detail.Substring(detail.IndexOf(AnteMeridian, StringComparison.Ordinal) + 4).Trim();
                    }
                    else if (isAfterNoon)
                    {
                        dateTimeFromLog = detail.Substring(0, detail.IndexOf(PostMeridian, StringComparison.Ordinal) + 4).Trim();
                        description = detail.Substring(detail.IndexOf(PostMeridian, StringComparison.Ordinal) + 4).Trim();
                    }
                }

                DateTime dateTime;
                bool isParsed = (string.IsNullOrEmpty(dateTimeFormat))
                    ? DateTime.TryParse(dateTimeFromLog, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)
                    : DateTime.TryParseExact(dateTimeFromLog, dateTimeFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dateTime);
                if (isParsed)
                {
                    newTask.StartDate = dateTime.ToUniversalTime();
                    // Get left of 200 characters.
                    var length = (description.Length > 200) ? 200 : description.Length;
                    newTask.Description = description.Substring(0, length);
                    newTask.CreatedDate = DateTime.UtcNow;
                    if (lastTask == null)
                    {
                        lastTask = newTask;
                        continue;
                    }

                    lastTask.EndDate = newTask.StartDate;
                    if (lastTask.Description != newTask.Description)
                    {
                        tasksToSave.Add(lastTask);
                        lastTask = newTask;
                        if (i == validTaskDetails.Count - 1)
                        {
                            // This is the last task to be added.
                            // Add 30 mins and close this task!
                            // Logically this should flow into the next day if this task is continued!
                            // Let me handle that later! :-)
                            newTask.EndDate = newTask.StartDate.AddMinutes(30);
                            tasksToSave.Add(newTask);
                        }
                    }
                }
            }
            return tasksToSave;
        }

        private static void ProcessFile(FileInfo fileInfo)
        {
            // TODO: Analyze the impact of running the below code in Task vs. Deep down the call hierarchy!
            ParallelTasks.Add(SystemTask.Factory.StartNew(() =>
            {
                var stopWatch = Stopwatch.StartNew();
                string[] taskDetails = File.ReadAllLines(fileInfo.FullName);
                if (fileInfo.Extension.ToLowerInvariant().Equals(CsvExtension))
                {
                    ProcessTaskDetailsFromCsv(taskDetails);
                }
                else
                {
                    ProcessTaskDetailsFromTxt(taskDetails);
                }
                stopWatch.Stop();
                var elapsedTime = stopWatch.ElapsedMilliseconds;
                Console.WriteLine(TimeTakenToProcessFile, fileInfo.Name, elapsedTime);
            }));
        }

        private static void ProcessFolders(string rootFolderPath)
        {
            var rootDirectoryInfo = new DirectoryInfo(rootFolderPath);
            ProcessSubDirectories(rootDirectoryInfo);
        }

        private static void ProcessSubDirectories(DirectoryInfo rootDirectoryInfo)
        {
            foreach (var directoryInfo in rootDirectoryInfo.GetDirectories())
            {
                var fileInfos = directoryInfo.GetFiles();
                foreach (var fileInfo in fileInfos)
                {
                    ProcessFile(fileInfo);
                }

                var directories = directoryInfo.GetDirectories();
                foreach (var directory in directories)
                {
                    ProcessSubDirectories(directory);
                }
            }
        }

        private static void ProcessTaskDetailsFromCsv(IEnumerable<string> taskDetails)
        {
            var validTaskDetails
                = taskDetails
                    .Select(taskDetail => taskDetail.Split(','))
                    .Where(parts => parts.Length == 4)
                    .ToList();
            ProcessValidTaskDetails(validTaskDetails);
        }

        private static void ProcessTaskDetailsFromTxt(IEnumerable<string> taskDetails)
        {
            var validTaskDetails
                = taskDetails
                    .Where(taskDetail => !taskDetail.StartsWith("*"))
                    .ToList();
            ProcessValidTaskDetailsTxt(validTaskDetails);
        }

        private static void ProcessValidTaskDetails(List<string[]> validTaskDetails)
        {
            IEnumerable<Task> tasksToSave = GetTasksToSave(validTaskDetails, "dd-MM-yy h:mm:ss tt");
            SaveTasks(tasksToSave);
        }

        private static void ProcessValidTaskDetailsTxt(List<string> validTaskDetails)
        {
            IEnumerable<Task> tasksToSave = GetTasksToSave(validTaskDetails);
            SaveTasks(tasksToSave);
        }

        private static void SaveTasks(IEnumerable<Task> tasksToSave)
        {
            var taskMonitorContext = new TaskMonitorContext();
            taskMonitorContext.Tasks.AddRange(tasksToSave);
            taskMonitorContext.SaveChangesAsync();
        }
    }
}