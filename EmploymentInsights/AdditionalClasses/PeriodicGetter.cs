using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using EmploymentInsights.AdditionalClasses;
using EmploymentInsights.Models;

namespace EmploymentInsights
{
    public class PeriodicGetter
    {
        public int interval { get; set; } //interval in minutes

        private bool started = false;

        Timer timer;

        public void Start() {
            if (!started)
            {
                timer = new Timer(interval * 60000);
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Start();
                started = true;
            }
        }

        public void Stop() {
            if (started) {
                timer.Stop();
                started = false;
            }
        }

        public bool isRunning() {
            return timer.Enabled;
        }

        private async static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            DBConnection dbConnection = new DBConnection();
            dbConnection.Initialize();
            AdzunaConnection adConn = new AdzunaConnection();
            IList<Vacature> vacatures = await adConn.GetVacatureAsync(1, 10);
            foreach (Vacature i in vacatures) {
                if (dbConnection.isNotExisting(int.Parse(i.id)) == true) {
                    dbConnection.UploadVacature(i);
                }
            }
        }

    }
}