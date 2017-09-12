using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace SHeDChecklist
{
    public partial class AdminForm : Form
    {
        string report = null;
        bool barGraph = false;
        Database d = new Database();
        bool timeGraph;
        public AdminForm()
        {
            InitializeComponent();
        }
        Series ser1 = new Series();
        Series ser2 = new Series();
        Series ser3 = new Series();
        Series ser4 = new Series();
        private void lineChart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                if (timeGraph == false && barGraph == false)
                {
                    int i = e.HitTestResult.PointIndex;
                    DataPoint dp = e.HitTestResult.Series.Points[i];
                    DateTime day1 = DateTime.Parse(dp.AxisLabel.ToString());
                    day1 = day1.AddDays(-4);
                    e.Text = string.Format("{0:F1}", day1.ToShortDateString() + " - " + dp.AxisLabel.ToString() + ":  " + dp.YValues[0]) + "%";
                }
                else if (timeGraph == false && barGraph == true)
                {
                    int i = e.HitTestResult.PointIndex;
                    DataPoint dp = e.HitTestResult.Series.Points[i];
                    string output = dp.YValues[0].ToString("F");
                    e.Text = string.Format(output + "%");
                }
                else
                {
                    try
                    {
                        double mins;
                        double hours;
                        string suffix;
                        int i = e.HitTestResult.PointIndex;
                        DataPoint dp = e.HitTestResult.Series.Points[i];
                        double j = dp.YValues[0];
                        if (j == 690)
                        {
                            mins = 0;
                            hours = 19;
                        }
                        else if (j == 540)
                        {
                            mins = 0;
                            hours = 17;
                        }
                        else
                        {
                            mins = j % 60;
                            hours = 7 + (j - mins) / 60;
                            if (hours == 7)
                                mins = 30 + mins;
                        }
                        if (hours < 12)
                            suffix = "am";
                        else
                        {
                            hours = hours - 12;
                            suffix = "pm";
                        }
                        TimeSpan output = new TimeSpan(int.Parse(hours.ToString()), int.Parse(mins.ToString()), 0);
                        string printout = output.ToString();
                        printout = printout.Substring(0, printout.Length - 3);
                        printout += " " + suffix;
                        e.Text = string.Format("{0:F1}", printout);
                    }
                    catch (Exception ex)
                    {
                        e.Text = string.Empty;
                    }
                }

            }
        }
        public int GetDayLabel(DayOfWeek d)
        {
            int days = 0;
            switch ((int)d)
            {
                case 0:
                    days = -2;
                    break;
                case 1:
                    days = -3;
                    break;
                case 2:
                    days = -4;
                    break;
                case 3:
                    days = -5;
                    break;
                case 4:
                    days = -6;
                    break;
                case 6:
                    days = 6;
                    break;
            }
            return days;
        }
        private void shedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            studentsListBox.Items.Clear();
            studentsListBox.Visible = false;
            if (shedRadioButton.Checked)
            {
                var points = d.GetDataPoints("SHEDWork");
                dgv1.DataSource = points;
                dgv1.Visible = true;
                report = "SHEDWork";
                weeklyLabel.Visible = true;
                dataGrid.Rows.Clear();
                dataGrid.Columns.Clear();
                dataGrid.Visible = true;
                dataGrid.Columns.Add("Student", "Initials");
                dataGrid.Columns.Add("Name", "Percent Contribution");
                double totalTasks = d.GetAllTasks(report);
                studentContributionLabel.Visible = true;
                foreach (string s in d.GetAllStudents(report))
                {
                    if (s != string.Empty)
                    {
                        double studentTasks = d.StudentCounts(report, s);
                        double percent = Math.Round(100 * (studentTasks / totalTasks),2);
                        dataGrid.Rows.Add(s, percent);
                    }
                    else
                    {
                        double studentTasks = d.StudentCounts(report, s);
                        double percent = Math.Round(100 *(studentTasks / totalTasks),2);
                        dataGrid.Rows.Add("INCOMPLETE TASKS", percent);
                    }
                }
            }                           
        }
        private void triageReport_CheckedChanged(object sender, EventArgs e)
        {
            if (triageReport.Checked)
            {
                studentsListBox.Items.Clear();
                studentsListBox.Visible = false;
                dataGrid.Visible = false;
                studentContributionLabel.Visible = false;
                generateButton.Visible = false;
                dataGrid.Visible = false;
                studentContributionLabel.Visible = false;
                var points = d.GetTriagePoints("SHEDWork", "Generate Triage List");
                dgv1.DataSource = points;
                dgv1.Visible = true;
            }
        }
        public List<ChecklistItem> Sorting ()
        {
            List<ChecklistItem> items = new List<ChecklistItem>();
            foreach (ChecklistItem c in d.GetTimes())
            {
                c.dateTime = DateTime.Parse(c.day);
                items.Add(c);
            }
            items.Sort((a, b) => a.dateTime.CompareTo(b.dateTime));
            return items;
        }
        public void CreateSHEDTriageReport()
        {
            lineChart.Visible = false;
            
          
   
        }       
        public List<ChecklistItem> SortingATC()
        {
            List<ChecklistItem> items = new List<ChecklistItem>();
            foreach (ChecklistItem c in d.GetATCTimes())
            {
                c.dateTime = DateTime.Parse(c.day);
                items.Add(c);
            }
            items.Sort((a, b) => a.dateTime.CompareTo(b.dateTime));
            return items;
        }

        private void individualTasksButton_CheckedChanged(object sender, EventArgs e)
        {
            barGraph = true;
            dataGrid.Visible = false;
            studentContributionLabel.Visible = false;
            studentsListBox.Visible = true;
            generateButton.Visible = true;
            List<string> students = d.GetAllStudents("IndividualWork");           
            lineChart.Series.Remove(ser1);
            lineChart.Series.Remove(ser2);
            lineChart.Series.Remove(ser3);
            lineChart.Series.Remove(ser4);
            dgv1.Visible = false;
            weeklyLabel.Visible = false;
            foreach (string s in students)
            {
                if (s != string.Empty)
                    studentsListBox.Items.Add(s);
            }         
            
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            timeGraph = false;
            lineChart.Series.Clear();
            lineChart.Visible = true;
            dgv1.Visible = false;
            List<string> students = new List<string>();
            List<string> tasks = new List<string>();
            lineChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            lineChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            foreach (string s in studentsListBox.CheckedItems)
            {
                students.Add(s);
            }          
            lineChart.ChartAreas[0].AxisY.Minimum = 0;
            lineChart.ChartAreas[0].AxisY.Maximum = 100;
            lineChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            lineChart.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 9);
            lineChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            lineChart.ChartAreas[0].AxisX.ScaleView.Size = 16;
            Series ser = new Series();
            ser.IsVisibleInLegend = false;
            lineChart.Series.Add(ser);
            ser.ChartType = SeriesChartType.Column;
            int counter = 0;
            foreach (ChecklistItem c in d.GetIndividualTasks())
            {
                tasks.Add(c.name);
            }
            int c2 = 0;
            foreach (string s in students)
            {
                int c = 0;                       
                foreach (string item in tasks)
                {
                    double perc = d.GetIndividualTaskPercent("IndividualWork", s, item);
                    DataPoint dp = new DataPoint();
                    dp.AxisLabel =  s + "\r\n" + item;
                    if (c2 == 0)
                    {
                        switch (c)
                        {
                            case 0:
                                dp.Color = System.Drawing.Color.FromArgb(113,12,11);
                                break;
                            case 1:
                                dp.Color = System.Drawing.Color.FromArgb(149, 15, 14);
                                break;
                            case 2:
                                dp.Color = System.Drawing.Color.FromArgb(177, 18, 17);
                                break;
                            case 3:
                                dp.Color = System.Drawing.Color.FromArgb(190, 20, 18);
                                c2++;
                                break;
                        }
                    }
                    else
                    {
                        switch (c)
                        {
                            case 0:
                                dp.Color = System.Drawing.Color.FromArgb(18, 61, 113);
                                break;
                            case 1:
                                dp.Color = System.Drawing.Color.FromArgb(25, 81, 151);
                                break;
                            case 2:
                                dp.Color = System.Drawing.Color.FromArgb(28, 95, 177);
                                break;
                            case 3:
                                dp.Color = System.Drawing.Color.FromArgb(30, 101, 190);
                                c2 = 0;
                                break;
                        }
                    }                             
                    dp.MarkerSize = 20;
                    dp.SetValueXY(counter++, perc);
                    ser.Points.Add(dp);
                    c++;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var d = new Database();
            var points = d.GetDataPoints("SHEDWork");
            dgv1.DataSource = points;
        }

        private void studentContributionLabel_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void atcRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (atcRadioButton.Checked)
            {
                var points2 = d.GetDataPoints("ATCWork");
                dgv1.DataSource = points2;
                report = "ATCWork";
                dgv1.Visible = true;
                weeklyLabel.Visible = true;
                dataGrid.Rows.Clear();
                dataGrid.Columns.Clear();
                dataGrid.Visible = true;
                dataGrid.Columns.Add("Student", "Initials");
                dataGrid.Columns.Add("Name", "Percent Contribution");
                double totalTasks = d.GetAllTasks(report);
                studentContributionLabel.Visible = true;
                foreach (string s in d.GetAllStudents(report))
                {
                    if (s != string.Empty)
                    {
                        double studentTasks = d.StudentCounts(report, s);
                        double percent = Math.Round(100*(studentTasks / totalTasks),2);
                        dataGrid.Rows.Add(s, percent);
                    }
                    else
                    {
                        double studentTasks = d.StudentCounts(report, s);
                        double percent = Math.Round(100 *(studentTasks / totalTasks),2);
                        dataGrid.Rows.Add("INCOMPLETE TASKS", percent);
                    }
                }
            }
        }

        private void atcTriageReport_CheckedChanged(object sender, EventArgs e)
        {
            if (atcTriageReport.Checked)
            {
                studentsListBox.Items.Clear();
                studentsListBox.Visible = false;
                dataGrid.Visible = false;
                studentContributionLabel.Visible = false;
                generateButton.Visible = false;
                dataGrid.Visible = false;
                studentContributionLabel.Visible = false;
                var points = d.GetTriagePoints("ATCWork", "Daily Triage");
                dgv1.DataSource = points;
                dgv1.Visible = true;
            }
        }
    }
}
