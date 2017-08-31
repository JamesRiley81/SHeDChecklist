using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SHeDChecklist
{
    public partial class AllStaffChecklist : Form
    {
        public AllStaffChecklist()
        {
            InitializeComponent();
        }
        Database d;
        private void AllStaffChecklist_Load(object sender, EventArgs e)
        {
            d = new Database();
            foreach (string s in d.TodaysStudents())
            {
                if (s != "Admin")
                    staffList.Items.Add(s);
            }
        }
        private void UpdateInputs()
        {
            d = new Database();        
            List<string> items = d.GetSolotasks(staffList.SelectedItem.ToString());
            taskList.Items.Clear();
            foreach (string i in items)
            {
                taskList.Items.Add(i);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (staffList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your initials from the dropdown list.");
                return;
            }
            else if (taskList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task from the dropdown list.");
                return;
            }
            else
            {
                string output = d.AddAllStaffCheckList(staffList.SelectedItem.ToString(), taskList.SelectedItem.ToString());
                descriptionTextBox.Text = output;
                ClearInputs();
            }
        }
        private void ClearInputs()
        {
            taskList.Text = "";
            taskList.Items.Clear();
            UpdateInputs();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void staffList_SelectedIndexChanged(object sender, EventArgs e)
        {         
            List<string> items = d.GetSolotasks(staffList.SelectedItem.ToString());
            taskList.Items.Clear();
            foreach (string i in items)
            {
                taskList.Items.Add(i);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void taskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var c = new ChecklistItem();
            c = d.GetSoloTaskDescription(taskList.SelectedItem.ToString());
            descriptionTextBox.Text = c.description;
            if (c.documentation != string.Empty)
            {
                linkTextLabel.Text = c.documentation;
                documentationLinkLabel.Text = "View documentation.";
            }
            else
                documentationLinkLabel.Text = string.Empty;
        }

        private void documentationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = linkTextLabel.Text;
            Process.Start(link);
        }
    }
}
