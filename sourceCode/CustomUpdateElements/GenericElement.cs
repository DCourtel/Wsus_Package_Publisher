using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CustomUpdateElements
{
    public partial class GenericElement : UserControl
    {
        public enum ConfigState
        {
            Misconfigured,
            NotConfigured,
            Configured
        }

        private bool _isSelected = false;
        private ConfigState _configurationState = ConfigState.NotConfigured;
        private Color unselectedColor = Color.Cornsilk;
        private Color selectedColor = Color.PowderBlue;
        private Pen configuratedPen = new Pen(Color.LimeGreen, 3);
        private Pen misConfiguratedPen = new Pen(Color.Red, 3);
        private string _description = "There is no description for this element.";

        public GenericElement()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.ID = Guid.NewGuid();
            this.IsTemplate = true;
            this.Image = Properties.Resources.DefaultImage;
            this.lblDescription.Text = Description;
            this.IsExpand = false;
        }

        #region (Public Properties - Propriétés public)

        /// <summary>
        /// Get or Set the image that will be shown in the PictureBox at upper left.
        /// </summary>
        public Image Image
        {
            get { return pctBxIcone.Image; }
            set { pctBxIcone.Image = value; }
        }

        /// <summary>
        /// Get or Set the text that will be shown in the textbox at upper right.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; lblDescription.Text = value; }
        }
        
        /// <summary>
        /// Get or Set if this is a template
        /// </summary>
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Get or Set if this Element is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (IsSelected)
                {
                    lblDescription.BackColor = selectedColor;
                    pctBxIcone.BackColor = selectedColor;
                    this.BackColor = selectedColor;
                    if (!IsTemplate && !IsExpand)
                        lblDescription.Text = Description + "\r\n\tDouble-Click on this Element to edit it.";
                }
                else
                {
                    lblDescription.BackColor = unselectedColor;
                    pctBxIcone.BackColor = unselectedColor;
                    this.BackColor = unselectedColor;
                    if (!IsTemplate)
                        lblDescription.Text = Description;
                }
            }
        }

        /// <summary>
        /// Get or Set if this Element is Shown in big size.
        /// </summary>
        public bool IsExpand { get; set; }

        /// <summary>
        /// Get or Set if this Element is configured or not.
        /// </summary>
        public ConfigState ConfigurationState
        {
            get { return _configurationState; }
            set { _configurationState = value; this.Refresh(); }
        }

        /// <summary>
        /// Get the Guid of this instance. This property is automaticly set by the constructor of the base Class.
        /// </summary>
        public Guid ID { get; set; }

        public virtual string ActionDescription
        {
            get { return GetActionDescription(); }
        }
        
        #endregion (Public Properties - Propriétés public)

        #region (Public Methods - Méthodes public)

        public virtual void ShowElement(List<VariableElement> variables)
        {
        }

        public virtual string GetXMLAction()
        {
            return "<Action>\r\n<ElementType>" + this.GetType() + "</ElementType>\r\n";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
            return "No Actions.";
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Responses to Events - Réponses aux événements)

        private void element_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsTemplate)
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    IsSelected = !IsSelected;
                    if (ElementSelect != null)
                        ElementSelect(this, Control.ModifierKeys == Keys.Control);
                }
                else
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        if (ElementRightClick != null)
                            ElementRightClick(this);
                    }
        }

        internal void element_DoubleClick(object sender, EventArgs e)
        {
            if (ElementDoubleClick != null)
                ElementDoubleClick(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!IsTemplate)
            {
                Graphics graph = e.Graphics;
                Pen rectPen = ConfigurationState == ConfigState.Configured ? configuratedPen : misConfiguratedPen;
                Rectangle border = new Rectangle(0, 0, this.Width - 3, this.Height - 3);
                graph.DrawRectangle(rectPen, border);
                graph.Flush();
            }
        }

        #endregion (Responses to Events - Réponses aux événements)

        #region (Public Event - Evenements Public)

        public delegate void ElementDoubleClickEventHandler(GenericElement sender);
        public event ElementDoubleClickEventHandler ElementDoubleClick;

        public delegate void ElementSelectEventHandler(GenericElement sender, bool controlKeyPressed);
        public event ElementSelectEventHandler ElementSelect;

        public delegate void ElementRightClickEventHandler(GenericElement sender);
        public event ElementRightClickEventHandler ElementRightClick;


        #endregion (Public Event - Evenements Public)
    }
}
