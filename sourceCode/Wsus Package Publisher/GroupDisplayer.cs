using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupAndRuleViewer
{
    internal partial class GroupDisplayer : UserControl
    {
        private Boolean _isSelected = false;
        private GroupOfRules _topLevelGroup;
        private List<RuleDisplayer> _RuleDisplayers = new List<RuleDisplayer>();

        internal GroupDisplayer()
        {
            InitializeComponent();
        }

        #region (Properties - Propriétés)

        internal Boolean IsSelected
        {
            get { return _isSelected; }
            set 
            {
                _isSelected = value;
                AdjustBackColor();
                if (SelectionChange != null)
                    SelectionChange(this);
            }
        }

        #endregion

        #region (Methods - Méthodes)

        internal void Initialize(GroupOfRules initialGroup)
        {
            _topLevelGroup = initialGroup;
            Display();
            AdjustBackColor();
        }

        private void Display()
        {
            tlpRulesAndGroups.Controls.Clear();
            _RuleDisplayers.Clear();

            rtbxStart.Text = "<Commencer " + _topLevelGroup.Type.ToString() + ">";

            foreach (Rule rule in _topLevelGroup.InnerRules)
            {
               RuleDisplayer tempRuleDisplayer = new RuleDisplayer(rule);
               _RuleDisplayers.Add(tempRuleDisplayer);
               tlpRulesAndGroups.Controls.Add(tempRuleDisplayer);
            }
            foreach (GroupOfRules group in _topLevelGroup.InnerGroups)
            {
                GroupDisplayer grpDisplayer = new GroupDisplayer();
                tlpRulesAndGroups.Controls.Add(grpDisplayer);
                grpDisplayer.Initialize(group);
            }

            rtbxEnd.Text = "<Terminer " + _topLevelGroup.Type.ToString() + ">";
            tlpRulesAndGroups.Refresh();
        }

        private void AdjustBackColor()
        {
            if (IsSelected)
            {
                rtbxStart.BackColor = Color.DarkSeaGreen;
                rtbxEnd.BackColor = Color.DarkSeaGreen;
            }
            else
                if (IsCursorOnControl())
                {
                    rtbxStart.BackColor = Color.LightGreen;
                    rtbxEnd.BackColor = Color.LightGreen;
                }
                else
                {
                    rtbxStart.BackColor = Color.Silver;
                    rtbxEnd.BackColor = Color.Silver;
                }
        }

        /// <summary>
        /// Determine if the cursor is on the control.
        /// </summary>
        /// <returns>True if the cursor is on the control.</returns>
        private Boolean IsCursorOnControl()
        {
            Rectangle controlRect = new Rectangle(this.PointToScreen(new Point(0, 0)), new Size(this.ClientRectangle.Width, this.ClientRectangle.Height));
            Point CurrentPos = Cursor.Position;

            return controlRect.Contains(CurrentPos);
        }

        #endregion

        #region (Responses to Events - Réponses aux événements)

        private void tlpRulesAndGroups_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            e.Control.Dock = DockStyle.Top;
            foreach (RowStyle style in tlpRulesAndGroups.RowStyles)
            {
                style.SizeType = SizeType.AutoSize;
            }
        }

        private void rtbxStart_Click(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }

        private void rtbxStart_DoubleClick(object sender, EventArgs e)
        {

        }

        private void rtbxStart_MouseEnter(object sender, EventArgs e)
        {
            if (!IsSelected)
            {
                rtbxStart.BackColor = Color.LightGreen;
                rtbxEnd.BackColor = Color.LightGreen;
            }
        }

        private void rtbxStart_MouseLeave(object sender, EventArgs e)
        {
            if (!IsSelected)
            {
                rtbxStart.BackColor = Color.Silver;
                rtbxEnd.BackColor = Color.Silver;
            }
        }

        #endregion

        #region (Inner Events - événements internes)

        public delegate void SelectionChangeEventHandler(GroupDisplayer sender);
        public event SelectionChangeEventHandler SelectionChange;


        #endregion

    }
}

