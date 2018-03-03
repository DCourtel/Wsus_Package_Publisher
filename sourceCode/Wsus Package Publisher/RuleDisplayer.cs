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
    internal partial class RuleDisplayer : RichTextBox
    {
        private Rule _DisplayedRule;
        private bool _isSelected = false;

        internal RuleDisplayer(Rule ruleToDisplay)
        {
            InitializeComponent();

            DisplayedRule = ruleToDisplay;
        }

        #region (Properties - Propriétés)

        internal Rule DisplayedRule
        {
            get { return _DisplayedRule; }
            set
            {
                _DisplayedRule = value;
                Refresh();
            }
        }

        internal bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                AdjustBackColor();
                if (SelectedChange != null)
                    SelectedChange(DisplayedRule);
            }
        }

        #endregion

        #region (Methods - Methodes)

        internal new void Refresh()
        {
            this.Text = _DisplayedRule.GetXmlRtfFormatted();
            AdjustHeigth();
            AdjustBackColor();
            base.Refresh();
        }

        internal void AdjustHeigth()
        {
            if (this.Lines.Length > 0)
                this.Height = (this.Lines.Length + 1) * this.FontHeight;
        }

        private void AdjustBackColor()
        {            
            if (IsSelected)
                this.BackColor = Color.SteelBlue;
            else
                if (IsCursorOnControl())                
                    this.BackColor = Color.LightSkyBlue;
                else
                    this.BackColor = Color.Gainsboro;
        }

        /// <summary>
        /// Determine if the cursor is on the control.
        /// </summary>
        /// <returns>True if the cursor is on the control.</returns>
        private Boolean IsCursorOnControl()
        {
            Rectangle controlRect = new Rectangle(this.PointToScreen(new Point(0,0)), new Size( this.ClientRectangle.Width, this.ClientRectangle.Height));
            Point CurrentPos = Cursor.Position;

            return controlRect.Contains(CurrentPos);
        }

        #endregion

        #region (Responses to Events - Réponses aux événements)

        private void RuleDisplayer_Click(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }

        private void RuleDisplayer_DoubleClick(object sender, EventArgs e)
        {
            if (EditionRequested != null)
                EditionRequested(DisplayedRule);
        }

        private void RuleDisplayer_MouseEnter(object sender, EventArgs e)
        {
            if (!IsSelected)
                this.BackColor = Color.LightSkyBlue;
        }

        private void RuleDisplayer_MouseLeave(object sender, EventArgs e)
        {
            if (!IsSelected)
                this.BackColor = Color.Gainsboro;
        }

        #endregion

        #region (Inner Events - événements internes)

        public delegate void SelectedChangeEventHandler(Rule ChangedRule);
        public SelectedChangeEventHandler SelectedChange;

        public delegate void EditionRequestedEventHandler(Rule RequestingRule);
        public EditionRequestedEventHandler EditionRequested;

        #endregion

    }
}
