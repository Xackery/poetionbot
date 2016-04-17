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
        private bool isLoaded;
        /// <summary>
        /// Are there unsaved changes?
        /// </summary>
        private bool isUnsaved;

        public poetionbot()
        {
            InitializeComponent();
        }

        private void poetionbot_Load(object sender, EventArgs e)
        {
            this.Text = "poetionbot " + Application.ProductVersion.ToString();
            changeIcon("blackFlask");
            instance = new bot();
            checkHandleStatus();            
            loadConfig();
            attachPoE();
        }

        private void loadConfig()
        {
            isLoaded = false;
            instance.LoadIni();

            tmrRefresh.Interval = instance.config.UpdateRateInMs;

            tck1.Value = (int)((float)instance.config.Rules[0].Percent * (float)100);
            tck2.Value = (int)((float)instance.config.Rules[1].Percent * (float)100);
            tck3.Value = (int)((float)instance.config.Rules[2].Percent * (float)100);
            tck4.Value = (int)((float)instance.config.Rules[3].Percent * (float)100);
            tck5.Value = (int)((float)instance.config.Rules[4].Percent * (float)100);

            
            chkHP1.Checked = instance.config.Rules[0].IsHPTrigger;
            chkHP2.Checked = instance.config.Rules[1].IsHPTrigger;
            chkHP3.Checked = instance.config.Rules[2].IsHPTrigger;
            chkHP4.Checked = instance.config.Rules[3].IsHPTrigger;
            chkHP5.Checked = instance.config.Rules[4].IsHPTrigger;
            isLoaded = true;
            cmdSave.Enabled = false;
           rulesync();
        }

        private void rulesync()
        {
            lblTck1.Text = "1 - " + (int)((float)instance.config.Rules[0].Percent * (float)100) + "%";
            lblTck2.Text = "2 - " + (int)((float)instance.config.Rules[1].Percent * (float)100) + "%";
            lblTck3.Text = "3 - " + (int)((float)instance.config.Rules[2].Percent * (float)100) + "%";
            lblTck4.Text = "4 - " + (int)((float)instance.config.Rules[3].Percent * (float)100) + "%";
            lblTck5.Text = "5 - " + (int)((float)instance.config.Rules[4].Percent * (float)100) + "%";
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
            } else if (currentFlask == "swirlFlask")
            {
                changeIcon("greenFlask");
            }


            //try {
                var retString = instance.CheckHealth();
                if (retString == "HP is invalid")
                {
                    changeIcon("blackFlask");
                    //detachPoE();
                    return;
                }
                
                if (retString.Length > 0)
                {
                    //MessageBox.Show(retString);
                    changeIcon("redFlask");
                    txtLog.Text = txtLog.Text + "\n" + DateTime.Now + ":" + retString + "\n";
                }
                retString = instance.CheckMana();
                if (retString == "Mana is invalid")
                {
                    changeIcon("blackFlask");
                    //detachPoE();
                    return;
                }

                if (retString.Length > 0)
                {
                   // MessageBox.Show(retString);
                    changeIcon("blueFlask");
                    txtLog.Text = txtLog.Text + "\n" + DateTime.Now + ":" + retString + "\n";
                }
               
           /* } catch (Exception err)
            {
                if (err.Message == "Memory Exception")
                {
                    notifyIcon1.Text = "poetionbot is attached, but memory is not properly aligned.";
                    changeIcon("purpleFlask");
                    //txtLog.Text += "Challenges!" + Environment.NewLine;
                    return;
                } else
                {
                    MessageBox.Show("An error:", err.Message);
                }
            }
            */
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
                instance.config.ManaPointerSet.SetBaseAddress(sourceProcess.MainModule.BaseAddress.ToInt32());
                
                var mana = w32.ReadProcessMemoryOffset(attachHandle, instance.config.ManaPointerSet, 0);
                if (mana == 0)
                {
                    MessageBox.Show("Hmm, something is wrong. Either the game patched, or you aren't logged into a character. Right-Click and attach once this situation comes up.");
                    detachPoE();
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
            notifyIcon1.Text = "Path of Exile is currently not attached.";
            tmrRefresh.Stop();
            tmrGreenReset.Stop();
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
            if (!isLoaded) return;
            instance.config.Rules[4].Percent = (float)((float)tck5.Value / (float)100);
           rulesync();
            unsavedUpdate();
        }

        private void chkHP1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[0].IsHPTrigger = chkHP1.Checked;            
           rulesync();
            unsavedUpdate();
        }
       
        private void chkHP2_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[1].IsHPTrigger = chkHP4.Checked;
           rulesync();
            unsavedUpdate();
        }

        private void chkHP3_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[2].IsHPTrigger = chkHP4.Checked;
           rulesync();
            unsavedUpdate();
        }

        private void chkHP4_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[3].IsHPTrigger = chkHP4.Checked;
           rulesync();
            unsavedUpdate();
        }
        private void chkHP5_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[4].IsHPTrigger = chkHP4.Checked;
           rulesync();
            unsavedUpdate();
        }

        private void tck1_Scroll(object sender, EventArgs e)
        {

        }

        private void tck1_ValueChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[0].Percent = (float)((float)tck1.Value / (float)100);
           rulesync();
            unsavedUpdate();
        }

        private void tck2_ValueChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[1].Percent = (float)((float)tck2.Value / (float)100);
           rulesync();
            unsavedUpdate();
        }

        private void tck3_ValueChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[2].Percent = (float)((float)tck3.Value / (float)100);
           rulesync();
            unsavedUpdate();
        }

        private void tck4_Scroll(object sender, EventArgs e)
        {

        }

        private void tck4_ValueChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            instance.config.Rules[3].Percent = (float)((float)tck4.Value / (float)100);
           rulesync();
            unsavedUpdate();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void unsavedUpdate()
        {
            isUnsaved = true;
            cmdSave.Enabled = true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {            
            if (!isUnsaved)
            {
                cmdSave.Enabled = false;
                return;
            }
            
            instance.SaveIni();
            isUnsaved = false;
            cmdSave.Enabled = false;
        }

        private void cmdReload_Click(object sender, EventArgs e)
        {
            isLoaded = false;
            instance.LoadIni();
            tmrRefresh.Interval = instance.config.UpdateRateInMs;
            tck1.Value = (int)((float)instance.config.Rules[0].Percent * (float)100);
            tck2.Value = (int)((float)instance.config.Rules[1].Percent * (float)100);
            tck3.Value = (int)((float)instance.config.Rules[2].Percent * (float)100);
            tck4.Value = (int)((float)instance.config.Rules[3].Percent * (float)100);
            tck5.Value = (int)((float)instance.config.Rules[4].Percent * (float)100);
            rulesync();
            isUnsaved = false;
            cmdSave.Enabled = false;
            isLoaded = true;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Poetionbot Version "+Application.ProductVersion.ToString());
        }
    }
}
