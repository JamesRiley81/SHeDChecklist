using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace SHeDChecklist
{
    public partial class ChecklistSelect : Form
    {
        public ChecklistSelect()
        {
            InitializeComponent();
        }
        private string ADMINPASSWORD = "ReportsAdmin";
        private void SHeDChecklist_Click(object sender, EventArgs e)
        {
            Form shedChecklist = new SHeDChecklist();
            shedChecklist.ShowDialog();
        }

        private void ATCButton_Click(object sender, EventArgs e)
        {
            Form atclist = new ATCChecklist();
            atclist.ShowDialog();
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            string input;
            do
            {
                input = Interaction.InputBox("Please input admin password.", "Admin Passwod");
                if (input == string.Empty)
                    return;
            } while (input != ADMINPASSWORD);
            Form aList = new AdminForm();
            aList.ShowDialog();
        }

        private void ChecklistSelect_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(181, 174, 249);
            Database d = new Database();
            d.GetDataPoints("SHEDWork");
            if (!d.CheckDay())
            {
                Task.Run(() => { MessageBox.Show("Program is busy updating values.  Do not attempt to restart or stop program until finished."); });
                d.MoveTempSHED();
                d.MoveTempATC();
                d.WriteIndividualTasks();
                d.ClearTables();
                d.ClearIndividualTaskTable();
                d.UpdateDate();               
            }
        }
        private void allButton_Click(object sender, EventArgs e)
        {
            Form allStaff = new AllStaffChecklist();
            allStaff.ShowDialog();
        }
    }
}
