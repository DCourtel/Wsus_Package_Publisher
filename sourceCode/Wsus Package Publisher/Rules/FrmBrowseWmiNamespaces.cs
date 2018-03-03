using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Wsus_Package_Publisher
{
    internal partial class FrmBrowseWmiNamespaces : Form
    {
        private List<string> namespaces = new List<string>();

        internal FrmBrowseWmiNamespaces()
        {
            InitializeComponent();
        }

        internal string SelectedNamespace
        {
            get
            {
                if (cmbBxWmiNamespaces.SelectedIndex != -1)
                    return cmbBxWmiNamespaces.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(cmbBxWmiNamespaces.Text))
                    return cmbBxWmiNamespaces.Text.Trim();
                return @"root\CIMV2";
            }
        }

        internal void ListWmiNamespace()
        {
            GetWmiNameSpaces("root");
        }

        private void GetWmiNameSpaces(string root)
        {
            try
            {
                ManagementClass nsClass = new ManagementClass(new ManagementScope(root), new ManagementPath("__namespace"), null);

                foreach (ManagementObject ns in nsClass.GetInstances())
                {
                    string namespaceName = root + "\\" + ns["Name"].ToString();
                    namespaces.Add(namespaceName);
                    //call the funcion recursively    
                    GetWmiNameSpaces(namespaceName);
                }
            }
            catch (Exception)
            {
            }
        }

        private void FillWmiNamespace()
        {
            lock (namespaces)
            {
                int index = -1;
                cmbBxWmiNamespaces.Items.Clear();

                for (int i = 0; i < namespaces.Count; i++)
                {
                    cmbBxWmiNamespaces.Items.Add(namespaces[i]);
                    if (string.Compare(namespaces[i], @"root\cimv2", true) == 0)
                        index = i;
                }
                if (index != -1)
                    cmbBxWmiNamespaces.SelectedIndex = index;

                cmbBxWmiNamespaces.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FrmBrowseWmiNamespaces_Shown(object sender, EventArgs e)
        {
            FillWmiNamespace();
        }
    }
}