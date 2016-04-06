using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace poetionbot
{
    public partial class poetionbot : Form
    {
        
        private IntPtr handle;

        public poetionbot()
        {
            InitializeComponent();
        }

        private void poetionbot_Load(object sender, EventArgs e)
        {

        }

        private void mnuAttach_Click(object sender, EventArgs e)
        {
                        
            if (handle != null)
            {
                checkHandleStatus();
                return;
            }

            var processes = w32.GetProcessList("notepad.exe");
            if (processes.Length == 1)
            {
                handle = w32.AttachProcess(processes[0]);
                checkHandleStatus();
            }
        }

        private void checkHandleStatus()
        {
            if (handle == null) {
                mnuAttach.Text = "Attach To Path of Exile";
            } else
            {
                mnuAttach.Text = "Detach From Path of Exile";
            }
        }
    }
}
