namespace SHeDChecklist
{
    partial class AllStaffChecklist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllStaffChecklist));
            this.staffList = new System.Windows.Forms.ComboBox();
            this.taskList = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.initialsLabel = new System.Windows.Forms.Label();
            this.pickTaskLabel = new System.Windows.Forms.Label();
            this.Exitbutton3 = new System.Windows.Forms.Button();
            this.linkTextLabel = new System.Windows.Forms.Label();
            this.documentationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // staffList
            // 
            this.staffList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staffList.FormattingEnabled = true;
            this.staffList.Location = new System.Drawing.Point(171, 7);
            this.staffList.Name = "staffList";
            this.staffList.Size = new System.Drawing.Size(71, 24);
            this.staffList.TabIndex = 0;
            this.staffList.SelectedIndexChanged += new System.EventHandler(this.staffList_SelectedIndexChanged);
            // 
            // taskList
            // 
            this.taskList.BackColor = System.Drawing.Color.AliceBlue;
            this.taskList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskList.FormattingEnabled = true;
            this.taskList.Location = new System.Drawing.Point(171, 49);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(143, 24);
            this.taskList.TabIndex = 1;
            this.taskList.SelectedIndexChanged += new System.EventHandler(this.taskList_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(34, 383);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(71, 21);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.AliceBlue;
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 154);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(381, 157);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(173, 382);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(71, 21);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // initialsLabel
            // 
            this.initialsLabel.AutoSize = true;
            this.initialsLabel.BackColor = System.Drawing.Color.AliceBlue;
            this.initialsLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.initialsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialsLabel.Location = new System.Drawing.Point(23, 9);
            this.initialsLabel.Name = "initialsLabel";
            this.initialsLabel.Size = new System.Drawing.Size(135, 19);
            this.initialsLabel.TabIndex = 5;
            this.initialsLabel.Text = "Choose Your Initials";
            // 
            // pickTaskLabel
            // 
            this.pickTaskLabel.AutoSize = true;
            this.pickTaskLabel.BackColor = System.Drawing.Color.AliceBlue;
            this.pickTaskLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pickTaskLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pickTaskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickTaskLabel.Location = new System.Drawing.Point(23, 52);
            this.pickTaskLabel.Name = "pickTaskLabel";
            this.pickTaskLabel.Size = new System.Drawing.Size(84, 19);
            this.pickTaskLabel.TabIndex = 6;
            this.pickTaskLabel.Text = "Pick A Task";
            // 
            // Exitbutton3
            // 
            this.Exitbutton3.Location = new System.Drawing.Point(312, 381);
            this.Exitbutton3.Name = "Exitbutton3";
            this.Exitbutton3.Size = new System.Drawing.Size(75, 23);
            this.Exitbutton3.TabIndex = 7;
            this.Exitbutton3.Text = "Exit";
            this.Exitbutton3.UseVisualStyleBackColor = true;
            this.Exitbutton3.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // linkTextLabel
            // 
            this.linkTextLabel.AutoSize = true;
            this.linkTextLabel.BackColor = System.Drawing.Color.White;
            this.linkTextLabel.Location = new System.Drawing.Point(280, 13);
            this.linkTextLabel.Name = "linkTextLabel";
            this.linkTextLabel.Size = new System.Drawing.Size(0, 13);
            this.linkTextLabel.TabIndex = 8;
            this.linkTextLabel.Visible = false;
            // 
            // documentationLinkLabel
            // 
            this.documentationLinkLabel.AutoSize = true;
            this.documentationLinkLabel.BackColor = System.Drawing.Color.White;
            this.documentationLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentationLinkLabel.Location = new System.Drawing.Point(123, 116);
            this.documentationLinkLabel.Name = "documentationLinkLabel";
            this.documentationLinkLabel.Size = new System.Drawing.Size(0, 17);
            this.documentationLinkLabel.TabIndex = 9;
            this.documentationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentationLinkLabel_LinkClicked);
            // 
            // AllStaffChecklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(421, 428);
            this.Controls.Add(this.documentationLinkLabel);
            this.Controls.Add(this.linkTextLabel);
            this.Controls.Add(this.Exitbutton3);
            this.Controls.Add(this.pickTaskLabel);
            this.Controls.Add(this.initialsLabel);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.taskList);
            this.Controls.Add(this.staffList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AllStaffChecklist";
            this.Text = "AllStaffChecklist";
            this.Load += new System.EventHandler(this.AllStaffChecklist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox staffList;
        private System.Windows.Forms.ComboBox taskList;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label initialsLabel;
        private System.Windows.Forms.Label pickTaskLabel;
        private System.Windows.Forms.Button Exitbutton3;
        private System.Windows.Forms.Label linkTextLabel;
        private System.Windows.Forms.LinkLabel documentationLinkLabel;
    }
}