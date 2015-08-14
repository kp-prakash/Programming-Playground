namespace Style
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.StyleCop;

    /// <summary>
    /// Simple example for running StyleCop environment.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The designer file extension.
        /// </summary>
        private const string DesignerFileExtension = ".designer.cs";

        /// <summary>
        /// Mains entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            if (ValidateCommandLineParameters(args))
            {
                return;
            }

            string projectPath = args[0];
            IEnumerable<string> files = GetAllFileNames(projectPath);
            var console = new StyleCopConsole(null, false, null, null, true);
            var project = new CodeProject(0, projectPath, new Configuration(null));

            AddSourceCodeFiles(files, console, project);
            SubscribeStyleCopConsoleEvents(console);
            StartStyleCopAnalysis(console, project);
            UnsubscribeStyleCopConsoleEvents(console);
            DisposeStyleCopConsole(console);
        }

        /// <summary>
        /// Adds the source code files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="console">The console.</param>
        /// <param name="project">The project.</param>
        private static void AddSourceCodeFiles(
            IEnumerable<string> files,
            StyleCopConsole console,
            CodeProject project)
        {
            foreach (var file in files)
            {
                console.Core.Environment.AddSourceCode(project, file, null);
            }
        }

        /// <summary>
        /// Disposes the style cop console.
        /// </summary>
        /// <param name="console">The console.</param>
        private static void DisposeStyleCopConsole(StyleCopConsole console)
        {
            console.Dispose();
        }

        /// <summary>
        /// Gets all file names.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <returns>List of files.</returns>
        private static IEnumerable<string> GetAllFileNames(string projectPath)
        {
            IList<string> files = new List<string>();
            if (string.IsNullOrWhiteSpace(projectPath) || !Directory.Exists(projectPath))
            {
                return files;
            }

            var directoryInfo = new DirectoryInfo(projectPath);
            ProcessDirectories(files, directoryInfo);
            return files;
        }

        /// <summary>
        /// Handles generated output.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OutputEventArgs"/> instance containing the event data.</param>
        private static void OnOutputGenerated(
            object sender,
            OutputEventArgs e)
        {
            //Console.WriteLine(e.Output);
        }

        /// <summary>
        /// Handles encountered violations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ViolationEventArgs"/> instance containing the event data.</param>
        private static void OnViolationEncountered(
            object sender,
            ViolationEventArgs e)
        {
            string elementName = (null != e.Element) ? e.Element.FullyQualifiedName : string.Empty;
            Console.WriteLine(
                "{0},{1},{2},{3},{4},{5}",
                e.Violation.Rule.CheckId,
                e.Message.Replace(',', ' '),
                e.SourceCode.Path.Replace(',', ' '),
                e.Violation.Rule.Namespace.Replace(',', ' '),
                elementName.Replace(',', ' '),
                e.LineNumber);
        }

        /// <summary>
        /// Processes the directories.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="directoryInfo">The directory info.</param>
        private static void ProcessDirectories(IList<string> files, DirectoryInfo directoryInfo)
        {
            var directories = directoryInfo.GetDirectories();
            if (directories.Length > 0)
            {
                foreach (var subDirectoryInfo in directoryInfo.GetDirectories())
                {
                    ProcessDirectories(files, subDirectoryInfo);
                }
            }

            ProcessFiles(files, directoryInfo);
        }

        /// <summary>
        /// Processes the files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="directoryInfo">The directory info.</param>
        private static void ProcessFiles(IList<string> files, DirectoryInfo directoryInfo)
        {
            var fileInfos = directoryInfo.GetFiles("*.cs");
            foreach (var fileInfo in fileInfos)
            {
                if (!fileInfo.FullName.ToLower().EndsWith(DesignerFileExtension))
                {
                    files.Add(fileInfo.FullName);
                }
            }
        }

        /// <summary>
        /// Starts the style cop analysis.
        /// </summary>
        /// <param name="console">The console.</param>
        /// <param name="project">The project.</param>
        private static void StartStyleCopAnalysis(StyleCopConsole console, CodeProject project)
        {
            console.Start(new[] { project }, true);
        }

        /// <summary>
        /// Subscribes the style cop console events.
        /// </summary>
        /// <param name="console">The console.</param>
        private static void SubscribeStyleCopConsoleEvents(StyleCopConsole console)
        {
            console.OutputGenerated += OnOutputGenerated;
            console.ViolationEncountered += OnViolationEncountered;
        }

        /// <summary>
        /// Unsubscribe style cop console events.
        /// </summary>
        /// <param name="console">The console.</param>
        private static void UnsubscribeStyleCopConsoleEvents(StyleCopConsole console)
        {
            console.OutputGenerated -= OnOutputGenerated;
            console.ViolationEncountered -= OnViolationEncountered;
        }

        /// <summary> Validates the command line parameters. </summary>
        /// <param name="args"> The command line argument. </param>
        /// <returns> True if argument is valid and false otherwise. </returns>
        private static bool ValidateCommandLineParameters(string[] args)
        {
            if (null == args || args.Length == 0 || args.Length > 1)
            {
                Console.WriteLine(
                    "Please pass on the C# project directory path as the only command "
                    + "line parameter. This is the directory where your .csproj file exists.");
                return true;
            }

            return false;
        }
    }
}