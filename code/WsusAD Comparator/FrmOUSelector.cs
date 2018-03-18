using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WPP.ActiveDirectory;

namespace WsusADComparator
{
    public partial class FrmOUSelector : Form
    {
        private string _domainName = String.Empty;
        private System.Threading.Thread _searcherThread;
        private Localization _localize = Localization.GetInstance();
        private TreeNode _rootNode = new TreeNode();
        private bool _ctrlKeyPressed = false;

        public FrmOUSelector(string domainName)
        {
            InitializeComponent();

            this._domainName = domainName;
            _searcherThread = new System.Threading.Thread(new System.Threading.ThreadStart(FillTreeview));
            _searcherThread.Start();
        }

        #region (Internal Properties - Propriétés internes)

        /// <summary>
        /// Gets, whether or not, the user wants to search computers in all Organizational Units of Active Directory.
        /// </summary>
        internal bool SearchInAllAD
        {
            get { return chkBxSearchAll.Checked; }
        }

        /// <summary>
        /// Gets the list of selected OU.
        /// </summary>
        internal List<OrganizationalUnit> SelectedOUList
        {
            get { return GetSelectedOU(trvOU.Nodes); }
        }
        
        #endregion (Internal Properties - Propriétés internes)

        #region (private methods - méthodes privés)

        private void FillTreeview()
        {
            try
            {
                OrganizationalUnit rootOU = GetOUList();
                _rootNode.Name = rootOU.Name;
                _rootNode.Text = rootOU.Name;
                this.PopulateTreeview(rootOU, _rootNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_localize.GetLocalizedString("UnableToBrowseAD") + ex.Message);
            }
        }

        private OrganizationalUnit GetOUList()
        {
            ActiveDirectory _ad = new ActiveDirectory();
            OrganizationalUnit rootOU = _ad.GetRootOU();

            return rootOU;
        }

        private void PopulateTreeview(OrganizationalUnit rootOU, TreeNode node)
        {
            foreach (OrganizationalUnit OU in rootOU.Childs)
            {
                TreeNode childNode = node.Nodes.Add(OU.Name);
                childNode.Name = OU.Name;
                childNode.Text += " (" + OU.ComputerCount + ")";
                childNode.Tag = OU;
                PopulateTreeview(OU, childNode);
            }
        }

        private List<OrganizationalUnit> GetSelectedOU(TreeNodeCollection rootNode)
        {
            List<OrganizationalUnit> result = new List<OrganizationalUnit>();

            foreach (TreeNode ou in rootNode)
            {
                if (ou.Checked)
                    result.Add((OrganizationalUnit)ou.Tag);
                result.AddRange(GetSelectedOU(ou.Nodes));
            }
            return result;
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

        #endregion (private methods - méthodes privés)

        #region (Responses to events - Réponses aux évènements)

        private void FrmOUSelector_Shown(object sender, EventArgs e)
        {
            if (_searcherThread.ThreadState == System.Threading.ThreadState.Running || _searcherThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                _searcherThread.Join();

            trvOU.Nodes.Clear();
            trvOU.Nodes.Add(_rootNode);

            btnClose.Enabled = true;
            chkBxSearchAll.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void chkBxSearchAll_CheckedChanged(object sender, EventArgs e)
        {
            trvOU.Enabled = !chkBxSearchAll.Checked;
            if (!chkBxSearchAll.Checked)
                this.trvOU.Focus();
        }

        private void trvOU_KeyPressedChanged(object sender, KeyEventArgs e)
        {
            _ctrlKeyPressed = e.Control;
        }

        private void trvOU_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (_ctrlKeyPressed)
            {
                CollapseAllSubNode(e.Node.Nodes);
            }
        }

        private void trvOU_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (_ctrlKeyPressed)
            {
                ExpandAllSubNode(e.Node.Nodes);
            }
        }

        private void trvOU_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_ctrlKeyPressed)
                CheckUnCheckAllSubNode(e.Node.Nodes, e.Node.Checked);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion (Responses to events - Réponses aux évènements)
    }
}
