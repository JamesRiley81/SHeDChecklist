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
        int daysBack = 0;
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
            timeGraph = false;
            lineChart.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            lineChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            lineChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            studentsListBox.Visible = false;
            generateButton.Visible = false;
            lineChart.ChartAreas[0].AxisY.Title = "Overall percentage";
            lineChart.ChartAreas[0].AxisX.Title = "For week of";
            lineChart.Series.Clear();
            timeGraph = false;
            SetDaysBack();
            ser1 = new Series();
            ser1.ChartType = SeriesChartType.Line;
            ser1.LegendText = "Actual Values";
            ser1.Name = "Actual Values";
            ser2.Points.Clear();
            ser3.Points.Clear();
            ser4.Points.Clear();           
            RadioButton button = sender as RadioButton;
            switch (button.Name)
            {
                case "shedRadioButton":
                    report = "SHEDWork";
                    break;
                case "atcRadioButton":
                    report = "ATCWork";
                    break;
            }
            if (report != null)
            {
                DateTime startday = DateTime.Now.AddDays(-1 * daysBack).Date;
                DateTime endday = startday.AddDays(6).Date;
                lineChart.ChartAreas[0].AxisY.Maximum = 100;
                lineChart.ChartAreas[0].AxisY.Minimum = 0;
                lineChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                ser1.BorderWidth = 3;
                ser1.Color = System.Drawing.Color.CadetBlue;
                ser1.MarkerStyle = MarkerStyle.Diamond;
                ser1.MarkerColor = System.Drawing.Color.DarkOrange;
                ser1.MarkerSize = 6;
                int counter = 0;
                do
                {
                    DataPoint data = new DataPoint();
                    data.AxisLabel = endday.AddDays(GetDayLabel(endday.DayOfWeek)).ToShortDateString();
                    double perc = d.GetPercent(report, startday, endday);
                    if (double.IsNaN(perc))
                        perc = 0;
                    data.SetValueXY(counter++, perc);
                    ser1.Points.Add(data);
                    startday = endday.AddDays(1);
                    endday = startday.AddDays(6);
                } while (startday < DateTime.Now);
                lineChart.ChartAreas[0].AxisX.ScaleView.Size = counter;
                lineChart.Series.Add(ser1);
            }
            else
                return;
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            dataGrid.Visible = true;
            dataGrid.Columns.Add("Student", "Initials");
            dataGrid.Columns.Add("Name", "Task");
            double totalTasks = d.GetAllTasks(report);
            studentContributionLabel.Visible = true;
            foreach (string s in d.GetAllStudents(report))
            {
                if (s != string.Empty)
                {
                    double studentTasks = d.StudentCounts(report, s);
                    string percent = (studentTasks / totalTasks).ToString("P");
                    dataGrid.Rows.Add(s, percent);
                }
                else
                {
                    double studentTasks = d.StudentCounts(report, s);
                    string percent = (studentTasks / totalTasks).ToString("P");
                    dataGrid.Rows.Add("INCOMPLETE TASKS", percent);
                }    
            }
        }
        public void SetDaysBack()
        {
            DateTime n = DateTime.Now;
            switch (n.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    daysBack = 181;
                    break;
                case DayOfWeek.Monday:
                    daysBack = 182;
                    break;
                case DayOfWeek.Tuesday:
                    daysBack = 183;
                    break;
                case DayOfWeek.Wednesday:
                    daysBack = 184;
                    break;
                case DayOfWeek.Thursday:
                    daysBack = 185;
                    break;
                case DayOfWeek.Friday:
                    daysBack = 186;
                    break;
                case DayOfWeek.Saturday:
                    daysBack = 187;
                    break;
            }
        }

        private void triageReport_CheckedChanged(object sender, EventArgs e)
        {
            studentsListBox.Items.Clear();
            timeGraph = false;
            lineChart.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            dataGrid.Visible = false;
            studentContributionLabel.Visible = false;
            studentsListBox.Visible = false;
            generateButton.Visible = false;
            RadioButton button = sender as RadioButton;
            dataGrid.Visible = false;
            studentContributionLabel.Visible = false;
            if (button.Name == "triageReport")
                CreateSHEDTriageReport();
            else if (button.Name == "atcTriageReport")
                CreateATCTriageReport();
            
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
            lineChart.Series.Clear();
            ser1 = new Series();
            ser2 = new Series();
            ser3 = new Series();
            ser4 = new Series();
            lineChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            lineChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            ser1.LegendText = "Actual Values";
            ser1.ChartType = SeriesChartType.Point;
            ser1.MarkerStyle = MarkerStyle.Diamond;
            ser1.MarkerColor = System.Drawing.Color.Blue;
            ser1.MarkerSize = 9;
            ser2.ChartType = SeriesChartType.Line;
            ser2.LegendText = "Regression";
            ser3.ChartType = SeriesChartType.Spline;
            ser3.LegendText = "STD+1";
            ser4.ChartType = SeriesChartType.Spline;
            ser4.LegendText = "STD+2";
            lineChart.ChartAreas[0].AxisY.Title = "Elapsed time in minutes";
            lineChart.ChartAreas[0].AxisX.Title = "Date";
            List<double> values = new List<double>();
            List<double> values2 = new List<double>();
            timeGraph = true;
            TimeSpan TS = new TimeSpan(7, 30, 00);
            lineChart.ChartAreas[0].AxisY.Minimum = 0;           
            TimeSpan start = new TimeSpan(7, 30, 00);
            TimeSpan End = new TimeSpan(12, 00, 00);
            int minutes = (start.Minutes - End.Minutes) + 60 * (End.Hours - start.Hours);
            lineChart.ChartAreas[0].AxisY.Maximum = minutes;
            int counter = 0;
            List<ChecklistItem> items = d.GetTimes();
            double SigmaY = 0;
            foreach (ChecklistItem c in Sorting())
            {
                DateTime day = DateTime.Parse(c.day);
                TimeSpan startTime = new TimeSpan(7, 30, 00);
                TimeSpan recordTime = TimeSpan.Parse(c.time);
                int differenceMins = recordTime.Minutes - startTime.Minutes;
                int differenceHours = recordTime.Hours - startTime.Hours;
                int output = differenceMins + differenceHours * 60;
                values.Add(output);
                DataPoint dp = new DataPoint();
                dp.AxisLabel = day.ToShortDateString();
                if (output >= 270)
                    output = 270;
                dp.SetValueXY(counter++, output);
                ser1.Points.Add(dp);
            }
            lineChart.ChartAreas[0].AxisX.ScaleView.Size = counter;
            //Linear Regression Graph
            double SigmaX = 0;
            double SigmaXY = 0;
            int n = values.Count;
            for (int i = n; i > 0; i--)
            {
                values2.Add(i);
            }
            SigmaX = values2.Sum();
            SigmaY = values.Sum();
            for (int y = 0; y < n; y++)
            {
                SigmaXY += values[y] * values2[y];
            }
            double SigmaXSquared = 0;
            for (int y = 0; y < n; y++)
            {
                SigmaXSquared += Math.Pow(values2[y], 2);
            }
            double slope = ((n * SigmaXY) - (SigmaX*SigmaY))/((n * SigmaXSquared) - (Math.Pow(SigmaX, 2)));
            slope = slope * -1;
            double intercept = (SigmaY - slope * (SigmaX)) / n;
            List<double> yVals = new List<double>();
            for (int i = 0; i < n; i++)
            {
                DataPoint data = new DataPoint();
                data.SetValueXY(i, intercept + slope * i);
                if (i % 4 == 0)
                    yVals.Add(intercept + slope * i);
                ser2.Points.Add(data);
            }
            int sections = n / 4;
            counter = 0;
            while (counter < sections)
            {
                double sum = 0;
                double mean = 0;
                double sqrt = 0;
                double mean2 = 0;
                double distance = 0;
                for (int i = counter * 4; i < (counter + 1) * 4; i++)
                {
                    sum += values[i];
                }
                mean = sum / 4;
                for (int j = counter * 4; j < (counter + 1) * 4; j++)
                {
                    distance += Math.Pow(values[j] - mean,2);
                }
                mean2 = distance / 4;
                sqrt = Math.Sqrt(mean2);
                DataPoint data2 = new DataPoint();
                data2.SetValueXY((counter) * 4, yVals[counter] + sqrt);
                DataPoint data3 = new DataPoint();
                data3.SetValueXY((counter) * 4, yVals[counter] - sqrt);
                ser3.Points.Add(data2);
                ser4.Points.Add(data3);
                counter++;               
            }
            lineChart.Series.Add(ser1);
            lineChart.Series.Add(ser2);
            lineChart.Series.Add(ser3);
            lineChart.Series.Add(ser4);
        }
        public void CreateATCTriageReport()
        {
            lineChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            lineChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            double SigmaX = 0;
            double SigmaXY = 0;
            double SigmaY = 0;
            lineChart.Series.Clear();
            ser1 = new Series();
            ser2 = new Series();
            ser3 = new Series();
            ser4 = new Series();
            ser1.LegendText = "Actual Values";
            ser1.ChartType = SeriesChartType.Point;
            ser1.MarkerStyle = MarkerStyle.Diamond;
            ser1.MarkerColor = System.Drawing.Color.Blue;
            ser1.MarkerSize = 9;
            ser2.ChartType = SeriesChartType.Line;
            ser2.LegendText = "Regression";
            ser2.MarkerStyle = MarkerStyle.Circle;
            ser2.MarkerColor = System.Drawing.Color.Orange;
            ser3.ChartType = SeriesChartType.Spline;
            ser3.LegendText = "STD+1";
            ser4.ChartType = SeriesChartType.Spline;
            ser4.LegendText = "STD+2";
            List<double> values = new List<double>();
            List<double> values2 = new List<double>();
            timeGraph = true;
            TimeSpan TS = new TimeSpan(8, 00, 00);
            lineChart.ChartAreas[0].AxisY.Minimum = 0;
            TimeSpan start = new TimeSpan(8, 00, 00);
            TimeSpan End = new TimeSpan(12, 00, 00);
            int minutes = (start.Minutes - End.Minutes) + 60 * (End.Hours - start.Hours);
            lineChart.ChartAreas[0].AxisY.Maximum = minutes;
            int counter = 0;
            List<ChecklistItem> items = d.GetATCTimes();
            foreach (ChecklistItem c in SortingATC())
            {
                DateTime day = DateTime.Parse(c.day);
                TimeSpan startTime = new TimeSpan(8, 00, 00);
                TimeSpan recordTime = TimeSpan.Parse(c.time);
                int differenceMins = recordTime.Minutes - startTime.Minutes;
                int differenceHours = recordTime.Hours - startTime.Hours;
                int output = differenceMins + differenceHours * 60;
                if (output >= 240)
                    output = 240;
                values.Add(output);
                DataPoint dp = new DataPoint();
                dp.AxisLabel = day.ToShortDateString();
                dp.SetValueXY(counter++, output);
                ser1.Points.Add(dp);
            }
            lineChart.ChartAreas[0].AxisX.ScaleView.Size = counter;
            int n = values.Count;
            for (int i = n; i > 0; i--)
            {
                values2.Add(i);
            }
            SigmaX = values2.Sum();
            SigmaY = values.Sum();
            for (int y = 0; y < n; y++)
            {
                SigmaXY += values[y] * values2[y];
            }
            double SigmaXSquared = 0;
            for (int y = 0; y < n; y++)
            {
                SigmaXSquared += Math.Pow(values2[y], 2);
            }
            double slope = ((n * SigmaXY) - (SigmaX * SigmaY)) / ((n * SigmaXSquared) - (Math.Pow(SigmaX, 2)));
            slope = slope * -1;
            double intercept = (SigmaY - slope * (SigmaX)) / n;
            List<double> yVals = new List<double>();
            for (int i = 0; i < n; i++)
            {
                DataPoint data = new DataPoint();
                data.SetValueXY(i, intercept + slope * i);
                if (i % 4 == 0)
                    yVals.Add(intercept + slope * i);
                ser2.Points.Add(data);
            }
            int sections = n / 4;
            counter = 0;
            while (counter < sections)
            {
                double sum = 0;
                double mean = 0;
                double sqrt = 0;
                double mean2 = 0;
                double distance = 0;
                for (int i = counter * 4; i < (counter + 1) * 4; i++)
                {
                    sum += values[i];
                }
                mean = sum / 4;
                for (int j = counter * 4; j < (counter + 1) * 4; j++)
                {
                    distance += Math.Pow(values[j] - mean, 2);
                }
                mean2 = distance / 4;
                sqrt = Math.Sqrt(mean2);
                DataPoint data2 = new DataPoint();
                data2.SetValueXY((counter) * 4, yVals[counter] + sqrt);
                DataPoint data3 = new DataPoint();
                data3.SetValueXY((counter) * 4, yVals[counter] - sqrt);
                ser3.Points.Add(data2);
                ser4.Points.Add(data3);
                counter++;
            }
            lineChart.Series.Add(ser1);
            lineChart.Series.Add(ser2);
            lineChart.Series.Add(ser3);
            lineChart.Series.Add(ser4);
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

        private void AdminForm_Load(object sender, EventArgs e)
        {
            //Causes JIT Error, queries to delete out old records should be done directly through access
            //d.RunAccessQueries();
        }
    }
}
