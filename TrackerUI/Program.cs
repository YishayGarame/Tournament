using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
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

            //Initilize the database connections
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql);

            Application.Run(new TournamentDashboardForm());
            //Application.Run(new TournamentDashboardForm());
        }
    }
}
  

// TODO check the email stuff
// TODO check the prize store procedure error
// TODO check the full tournament if i can change winners and see what is happening
// TODO fix windows size
// TODO check the tournament name if empty
// TODO sqlConnector line 182
// TODO check why it doesnt close the event handler



