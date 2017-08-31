namespace SHeDChecklist
{
    partial class ATCChecklist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ATCChecklist));
            this.staffList = new System.Windows.Forms.ComboBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.documentationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.initialsLabel = new System.Windows.Forms.Label();
            this.taskLabel = new System.Windows.Forms.Label();
            this.linkTextLabel = new System.Windows.Forms.Label();
            this.taskList = new System.Windows.Forms.ListBox();
            this.completePictureBox = new System.Windows.Forms.PictureBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.completePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // staffList
            // 
            this.staffList.BackColor = System.Drawing.Color.AliceBlue;
            this.staffList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staffList.FormattingEnabled = true;
            this.staffList.Location = new System.Drawing.Point(171, 7);
            this.staffList.Name = "staffList";
            this.staffList.Size = new System.Drawing.Size(71, 24);
            this.staffList.TabIndex = 0;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.AliceBlue;
            this.descriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 154);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(381, 157);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 356);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(164, 356);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(322, 356);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // documentationLinkLabel
            // 
            this.documentationLinkLabel.AutoSize = true;
            this.documentationLinkLabel.BackColor = System.Drawing.Color.White;
            this.documentationLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentationLinkLabel.Location = new System.Drawing.Point(123, 118);
            this.documentationLinkLabel.Name = "documentationLinkLabel";
            this.documentationLinkLabel.Size = new System.Drawing.Size(0, 17);
            this.documentationLinkLabel.TabIndex = 6;
            this.documentationLinkLabel.Click += new System.EventHandler(this.documentationLinkLabel_Click);
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
            this.initialsLabel.TabIndex = 7;
            this.initialsLabel.Text = "Choose Your Initials";
            // 
            // taskLabel
            // 
            this.taskLabel.AutoSize = true;
            this.taskLabel.BackColor = System.Drawing.Color.AliceBlue;
            this.taskLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.taskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskLabel.Location = new System.Drawing.Point(23, 52);
            this.taskLabel.Name = "taskLabel";
            this.taskLabel.Size = new System.Drawing.Size(84, 19);
            this.taskLabel.TabIndex = 8;
            this.taskLabel.Text = "Pick A Task";
            // 
            // linkTextLabel
            // 
            this.linkTextLabel.AutoSize = true;
            this.linkTextLabel.Location = new System.Drawing.Point(298, 9);
            this.linkTextLabel.Name = "linkTextLabel";
            this.linkTextLabel.Size = new System.Drawing.Size(0, 13);
            this.linkTextLabel.TabIndex = 9;
            this.linkTextLabel.Visible = false;
            // 
            // taskList
            // 
            this.taskList.FormattingEnabled = true;
            this.taskList.Location = new System.Drawing.Point(164, 52);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(229, 56);
            this.taskList.TabIndex = 10;
            this.taskList.SelectedIndexChanged += new System.EventHandler(this.taskList_SelectedIndexChanged_1);
            // 
            // completePictureBox
            // 
            this.completePictureBox.Location = new System.Drawing.Point(-2, -1);
            this.completePictureBox.Name = "completePictureBox";
            this.completePictureBox.Size = new System.Drawing.Size(422, 312);
            this.completePictureBox.TabIndex = 11;
            this.completePictureBox.TabStop = false;
            this.completePictureBox.Visible = false;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(-2, 318);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(422, 20);
            this.outputTextBox.TabIndex = 12;
            this.outputTextBox.Visible = false;
            // 
            // ATCChecklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(421, 428);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.completePictureBox);
            this.Controls.Add(this.taskList);
            this.Controls.Add(this.linkTextLabel);
            this.Controls.Add(this.taskLabel);
            this.Controls.Add(this.initialsLabel);
            this.Controls.Add(this.documentationLinkLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.staffList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ATCChecklist";
            this.Text = "ATCChecklist";
            this.Load += new System.EventHandler(this.ATCChecklist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.completePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox staffList;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.LinkLabel documentationLinkLabel;
        private System.Windows.Forms.Label initialsLabel;
        private System.Windows.Forms.Label taskLabel;
        private System.Windows.Forms.Label linkTextLabel;
        private System.Windows.Forms.ListBox taskList;
        private System.Windows.Forms.PictureBox completePictureBox;
        private System.Windows.Forms.TextBox outputTextBox;
    }
}