using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace SHeDChecklist
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex mutex = null;
        [STAThread]      
        static void Main()
        {
            string mutexName = String.Format("Global\\{{{0}}}", "Application Name");
            bool mutexCreated = true;
            using (mutex = new Mutex(true, mutexName, out mutexCreated))
            {
                if (!mutexCreated)
                {
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ChecklistSelect());
            }
        }
    }
}
