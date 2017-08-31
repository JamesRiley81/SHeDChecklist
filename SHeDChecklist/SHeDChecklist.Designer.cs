namespace SHeDChecklist
{
    partial class SHeDChecklist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SHeDChecklist));
            this.currentStudents = new System.Windows.Forms.ComboBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.documentationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.Chooseinitialslabel = new System.Windows.Forms.Label();
            this.Picktasklabel = new System.Windows.Forms.Label();
            this.linkTextLabel = new System.Windows.Forms.Label();
            this.groupTasks = new System.Windows.Forms.ListBox();
            this.completePictureBox = new System.Windows.Forms.PictureBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.completePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // currentStudents
            // 
            this.currentStudents.BackColor = System.Drawing.Color.AliceBlue;
            this.currentStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentStudents.ForeColor = System.Drawing.Color.Black;
            this.currentStudents.FormattingEnabled = true;
            this.currentStudents.Location = new System.Drawing.Point(171, 7);
            this.currentStudents.Name = "currentStudents";
            this.currentStudents.Size = new System.Drawing.Size(71, 24);
            this.currentStudents.TabIndex = 0;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.AliceBlue;
            this.descriptionTextBox.Enabled = false;
            this.descriptionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionTextBox.ForeColor = System.Drawing.Color.White;
            this.descriptionTextBox.Location = new System.Drawing.Point(28, 152);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(381, 157);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 358);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(171, 358);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(318, 358);
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
            this.documentationLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentationLinkLabel.Location = new System.Drawing.Point(123, 116);
            this.documentationLinkLabel.Name = "documentationLinkLabel";
            this.documentationLinkLabel.Size = new System.Drawing.Size(0, 18);
            this.documentationLinkLabel.TabIndex = 6;
            this.documentationLinkLabel.Click += new System.EventHandler(this.documentationLinkLabel_Click);
            // 
            // Chooseinitialslabel
            // 
            this.Chooseinitialslabel.AutoSize = true;
            this.Chooseinitialslabel.BackColor = System.Drawing.Color.AliceBlue;
            this.Chooseinitialslabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Chooseinitialslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chooseinitialslabel.ForeColor = System.Drawing.Color.Black;
            this.Chooseinitialslabel.Location = new System.Drawing.Point(23, 9);
            this.Chooseinitialslabel.Name = "Chooseinitialslabel";
            this.Chooseinitialslabel.Size = new System.Drawing.Size(142, 20);
            this.Chooseinitialslabel.TabIndex = 7;
            this.Chooseinitialslabel.Text = "Choose Your Initials";
            // 
            // Picktasklabel
            // 
            this.Picktasklabel.AutoSize = true;
            this.Picktasklabel.BackColor = System.Drawing.Color.AliceBlue;
            this.Picktasklabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picktasklabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Picktasklabel.ForeColor = System.Drawing.Color.Black;
            this.Picktasklabel.Location = new System.Drawing.Point(23, 52);
            this.Picktasklabel.Name = "Picktasklabel";
            this.Picktasklabel.Size = new System.Drawing.Size(89, 20);
            this.Picktasklabel.TabIndex = 8;
            this.Picktasklabel.Text = "Pick A Task";
            // 
            // linkTextLabel
            // 
            this.linkTextLabel.AutoSize = true;
            this.linkTextLabel.Location = new System.Drawing.Point(298, 12);
            this.linkTextLabel.Name = "linkTextLabel";
            this.linkTextLabel.Size = new System.Drawing.Size(0, 13);
            this.linkTextLabel.TabIndex = 9;
            this.linkTextLabel.Visible = false;
            // 
            // groupTasks
            // 
            this.groupTasks.FormattingEnabled = true;
            this.groupTasks.Location = new System.Drawing.Point(171, 53);
            this.groupTasks.Name = "groupTasks";
            this.groupTasks.Size = new System.Drawing.Size(222, 56);
            this.groupTasks.TabIndex = 10;
            this.groupTasks.SelectedIndexChanged += new System.EventHandler(this.groupTasks_SelectedIndexChanged);
            // 
            // completePictureBox
            // 
            this.completePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.completePictureBox.Location = new System.Drawing.Point(1, -1);
            this.completePictureBox.Name = "completePictureBox";
            this.completePictureBox.Size = new System.Drawing.Size(420, 327);
            this.completePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.completePictureBox.TabIndex = 11;
            this.completePictureBox.TabStop = false;
            this.completePictureBox.Visible = false;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(1, 332);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(420, 20);
            this.outputTextBox.TabIndex = 12;
            this.outputTextBox.Visible = false;
            // 
            // SHeDChecklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(421, 428);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.completePictureBox);
            this.Controls.Add(this.groupTasks);
            this.Controls.Add(this.linkTextLabel);
            this.Controls.Add(this.Picktasklabel);
            this.Controls.Add(this.Chooseinitialslabel);
            this.Controls.Add(this.documentationLinkLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.currentStudents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SHeDChecklist";
            this.Text = "SHED Checklist";
            this.Load += new System.EventHandler(this.SHeDChecklist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.completePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox currentStudents;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.LinkLabel documentationLinkLabel;
        private System.Windows.Forms.Label Chooseinitialslabel;
        private System.Windows.Forms.Label Picktasklabel;
        private System.Windows.Forms.Label linkTextLabel;
        private System.Windows.Forms.ListBox groupTasks;
        private System.Windows.Forms.PictureBox completePictureBox;
        private System.Windows.Forms.TextBox outputTextBox;
    }
}

