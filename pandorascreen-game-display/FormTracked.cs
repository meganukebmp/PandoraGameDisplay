using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pandorascreen_game_display
{
    public partial class FormTracked : Form
    {
        public FormTracked()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["FormMain"];

        private void FormTracked_Load(object sender, EventArgs e)
        {
            UpdateTrackerList();
        }

        // update display on list selection
        private void lstTracked_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelectedTracked.Text = lstTracked.GetItemText(lstTracked.SelectedItem);
            if (lstTracked.GetItemText(lstTracked.SelectedItem) != "")
            {
                btnRemoveTracked.Enabled = true;
                btnTopPriority.Enabled = true;
            }
            else
            {
                btnRemoveTracked.Enabled = false;
                btnTopPriority.Enabled = false;
            }
        }

        private void btnRemoveTracked_Click(object sender, EventArgs e)
        {
            ((FormMain)f).RemoveTracker(lstTracked.GetItemText(lstTracked.SelectedItem));
            ((FormMain)f).UpdateConfig("tracking", ((FormMain)f).tracking);
            UpdateTrackerList();
        }

        private void UpdateTrackerList()
        {
            lstTracked.ClearSelected();
            lstTracked.Items.Clear(); // clear the list

            // for each item add to the list
            foreach (string item in ((FormMain)f).trackedProcesses)
            {
                lstTracked.Items.Add(item);
            }
        }

        private void btnTopPriority_Click(object sender, EventArgs e)
        {
            
            ((FormMain)f).trackedProcesses.Remove(lstTracked.GetItemText(lstTracked.SelectedItem));
            ((FormMain)f).UpdateConfig("tracking", ((FormMain)f).tracking);

            ((FormMain)f).trackedProcesses.Insert(0, lstTracked.GetItemText(lstTracked.SelectedItem)); // add the item to the list

            ((FormMain)f).tracking = string.Join(",", ((FormMain)f).trackedProcesses); // update tracking string
            ((FormMain)f).UpdateConfig("tracking", ((FormMain)f).tracking); // update the config with the added tracker

            lstTracked.ClearSelected();
            UpdateTrackerList();
        }
    }
}
