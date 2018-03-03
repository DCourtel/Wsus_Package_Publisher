using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class FrmInstallPendingUpdatesNow : Form
    {
        private struct DataForComputer
        {
            internal DataGridViewRow Row { get; set; }
            internal string[] Options { get; set; }
            internal System.Threading.CountdownEvent CountDown { get; set; }
        }
        List<ADComputer> _computers = new List<ADComputer>();
        private static bool _closing;
        private static bool _aborting;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmInstallPendingUpdatesNow).Assembly);

        public FrmInstallPendingUpdatesNow()
        {
            Logger.EnteringMethod("FrmInstallPendingUpdatesNow");
            InitializeComponent();
            _closing = false;
            _aborting = false;
        }

        #region (Properties - Propriétés)

        internal List<ADComputer> Computers
        {
            get { return _computers; }
            set
            {
                Logger.Write("Setting computers (" + value.Count + ")");
                _computers = value;
            }
        }

        internal string Username { get; set; }

        internal string Password { get; set; }

        internal bool PersonalizeSearchString
        {
            get { return chkBxPersonalizeSearchString.Checked; }
            set
            {
                Logger.Write("Using personalize SearchString ? " + value.ToString());
                chkBxPersonalizeSearchString.Checked = value;
                if (value == false)
                    txtBxPersonalizeSearchString.Text = "IsInstalled=0 And IsHidden=0 And Type='Software'";
            }
        }

        internal string SearchString
        {
            get { return txtBxPersonalizeSearchString.Text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Logger.Write("Setting SearchString to : " + value);
                    chkBxPersonalizeSearchString.Checked = true;
                    txtBxPersonalizeSearchString.Text = value;
                }
            }
        }

        internal bool IncludeUpdatesWithRebootRequire
        {
            get { return chkBxIncludeUpdatesthatRequireReboot.Checked; }
            set
            {
                Logger.Write("Include Updates With Reboot required ? " + value.ToString());
                chkBxIncludeUpdatesthatRequireReboot.Checked = value;
            }
        }

        internal bool CancelIfRebootPending
        {
            get { return chkBxCancelIfRebootIsPending.Checked; }
            set
            {
                Logger.Write("Cancel if reboot is pending ? " + value.ToString());
                chkBxCancelIfRebootIsPending.Checked = value;
            }
        }

        #endregion (Properties - Propriétés)

        #region (Private Methods - Méthodes privées)

        private string[] GetOptions()
        {
            List<string> result = new List<string>();

            if (chkBxPersonalizeSearchString.Checked)
                result.Add("searchstring=" + txtBxPersonalizeSearchString.Text);
            if (chkBxIncludeUpdatesthatRequireReboot.Checked)
                result.Add("includeupdateswithrebootrequire=true");
            if (!chkBxCancelIfRebootIsPending.Checked)
                result.Add("cancelifrebootrequire=false");
            string tempResult = string.Empty;
            foreach (string option in result)
            {
                tempResult += option + "\r\n";
            }
            Logger.Write("Returning Options : " + tempResult);
            return result.ToArray();
        }

        private void SendUpdateCommand()
        {
            Logger.EnteringMethod();
            string[] options = GetOptions();

            if (dtGrvComputers.Rows.Count > 0)
            {
                System.Threading.CountdownEvent countDown = new System.Threading.CountdownEvent(1);
                foreach (DataGridViewRow row in dtGrvComputers.Rows)
                {
                    if (_aborting || _closing)
                        break;
                    countDown.AddCount();
                    DataForComputer data = new DataForComputer();
                    data.Row = row;
                    data.Options = options;
                    data.CountDown = countDown;
                    System.Threading.ThreadPool.QueueUserWorkItem(SendCommandToComputer, data);
                }
                countDown.Signal();
                countDown.Wait();
            }
            Action finishAction = () =>
            {
                chkBxCancelIfRebootIsPending.Enabled = true;
                chkBxIncludeUpdatesthatRequireReboot.Enabled = true;
                chkBxPersonalizeSearchString.Enabled = true;
                txtBxPersonalizeSearchString.Enabled = true;
                btnStartUpdating.Enabled = true;
                btnClose.Enabled = true;
                _aborting = false;
            };
            if (!_closing)
                this.Invoke(finishAction);
        }

        private void SendCommandToComputer(object data)
        {
            ADComputer computer = null;
            DataForComputer dataForComputer = (DataForComputer)data;
            DataGridViewRow row = dataForComputer.Row;
            String[] options = dataForComputer.Options;
            System.Threading.CountdownEvent countDown = dataForComputer.CountDown;

            try
            {
                Action startAction = () =>
                {
                    lock (dtGrvComputers)
                    {
                        if (!_closing && !_aborting)
                        {
                            row.Cells["Status"].Value = resMan.GetString("SendingCommand");
                            computer = (ADComputer)row.Cells["Computer"].Value;
                        }
                    }
                    Logger.Write("Will try to send InstallPendingUpdates on : " + computer.Name);
                };
                if (!_closing && !_aborting)
                    this.Invoke(startAction);

                ADComputer.InstallPendingUpdatesResult result = ADComputer.InstallPendingUpdatesResult.FailToSendCommand;
                if (!_closing && !_aborting)
                    result = computer.InstallPendingUpdates(options, Username, Password);

                Action endAction = () =>
                {
                    Logger.Write("Result for " + computer.Name + " is : " + result.ToString());

                    lock (dtGrvComputers)
                    {
                        if (!_closing && !_aborting)
                        {
                            row.Cells["Status"].Value = resMan.GetString(result.ToString());
                        }
                    }
                };
                if (!_closing && !_aborting)
                    this.Invoke(endAction);
            }
            catch (Exception) { }
            finally { countDown.Signal(); }
        }

        private void ClearStatus()
        {
            dtGrvComputers.SuspendLayout();

            foreach (DataGridViewRow row in dtGrvComputers.Rows)
            {
                row.Cells["Status"].Value = string.Empty;
            }

            dtGrvComputers.ResumeLayout();
        }

        #endregion (Private Methods - Méthodes privées)

        #region (Responses to Event - Réponses aux événements)

        private void FrmInstallPendingUpdatesNow_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            foreach (ADComputer computer in Computers)
            {
                Logger.Write("Adding computer : " + computer.Name);
                dtGrvComputers.Rows.Add(computer, computer.Name, string.Empty);
            }
        }

        private void chkBxPersonalizeSearchString_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Write(chkBxPersonalizeSearchString);
            txtBxPersonalizeSearchString.Enabled = chkBxPersonalizeSearchString.Checked;
        }

        private void btnStartUpdating_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnStartUpdating.Enabled = false;
            btnClose.Enabled = false;
            chkBxCancelIfRebootIsPending.Enabled = false;
            chkBxIncludeUpdatesthatRequireReboot.Enabled = false;
            chkBxPersonalizeSearchString.Enabled = false;
            txtBxPersonalizeSearchString.Enabled = false;
            ClearStatus();
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(SendUpdateCommand));
            t.Priority = System.Threading.ThreadPriority.BelowNormal;
            t.Start();
            btnAbort.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            _aborting = true;
            btnAbort.Enabled = false;
        }

        private void chkBxIncludeUpdatesthatRequireReboot_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Write(chkBxIncludeUpdatesthatRequireReboot);
            lblRebootWarning.Enabled = chkBxIncludeUpdatesthatRequireReboot.Checked;
        }

        private void FrmInstallPendingUpdatesNow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
        }

        #endregion (Responses to Event - Réponses aux événements)

    }
}
