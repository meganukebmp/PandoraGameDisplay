using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pandorascreen_game_display
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        public const bool debug = false; // debugger boolean
        public const string configPath = "PandoraGameDisplay"; // main path for configuration an images
        public const string imagesPath = "images"; // path within the main path for image storage
        public const string configFileName = "PandoraGameDisplay.config"; // config filename within the main path
        public const string defaultConfigValues = "displayExecutablePath=\n" + "tracking=";
        public string displayExecutablePath;
        public string tracking;
        public string currentTracking;
        public List<string> trackedProcesses = new List<string>();

        // on load
        private void FormMain_Load(object sender, EventArgs e)
        {
            CheckPaths();
            ReadConfig();
            PickDisplayExecutable();
            ReloadProcessList(false);
            timerUpdate.Start();
        }

        // check configuration paths and create them if necessary
        public void CheckPaths()
        {
            // if config path missing create it
            if (!System.IO.Directory.Exists(configPath))
            {
                if (debug) { Console.WriteLine("Config path missing! Creating..."); } // debug
                System.IO.Directory.CreateDirectory(configPath); // create path
            }

            // if images path missing create it
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(configPath, imagesPath)))
            {
                if (debug) { Console.WriteLine("Images path missing! Creating..."); } // debug
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(configPath, imagesPath)); // create path
            }

            // if config file missing create it
            if (!System.IO.File.Exists(System.IO.Path.Combine(configPath, configFileName)))
            {
                if (debug) { Console.WriteLine("Config file missing! Creating..."); } // debug
                System.IO.File.WriteAllText(System.IO.Path.Combine(configPath, configFileName), defaultConfigValues);
            }
        }

        private void PickDisplayExecutable()
        {
            // if the display executable path is none, do a thing.
            if (displayExecutablePath == "")
            {   
                MessageBox.Show("Your display application is not detected. Please select the BitFenix™ ICON application exe", "Pick file");
                // create new file dialog made for picking EXE files
                OpenFileDialog displayExecutableDialog = new OpenFileDialog();
                displayExecutableDialog.Filter = "Executable Files|*.exe; *.bat";
                displayExecutableDialog.Title = "Select the BitFenix™ ICON application exe";

                // show the file picker. On selection OK fire event else shutdown
                if (displayExecutableDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {   
                    UpdateConfig("displayExecutablePath", displayExecutableDialog.FileName); // update the config key
                    ReadConfig(); // reload the config
                }
                else
                {   
                    // handle cancel or othe errors
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, "FILE SELECTION ERROR! Program will now close.", "ERROR");
                    Application.Exit();
                }
            }
        }

        // read and parse the config file
        public void ReadConfig()
        {
            string[] configLines = System.IO.File.ReadAllLines(System.IO.Path.Combine(configPath, configFileName)); // array of every single line in the config file
            // iterate through config file lines
            foreach (string line in configLines)
            {
                // if its the executable path line
                if (line.StartsWith("displayExecutablePath="))
                {
                    displayExecutablePath = Regex.Replace(line, "displayExecutablePath=", ""); //set the executable path to the one in the config
                }
                // if its the tracking line
                else if (line.StartsWith("tracking=")) {
                    tracking = Regex.Replace(line, "tracking=", ""); //set the tracking string to the one in the config
                    // if not empty
                    if (tracking.Length > 0)
                    {
                        trackedProcesses = tracking.Split(',').ToList(); // turn string to list
                    }
                }
            }
        }

        // read parse and update the config file
        public void UpdateConfig(string key, string value)
        {
            string[] configLines = System.IO.File.ReadAllLines(System.IO.Path.Combine(configPath, configFileName)); // array of every single line in the config file
            // iterate through config file lines
            for ( int i = 0; i < configLines.Length; i++)
            {
                string line = configLines[i]; // bind

                // if a line starts with the selected key
                if (line.StartsWith(key + "="))
                {
                    configLines[i] = key + "=" + value; // update the key with the new value
                }
            }
            System.IO.File.WriteAllLines(System.IO.Path.Combine(configPath, configFileName), configLines); // rewrite the file with the new data
        }

        // gets all running processes and lists them
        public void ReloadProcessList(bool allProc)
        {
            Process[] processes = Process.GetProcesses(); // get an array of all processes
            lstProcesses.Items.Clear(); // remove all items from listbox collection

            // iterate through all processes
            foreach (Process item in processes)
            {   
                // if allProc false show only programs that have a window title
                if (!allProc && item.MainWindowTitle != "")
                {
                    lstProcesses.Items.Add(item.ProcessName); // push item to listbox collection
                }
                // if not show all
                else if (allProc)
                {
                    lstProcesses.Items.Add(item.ProcessName); // push item to listbox collection
                }
            }
        }

        // show all checkbox state changed event
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            lstProcesses.ClearSelected();
            // if checked reload showing all
            if (chkShowAll.Checked)
            {
                ReloadProcessList(true);
            }
            // if not reload showing only some
            else
            {
                ReloadProcessList(false);
            }
        }

        // fired when the user selects an item in the listbox
        private void lstProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {   
            lblSelected.Text = lstProcesses.GetItemText(lstProcesses.SelectedItem); // change selected item label to the selected item

            // if the selected object is value enable the control
            if (lstProcesses.GetItemText(lstProcesses.SelectedItem) != "" )
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        // fired when the add button is pressed
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTracker(lstProcesses.GetItemText(lstProcesses.SelectedItem));
        }

        // add an item to the tracker with an image
        public void AddTracker(string procName)
        {

            RemoveTracker(procName); // remove the tracker before adding it

            MessageBox.Show("Please select an image to display for " + procName, "Select Image");
            // new file picker dialog configuration
            OpenFileDialog displayImageDialog = new OpenFileDialog();
            displayImageDialog.Filter = "Image Files|*.png; *.jpg; *.jpeg; *.gif";
            displayImageDialog.Title = "Select display image for " + procName;
            // show the file picker. On selection OK fire event
            if (displayImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   
                // copy the image to the local dir
                System.IO.File.Copy(displayImageDialog.FileName, System.IO.Path.Combine(System.IO.Path.Combine(configPath, imagesPath), procName + System.IO.Path.GetExtension(displayImageDialog.SafeFileName)));
                trackedProcesses.Add(procName); // add the item to the list
                tracking = string.Join(",",trackedProcesses); // update tracking string
                UpdateConfig("tracking", tracking); // update the config with the added tracker
            }
            // stop void execution and do nothing.
            else
            {
                MessageBox.Show("Error setting image! Tracker removed.", "Error");
                return;
            }
        }

        // removes a tracker and image
        public void RemoveTracker(string procName)
        {   
            // remove item from tracker
            trackedProcesses.Remove(procName);
            // gets all files in a directory that match the item name
            string[] images = System.IO.Directory.GetFiles(System.IO.Path.Combine(configPath, imagesPath), procName + ".*");
            // removes those items
            foreach (string image in images)
            {
                System.IO.File.Delete(image); // delete the image
            }
            tracking = string.Join(",", trackedProcesses); // update tracking string
        }

        // fired when the view tracked button is pressed
        private void btnViewTracked_Click(object sender, EventArgs e)
        {
            FormTracked formTracked = new FormTracked();
            formTracked.Show();
        }

        // On double click of tray icon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        // don't close form on X button press. Show a balloon notification and minimize to tray
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1);
                this.Hide();
                e.Cancel = true;
            }
        }

        // exit button
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // every tick
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (trackedProcesses.Count > 0)
            {
                foreach (string item in trackedProcesses)
                {
                    if (Process.GetProcessesByName(item).Length > 0)
                    {
                        if (debug) { Console.WriteLine("found " + item); } // debug

                        if (currentTracking != item)
                        {
                            Process pandoraProcess = new Process();
                            pandoraProcess.StartInfo.UseShellExecute = false;
                            pandoraProcess.StartInfo.FileName = displayExecutablePath;
                            pandoraProcess.StartInfo.Arguments = '"' + System.IO.Directory.GetFiles(System.IO.Path.Combine(configPath, imagesPath), item + ".*")[0] + '"';
                            pandoraProcess.StartInfo.CreateNoWindow = true;
                            pandoraProcess.Start();
                            currentTracking = item;
                        }
                        break;
                    }
                    
                }
            }
        }
    }
}
