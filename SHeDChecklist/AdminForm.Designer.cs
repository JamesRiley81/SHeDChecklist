namespace SHeDChecklist
{
    partial class AdminForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.lineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.shedRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.individualTasksButton = new System.Windows.Forms.RadioButton();
            this.atcTriageReport = new System.Windows.Forms.RadioButton();
            this.triageReport = new System.Windows.Forms.RadioButton();
            this.atcRadioButton = new System.Windows.Forms.RadioButton();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.studentContributionLabel = new System.Windows.Forms.Label();
            this.studentsListBox = new System.Windows.Forms.CheckedListBox();
            this.generateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lineChart
            // 
            this.lineChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lineChart.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.lineChart.ChartAreas.Add(chartArea1);
            this.lineChart.Cursor = System.Windows.Forms.Cursors.Default;
            legend1.Name = "Legend1";
            this.lineChart.Legends.Add(legend1);
            this.lineChart.Location = new System.Drawing.Point(38, 168);
            this.lineChart.Name = "lineChart";
            this.lineChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            this.lineChart.Size = new System.Drawing.Size(1409, 527);
            this.lineChart.TabIndex = 1;
            this.lineChart.Text = "Student Work Report";
            this.lineChart.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.lineChart_GetToolTipText);
            // 
            // shedRadioButton
            // 
            this.shedRadioButton.AutoSize = true;
            this.shedRadioButton.Location = new System.Drawing.Point(3, 3);
            this.shedRadioButton.Name = "shedRadioButton";
            this.shedRadioButton.Size = new System.Drawing.Size(90, 17);
            this.shedRadioButton.TabIndex = 3;
            this.shedRadioButton.TabStop = true;
            this.shedRadioButton.Text = "SHED Report";
            this.shedRadioButton.UseVisualStyleBackColor = true;
            this.shedRadioButton.CheckedChanged += new System.EventHandler(this.shedRadioButton_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.individualTasksButton);
            this.panel1.Controls.Add(this.atcTriageReport);
            this.panel1.Controls.Add(this.triageReport);
            this.panel1.Controls.Add(this.atcRadioButton);
            this.panel1.Controls.Add(this.shedRadioButton);
            this.panel1.Location = new System.Drawing.Point(220, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(131, 126);
            this.panel1.TabIndex = 4;
            // 
            // individualTasksButton
            // 
            this.individualTasksButton.AutoSize = true;
            this.individualTasksButton.Location = new System.Drawing.Point(4, 98);
            this.individualTasksButton.Name = "individualTasksButton";
            this.individualTasksButton.Size = new System.Drawing.Size(102, 17);
            this.individualTasksButton.TabIndex = 7;
            this.individualTasksButton.TabStop = true;
            this.individualTasksButton.Text = "Individual Tasks";
            this.individualTasksButton.UseVisualStyleBackColor = true;
            this.individualTasksButton.CheckedChanged += new System.EventHandler(this.individualTasksButton_CheckedChanged);
            // 
            // atcTriageReport
            // 
            this.atcTriageReport.AutoSize = true;
            this.atcTriageReport.Location = new System.Drawing.Point(3, 74);
            this.atcTriageReport.Name = "atcTriageReport";
            this.atcTriageReport.Size = new System.Drawing.Size(114, 17);
            this.atcTriageReport.TabIndex = 6;
            this.atcTriageReport.TabStop = true;
            this.atcTriageReport.Text = "ATC Triage Report";
            this.atcTriageReport.UseVisualStyleBackColor = true;
            this.atcTriageReport.CheckedChanged += new System.EventHandler(this.triageReport_CheckedChanged);
            // 
            // triageReport
            // 
            this.triageReport.AutoSize = true;
            this.triageReport.Location = new System.Drawing.Point(3, 49);
            this.triageReport.Name = "triageReport";
            this.triageReport.Size = new System.Drawing.Size(123, 17);
            this.triageReport.TabIndex = 5;
            this.triageReport.TabStop = true;
            this.triageReport.Text = "SHED Triage Report";
            this.triageReport.UseVisualStyleBackColor = true;
            this.triageReport.CheckedChanged += new System.EventHandler(this.triageReport_CheckedChanged);
            // 
            // atcRadioButton
            // 
            this.atcRadioButton.AutoSize = true;
            this.atcRadioButton.Location = new System.Drawing.Point(3, 26);
            this.atcRadioButton.Name = "atcRadioButton";
            this.atcRadioButton.Size = new System.Drawing.Size(81, 17);
            this.atcRadioButton.TabIndex = 4;
            this.atcRadioButton.TabStop = true;
            this.atcRadioButton.Text = "ATC Report";
            this.atcRadioButton.UseVisualStyleBackColor = true;
            this.atcRadioButton.CheckedChanged += new System.EventHandler(this.shedRadioButton_CheckedChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(676, 48);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGrid.Size = new System.Drawing.Size(227, 114);
            this.dataGrid.TabIndex = 5;
            this.dataGrid.Visible = false;
            // 
            // studentContributionLabel
            // 
            this.studentContributionLabel.AutoSize = true;
            this.studentContributionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentContributionLabel.Location = new System.Drawing.Point(672, 22);
            this.studentContributionLabel.Name = "studentContributionLabel";
            this.studentContributionLabel.Size = new System.Drawing.Size(236, 20);
            this.studentContributionLabel.TabIndex = 6;
            this.studentContributionLabel.Text = "Overall Contribution Per Student";
            this.studentContributionLabel.Visible = false;
            // 
            // studentsListBox
            // 
            this.studentsListBox.FormattingEnabled = true;
            this.studentsListBox.Location = new System.Drawing.Point(1195, 39);
            this.studentsListBox.Name = "studentsListBox";
            this.studentsListBox.Size = new System.Drawing.Size(79, 109);
            this.studentsListBox.Sorted = true;
            this.studentsListBox.TabIndex = 7;
            this.studentsListBox.Visible = false;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(1280, 114);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 34);
            this.generateButton.TabIndex = 8;
            this.generateButton.Text = "Generate Report";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Visible = false;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1459, 736);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.studentsListBox);
            this.Controls.Add(this.studentContributionLabel);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lineChart);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart lineChart;
        private System.Windows.Forms.RadioButton shedRadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton atcRadioButton;
        private System.Windows.Forms.RadioButton triageReport;
        private System.Windows.Forms.RadioButton atcTriageReport;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label studentContributionLabel;
        private System.Windows.Forms.RadioButton individualTasksButton;
        private System.Windows.Forms.CheckedListBox studentsListBox;
        private System.Windows.Forms.Button generateButton;
    }
}