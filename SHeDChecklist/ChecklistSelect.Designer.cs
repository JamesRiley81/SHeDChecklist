namespace SHeDChecklist
{
    partial class ChecklistSelect
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
            this.SHEDButton = new System.Windows.Forms.Button();
            this.ATCButton = new System.Windows.Forms.Button();
            this.adminButton = new System.Windows.Forms.Button();
            this.allButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SHEDButton
            // 
            this.SHEDButton.Location = new System.Drawing.Point(60, 48);
            this.SHEDButton.Name = "SHEDButton";
            this.SHEDButton.Size = new System.Drawing.Size(150, 29);
            this.SHEDButton.TabIndex = 0;
            this.SHEDButton.Text = "SHeD Checklist";
            this.SHEDButton.UseVisualStyleBackColor = true;
            this.SHEDButton.Click += new System.EventHandler(this.SHeDChecklist_Click);
            // 
            // ATCButton
            // 
            this.ATCButton.ForeColor = System.Drawing.Color.Black;
            this.ATCButton.Location = new System.Drawing.Point(331, 48);
            this.ATCButton.Name = "ATCButton";
            this.ATCButton.Size = new System.Drawing.Size(150, 29);
            this.ATCButton.TabIndex = 1;
            this.ATCButton.Text = "ATC Checklist";
            this.ATCButton.UseVisualStyleBackColor = true;
            this.ATCButton.Click += new System.EventHandler(this.ATCButton_Click);
            // 
            // adminButton
            // 
            this.adminButton.Location = new System.Drawing.Point(192, 202);
            this.adminButton.Name = "adminButton";
            this.adminButton.Size = new System.Drawing.Size(150, 29);
            this.adminButton.TabIndex = 2;
            this.adminButton.Text = "Administrator";
            this.adminButton.UseVisualStyleBackColor = true;
            this.adminButton.Click += new System.EventHandler(this.adminButton_Click);
            // 
            // allButton
            // 
            this.allButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.allButton.Location = new System.Drawing.Point(192, 126);
            this.allButton.Name = "allButton";
            this.allButton.Size = new System.Drawing.Size(150, 29);
            this.allButton.TabIndex = 3;
            this.allButton.Text = " All Staff Checklist";
            this.allButton.UseVisualStyleBackColor = true;
            this.allButton.Click += new System.EventHandler(this.allButton_Click);
            // 
            // ChecklistSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(537, 295);
            this.Controls.Add(this.allButton);
            this.Controls.Add(this.adminButton);
            this.Controls.Add(this.ATCButton);
            this.Controls.Add(this.SHEDButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChecklistSelect";
            this.Text = "ChecklistSelect";
            this.Load += new System.EventHandler(this.ChecklistSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SHEDButton;
        private System.Windows.Forms.Button ATCButton;
        private System.Windows.Forms.Button adminButton;
        private System.Windows.Forms.Button allButton;
    }
}