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
    public partial class GroupDisplayer : UserControl
    {

        internal static Font normalFont = new Font("Arial", 8, FontStyle.Regular);
        internal static Font elementAndAttributeFont = new Font("Arial", 9, FontStyle.Regular);
        internal static Font boldFont = new Font("Arial", 9, FontStyle.Bold);
        internal static Color green = System.Drawing.Color.ForestGreen;
        internal static Color black = System.Drawing.Color.Black;
        internal static Color red = System.Drawing.Color.Red;
        internal static Color blue = System.Drawing.Color.DarkBlue;

        private RulesGroup _thisGroup;
        private List<RulesGroup> _selectedGroups = new List<RulesGroup>();
        private List<GenericRule> _selectedRules = new List<GenericRule>();
        private List<GroupDisplayer> _innerGroupDisplayers = new List<GroupDisplayer>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(GroupDisplayer).Assembly);

        public GroupDisplayer()
        {
            InitializeComponent();
        }

        #region (Properties - Propriétés)

        internal Boolean IsSelected
        {
            get { return _thisGroup.IsSelected; }
            set
            {
                _thisGroup.IsSelected = value;
                AdjustBackColor();
                UpdateSelectedRulesAndGroupsList();
                if (SelectionChange != null)
                    SelectionChange(this);
            }
        }

        internal RulesGroup InnerGroup
        {
            get { return _thisGroup; }
            set { _thisGroup = value; }
        }

        internal List<RulesGroup> SelectedGroups
        {
            get { return _selectedGroups; }
        }

        internal List<GenericRule> SelectedRules
        {
            get { return _selectedRules; }
        }

        #endregion

        #region (Methods - Méthodes)

        internal void Initialize(RulesGroup initialGroup)
        {
            InnerGroup = initialGroup;
            Display();
            AdjustBackColor();
            UpdateSelectedRulesAndGroupsList();
            if (SelectionChange != null)
                SelectionChange(this);
        }

        private void Display()
        {
            tlpRulesAndGroups.Controls.Clear();
            tlpRulesAndGroups.AutoScroll = false;
            tlpRulesAndGroups.VerticalScroll.Enabled = true;
            tlpRulesAndGroups.VerticalScroll.Visible = true;
            tlpRulesAndGroups.HorizontalScroll.Enabled = false;
            tlpRulesAndGroups.HorizontalScroll.Visible = false;
            //tlpRulesAndGroups.SuspendLayout();
            _innerGroupDisplayers.Clear();
            rtbxStart.Text = "";
            rtbxEnd.Text = "";

            if (InnerGroup.GroupType == RulesGroup.GroupLogicalOperator.And)
            {
                print(rtbxStart, normalFont, green, resMan.GetString("GroupStart"));
                print(rtbxStart, boldFont, black, resMan.GetString("RuleAnd"));
                print(rtbxStart, normalFont, green, ">");
            }
            else
            {
                print(rtbxStart, normalFont, green, resMan.GetString("GroupStart"));
                print(rtbxStart, boldFont, black, resMan.GetString("RuleOR"));
                print(rtbxStart, normalFont, green, ">");
            }

            foreach (GenericRule rule in InnerGroup.InnerRules.Values)
            {
                RuleDisplayer tempRuleDisplayer = new RuleDisplayer(rule);
                tempRuleDisplayer.SelectedChange += new RuleDisplayer.SelectedChangeEventHandler(Rule_SelectedChange);
                tempRuleDisplayer.EditionRequested += new RuleDisplayer.EditionRequestedEventHandler(Rule_EditionRequested);
                tlpRulesAndGroups.Controls.Add(tempRuleDisplayer);
            }
            foreach (RulesGroup group in InnerGroup.InnerGroups.Values)
            {
                GroupDisplayer grpDisplayer = new GroupDisplayer();
                grpDisplayer.Dock = DockStyle.Top;
                _innerGroupDisplayers.Add(grpDisplayer);
                grpDisplayer.SelectionChange += new SelectionChangeEventHandler(grpDisplayer_SelectionChange);
                grpDisplayer.EditionRequest += new EditionRequestEventHandler(grpDisplayer_EditionRequest);
                grpDisplayer.RuleEditionRequest += new RuleEditionRequestEventHandler(Rule_EditionRequested);
                tlpRulesAndGroups.Controls.Add(grpDisplayer);
                grpDisplayer.Initialize(group);
            }

            if (InnerGroup.GroupType == RulesGroup.GroupLogicalOperator.And)
            {
                print(rtbxEnd, normalFont, green, resMan.GetString("GroupEnd"));
                print(rtbxEnd, boldFont, black, resMan.GetString("RuleAnd"));
                print(rtbxEnd, normalFont, green, ">");
            }
            else
            {
                print(rtbxEnd, normalFont, green, resMan.GetString("GroupEnd"));
                print(rtbxEnd, boldFont, black, resMan.GetString("RuleOR"));
                print(rtbxEnd, normalFont, green, ">");
            }
            //tlpRulesAndGroups.ResumeLayout();
            //tableLayoutPanel1.PerformLayout();
            tlpRulesAndGroups.AutoScroll = true;
            //tableLayoutPanel1.Refresh();
        }

        void grpDisplayer_EditionRequest(GroupDisplayer sender)
        {
            sender.InnerGroup.Edit();
            sender.Initialize(sender.InnerGroup);
        }

        private void print(RichTextBox rTxtBx, System.Drawing.Font font, Color color, string text)
        {
            rTxtBx.SelectionFont = font;
            rTxtBx.SelectionColor = color;
            rTxtBx.SelectedText += text;
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

        private void UpdateSelectedRulesAndGroupsList()
        {
            _selectedGroups.Clear();
            _selectedRules.Clear();

            foreach (GenericRule rule in InnerGroup.InnerRules.Values)
            {
                if (rule.IsSelected)
                    _selectedRules.Add(rule);
            }
            foreach (GroupDisplayer grpDsp in _innerGroupDisplayers)
            {
                foreach (GenericRule rule in grpDsp.SelectedRules)
                {
                    _selectedRules.Add(rule);
                }
            }

            if (IsSelected)
                _selectedGroups.Add(InnerGroup);
            foreach (GroupDisplayer grpDsp in _innerGroupDisplayers)
            {
                foreach (RulesGroup group in grpDsp.SelectedGroups)
                {
                    _selectedGroups.Add(group);
                }
            }
            if (SelectionChange != null)
                SelectionChange(this);
        }

        #endregion

        #region (Responses to Events - Réponses aux événements)

        private void Rule_SelectedChange(GenericRule ChangedRule)
        {
            UpdateSelectedRulesAndGroupsList();
        }

        private void Rule_EditionRequested(GenericRule RequestingRule)
        {
            if (RuleEditionRequest != null)
                RuleEditionRequest(RequestingRule);
        }

        private void grpDisplayer_SelectionChange(GroupDisplayer sender)
        {
            UpdateSelectedRulesAndGroupsList();
        }

        private void tlpRulesAndGroups_ControlAdded(object sender, ControlEventArgs e)
        {
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
            if (EditionRequest != null)
                EditionRequest(this);
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

        public delegate void EditionRequestEventHandler(GroupDisplayer sender);
        public event EditionRequestEventHandler EditionRequest;

        public delegate void RuleEditionRequestEventHandler(GenericRule ResquestingRule);
        public event RuleEditionRequestEventHandler RuleEditionRequest;


        #endregion
    }
}

