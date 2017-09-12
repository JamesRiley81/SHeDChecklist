using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Office.Interop.Access.Dao;


namespace SHeDChecklist
{
    public class DataPoints
    {
        public string Week_Of { get; set; }
        public double Completed_Tasks { get; set; }
        public double Total_Tasks { get; set; }
        public double Percent_Completed { get; set; } 
    }
    public class TriagePoints
    {
        public string Week_Of { get; set; }
        public double Average_Elasped_Time { get; set; }
    }
    public class Database
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private Student s;
        private List<Student> students;
        private DailychecklistItem dailyItems;
        private ChecklistItem c;
        private List<ChecklistItem> checklistItems;
        private string spreadsheetId;
        private string range;
        private string range2;
        private const string APPLICATIONNAME = "Daily Checklist";
        private IList<IList<Object>> values;
        private IList<IList<Object>> values2;
        private ValueRange response;
        private ValueRange response2;
        private const string CURRENTDAY = "CurrentDay";
        private const string TEMPSHED = "TempShed";
        private const string TEMPATC = "TempATC";
        private const string TEMPDAILY = "DailyStaff";
        private const string STAFFCHECKLIST = "StaffChecklist";
        private const string INDIVIDUALWORK = "IndividualWork";
        private const string SHEDWORK = "SHEDWork";
        private const string ATCWORK = "ATCWork";
        private const string EXCELCONNECTION = @"J:\SHeDChecklist\SHeDChecklist\";
        private OleDbConnection conn;
        private OleDbCommand com;
        private OleDbDataReader read;
        private string query;
        private const string FILENAME = "StudentWork.accdb";
        private string CONNECTIONSTRING = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=J:\SHeDChecklist\SHeDChecklist\" +  FILENAME;
        //this method checks the day the checklist was used.  If it isn't current day method returns false.
        public bool CheckDay()
        {
            try
            {
                query = "SELECT ChecklistDay FROM " + CURRENTDAY;
                conn = new OleDbConnection(CONNECTIONSTRING);
                com = new OleDbCommand(query, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    if (DateTime.Now.ToShortDateString() == read[0].ToString())
                        return true;                     
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not get the last checklist date from database, contact admin for help.  " + ex);
                return true;
            }
            return false;
        }
        //These methods get values from Google Sheets using client_secret.json file.
        public SheetsService CreateService()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATIONNAME,
            });
            return service;
        }
        public List<Student> GetStudents()
        {
            SpreadsheetsResource.ValuesResource.GetRequest request;
            SpreadsheetsResource.ValuesResource.GetRequest request2;
            students = new List<Student>();
            int WeekDay = (int)DateTime.Now.DayOfWeek;
            string day = DateTime.Now.ToString("dddd");
            if (day == "Sunday" || day == "Saturday")
                day = "Monday";
            var service = CreateService();
            // Define request parameters.
            spreadsheetId = GetCurrentTerm();
            range = day + "!B2:AF2";
            range2 = day + "!B28:AF28";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            response = request.Execute();
            request2 =service.Spreadsheets.Values.Get(spreadsheetId, range2);
            values = response.Values;
            response2 = request2.Execute();
            values2 = response2.Values;
            if (values != null && values.Count > 0)
            {
                for (int i = 0; i < values[0].Count; i++)
                {
                    s = new Student(values[0][i].ToString(), values2[0][i].ToString());
                    students.Add(s);
                }
            }
            return students;
        }
        public List<TriagePoints> GetTriagePoints(string tableName, string triageTitle)
        {
            var items = new List<TriagePoints>();
            var elapsedTimes = new List<int>();
            string query = "";
            try
            {
                for (int i = 182; i >= 7; i -= 7)
                {
                    query += "SELECT " + i.ToString() + " AS History,  SUM(DateDiff('s', TIMEVALUE('7:30'), TIMEVALUE(TimeOfDay)))/(SELECT COUNT(*) FROM " + tableName + " WHERE Task = '" + triageTitle + "' AND DATEVALUE(TaskDate) BETWEEN Now() -" + i.ToString() + " AND Now() -" + (i - 7).ToString()+")  AS TimeIntervals FROM " + tableName + "  WHERE Task = '" + triageTitle + "'  AND DATEVALUE(TaskDate) BETWEEN Now() -" + i.ToString() + " AND Now() -" + (i - 7).ToString() + " UNION ";
                }

                conn = new OleDbConnection(CONNECTIONSTRING);
                string getQuery = query.Remove(query.Length - 6);
                getQuery += " Order By History Desc";
                com = new OleDbCommand(getQuery, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    TriagePoints tp = new TriagePoints();
                    int daysBack = int.Parse(read[0].ToString());
                    tp.Week_Of = DateTime.Now.AddDays(-1 * daysBack + 1).ToShortDateString();
                    string time = read[1].ToString();
                    tp.Average_Elasped_Time = Math.Round(double.Parse(time) / 60,2);
                    items.Add(tp);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return items; 
        }

        //These methods create a table within the local database using values returned from SheetsAPI 
        public void CreateStaffTable() //Create a table to store staff with hours for day
        {
            List<string> headers = new List<string>();
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            query = "CREATE TABLE " + TEMPDAILY + " ([Initials] Text, [Hours] Int, [Day] Text, ";
            try
            {
                //Get values for each staff member to do for today
                string headersCommand = "SELECT [Task] FROM [" + STAFFCHECKLIST + "]";
                OleDbCommand getHeaders = new OleDbCommand(headersCommand, conn);
                conn.Open();
                read = getHeaders.ExecuteReader();
                while (read.Read())
                {
                    query += "[" + read[0].ToString() + "] Logical, ";
                }
                query = query.Substring(0, query.Length - 2);
                query += ")";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not retrieve headers for StaffTable, contact admin for help.  " + ex);
                conn.Close();
                return;
            }
            conn.Open();
            try
            {       
                com = new OleDbCommand();
                com.Connection = conn;
                com.CommandText = query;
                com.ExecuteNonQuery();              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in creating table.  Please contact admin. " + ex);
                return;
            }
             conn.Close();
        }

        //Methods for work on staff table
        public void SetupStaffTable()//Based upon current day, creates table with staff for today, their hours and the current day
        {
            conn = new OleDbConnection(CONNECTIONSTRING);
            string query = "INSERT INTO " + TEMPDAILY + " (Initials, Hours, [Day]) VALUES (?, ?, ?)";
            foreach (Student s in GetStudents())
            {
                if (double.Parse(s.hours) > 0)
                {
                    try
                    {
                        conn.Open();
                        com = new OleDbCommand(query, conn);
                        com.Parameters.AddWithValue("@Initials", s.name);
                        com.Parameters.AddWithValue("@Hours", double.Parse(s.hours));
                        com.Parameters.AddWithValue("@Day", DateTime.Now.ToShortDateString());
                        com.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(" " + ex);
                        return;
                    }
                }
            }
        }
        private List<ChecklistItem> GetTempSHEDValues()
        {
            checklistItems = new List<ChecklistItem>();
            query = "SELECT * FROM " + TEMPSHED;
            conn = new OleDbConnection(CONNECTIONSTRING);
            com = new OleDbCommand(query, conn);
            conn.Open();
            try
            {
                read = com.ExecuteReader();
                while (read.Read())
                {
                    c = new ChecklistItem();
                    c.name = read[1].ToString();
                    c.initials = read[0].ToString();
                    c.day = read[2].ToString();
                    c.time = read[5].ToString();
                    checklistItems.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get values from TempSHED table, contact admin for help.  " + ex);
                return null;
            }
            conn.Close();
            return checklistItems;
        }
        public void MoveTempSHED()
        {
            conn = new OleDbConnection(CONNECTIONSTRING);
            try
            {               
                foreach (ChecklistItem c in GetTempSHEDValues())
                {
                    conn.Open();
                    query = "INSERT INTO [" + SHEDWORK + "] ([Initials], [Task], [TaskDate], [TimeOfDay]) VALUES (?, ?, ?, ?)";
                    com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Initials", c.initials);
                    com.Parameters.AddWithValue("@Task", c.name);
                    com.Parameters.AddWithValue("@TaskDate", c.day);
                    com.Parameters.AddWithValue("@TimeOfDay", c.time);
                    com.ExecuteNonQuery();
                    conn.Close();
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not move temporary SHED tasks to permanent table, contact admin for help.  " + ex);
                return;
            }           
        }
        private List<ChecklistItem> GetTempATCValues()
        {
            checklistItems = new List<ChecklistItem>();
            query = "SELECT * FROM " + TEMPATC;
            conn = new OleDbConnection(CONNECTIONSTRING);
            com = new OleDbCommand(query, conn);
            conn.Open();
            try
            {
                read = com.ExecuteReader();
                while (read.Read())
                {
                    c = new ChecklistItem();
                    c.name = read[1].ToString();
                    c.initials = read[0].ToString();
                    c.day = read[2].ToString();
                    c.time = read[5].ToString();
                    checklistItems.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get values from TempATC table, contact admin for help.  " + ex);
                return null;
            }
            conn.Close();
            return checklistItems;
        }
        public void MoveTempATC()
        {
            conn = new OleDbConnection(CONNECTIONSTRING);           
            try
            {                
                foreach (ChecklistItem c in GetTempATCValues())
                {
                    conn.Open();
                    query = "INSERT INTO [" + ATCWORK + "] ([Initials], [Task], [TaskDate], [TimeOfDay]) VALUES (?, ?, ?, ?)";
                    com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Initials", c.initials);
                    com.Parameters.AddWithValue("@Task", c.name);
                    com.Parameters.AddWithValue("@TaskDate", c.day);
                    com.Parameters.AddWithValue("@TimeOfDay", c.time);
                    com.ExecuteNonQuery();
                    conn.Close();
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not move temporary ATCtasks to permanent table, contact admin for help.  " + ex);
                return;
            }           
        }
        public List<ChecklistItem> GetSHEDTasks()//Gets the tasks from the group tasks for the SHED that have no initials assigned to them.
        {
            checklistItems = new List<ChecklistItem>();
            string query = "SELECT * FROM [" + TEMPSHED + "]";
            try
            {
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    if (read[0].ToString() == string.Empty)
                    {
                        ChecklistItem c = new ChecklistItem();
                        c.name = read[1].ToString();
                        c.description = read[2].ToString();
                        c.time = read[4].ToString();
                        checklistItems.Add(c);
                    }
                }
                conn.Close();
                return checklistItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not return checklist items from local SHED database.  Contact admin.  " + ex);
                return null;
            }
        }
        public string AddSHEDEntry(string init, string task)//Updates the temporary shed task table to provide initials for the task completed by the student specified.
        {
            string output;
            string query = "UPDATE " + TEMPSHED + " SET Initials = ?, [TimeOfDay] = ? WHERE Task = ?";
            try
            {
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                com.Parameters.AddWithValue("@Initials", init);
                com.Parameters.AddWithValue("@TimeOfDay", DateTime.Now.ToString("HH:mm"));
                com.Parameters.AddWithValue("@Task", task);               
                conn.Open();
                com.ExecuteNonQuery();
                conn.Close();
                output = "Successfully added record.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update database. " + ex);
                return null;
            }
            return output;
        }
        public List<ChecklistItem> GetATCTasks()//gets tasks from tempatc table that are incomplete
        {
            checklistItems = new List<ChecklistItem>();
            string query = "SELECT * FROM [" + TEMPATC + "]";
            try
            {
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    if (read[0].ToString() == string.Empty)
                    {
                        ChecklistItem c = new ChecklistItem();
                        c.name = read[1].ToString();
                        c.description = read[2].ToString();
                        c.time = read[4].ToString();
                        checklistItems.Add(c);
                    }
                }
                conn.Close();
                return checklistItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not return checklist from local ATC database.  Contact admin." + ex);
                return null;
            }
        }

        public void CreateSHEDTable()//Create new instance of temporary shed table, should only be run if that table is somehow dropped.
        {
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            conn.Open();
            string strTemp = " [Initials] Text, [Task] Text, [TaskDate] Text, [Description] Text";
            OleDbCommand com = new OleDbCommand();
            com.Connection = conn;
            try
            {       
                com.CommandText = "CREATE TABLE ["+ TEMPSHED+"](" + strTemp + ")";
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error in creating local SHED table.  Please contact admin. " + e);
                return;
            } 
        }
   
        public void CreateATCTable()//creates a new instance of atctemp table within database, should only be run if table is somehow deleted
        {
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            conn.Open();
            string strTemp = " [Initials] Text, [Task] Text, [TaskDate] Text, [Description] Text";
            OleDbCommand com = new OleDbCommand();
            com.Connection = conn;
            try
            {
                com.CommandText = "CREATE TABLE [" + TEMPATC+ "](" + strTemp + ")";
                com.ExecuteNonQuery();
                conn.Close();
                SetupATCTable();
            }
            catch (Exception e)
            {
                if (e.Message == "Table '" + TEMPATC + "' already exists.")
                {
                    conn.Close();
                    return;
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Error in creating local ATC table.  Please contact admin. " + e);
                    return;
                }
            }
        }
        public void SetupSHEDTable()//gets values using sheets API and places tasks into local database table
        {
            SpreadsheetsResource.ValuesResource.GetRequest request;
            checklistItems = new List<ChecklistItem>();
            var service = CreateService();
            spreadsheetId = "1sYXuUY8fpeuOTDX_g-5BlyitsZ4TYG2Is_D7os4dzMs";
            range = "SHEDChecklist!A1:C100";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            response = request.Execute();
            values = response.Values;
            foreach (var items in values)
            {
                if (items.Count == 3)
                    c = new ChecklistItem(items[0].ToString(), items[1].ToString(), items[2].ToString());
                else
                    c = new ChecklistItem(items[0].ToString(), items[1].ToString(), string.Empty);
                checklistItems.Add(c);
            }
            checklistItems.RemoveAt(0);
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            try
            {
                foreach (ChecklistItem c in checklistItems)
                {
                    conn.Open();
                    string query = "INSERT INTO " + TEMPSHED + " (Task, Description, TaskDate, [Link]) VALUES(?, ?, ?, ?)";
                    OleDbCommand com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Task", c.name);
                    com.Parameters.AddWithValue("@Description", c.description);
                    com.Parameters.AddWithValue("@TaskDate", DateTime.Now.ToShortDateString());
                    com.Parameters.AddWithValue("@Link", c.documentation);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update values into daily tasks." + ex);
                return;
            }
        }               
        public List<string> TodaysStudents()//gets a list of the students working for today
        {
            try
            {
                List<string> students = new List<string>();
                string query = "SELECT Initials FROM " + TEMPDAILY;
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    students.Add(read[0].ToString());
                }
                students.Add("Admin");
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get student list from local database, please contact admin for help.  " + ex);
                return null;
            }
        }
    
        
        
        public List<DataPoints> GetDataPoints(string table)
        {
            var items = new List<DataPoints>();
            string query = "";
            for (int i = 182; i >= 7; i-=7)
            {
                query += "SELECT " + i.ToString() + " AS History, SUM(IIF(Initials <> '', 1, 0)) AS Completed, COUNT(*) AS Total, ROUND(100 * SUM(IIF(Initials <> '', 1, 0))/COUNT(*),2) AS [Percent Complete] FROM " + table + " WHERE DATEVALUE(TaskDate) BETWEEN Now() - " + i.ToString() + " AND Now() - " + (i - 7).ToString() + " UNION "; 
            }
            try
            {
                conn = new OleDbConnection(CONNECTIONSTRING);
                string queryString = query.Remove(query.Length - 6);
                queryString += " ORDER BY History Desc";
                com = new OleDbCommand(queryString, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    DataPoints d = new DataPoints();
                    int daysAgo;
                    daysAgo = -1 * int.Parse(read[0].ToString());
                    d.Week_Of = DateTime.Now.AddDays(daysAgo + 1).ToShortDateString();
                    d.Completed_Tasks = double.Parse(read[1].ToString());
                    d.Total_Tasks = double.Parse(read[2].ToString());
                    d.Percent_Completed = double.Parse(read[3].ToString());
                    items.Add(d);
                }
                conn.Close();
                return items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public ChecklistItem GetSHEDTaskDescription(string title)//based upon which task user selects, returns the description of that task
        {
            c = new ChecklistItem();
            try
            {
                string query = "SELECT Description, [Link] FROM " + TEMPSHED  + " WHERE Task = ?";
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                com.Parameters.AddWithValue("@Task", title);
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    c.description = read[0].ToString();
                    c.documentation = read[1].ToString();
                }
                conn.Close();
                c.description = FormatTaskString(c.description);
                return c;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get descriptions from local database, contact admin for help.  " + ex);
                return null;
            }
        }
        public ChecklistItem GetSoloTaskDescription(string title)
        {
            c = new ChecklistItem();
            try
            {
                string query = "Select Description, [Link] FROM " + STAFFCHECKLIST + " Where Task = ?";
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                com.Parameters.AddWithValue("@Task", title);
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    c.description = read[0].ToString();
                    c.documentation = read[1].ToString();
                }
                conn.Close();
                c.description = FormatTaskString(c.description);
                return c;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get descriptions from local database, contact admin for help.  " + ex);
                return null;
            }   
        }
        public ChecklistItem GetATCTaskDescription(string title)//based upon which task user selects, returns the description of that task
        {
            c = new ChecklistItem();
            try
            {
                string query = "SELECT Description, [Link] FROM " + TEMPATC + " WHERE Task = ?";
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                com.Parameters.AddWithValue("@Task", title);
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    c.description = read[0].ToString();
                    c.documentation = read[1].ToString();
                }
                conn.Close();
                c.description = FormatTaskString(c.description);
                return c;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get descriptions from local database, contact admin for help.  " + ex);
                return null;
            }
        }
        public void SetupATCTable()//uses google sheets api to obtain values to assign tasks into temporary ATC checklist
        {
            SpreadsheetsResource.ValuesResource.GetRequest request;
            checklistItems = new List<ChecklistItem>();
            var service = CreateService();
            spreadsheetId = "1sYXuUY8fpeuOTDX_g-5BlyitsZ4TYG2Is_D7os4dzMs";
            range = "ATCChecklist!A1:C100";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            response = request.Execute();
            values = response.Values;
            foreach (var items in values)
            {
                if (items.Count == 3)
                    c = new ChecklistItem(items[0].ToString(), items[1].ToString(), items[2].ToString());
                else
                    c = new ChecklistItem(items[0].ToString(), items[1].ToString(), string.Empty);
                checklistItems.Add(c);
            }       
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            conn.Open();
            checklistItems.RemoveAt(0);
            try
            {
                foreach (ChecklistItem c in checklistItems)
                {                
                    string query = "INSERT INTO [" + TEMPATC + "] (Task, Description, TaskDate, [Link]) VALUES(?, ?, ?, ?)";
                    OleDbCommand com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Task", c.name);
                    com.Parameters.AddWithValue("@Description", c.description);
                    com.Parameters.AddWithValue("@TaskDate", DateTime.Now.ToShortDateString());
                    com.Parameters.AddWithValue("@Link", c.documentation);
                    com.ExecuteNonQuery();                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update values into daily tasks." + ex);
                conn.Close();
                return;
            }
            conn.Close();
        }
        public string AddATCEntry(string init, string task)//assigns a student's initials to a task within the tempatc table
        {
            string output;
            string createdATCTable = "[ATC " + DateTime.Now.ToShortDateString() + "]";
            string query = "UPDATE " + TEMPATC + " SET Initials = ?, TimeOfDay = ? WHERE Task = ?";
            try
            {
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                com.Parameters.AddWithValue("@Initials", init);
                com.Parameters.AddWithValue("@TimeOfDay", DateTime.Now.ToString("HH:mm"));
                com.Parameters.AddWithValue("@Task", task);
                conn.Open();
                com.ExecuteNonQuery();
                conn.Close();
                output = "Successfully added record.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update database. " + ex);
                return null;
            }
            return output;
        }
        public string GetCurrentTerm()//uses sheets api to check for the current term to obtain the correct student list
        {
            SpreadsheetsResource.ValuesResource.GetRequest request;
            var service = CreateService();
            string currentTermSpreadsheet = null;
            spreadsheetId = "1sYXuUY8fpeuOTDX_g-5BlyitsZ4TYG2Is_D7os4dzMs";
            range = "Term Dates!A1:C100";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            response = request.Execute();
            values = response.Values;
            foreach (var items in values)
            {
                if (DateTime.Parse(items[1].ToString()) < DateTime.Now)
                    currentTermSpreadsheet = items[2].ToString();
                else if (DateTime.Parse(items[1].ToString()) < DateTime.Now)
                    break;                    
            }
            return currentTermSpreadsheet;
        }
        public void SetupStaffChecklist()
        {
            SpreadsheetsResource.ValuesResource.GetRequest request;
            var service = CreateService();
            checklistItems = new List<ChecklistItem>();
            spreadsheetId = "1sYXuUY8fpeuOTDX_g-5BlyitsZ4TYG2Is_D7os4dzMs";
            range = "StaffChecklist!A1:C100";
            request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            response = request.Execute();
            values = response.Values;
            foreach (var item in values)
            {
                if (item.Count == 3)
                    c = new ChecklistItem(item[0].ToString(), item[1].ToString(), item[2].ToString());
                else
                    c = new ChecklistItem(item[0].ToString(), item[1].ToString(), string.Empty);
                checklistItems.Add(c);
            }
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            checklistItems.RemoveAt(0);
            conn.Open();
            string query = "INSERT INTO [" + STAFFCHECKLIST + "] ([Task], [Description], [Link]) Values(?,?,?)";
            try
            {
                foreach (ChecklistItem item in checklistItems)
                {
                    if (item.documentation == null)
                        item.documentation = string.Empty;
                    OleDbCommand com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Task", item.name);
                    com.Parameters.AddWithValue("@Description", item.description);
                    com.Parameters.AddWithValue("@Link", item.documentation);
                    com.ExecuteNonQuery();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not store individual checklist items into local database.  Contact admin for help.  " + ex);
                conn.Close();
                return;
            }
            conn.Close();
        }
        public void CreateStaffChecklist()
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                conn.Open();
                string command = "CREATE TABLE [" + STAFFCHECKLIST + "] ([Task] Text, [Description] Text, [Link] Text)";
                OleDbCommand com = new OleDbCommand();
                com.Connection = conn;
                com.CommandText = command;
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {             
                MessageBox.Show("Error in creating local staff checklist table.  Please contact admin. " + ex);
                return;
            }
        }
        public List<ChecklistItem> GetIndividualTasks(string name = null)
        {
            try
            {
                checklistItems = new List<ChecklistItem>();
                string query = "SELECT * FROM " + STAFFCHECKLIST;
                OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
                OleDbCommand com = new OleDbCommand(query, conn);
                conn.Open();
                OleDbDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    c = new ChecklistItem(read[0].ToString(), read[1].ToString());
                    c.documentation = read[2].ToString();
                    checklistItems.Add(c);
                }             
                return checklistItems;
            }
            catch (Exception ex) {
                MessageBox.Show("Could not get tasks from local database, contact admin for help.  " + ex);
                return null;
            }
        }
        public string AddAllStaffCheckList(string name, string task)
        {
            string output;
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            try
            {
                string table = "DailyStaff";
                string query = "UPDATE " + table + " SET [" + task + "] = ? WHERE Initials = ?";
                OleDbCommand com = new OleDbCommand(query, conn);
                com.Parameters.AddWithValue("@" + task, true);
                com.Parameters.AddWithValue("@Initials", name);
                conn.Open();
                com.ExecuteNonQuery();
                conn.Close();
                output = "Successfully added record.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not successfuly update daily task.  Contact admin for help.  " + ex);
                conn.Close();
                return null;
            }
            return output;
        }
        public void ClearTables()
        {
            conn = new OleDbConnection(CONNECTIONSTRING);
            conn.Open();
            try
            {
                query = "DELETE * FROM " + TEMPSHED;
                com = new OleDbCommand(query, conn);
                com.ExecuteNonQuery();
                query = "DELETE * FROM " + TEMPATC;
                com = new OleDbCommand(query, conn);
                com.ExecuteNonQuery();               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not clear values from local temporary task tables.  Contact admin for help.  " + ex);
            }
            conn.Close();
            SetupATCTable();
            SetupSHEDTable();
        }
  
        public List<ChecklistItem> StoreIndividualTasks()
        {
            checklistItems = new List<ChecklistItem>();
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            string headersCommand = "SELECT [Task] FROM [" + STAFFCHECKLIST + "]";
            OleDbCommand getHeaders = new OleDbCommand(headersCommand, conn);
            conn.Open();
            OleDbDataReader read2 = getHeaders.ExecuteReader();
            while (read2.Read())
            {
                c = new ChecklistItem();
                c.name = read2[0].ToString();
                checklistItems.Add(c);
            }
            conn.Close();           
            var Items = new List<ChecklistItem>();
            foreach (string s in TodaysStudents())
            {
                foreach (ChecklistItem item in checklistItems)
                {
                    conn.Open();
                    query = "SELECT [" + item.name + "] FROM " + TEMPDAILY + " WHERE Initials = ?";
                    com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Initials", s);
                    read2 = com.ExecuteReader();
                    while (read2.Read())
                    {
                        c = new ChecklistItem();
                        c.day = DateTime.Now.ToShortDateString();
                        c.initials = s;
                        c.name = item.name;
                        c.complete = bool.Parse(read2[0].ToString());
                        Items.Add(c);
                    }
                    conn.Close();
                }
            }
            conn.Close();
            conn.Open();
            query = "DELETE * FROM " + STAFFCHECKLIST;
            com = new OleDbCommand(query, conn);
            com.ExecuteNonQuery();
            conn.Close();
            SetupStaffChecklist();
            return Items;
        }
        public void WriteIndividualTasks()
        {
            string query = "INSERT INTO " + INDIVIDUALWORK + " (Initials, Task, TaskDate, Completed) VALUES (?, ?, ?, ?)";
            conn = new OleDbConnection(CONNECTIONSTRING);
            conn.Open();
            try
            {
                foreach (ChecklistItem c in StoreIndividualTasks()) {
                    com = new OleDbCommand(query, conn);
                    com.Parameters.AddWithValue("@Initials", c.initials);
                    com.Parameters.AddWithValue("@Task", c.name);
                    com.Parameters.AddWithValue("@TaskDate", c.day);
                    com.Parameters.AddWithValue("@Completed", c.complete);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update values into individual tasks table.  Contact admin for help.  " + ex);
                return;
            }
            conn.Close();
        }
        public void ClearIndividualTaskTable()
        {
            OleDbConnection conn = new OleDbConnection(CONNECTIONSTRING);
            query = "DELETE * FROM " + TEMPDAILY;
            conn.Open();
            try
            {
                com = new OleDbCommand(query, conn);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not remove records for daily staff table.  Contact admin for help.  " + ex);
                return;
            }
            conn.Close();
            SetupStaffTable();
        }
        public void UpdateDate()
        {
            query = "UPDATE " + CURRENTDAY + " SET ChecklistDay = ? WHERE [ID] = ?";
            conn = new OleDbConnection(CONNECTIONSTRING);
            conn.Open();
            try
            {
                com = new OleDbCommand(query, conn);
                com.Parameters.AddWithValue("@ChecklistDay", DateTime.Now.ToShortDateString());
                com.Parameters.AddWithValue("@ID", 1);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update the current day of checklist, contact admin for help.  "  + ex);
                return;
            }               
        }    
        public double GetPercent(string table, DateTime sday, DateTime eday)
        {
            string theTable = "";
            if (table == ATCWORK)
                theTable = ATCWORK;
            else if (table == SHEDWORK)
                    theTable = SHEDWORK;
            double total = 0;
            double part = 0;
            double multiplier = 100;
            query = "SELECT [Initials], [Task], [TaskDate] FROM " + theTable;
            conn = new OleDbConnection(CONNECTIONSTRING);
            com = new OleDbCommand(query, conn);
            conn.Open();
            read = com.ExecuteReader();
            while (read.Read())
            {
                string currentDay = read[2].ToString();
                string student = read[0].ToString();
                if (student == string.Empty && DateTime.Parse(currentDay) >= sday && DateTime.Parse(currentDay) <= eday)
                    total++;
                else if (DateTime.Parse(currentDay) >= sday && DateTime.Parse(currentDay) <= eday)
                {
                    total++;
                    part++;
                }
            }
            conn.Close();
            double output = Math.Round((part / total * multiplier), 2);
            return (output);
        }
        public List<string> GetSolotasks(string student)//get incomplete solo tasks for individual students
        {
            conn = new OleDbConnection(CONNECTIONSTRING);
            List<string> items = new List<string>();
            query = "SELECT ";
            try
            {
                //Get values for each staff member to do for today
                string headersCommand = "SELECT [Task] FROM [" + STAFFCHECKLIST + "]";
                OleDbCommand getHeaders = new OleDbCommand(headersCommand, conn);
                conn.Open();
                read = getHeaders.ExecuteReader();
                while (read.Read())
                {
                    query += "[" + read[0].ToString() + "], ";
                    items.Add(read[0].ToString());
                }
                query = query.Substring(0, query.Length - 2);
                query += " FROM " + TEMPDAILY + " WHERE [Initials] = ?";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get checklist items for all staff members.  " + ex);
                conn.Close();
                return null;
            }
            conn.Open();
            try
            {
                string test = string.Empty; 
                com = new OleDbCommand();
                com.Connection = conn;
                com.CommandText = query;
                com.Parameters.AddWithValue("@Initials", student);
                read = com.ExecuteReader();
                while (read.Read())
                {
                    dailyItems = new DailychecklistItem(bool.Parse(read[0].ToString()), bool.Parse(read[1].ToString()), bool.Parse(read[2].ToString()), bool.Parse(read[3].ToString()), bool.Parse(read[4].ToString()));
                }       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in creating table.  Please contact admin. " + ex);
                return null;
            }            
            conn.Close();
            if (dailyItems.c5 == true)
                items.RemoveAt(4);
            if (dailyItems.c4 == true)
                items.RemoveAt(3);
            if (dailyItems.c3 == true)
                items.RemoveAt(2);
            if (dailyItems.c2 == true)
                items.RemoveAt(1);
            if (dailyItems.c1 == true)
                items.RemoveAt(0);
            return items;
        }   
        public string FormatTaskString(string task)
        {
            task = task.Replace(".", ".\r\n\r\n");
            return task;
        }
        public List<ChecklistItem> GetTimes()
        {
            checklistItems = new List<ChecklistItem>();
            conn = new OleDbConnection(CONNECTIONSTRING);
            query = "SELECT * FROM " + SHEDWORK;
            try
            {
                com = new OleDbCommand(query, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    c = new ChecklistItem();
                    c.name = read[2].ToString();
                    if (c.name == "Generate Triage List")
                    {
                        c.day = read[3].ToString();
                        if (read[4].ToString() == string.Empty)
                            c.time = "19:00";
                        else
                            c.time = read[4].ToString();
                        checklistItems.Add(c);
                    }
                    else
                        continue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
            conn.Close();
            return checklistItems;
        }
        public List<ChecklistItem> GetATCTimes()
        {

            checklistItems = new List<ChecklistItem>();
            conn = new OleDbConnection(CONNECTIONSTRING);
            query = "SELECT * FROM " + ATCWORK;
            try
            {
                com = new OleDbCommand(query, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    c = new ChecklistItem();
                    c.name = read[2].ToString();
                    if (c.name == "Daily Triage")
                    {
                        c.day = read[3].ToString();
                        if (read[4].ToString() == string.Empty)
                            c.time = "17:00";
                        else
                            c.time = read[4].ToString();
                        checklistItems.Add(c);
                    }
                    else
                        continue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
            conn.Close();
            return checklistItems;
        }
        public double GetAllTasks(string table)
        {
            checklistItems = new List<ChecklistItem>();
            conn = new OleDbConnection(CONNECTIONSTRING);
            query = "SELECT * FROM " + table;
            try
            {
                com = new OleDbCommand(query, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    c = new ChecklistItem();
                    c.name = read[2].ToString();
                    c.initials = read[1].ToString();
                    checklistItems.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not return tasks report from database. " + ex);
            }
            conn.Close();
            return checklistItems.Count;
        }
        public double StudentCounts(string table, string student)
        {
            double counter = 0;
            checklistItems = new List<ChecklistItem>();
            conn = new OleDbConnection(CONNECTIONSTRING);
            query = "SELECT * FROM " + table;
            try
            {
                com = new OleDbCommand(query, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    string initials = read[1].ToString();
                    if (initials == student)
                        counter++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not return tasks report from database. " + ex);
            }
            conn.Close();
            return counter;
        }
        public List<string> GetAllStudents(string table)
        {
            List<string> students = new List<string>();
            conn = new OleDbConnection(CONNECTIONSTRING);
            query = "SELECT * FROM " + table;
            try
            {
                com = new OleDbCommand(query, conn);
                conn.Open();
                read = com.ExecuteReader();
                while (read.Read())
                {
                    students.Add(read[1].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not return tasks report from database. " + ex);
            }
            List<string> unique = students.Distinct().ToList();
            return unique;
        }
        public double GetIndividualTaskPercent(string table, string student, string task)
        {
            double counter = 0;
            double counter2 = 0;
            checklistItems = new List<ChecklistItem>();
            conn = new OleDbConnection(CONNECTIONSTRING);
            query = "SELECT * FROM " + table + " WHERE [Initials] = ? AND [Task] = ?";
            try
            {
                com = new OleDbCommand(query, conn);
                conn.Open();
                com.Parameters.AddWithValue("@Initials", student);
                com.Parameters.AddWithValue("@Task", task);
                read = com.ExecuteReader();
                while (read.Read())
                {
                    if (bool.Parse(read[4].ToString()) == true)
                    {
                        counter++;
                        counter2++;
                    }
                    else
                        counter++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not return tasks report from database. " + ex);
            }
            conn.Close();

            return counter2/counter * 100;
        }
     
    }
}
