using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrsova
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new LoginForm());
            //Application.Run(new MainForm());
            // Application.Run(new clientForm());
            //Application.Run(new reservForm());
            //Application.Run(new workersForm());
            //Application.Run(new airlinesForm());
            //Application.Run(new hotelsForm());
            //Application.Run(new tursForm());
        }
    }
}
