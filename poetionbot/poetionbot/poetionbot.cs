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
using System.Runtime.InteropServices;

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

        private bool isSystrayExit;

        public poetionbot()
        {
            InitializeComponent();
        }

        private void poetionbot_Load(object sender, EventArgs e)
        {
           

            changeIcon("blackFlask");
            instance = new bot();

            
            
            checkHandleStatus();
            attachPoE();
            loadConfig();
        }

        private void loadConfig()
        {

            instance.LoadIni();

            tck1.Value = (int)((float)instance.rules[0].percent * (float)100);
            tck2.Value = (int)((float)instance.rules[1].percent * (float)100);
            tck3.Value = (int)((float)instance.rules[2].percent * (float)100);
            tck4.Value = (int)((float)instance.rules[3].percent * (float)100);
            tck5.Value = (int)((float)instance.rules[4].percent * (float)100);

            
            chkHP1.Checked = !instance.rules[0].isHP;
            chkHP2.Checked = !instance.rules[1].isHP;
            chkHP3.Checked = !instance.rules[2].isHP;
            chkHP4.Checked = !instance.rules[3].isHP;
            chkHP5.Checked = !instance.rules[4].isHP;
            
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
            notifyIcon1.Text = instance.GetStats();
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
                   // MessageBox.Show(retString);
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
            if (processes.Length == 0 )
            {
                changeIcon("blackFlask");
            }
            else if (processes.Length == 1)
            {
                attachHandle = w32.AttachProcess(processes[0]);
                sourceProcess = processes[0];
                processHandleWindow = processes[0].MainWindowHandle;
                instance.ps.baseAddress = sourceProcess.MainModule.BaseAddress.ToInt32();
                
                var mana = w32.ReadProcessMemoryOffset(attachHandle, instance.ps, 0);
                if (mana == 0)
                {
                    MessageBox.Show("Pointers don't seem to be aligned properly...");
                    return;
                }

                //MessageBox.Show(mana+"");

                instance.handle = attachHandle;
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
            isSystrayExit = true;
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
            if (!isSystrayExit)
            { //Only exit if the systray invokes it.
                e.Cancel = true;
            }
            
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
                    return;
                case "blueFlask":
                    notifyIcon1.Icon = Properties.Resources.blueFlask;                    
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
