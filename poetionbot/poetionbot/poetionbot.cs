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

        private string currentFlask;

        public poetionbot()
        {
            InitializeComponent();
        }

        private void poetionbot_Load(object sender, EventArgs e)
        {
            changeIcon("blackFlask");
            instance = new bot();
            unchecked
            {
                instance.manaAddress = (int)0xB0165C3C;
            }
           checkHandleStatus();
           attachPoE();
            loadConfig();
        }

        private void loadConfig()
        {
            instance.rules[1].isHP = true;
            instance.rules[1].percent = 0.4f;
            
            instance.rules[3].isHP = true;
            instance.rules[3].percent = 0.4f;

            instance.rules[2].isHP = true;
            instance.rules[2].percent = 0.5f;

            instance.rules[0].isHP = true;
            instance.rules[0].percent = 0.8f;

            instance.rules[4].isHP = false;
            instance.rules[4].percent = 0.1f;

            chkHP1.Checked = instance.rules[0].isHP;
            chkHP2.Checked = instance.rules[1].isHP;
            chkHP3.Checked = instance.rules[2].isHP;
            chkHP4.Checked = instance.rules[3].isHP;
            chkHP5.Checked = instance.rules[4].isHP;

            tck1.Value = (int)((float)instance.rules[0].percent * (float)100);
            tck2.Value = (int)((float)instance.rules[1].percent * (float)100);
            tck3.Value = (int)((float)instance.rules[2].percent * (float)100);
            tck4.Value = (int)((float)instance.rules[3].percent * (float)100);
            tck5.Value = (int)((float)instance.rules[4].percent * (float)100);

        }

        private void ruleSync()
        {
            lblTck1.Text = "1 - " + (int)((float)instance.rules[0].percent * (float)100);
            lblTck2.Text = "2 - " + (int)((float)instance.rules[1].percent * (float)100);
            lblTck3.Text = "3 - " + (int)((float)instance.rules[2].percent * (float)100);
            lblTck4.Text = "4 - " + (int)((float)instance.rules[3].percent * (float)100);
            lblTck5.Text = "5 - " + (int)((float)instance.rules[4].percent * (float)100);
        }


        private void checkHandleStatus()
        {
            /*
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
          //  }
        
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
            if (processHandleWindow != w32.GetForegroundWindow())
            {
                changeIcon("swirlFlask");
                return;
            } else
            {
                changeIcon("greenFlask");
            }

            try {
                var retString = instance.CheckHealth();
                if (retString.Length > 0)
                {
                    //MessageBox.Show(retString);
                    changeIcon("redFlask");
                    txtLog.Text = txtLog.Text + "\n" + DateTime.Now + ":" + retString + "\n";
                }
                retString = instance.CheckMana();
                if (retString.Length > 0)
                {
                    //MessageBox.Show(retString);
                    changeIcon("blueFlask");
                    txtLog.Text = txtLog.Text + "\n" + DateTime.Now + ":" + retString + "\n";
                }
            } catch (Exception err)
            {
                if (err.Message == "Memory Exception")
                {
                    notifyIcon1.Text = "poetionbot is attached, but memory is not properly aligned.";
                    changeIcon("purpleFlask");
                    return;
                }
            }
           

        }


        private void attachPoE()
        {
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
                changeIcon("greenFlask");
                attachToPathOfExileToolStripMenuItem.Text = "Detach from Path of Exile";
                notifyIcon1.Text = "poetionbot is attached";
            }
            else
            {
                MessageBox.Show(processes.Length + " Instances of Path of Exile found, which is not currently supported.");
                notifyIcon1.Text = "poetionbot is detached";
                return;
            }
        }

        private void detachPoE()
        {
            attachHandle = new IntPtr();
            checkHandleStatus();
            changeIcon("blackFlask");
            attachToPathOfExileToolStripMenuItem.Text = "Attach to Path of Exile";
        }

        private void attachToPathOfExileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attachHandle.ToString() != "0")
            {
                detachPoE();
                return;
            }

            attachPoE();
            
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

        private void changeIcon(string iconType)
        {
            if (currentFlask == iconType)
            {
                return;
            }
            currentFlask = iconType;
            switch (iconType)
            {
                case "blackFlask":
                    notifyIcon1.Icon = Properties.Resources.blackFlask;
                    return;
                case "redFlask":
                    notifyIcon1.Icon = Properties.Resources.redFlask;
                    if (!tmrGreenReset.Enabled) {
                        tmrGreenReset.Start();
                    } else
                    {
                        tmrGreenReset.Stop();
                        tmrGreenReset.Start();
                    }
                    return;
                case "blueFlask":
                    notifyIcon1.Icon = Properties.Resources.blueFlask;
                    if (!tmrGreenReset.Enabled)
                    {
                        tmrGreenReset.Start();
                    } else
                    {
                        tmrGreenReset.Stop();
                        tmrGreenReset.Start();
                    }
                    return;
                case "greenFlask":
                    notifyIcon1.Icon = Properties.Resources.greenFlask;
                    return;
                case "purpleFlask":
                    notifyIcon1.Icon = Properties.Resources.purpleFlask;
                    return;
                case "swirlFlask":
                    notifyIcon1.Icon = Properties.Resources.swirlFlask;
                    return;
            }

        }

        private void tmrGreenReset_Tick(object sender, EventArgs e)
        {
            changeIcon("greenFlask");
        }

        private void tck5_ValueChanged(object sender, EventArgs e)
        {
            instance.rules[4].percent = (float)((float)tck5.Value / (float)100);
            ruleSync();
        }

        private void chkHP1_CheckedChanged(object sender, EventArgs e)
        {
            instance.rules[0].isHP = chkHP1.Checked;
            ruleSync();
        }
       
        private void chkHP2_CheckedChanged(object sender, EventArgs e)
        {
            instance.rules[1].isHP = chkHP4.Checked;
            ruleSync();
        }

        private void chkHP3_CheckedChanged(object sender, EventArgs e)
        {
            instance.rules[2].isHP = chkHP4.Checked;
            ruleSync();
        }

        private void chkHP4_CheckedChanged(object sender, EventArgs e)
        {
            instance.rules[3].isHP = chkHP4.Checked;
            ruleSync();
        }
        private void chkHP5_CheckedChanged(object sender, EventArgs e)
        {
            instance.rules[4].isHP = chkHP4.Checked;
            ruleSync();
        }

        private void tck1_Scroll(object sender, EventArgs e)
        {

        }

        private void tck1_ValueChanged(object sender, EventArgs e)
        {
            instance.rules[0].percent = (float)((float)tck1.Value / (float)100);
            ruleSync();
        }

        private void tck2_ValueChanged(object sender, EventArgs e)
        {
            instance.rules[1].percent = (float)((float)tck2.Value / (float)100);
            ruleSync();
        }

        private void tck3_ValueChanged(object sender, EventArgs e)
        {
            instance.rules[2].percent = (float)((float)tck3.Value / (float)100);
            ruleSync();
        }

        private void tck4_Scroll(object sender, EventArgs e)
        {

        }

        private void tck4_ValueChanged(object sender, EventArgs e)
        {
            instance.rules[3].percent = (float)((float)tck4.Value / (float)100);
            ruleSync();
        }
    }
}
