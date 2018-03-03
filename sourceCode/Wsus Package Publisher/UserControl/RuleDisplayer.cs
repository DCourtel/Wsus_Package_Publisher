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
    internal partial class RuleDisplayer : RichTextBox
    {
        private GenericRule _DisplayedRule;

        internal RuleDisplayer(GenericRule ruleToDisplay)
        {
            InitializeComponent();
            DisplayedRule = ruleToDisplay;
        }

        #region (Properties - Propriétés)

        internal GenericRule DisplayedRule
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
            get { return _DisplayedRule.IsSelected; }
            set
            {
                _DisplayedRule.IsSelected = value;
                AdjustBackColor();
                if (SelectedChange != null)
                    SelectedChange(DisplayedRule);
            }
        }

        #endregion

        #region (Methods - Methodes)

        internal new void Refresh()
        {
            this.Clear();
            this.Rtf = _DisplayedRule.GetRtfFormattedRule();
            AdjustHeigth();
            AdjustBackColor();
            base.Refresh();
        }

        internal void AdjustHeigth()
        {
            if (this.Lines.Length > 0)
            {
                int totalHeight = 2;
                float rtfWidth = this.Width;
                Graphics g = CreateGraphics();
                
                foreach (string line in this.Lines)
                {
                    float widthLine = g.MeasureString(line, this.Font).Width;
                    
                    int numberOfLine = (int)(Math.Ceiling(widthLine / rtfWidth));
                    totalHeight += numberOfLine;
                }
                this.Height = totalHeight * this.FontHeight;
            }
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
            Refresh();
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

        private void RuleDisplayer_SizeChanged(object sender, EventArgs e)
        {
            AdjustHeigth();
        }

        #endregion

        #region (Inner Events - événements internes)

        public delegate void SelectedChangeEventHandler(GenericRule ChangedRule);
        public event SelectedChangeEventHandler SelectedChange;

        public delegate void EditionRequestedEventHandler(GenericRule RequestingRule);
        public event EditionRequestedEventHandler EditionRequested;

        #endregion        
    }
}
