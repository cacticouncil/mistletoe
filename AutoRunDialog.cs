using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinMoss
{
    public partial class AutoRunDialog : Form
    {
        private int startTime;
        public AutoRunDialog()
        {
            InitializeComponent();
        }
        
        private void AutoRunDialog_Load(object sender, EventArgs e)
        {
            this.startTime = System.Environment.TickCount;
        }

        private void btnRunNow_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrAutoRun_Tick(object sender, EventArgs e)
        {
            int currentTime = System.Environment.TickCount;
            int timeDiff = currentTime - this.startTime;

            if (timeDiff > 15000)
            {
                this.btnRunNow.PerformClick();
            }
            else
            {
                this.lblRunningIn.Text = "MOSS AutoRunning in " + ((15000-timeDiff) / 1000).ToString() + " seconds";
            }

        }




    }
}
