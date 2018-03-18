using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class FrmEnvironmentVariables : Form
    {
        public FrmEnvironmentVariables()
        {
            InitializeComponent();
        }

        private void cmbBxUsualVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cmbBxUsualVariables.SelectedItem != null && this.cmbBxUsualVariables.SelectedIndex != -1)
            {
                this.txtBxVariable.Text = this.cmbBxUsualVariables.SelectedItem.ToString();
            }
        }

        private void txtBxVariable_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtBxVariable.Text))
                this.txtBxTranslation.Text = this.GetEnvironmentVariable(this.txtBxVariable.Text);
        }

        private string GetEnvironmentVariable(string variable)
        {
            string expandedPath = System.Environment.ExpandEnvironmentVariables(variable);
            this.lblReferenceToUserProfile.Visible = GenericAction.HasReferenceToUserProfile(expandedPath);

            return expandedPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Close();
        }
    }
}
