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
    internal partial class FrmChangeMaxCabSize : Form
    {
        WsusWrapper _wsus = WsusWrapper.GetInstance();
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmChangeMaxCabSize).Assembly);

        internal FrmChangeMaxCabSize(string filename, int fileSize)
        {
            Logger.EnteringMethod(filename + " : " + fileSize.ToString());
            InitializeComponent();
            this.nupFileSize.Maximum = int.MaxValue;
            this.lblCurrentSetting.Text = _wsus.LocalPublishingMaxCabSize.ToString() + " " + resManager.GetString("MegaBytes");
            if (!String.IsNullOrEmpty(filename))
            {
                this.lblDescription.Text = resManager.GetString("ChangeMaxCabSizeForFile");
                this.txtBxFilename.Visible = true;
                this.nupFileSize.Visible = true;
                this.lblFilename.Visible = true;
                this.lblSize.Visible = true;
                this.txtBxFilename.Text = filename;
                this.nupFileSize.Value = fileSize;
            }
            else
            {
                this.lblDescription.Text = resManager.GetString("ChangeMaxCabSizeSetting");
                this.txtBxFilename.Visible = false;
                this.nupFileSize.Visible = false;
                this.lblFilename.Visible = false;
                this.lblSize.Visible = false;
            }
            this.nupNewSetting.Value = System.Math.Max(384, System.Math.Min((int)(fileSize + 0.1 * fileSize), 4096));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Cursor = Cursors.WaitCursor;
            this.btnCancel.Enabled = false;
            this.btnOk.Enabled = false;
            this.btnReset.Enabled = false;
            this.nupNewSetting.Enabled = false;

            try
            {
                _wsus.SetLocalPublishingMaxCabSize((int)this.nupNewSetting.Value);
                Logger.Write("Successfuly change MaxCabFile Size to : " + this.nupNewSetting.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(resManager.GetString("ErrorSettingMaxCabSize") + "\r\n" + ex.Message);
                Logger.Write("Error setting MaxCabFile Size. " + ex.Message);
            }

            this.Cursor = Cursors.Default;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.nupNewSetting.Value = 384;
        }
    }
}
