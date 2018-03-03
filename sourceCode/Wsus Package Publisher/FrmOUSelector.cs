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
    internal partial class FrmOUSelector : Form
    {
        private bool isControlPressed = false;
        System.Threading.Thread ouSearcherThread;
        private System.Windows.Forms.TreeNode rootNode = new TreeNode();
        private static object ouSearcherLocker = new object();

        internal FrmOUSelector()
        {
            InitializeComponent();

            ouSearcherThread = new System.Threading.Thread(new System.Threading.ThreadStart(SearchAllOU));
            ouSearcherThread.Start();
        }

        #region (Internal Properties - Propriétés internes)

        internal bool SearchInAllAD
        {
            get { return chkBxSearchInAllAD.Checked; }
        }

        internal List<string> SelectedOUList
        {
            get { return GetOUList(trvOU.Nodes); }
        }

        #endregion (Internal Properties - Propriétés internes)

        #region (private methods - Méthodes privées)

        private List<string> GetOUList(TreeNodeCollection rootNode)
        {
            List<string> result = new List<string>();

            foreach (TreeNode ou in rootNode)
            {
                if (ou.Checked)
                    result.Add(ou.Tag.ToString());
                result.AddRange(GetOUList(ou.Nodes));
            }
            return result;
        }

        private void SearchAllOU()
        {
            lock (ouSearcherLocker)
            {
                rootNode.Text = ADHelper.GetDomainName();
                rootNode.Tag = "LDAP://" + ADHelper.GetDomainName();
                rootNode = ADHelper.GetOUList(rootNode);
            }
        }

        private void CollapseAllSubNode(TreeNodeCollection rootNode)
        {
            foreach (TreeNode node in rootNode)
            {
                CollapseAllSubNode(node.Nodes);
                node.Collapse();
            }
        }

        private void ExpandAllSubNode(TreeNodeCollection rootNode)
        {
            foreach (TreeNode node in rootNode)
            {
                ExpandAllSubNode(node.Nodes);
                node.Expand();
            }
        }

        private void CheckUnCheckAllSubNode(TreeNodeCollection rootNode, bool check)
        {
            foreach (TreeNode node in rootNode)
            {
                CheckUnCheckAllSubNode(node.Nodes, check);
                node.Checked = check;
            }
        }

        #endregion (private methods - Méthodes privées)

        #region (Responses to events - Réponses aux évènements)

        private void chkBxSearchInAllAD_CheckedChanged(object sender, EventArgs e)
        {
            trvOU.Enabled = !chkBxSearchInAllAD.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FrmOUSelector_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnClose.Enabled = false;
            this.Refresh();
            if (ouSearcherThread.ThreadState == System.Threading.ThreadState.Running)
                ouSearcherThread.Join();

            trvOU.Nodes.Clear();
            trvOU.Nodes.Add(rootNode);

            btnClose.Enabled = true;
            chkBxSearchInAllAD.Enabled = true;
            this.Cursor = Cursors.Default;
        }
        
        private void trvOU_KeyDown(object sender, KeyEventArgs e)
        {
           isControlPressed = e.Control;
        }

        private void trvOU_KeyUp(object sender, KeyEventArgs e)
        {
            isControlPressed = e.Control;
        }

        private void trvOU_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if(isControlPressed)
            {
                CollapseAllSubNode(e.Node.Nodes);
            }
        }

        private void trvOU_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (isControlPressed)
            {
                ExpandAllSubNode(e.Node.Nodes);
            }
        }

        private void trvOU_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isControlPressed)
                CheckUnCheckAllSubNode(e.Node.Nodes, e.Node.Checked);
        }

        #endregion (Responses to events - Réponses aux évènements)


    }
}
