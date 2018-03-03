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
    public partial class FrmCertificateManagement : Form
    {
        WsusWrapper _wsus;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmCertificateManagement).Assembly);

        public FrmCertificateManagement()
        {
            Logger.EnteringMethod("FrmCertificateManagement");
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
            btnLoad.Enabled = false;
            btnSave.Enabled = (_wsus.GetCertificateStatus != CertificateHelper.CertificateStatus.Absent);
        }

        #region (Private Methods - Méthodes privées)

        private bool IsValidCertificate(string filePath, string certPassword)
        {
            bool IsValid = false;

            try
            {
                IsValid = CertificateHelper.IsValid(filePath, certPassword);
                Logger.Write("IsValideCertificate = " + IsValid.ToString());
                if (!IsValid && _wsus.CurrentServer.IgnoreCertificateErrors)
                {
                    Logger.Write("Ignore Certificate validation errors");
                    IsValid = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Problem with the certificate : " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            return IsValid;
        }

        private bool IsCurrentUserHasAdminRights()
        {
            Logger.EnteringMethod();
            bool isAdmin = false;

            try
            {
                System.Security.Principal.WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(id);
                isAdmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }

            return isAdmin;
        }

        private bool IsSureToWantToOverwriteCurrentCertificate()
        {
            Logger.EnteringMethod();

            WsusWrapper _wsus = WsusWrapper.GetInstance();

            if (_wsus.GetCertificateStatus == CertificateHelper.CertificateStatus.Valid || _wsus.GetCertificateStatus == CertificateHelper.CertificateStatus.NearExpiration)
            {
                Logger.Write("There is already a valid certificate.");
                if (MessageBox.Show(resMan.GetString("AreYouSureToWantToOverwriteCurrentCertificate"), String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    Logger.Write("Cancel Generate or Load certificate.");
                    return false;
                }
            }
            Logger.Write("Generate or Load the certificate.");
            return true;
        }

        #endregion (Private Methods - Méthodes privées)

        #region (response to events - réponse aux événemments)

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            if (this.IsSureToWantToOverwriteCurrentCertificate())
            {
                btnGenerate.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                if (IsCurrentUserHasAdminRights())
                {
                    if (_wsus.UserRole == Microsoft.UpdateServices.Administration.UpdateServerUserRole.Administrator)
                    {
                        try
                        {
                            if (_wsus.GenerateSelfSignedCertificate())
                            {
                                MessageBox.Show(resMan.GetString("CertificateSuccessfullyGenerate"));
                                btnSave.Enabled = (_wsus.GetCertificateStatus != CertificateHelper.CertificateStatus.Absent);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Write("**** " + ex.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        Logger.Write("The current user is not Wsus Admin");
                        MessageBox.Show(resMan.GetString("MustBeWsusAdmin"));
                    }
                }
                else
                {
                    Logger.Write("The current user is not Local Admin");
                    MessageBox.Show(resMan.GetString("MustHaveAdminPrivileges"));
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            if (this.IsSureToWantToOverwriteCurrentCertificate())
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(openFileDialog1.FileName);
                    if (fileInfo.Extension.ToLower() == ".pfx")
                    {
                        Logger.Write("Will load certificate : " + openFileDialog1.FileName);
                        this.Cursor = Cursors.WaitCursor;
                        try
                        {
                            if (IsValidCertificate(openFileDialog1.FileName, txtBxPassword.Text))
                            {
                                _wsus.UseExistingCertificate(openFileDialog1.FileName, txtBxPassword.Text);
                                Logger.Write("Successfuly load provided certificate");
                                MessageBox.Show(resMan.GetString("CertificateSuccessfullyLoaded"));
                            }
                            else
                                MessageBox.Show(resMan.GetString("CertificateFailedtoBeLoaded"));
                        }
                        catch (Exception ex)
                        {
                            Logger.Write("**** " + ex.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        Logger.Write(openFileDialog1.FileName + " is not a .PFX file");
                        MessageBox.Show(openFileDialog1.FileName + resMan.GetString("NotaPFXFile"));
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                if (_wsus.SaveCertificate(saveFileDialog1.FileName))
                    MessageBox.Show(resMan.GetString("CertificateSuccessfullySaved"));
                this.Cursor = Cursors.Default;
            }
        }

        private void txtBxPassword_TextChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = (txtBxPassword.Text.Length != 0 && (_wsus.CurrentServer.UseSSL || _wsus.CurrentServer.IsLocal));
        }

        #endregion
    }
}
