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

        private Process sourceProcess;
        private IntPtr attachHandle;
        private IntPtr processHandleWindow;

        private bot instance;
        private poetionbot mainForm;

        public poetionbot()
        {
            InitializeComponent();
        }

        private void poetionbot_Load(object sender, EventArgs e)
        {
            instance = new bot();
            unchecked
            {
                instance.manaAddress = (int)0xB0165C3C;
            }
           checkHandleStatus();
        }
        

        private void checkHandleStatus()
        {

            if (attachHandle.ToString() == "0") {
                mnuAttach.Text = "Attach To Path of Exile";
                notifyIcon1.Text = "poetionbot is attached";
            } else
            {
                
                mnuAttach.Text = "Detach From Path of Exile (" + attachHandle.ToString()+")";
                notifyIcon1.Text = "poetionbot is not attached";
                instance.handle = attachHandle;
                var ps = new pointerset();
                

                //MessageBox.Show(sourceProcess. .ToString("X"));

               // ps.baseAddress = sourceProcess.MainModule.BaseAddress.ToInt32();// .MainModule.EntryPointAddress.ToInt32();
                                                                       // ps.baseAddress = 0x00905A4D;
                                                                       //ps.baseAddress += 0x009D03C4;
                                                                       //ps.baseAddress += 0x009D03C4;
                                                                       //ps.baseAddress = 0x79D54610 - ps.baseAddress;
               // ps.baseAddress = 0x009D03C4;
              //  MessageBox.Show(ps.baseAddress.ToString("X") + "");

                //ps.baseAddress = 0x79A4A780;
             /*   ps.offsets = new int[5];
                ps.offsets[0] = 0xA4;
                ps.offsets[1] = 0x408;
                ps.offsets[2] = 0x378;
                ps.offsets[3] = 0x188;
                ps.offsets[4] = 0x7DC;
                var mana = w32.ReadProcessMemoryOffset(attachHandle, ps);
                MessageBox.Show("MAna:" + mana);
                */
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          //  var didPress = w32.PressKey(0x02, 1);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (processHandleWindow != w32.GetForegroundWindow()) return;

            var retString = instance.CheckStats();
            if (retString.Length > 0) {
                //MessageBox.Show(retString);
                txtLog.Text = txtLog.Text + "\n" + DateTime.Now + ":" +retString +"\n";
            }

        }

        private void attachToPathOfExileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attachHandle.ToString() != "0")
            {
                attachHandle = new IntPtr();
                checkHandleStatus();
                return;
            }

            timer1.Start();

            
            var processes = w32.GetProcessList("pathofexilesteam");
            if (processes.Length == 1)
            {
                attachHandle = w32.AttachProcess(processes[0]);
                sourceProcess = processes[0];
                processHandleWindow = processes[0].MainWindowHandle;

                var mana = w32.ReadProcessMemory(attachHandle, instance.manaAddress);
                if (mana == 0)
                {
                    MessageBox.Show("Mana is not set!");
                    return;
                }
                tmrRefresh.Start();
                checkHandleStatus();
            }
            else
            {                
                MessageBox.Show(processes.Length + "Instance of Path of Exile found, which is not currently supported.");
                return;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void poetionbot_Activated(object sender, EventArgs e)
        {
            
            if (mainForm == null)
            {
                mainForm = this;
                mainForm.Hide();
            }
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            mainForm.Activate();                        
        }

        private void poetionbot_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void poetionbot_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Hide();
            e.Cancel = true;
        }
    }
}
