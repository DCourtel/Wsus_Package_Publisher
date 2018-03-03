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
    internal partial class FrmDeleteObsoleteFolders : Form
    {
        List<System.IO.DirectoryInfo> _obsoleteFolders;

        public FrmDeleteObsoleteFolders(List<System.IO.DirectoryInfo> obsoleteFolders)
        {
            Logger.EnteringMethod("FrmDeleteObsoleteFolders");
            InitializeComponent();
            _obsoleteFolders = obsoleteFolders;
            this.label1.Text += " (" + obsoleteFolders.Count + ")";
            foreach (System.IO.DirectoryInfo directory in _obsoleteFolders)
                chkLstBxFoldersToDelete.Items.Add(directory, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            prgBarDelete.Minimum = 0;
            prgBarDelete.Maximum = chkLstBxFoldersToDelete.CheckedItems.Count;
            prgBarDelete.Value = 0;

            Logger.Write(chkLstBxFoldersToDelete.CheckedItems.Count + " folders to delete.");
            foreach (Object directory in chkLstBxFoldersToDelete.CheckedItems)
            {
                Logger.Write("Deleting " + (directory as System.IO.DirectoryInfo).Name);
                try
                {
                    (directory as System.IO.DirectoryInfo).Delete(true);
                }
                catch (Exception ex) { Logger.Write("**** " + ex.Message); }
                prgBarDelete.Value++;
                prgBarDelete.Refresh();
            }
                
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
