#region References

using System;
using System.IO;
using System.Windows.Forms;
using TaskMonitor.Properties;

#endregion References

namespace TaskMonitor
{
    /// <summary>
    /// Task Monitor.
    /// </summary>
    public partial class TaskMonitor : Form
    {
        private const string Separator = ",";

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskMonitor"/> class.
        /// </summary>
        public TaskMonitor()
        {
            InitializeComponent();
            lblTitle.Text = Settings.Default.Label.Trim();
            txtTask.Text = ReadLastTask();
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        private static void CreateDirectory(string folderName)
        {
            if (Directory.Exists(folderName)) return;
            Directory.CreateDirectory(folderName);
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="task">The task.</param>
        private static void WriteToFile(string fileName, string task)
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

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            ValidateAndClose(false);
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTask.Text == String.Empty)
                {
                    MessageBox.Show("Task cannot be empty!", Settings.Default.MessageBoxTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtTask.Select();
                    return;
                }
                string folderName = Settings.Default.Path.Trim()
                    + DateTime.Now.ToString(Settings.Default.FolderFormat);
                string fileName = string.Format(@"{0}\{1}.csv",
                    folderName, DateTime.Now.ToString(Settings.Default.FileNameFormat));
                CreateDirectory(folderName);
                WriteToFile(fileName, txtTask.Text.Trim());
                Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(string.Format("{0} or the drive is not present!", ex.Message),
                    Settings.Default.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Settings.Default.MessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the TaskMonitor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void TaskMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateAndClose(true);
        }

        /// <summary>
        /// Onload Event.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">EventArgs</param>
        private void TaskMonitor_Load(object sender, EventArgs e)
        {
            txtTask.Focus();
        }

        /// <summary>
        /// Validates the and close.
        /// </summary>
        /// <param name="formCloseButton">if set to <c>true</c> [form close button].</param>
        private void ValidateAndClose(bool formCloseButton)
        {
            if (formCloseButton) return;
            DialogResult Result;
            Result = MessageBox.Show("Are you sure, you want to close the screen?",
                Settings.Default.MessageBoxTitle,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}