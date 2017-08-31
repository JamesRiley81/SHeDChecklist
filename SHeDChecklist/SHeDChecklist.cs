using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace SHeDChecklist
{
    public partial class SHeDChecklist : Form
    {
        ChecklistItem c;
        Database d;
        ImageList theImage = new ImageList();
        public SHeDChecklist()
        {
            InitializeComponent();
            
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            string output;
            if (currentStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your initials from the drop down list.");
                return;
            }
            else if (groupTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task from the drop down list.");
                return;
            }
            else 
            {
                output = d.AddSHEDEntry(currentStudents.SelectedItem.ToString(), groupTasks.SelectedItem.ToString());
                descriptionTextBox.Text = output;
                ClearInputs();
            }
        }
    private void groupTasks_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            linkTextLabel.Text = string.Empty;
            documentationLinkLabel.Text = string.Empty; 
            c = new ChecklistItem();
            c = d.GetSHEDTaskDescription(groupTasks.SelectedItem.ToString());
            descriptionTextBox.Text = c.description;
            linkTextLabel.Text = c.documentation;
            if (c.documentation != string.Empty)
                documentationLinkLabel.Text = "Documentation for task";
        }
        private void ClearInputs()
        {
            groupTasks.Text = "";
            documentationLinkLabel.Text = string.Empty;
            groupTasks.Items.Clear();
            UpdateInputs();
        }

        private void SHeDChecklist_Load(object sender, EventArgs e)
        {           
            UpdateInputs();
            foreach (string s in d.TodaysStudents())
            {
                currentStudents.Items.Add(s);
            }
        }
        private void UpdateInputs()
        {
            d = new Database();
            foreach (ChecklistItem c in d.GetSHEDTasks())
            {
                groupTasks.Items.Add(c.name);
            }  
            if (groupTasks.Items.Count == 0)
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

        private void groupTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new ChecklistItem();
            c = d.GetSHEDTaskDescription(groupTasks.SelectedItem.ToString());
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
