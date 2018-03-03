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
    public partial class FrmAddCveID : Form
    {
        System.Text.RegularExpressions.RegexOptions options = System.Text.RegularExpressions.RegexOptions.IgnoreCase;
        System.Text.RegularExpressions.Regex regexpr;

        public FrmAddCveID()
        {
            Logger.EnteringMethod("FrmAddCveID");
            InitializeComponent();
             regexpr = new System.Text.RegularExpressions.Regex(@"CVE-\d{4}-\d{4}", options); 
        }

        internal List<string> CVEArray 
        { 
            get 
            {
                List<string> result = new List<string>();

                System.Text.RegularExpressions.MatchCollection cve = regexpr.Matches(txtBxCveID.Text);
                foreach (System.Text.RegularExpressions.Match item in cve)
                {
                    result.Add(item.Value.ToUpper());
                }
                return result; 
            }
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
