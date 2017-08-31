using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace SHeDChecklist
{
    public partial class ATCChecklist : Form
    {
        private Database d;
        private ChecklistItem c;
        ImageList theImage = new ImageList();

        public ATCChecklist()
        {
            InitializeComponent();
        }
        
        private void ATCChecklist_Load(object sender, EventArgs e)
        {            
            UpdateInputs();
            foreach (string s in d.TodaysStudents())
            {
                staffList.Items.Add(s);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string output;
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
                output = d.AddATCEntry(staffList.SelectedItem.ToString(), taskList.SelectedItem.ToString());
                descriptionTextBox.Text = output;
                ClearInputs();
            }
        }
        private void UpdateInputs()
        {
            d = new Database();
            foreach (ChecklistItem c in d.GetATCTasks())
            {
                taskList.Items.Add(c.name);
            }
            if (taskList.Items.Count == 0)
            {
                CompleteImages image = new CompleteImages();
                image = theImage.GetImage();
                completePictureBox.Visible = true;
                completePictureBox.Image = Image.FromFile(image.lText);
                outputTextBox.Visible = true;
                outputTextBox.Text = image.tText;
                saveButton.Visible = false;
            }
        }
        private void ClearInputs()
        {
            taskList.Text = "";
            documentationLinkLabel.Text = string.Empty;
            taskList.Items.Clear();
            UpdateInputs();
        }

        private void taskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            linkTextLabel.Text = string.Empty;
            documentationLinkLabel.Text = string.Empty;
            c = new ChecklistItem();
            c = d.GetATCTaskDescription(taskList.SelectedItem.ToString());
            descriptionTextBox.Text = c.description;
            linkTextLabel.Text = c.documentation;
            if (c.documentation != string.Empty)
                documentationLinkLabel.Text = "Documentation for task";
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void documentationLinkLabel_Click(object sender, EventArgs e)
        {
            string link = linkTextLabel.Text;
            Process.Start(link);
        }

        private void taskList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            c = new ChecklistItem();
            c = d.GetATCTaskDescription(taskList.SelectedItem.ToString());
            descriptionTextBox.Text = c.description;
            if (c.documentation != string.Empty)
            {
                linkTextLabel.Text = c.documentation;
                documentationLinkLabel.Text = "View documentation.";
            }
            else
                documentationLinkLabel.Text = string.Empty;
        }
    }
}
