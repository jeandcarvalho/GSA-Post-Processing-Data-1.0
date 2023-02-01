using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FfmpegApp.Class
{
    public class User_Info
    {
        public User_Info() 
        {
        }

        public void InfoStop()
        {
            for (int i = 0; i < 100000; i++)
            {
                Thread.Sleep(1000);
                if (Process.GetProcessesByName("ffmpeg").Length == 1)
                {
                    MessageBox.Show("Process is complete!");
                    break;
                }
            }
        }
    }
}
