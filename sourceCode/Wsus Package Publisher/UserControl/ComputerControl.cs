using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class ComputerControl : UserControl
    {
        private Dictionary<Guid, Company> _companies;
        private Guid _computerGroupId;

        public ComputerControl()
        {
            Logger.EnteringMethod("ComputerControl");
            InitializeComponent();
            computerListViewer1.SelectionChanged += new ComputerListViewer.SelectionChangedEventHandler(computerListViewer1_SelectionChanged);
            computerDetailViewer1.ComputerCtrl = this;
        }

        internal new void Dispose()
        {
            computerDetailViewer1.Dispose();
            computerListViewer1.Dispose();
        }

#region (Properties - Propriétés)

        internal Dictionary<Guid, Company> Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }

        internal Guid ComputerGroupID
        {
            get { return _computerGroupId; }
            private set { _computerGroupId = value; }
        }

#endregion

        internal void Display(Guid computerGroupId)
        {
            Logger.EnteringMethod(computerGroupId.ToString());
            ComputerGroupID = computerGroupId;
            computerListViewer1.Display(computerGroupId);
        }

        private void computerListViewer1_SelectionChanged(DataGridViewSelectedRowCollection rows)
        {
            Logger.EnteringMethod(rows.Count.ToString());
            computerDetailViewer1.Display(rows);
        }
    }
}
