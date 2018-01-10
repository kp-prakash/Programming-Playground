namespace TaskMonitor.Console
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Console = System.Console;
    using System;
    using System.IO;
    using TaskMonitor.Console.Properties;

    internal class Program
    {
        private const string Separator = ",";

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        private static void CreateDirectory(string folderName)
        {
            if (Directory.Exists(folderName)) return;
            Directory.CreateDirectory(folderName);
        }

        private static void Main()
        {
            Task.Factory.StartNew(RefreshTitle);

            while (true)
            {
                Console.ForegroundColor = Settings.Default.MessageForeground;
                string oldTask = ReadLastTask();
                Console.WriteLine(Settings.Default.Label);
                Console.ForegroundColor = Settings.Default.TaskForegroundColor;
                Console.WriteLine(oldTask);
                Console.ForegroundColor = Settings.Default.MessageForeground;
                Console.WriteLine(Settings.Default.NewTaskMessage);
                Console.ForegroundColor = Settings.Default.TaskForegroundColor;
                string newTask = Console.ReadLine();
                if (string.IsNullOrEmpty(newTask))
                {
                    newTask = oldTask;
                }

                SaveTask(newTask);
                Console.Clear();
                Console.ForegroundColor = Settings.Default.MessageForeground;
                Console.Write(Settings.Default.SavingMessage);
                for (int i = 0; i < 3; ++i)
                {
                    Thread.Sleep(Settings.Default.SaveDelay);
                    Console.Write('.');
                }
                Console.WriteLine();
                Console.WriteLine(Settings.Default.SaveMessage);
                Thread.Sleep(Settings.Default.PostSaveDelay);
                Console.Clear();
            }
        }

        private static void RefreshTitle()
        {
            while (true)
            {
                Console.Title = string.Format(
                    "Date & Time : {0} - Task Monitor © {1} Srihari Sridharan",
                    DateTime.Now,
                    DateTime.Now.Year);
                Thread.Sleep(Settings.Default.TimerRefreshInterval);
            }
        }

        /// <summary>
        /// Reads the last task.
        /// </summary>
        /// <returns></returns>
        private static string ReadLastTask()
        {
            string lastTask = string.Empty;
            string lastTaskFileName = Settings.Default.LastTaskFileName;
            if (File.Exists(lastTaskFileName))
            {
                lastTask = File.ReadAllText(lastTaskFileName);
            }
            return lastTask;
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static void SaveTask(string task)
        {
            string folderName = AssemblyDirectory.Trim() + @"\"
                    + DateTime.Now.ToString(Settings.Default.FolderFormat);
            string fileName = string.Format(@"{0}\{1}.csv",
                folderName, DateTime.Now.ToString(Settings.Default.FileNameFormat));
            CreateDirectory(folderName);
            WriteToFile(fileName, task.Trim());
        }

        /// <summary>
        /// Writes the task to be reloaded next time.
        /// </summary>
        /// <param name="task">The task.</param>
        private static void WriteTask(string task)
        {
            string lastTaskFileName = Settings.Default.LastTaskFileName;
            if (File.Exists(lastTaskFileName))
            {
                File.Delete(lastTaskFileName);
            }
            StreamWriter binWriter = File.CreateText(lastTaskFileName);
            binWriter.Write(task);
            binWriter.Close();
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="task">The task.</param>
        private static void WriteToFile(string fileName, string task)
        {
            try
            {
                StreamWriter sw = (!File.Exists(fileName))
                    ? File.CreateText(fileName) : File.AppendText(fileName);
                sw.WriteLine(DateTime.Now.ToString(Settings.Default.DateFormat) + Separator
                    + DateTime.Now.ToString(Settings.Default.DayFormat) + Separator
                    + DateTime.Now.ToString(Settings.Default.TimeFormat) + Separator
                    + task);
                sw.Close();
                WriteTask(task);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
                Console.WriteLine(Settings.Default.PressAnyKeyToContinue);
                Console.ReadKey();
            }
        }
    }
}
