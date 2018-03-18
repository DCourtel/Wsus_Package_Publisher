using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class GenericAction : UserControl
    {
        public enum ConfigurationStates
        {
            Misconfigured,
            NotConfigured,
            Configured
        }

        private const float _round = 8f;                                                            // Angle for the rounded rectangle of this Control.
        private const int _topBorder = 5;                                                           // Number of pixels for the top border of this Control.
        private const int _leftBorder = 5;                                                          // Number of pixels for the left border of this Control.
        private const int _rightBorder = 5;                                                         // Number of pixels for the right border of this Control.
        private const int _minimalWidth = 90;                                                       // Minimal Width of this Control.
        private const int _minimalHeight = 55;                                                      // Minimal Height of this Control.
        private const int _actionIconWidth = 45;                                                    // Width of the top left icon.
        private const int _actionIconHeight = 45;                                                   // Height of the top left icon.
        private const int _helpIconWidth = 22;                                                      // Width of the Help icon.
        private const int _helpIconHeight = 22;                                                     // Height of the Help icon.
        private const int _configurationStateIconWidth = 22;                                        // Width of the Configuration State icon.
        private const int _configurationStateIconHeigth = 22;                                       // Heigth of the Configuration State icon.
        private const int _userProfileIconWidth = 32;                                               // Width of the User Profile icon.
        private const int _userProfileIconHeigth = 32;                                              // Heigth of the User Profile icon.
        private const int _arrowUpDownWidth = 15;                                                   // Width of the arrow icon.
        private const int _arrowUpDownHeight = 15;                                                  // Heigth of the arrow icon.

        private Color _backgroundUnselectedColor = Color.Cornsilk;                                  // Color used to draw the background of this Control when not selected.
        private Color _backgroundSelectedColor = Color.PowderBlue;                                  // Color used to draw the background of this Control when selected.
        private Color _surroundColor = Color.SteelBlue;                                             // Color used to draw the surrounding line of the Control.
        private Color _foreColor = Color.Black;                                                     // Color used to draw the text of the Control.
        private Brush _backgroundUnselectedColorBrush;                                              // Brush used to draw the background of the Control when not selected.
        private Brush _backgroundSelectedColorBrush;                                                // Brush used to draw the background of the Control when selected.
        private Pen _surroundColorPen;                                                              // Pen used to draw the surrounding line of the Control.
        private SolidBrush _foreColorBrush;                                                         // Brush used to draw the text.
        private Image _actionIcon;                                                                  // Used by the property : ActionIcon.
        private bool _isOnHelpIcon = false;                                                         // Used by the property : IsOnHelpIcon.
        private bool _isOnConfigurationStateIcon = false;                                           // Used by the property : IsOnConfigurationStateIcon.
        private bool _isOnUserProfileIcon = false;                                                  // Used by the property : IsOnUserProfileIcon.
        private bool _isOnUpDownArrowIcon = false;                                                  // Used by the property : IsOnUpDownArrowIcon.
        private string _defaultDescription = "There is no description for this CustomAction.";
        private string _helpMessage = string.Empty;                                                 // Used by the property : HelpMessage.
        private Color _defaultBackgroundUnselectedColor = Color.Cornsilk;                           // Default color used for drawing the background of the Control when not selected.
        private Color _defaultBackgroundSelectedColor = Color.PowderBlue;                           // Default color used for drawing the background of the Control when selected.
        private Color _defaultforeColor = Color.Black;                                              // Default color used for drawing the text of the Control.
        private Color _defaultSurroundingColor = Color.SteelBlue;                                   // Default color used for drawing the surrounding line of the Control.
        private bool _isSelected = false;                                                           // Used by the property : IsSelected.
        private bool _isExpand = false;                                                             // Used by the property : IsExpand.
        private bool _expandCollapseInProgress = false;                                             // Used by the property : ExpandCollapseInProgress
        private ConfigurationStates _configurationState = ConfigurationStates.NotConfigured;        // Used by the property : ConfigurationState.
        private Rectangle actionIconRect;
        private Image _helpIcon = Properties.Resources.QuestionMark;
        private Image _configStateIcon;
        private Image _userProfileIcon = Properties.Resources.UserProfile;
        private Image _arrow;
        private Graphics onPaintGraph;
        private Rectangle onPaintRect;
        private DateTime _lastPaintTime = DateTime.Now;

        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("CustomActions.Localization.CustomActionsStrings", typeof(GenericAction).Assembly);

        public GenericAction()
            : base()
        {
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.Padding = new Padding(5);
            this.Margin = new Padding(3);
            this.BackColor = Color.Transparent;
            this.SurroundColor = this._surroundColor;
            this.ForeColor = this._foreColor;
            this.BackgroundUnselectedColor = this._backgroundUnselectedColor;
            this.BackgroundSelectedColor = this._backgroundSelectedColor;
            this.Width = 470;
            this.Height = 50;
            this._actionIcon = Properties.Resources.DefaultActionImage;
            this.Text = this._defaultDescription;
            this.DragDropHasStarted = false;
            this.IsMouseDown = false;
            this.ConfigurationState = ConfigurationStates.NotConfigured;
            this.IsTemplate = true;
            this.IsExpanded = false;
            this.ExpandedHeigth = 250;
            this.CollapsedHeigth = 50;
            this.HelpMessage = this.GetLocalizedString("NoHelpAvailable");
            this.RefersToHKeyCurrentUser = false;
            this.RefersToUserProfile = false;
            this.RequestUserInteraction = false;
            this.ExpandCollapseInProgress = false;
            this.toolTip1 = new ToolTip();

            actionIconRect = new Rectangle(_leftBorder, _topBorder, _actionIconWidth, _actionIconHeight);
        }

        #region (Public Properties)

        [Browsable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = Color.Transparent;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }

        /// <summary>
        /// Color used to draw the background of this Control when not selected. 
        /// </summary>
        [Category("Appearance"), Description("Color used to draw the background of this Control when not selected."), DefaultValue(typeof(Color), "Cornsilk")]
        public Color BackgroundUnselectedColor
        {
            get { return _backgroundUnselectedColor; }
            set
            {
                this._backgroundUnselectedColor = value;
                this._backgroundUnselectedColorBrush = new SolidBrush(this._backgroundUnselectedColor);
                this.Invalidate();
            }
        }

        /// <summary>
        /// Color used to draw the background of this Control when  selected. 
        /// </summary>
        [Category("Appearance"), Description("Color used to draw the background of this Control when selected."), DefaultValue(typeof(Color), "PowerBlue")]
        public Color BackgroundSelectedColor
        {
            get { return _backgroundSelectedColor; }
            set
            {
                this._backgroundSelectedColor = value;
                this._backgroundSelectedColorBrush = new SolidBrush(this._backgroundSelectedColor);
                this.Invalidate();
            }
        }

        /// <summary>
        /// Color used to draw the text of the Control.
        /// </summary>
        [Category("Appearance"), Description("Color used to draw the text of the Control."), DefaultValue(typeof(Color), "Black")]
        public override Color ForeColor
        {
            get { return this._foreColor; }
            set
            {
                this._foreColor = value;
                this._foreColorBrush = new SolidBrush(this._foreColor);
                this.Invalidate();
            }
        }

        /// <summary>
        /// Color used to draw the surrounding line of the Control.
        /// </summary>
        [Category("Appearance"), Description("Color used to draw the surrounding line of the Control."), DefaultValue(typeof(Color), "SteelBlue")]
        public Color SurroundColor
        {
            get { return _surroundColorPen.Color; }
            set
            {
                this._surroundColor = value;
                this._surroundColorPen = new Pen(_surroundColor);
                this.Invalidate();
            }
        }

        /// <summary>
        /// The image shown at the top left of the control.
        /// </summary>
        [Category("Appearance"), Description("The image shown at the top left of the control.")]
        public Image ActionIcon
        {
            get { return this._actionIcon; }
            set
            {
                if (value != null)
                    this._actionIcon = value;
                else
                    this._actionIcon = Properties.Resources.DefaultActionImage;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The text that will be shown to describe this CustomAction.
        /// </summary>
        [Category("Appearance"), Description("The text that will be shown to describe this CustomAction.")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    base.Text = value.Substring(0, System.Math.Min(255, value.Length));
                else
                    base.Text = this._defaultDescription;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or Sets if this control is selected.
        /// </summary>
        [Browsable(false)]
        public bool IsSelected
        {
            get { return this._isSelected; }
            set
            {
                this._isSelected = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or Sets if this is a template
        /// </summary>
        [Browsable(false)]
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Gets or Sets of the control when the user expand it.
        /// </summary>
        [Category("Disposition"), Description("Height of the control when the user click on the bottom right arrow to expand this control."), DefaultValue(250)]
        public int ExpandedHeigth { get; set; }

        /// <summary>
        /// Gets or Sets of the control when the user collapse it.
        /// </summary>
        [Category("Disposition"), Description("Height of the control when the user click on the bottom right arrow to collapse this control."), DefaultValue(50)]
        public int CollapsedHeigth { get; set; }

        /// <summary>
        /// Text to display when the user click on the 'Help' icon. Can't exceed 512 characters.
        /// </summary>
        [Category("Comportement"), Description("Message to display when the user click on the 'Help' icon."), DefaultValue("No help available for this Action.")]
        public string HelpMessage
        {
            get { return this._helpMessage; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    this._helpMessage = value.Substring(0, System.Math.Min(512, value.Length));
            }
        }

        /// <summary>
        /// Gets or Sets whether or not this Custom Action refers to a Registry Key in the HKey_CURRENT_USER hive.
        /// </summary>
        [Browsable(false), DefaultValue(false)]
        public bool RefersToHKeyCurrentUser { get; set; }

        /// <summary>
        /// Gets or Sets whether or not this Custom Action refers to the user profile.
        /// </summary>
        [Browsable(false), DefaultValue(false)]
        public bool RefersToUserProfile { get; set; }

        /// <summary>
        /// Gets or Sets whether or not this Custom Action request user interaction.
        /// </summary>
        [Browsable(false), DefaultValue(false)]
        public bool RequestUserInteraction { get; set; }

        /// <summary>
        /// Gets or Sets if this Element is configured or not.
        /// </summary>
        [Browsable(false)]
        public ConfigurationStates ConfigurationState
        {
            get { return _configurationState; }
            set
            {
                this._configurationState = value;
                switch (value)
                {
                    case ConfigurationStates.Misconfigured:
                    case ConfigurationStates.NotConfigured:
                        _configStateIcon = Properties.Resources.NOk;
                        break;
                    case ConfigurationStates.Configured:
                        _configStateIcon = Properties.Resources.Ok;
                        break;
                    default:
                        _configStateIcon = Properties.Resources.NOk;
                        break;
                }

                this.Invalidate();
            }
        }

        #endregion (Public Properties)

        #region (Private Properties)

        /// <summary>
        /// Gets or Sets if this Element is Shown in big size.
        /// </summary>
        private bool IsExpanded
        {
            get { return this._isExpand; }
            set
            {
                this._isExpand = value;

                _arrow = this.IsExpanded ? Properties.Resources.Arrow_Up : Properties.Resources.Arrow_Down;
                this.Invalidate();

            }
        }

        /// <summary>
        /// Gets or Sets if an Expand or Collapse operation is in progress.
        /// </summary>
        private bool ExpandCollapseInProgress
        {
            get { return this._expandCollapseInProgress; }
            set
            {
                this._expandCollapseInProgress = value;
                if (value)
                {
                    if (this.ExpandInProgress != null)
                        ExpandInProgress(this);
                }
                else
                {
                    if (this.CollapseInProgress != null)
                        CollapseInProgress(this);
                }
            }
        }

        /// <summary>
        /// Gets or Sets if the cursor is on the Help Icon.
        /// </summary>
        public bool IsOnHelpIcon
        {
            get { return _isOnHelpIcon; }
            set
            {
                if (this._isOnHelpIcon != value)
                {
                    _isOnHelpIcon = value;
                    if (!this.IsTemplate)
                        this.Cursor = value ? Cursors.Hand : Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Gets or Sets if the cursor is on the Configuration State Icon.
        /// </summary>
        public bool IsOnConfigurationStateIcon
        {
            get { return this._isOnConfigurationStateIcon; }
            set
            {
                if (this._isOnConfigurationStateIcon != value)
                {
                    this._isOnConfigurationStateIcon = value;
                    if (!this.IsTemplate)
                        this.Cursor = value ? Cursors.Hand : Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Gets or Sets if the cursor si on the User Profile Icon
        /// </summary>
        private bool IsOnUserProfileIcon
        {
            get { return this._isOnUserProfileIcon; }
            set
            {
                if (this._isOnUserProfileIcon != value)
                {
                    this._isOnUserProfileIcon = value;
                    if (!this.IsTemplate)
                        this.Cursor = (value && (RefersToUserProfile || RequestUserInteraction || RefersToHKeyCurrentUser)) ? Cursors.Hand : Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Gets or Sets if the cursor is on the Help Icon.
        /// </summary>
        public bool IsOnUpDownArrowIcon
        {
            get { return this._isOnUpDownArrowIcon; }
            set
            {
                if (this._isOnUpDownArrowIcon != value)
                {
                    this._isOnUpDownArrowIcon = value;
                    if (!this.IsTemplate)
                        this.Cursor = value ? Cursors.Hand : Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Set to true when the user press the left button of the mouse, and to false when he release the button.
        /// </summary>
        public bool IsMouseDown { get; set; }

        /// <summary>
        /// Set to true when the control start a drag'n drop operation, set to false when releasing the button of the mouse.
        /// </summary>
        public bool DragDropHasStarted { get; set; }

        #endregion (Private Properties)

        #region Public Methods

        public virtual string GetXMLAction()
        {
            return "<Action>\r\n<ElementType>" + this.GetType() + "</ElementType>\r\n";
        }

        /// <summary>
        /// Display a text that describe how to configure this CustomAction.
        /// </summary>
        public virtual void DisplayHelp()
        {
            //ToDo : Implémenter cette méthode dans les classes enfants.
            MessageBox.Show(this.HelpMessage);
        }

        /// <summary>
        /// Initialize properties of this Action with values contains in the dictionary.
        /// </summary>
        /// <param name="properties">A dictionary where keys are propertie name, and values are propertie value.</param>
        public void InitializeProperties(System.Collections.Generic.Dictionary<string, string> properties)
        {
            System.Reflection.PropertyInfo[] thisProperties = this.GetType().GetProperties();

            foreach (System.Collections.Generic.KeyValuePair<string, string> property in properties)
            {
                string propertyName = property.Key;
                string propertyValue = property.Value;
                bool found = false;

                for (int i = 0; i < thisProperties.Length; i++)
                {
                    if (thisProperties[i].Name.ToLower() == propertyName.ToLower())
                    {
                        found = true;

                        if (thisProperties[i].PropertyType.BaseType == typeof(System.Enum))
                        {
                            if (thisProperties[i].PropertyType == typeof(CustomActions.RegistryHelper.ValueType))
                                thisProperties[i].SetValue(this, GenericAction.ConvertToEnum<CustomActions.RegistryHelper.ValueType>(propertyValue), null);
                            if (thisProperties[i].PropertyType == typeof(CustomActions.RegistryHelper.RegistryHive))
                                thisProperties[i].SetValue(this, GenericAction.ConvertToEnum<CustomActions.RegistryHelper.RegistryHive>(propertyValue), null);
                        }
                        else
                            thisProperties[i].SetValue(this, Convert.ChangeType(propertyValue, thisProperties[i].PropertyType), null);

                        break;
                    }
                }
                if (!found)
                    throw new Exception(String.Format(this.GetLocalizedString("TheXmlElementHasNotBeenMatchWithProperty"), propertyName, this.GetType()));
            }
        }

        /// <summary>
        /// Convert the string value to an Enum value.
        /// </summary>
        /// <typeparam name="T">Destination Enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Return the Enum value corresponding to the string.</returns>
        public static T ConvertToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion Public Methods

        #region (Internal Methods)

        /// <summary>
        /// Search for a reference to the user profile (such as %AppData%, %UserProfile% or ':\Users\'...)
        /// </summary>
        /// <param name="evidence">The string where to search.</param>
        /// <returns>Return true if a reference to the user profile is found.</returns>
        internal static bool HasReferenceToUserProfile(string evidence)
        {
            // %HomePath%
            // %LocalAppData%
            // %UserProfile%
            // %AppData%
            // %UserName%

            bool result = false;
            string[] references = new string[] { "%homepath%", "%localappdata%", "%userprofile%", "%appdata%", "%username%", @":\users\", @":\documents and settings\" };

            try
            {
                string loweredEvidence = evidence.ToLower();

                foreach (string item in references)
                {
                    if (loweredEvidence.Contains(item))
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// Search for illegal characters in a string.
        /// </summary>
        /// <param name="dirtyString">A string where to search for illegal characters.</param>
        /// <returns>Return if the string contains illegal characters.</returns>
        internal static bool ContainsIllegalCharacters(string dirtyString)
        {
            char[] illegalCharacters = System.IO.Path.GetInvalidFileNameChars();

            if (dirtyString.IndexOfAny(illegalCharacters) != -1)
                return true;
            return false;
        }

        /// <summary>
        /// Compare the given string with a set of illegal filename and foldername.
        /// </summary>
        /// <param name="fileOrFolderName">The filename or foldername to compare against illegal names list.</param>
        /// <returns>Return true if the filename or foldername is authorized. Otherwise, return false.</returns>
        internal static bool IsValidFileOrFolderName(string fileOrFolderName)
        {
            string[] illegalNames = new string[] { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

            if (fileOrFolderName.IndexOf('.') != -1)
                fileOrFolderName = fileOrFolderName.Substring(0, fileOrFolderName.IndexOf('.'));

            foreach (string illegalName in illegalNames)
            {
                if (String.Compare(fileOrFolderName, illegalName, true) == 0)
                    return false;
            }

            return true;
        }

        #endregion

        #region (Overrided Methods)

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            onPaintGraph = paintEventArgs.Graphics;
            onPaintRect = this.ClientRectangle;

            this.DrawBackground(onPaintGraph, onPaintRect);
            this.DrawUpDownArrow(onPaintGraph);
            this.DrawActionIcon(onPaintGraph);
            this.DrawHelpIcon(onPaintGraph);
            this.DrawConfigurationStateIcon(onPaintGraph);
            this.DrawUserProfileIcon(onPaintGraph);
            this.DrawDescription(onPaintGraph);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.Width < _minimalWidth)
                this.Width = _minimalWidth;
            if (this.Height < _minimalHeight)
                this.Height = _minimalHeight;
            base.OnSizeChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.IsOnUserProfileIcon = this.IsCursorOverUserProfileIcon(e);
            this.IsOnHelpIcon = this.IsCursorOverHelpIcon(e);
            this.IsOnConfigurationStateIcon = this.IsCursorOverConfigurationStateIcon(e);
            this.IsOnUpDownArrowIcon = this.IsCursorOverUpDownArrow(e);

            if (!this.IsOnHelpIcon && !this.IsOnConfigurationStateIcon && !this.IsOnUserProfileIcon && !this.IsOnUpDownArrowIcon && e.Button == System.Windows.Forms.MouseButtons.Left && !this.DragDropHasStarted)
            {
                this.DragDropHasStarted = true;
                this.StartDragDropOperation();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.DragDropHasStarted = false;
            if (this.IsOnConfigurationStateIcon)
            {
                if (!this.IsTemplate)
                {
                    this.toolTip1.Show(this.GetLocalizedString("ThisActionIs") + " " + GetLocalizedString(this.ConfigurationState.ToString()) + ".", this, e.Location, 5000);
                }
            }
            else if (this.IsOnUserProfileIcon)
            {
                if (!this.IsTemplate && (RefersToUserProfile || RequestUserInteraction || RefersToHKeyCurrentUser))
                {
                    this.toolTip1.Show(this.GetLocalizedString("ThisActionRefersToUserProfile"), this, e.Location, 5000);
                }
            }
            else
                this.IsMouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.toolTip1.Hide(this);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.DragDropHasStarted)
                    this.DragDropHasStarted = false;
                if (this.IsOnHelpIcon)
                {
                    this.DisplayHelp();
                }
                if (!this.IsOnConfigurationStateIcon && !this.IsOnUserProfileIcon && !this.IsOnUpDownArrowIcon && !this.IsOnHelpIcon)
                    this.IsSelected = !this.IsSelected;
                if (this.IsOnUpDownArrowIcon && !this.ExpandCollapseInProgress)
                {
                    this.ExpandOrCollapse();
                }
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            this.ExpandOrCollapse();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected virtual void StartDragDropOperation()
        {
            DoDragDrop(new DragDropInfo(new GenericAction()), DragDropEffects.Move);
        }

        protected virtual string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("ThisActionDoesNothing");
        }

        #endregion (Overrided Methods)

        #region (Private Methods)

        /// <summary>
        /// Draw the background and the line that surround the control.
        /// </summary>
        /// <param name="e">Settings of the Paint Event.</param>
        private void DrawBackground(Graphics graph, Rectangle rect)
        {
            Rectangle surroundingRectangle = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            System.Drawing.Drawing2D.GraphicsPath roundedBorder = this.GetRoundedRect(surroundingRectangle, _round, _round, _round, _round);
            //if (this.IsSelected)
            //    graph.FillPath(this._backgroundSelectedColorBrush, roundedBorder);
            //else
            graph.FillPath(this._backgroundUnselectedColorBrush, roundedBorder);
            graph.DrawPath(this._surroundColorPen, roundedBorder);
        }

        /// <summary>
        /// Returns a GraphicsPath which represent a rounded rectangle.
        /// </summary>
        /// <param name="r">Rectangle that contains logically the rounded rectangle.</param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="r3"></param>
        /// <param name="r4"></param>
        /// <returns></returns>
        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRect(Rectangle r, float r1, float r2, float r3, float r4)
        {
            float X = r.X, Y = r.Y, w = r.Width, h = r.Height;
            System.Drawing.Drawing2D.GraphicsPath rr = new System.Drawing.Drawing2D.GraphicsPath();
            rr.AddBezier(X, Y + r1, X, Y, X + r1, Y, X + r1, Y);
            rr.AddLine(X + r1, Y, X + w - r2, Y);
            rr.AddBezier(X + w - r2, Y, X + w, Y, X + w, Y + r2, X + w, Y + r2);
            rr.AddLine(X + w, Y + r2, X + w, Y + h - r3);
            rr.AddBezier(X + w, Y + h - r3, X + w, Y + h, X + w - r3, Y + h, X + w - r3, Y + h);
            rr.AddLine(X + w - r3, Y + h, X + r4, Y + h);
            rr.AddBezier(X + r4, Y + h, X, Y + h, X, Y + h - r4, X, Y + h - r4);
            rr.AddLine(X, Y + h - r4, X, Y + r1);
            return rr;
        }

        /// <summary>
        /// Draw the top left image.
        /// </summary>
        /// <param name="e">Settings of the Paint Event.</param>
        private void DrawActionIcon(Graphics graph)
        {
            graph.DrawImage(this.ActionIcon, actionIconRect);
        }

        /// <summary>
        /// Draw the Help Icon at top right of the control.
        /// </summary>
        /// <param name="e">Settings of the Paint Event.</param>
        private void DrawHelpIcon(Graphics graph)
        {
            Rectangle rect = new Rectangle(this.Width - _rightBorder - _helpIconWidth, _topBorder, _helpIconWidth, _helpIconHeight);
            graph.DrawImage(_helpIcon, rect);
        }

        /// <summary>
        /// Draw an icon at left of the Help Icon, indicating the state of the configuration of this Action.
        /// </summary>
        /// <param name="e">Settings of the Paint Event.</param>
        private void DrawConfigurationStateIcon(Graphics graph)
        {
            if (!this.IsTemplate)
            {
                Rectangle rect = new Rectangle(this.Width - _rightBorder - _helpIconWidth - _rightBorder - _configurationStateIconWidth, _topBorder, _configurationStateIconWidth, _configurationStateIconHeigth);
                graph.DrawImage(_configStateIcon, rect);
            }
        }

        /// <summary>
        /// Draw an icon at left of the configuration state Icon, indocating if this Action make a reference to the user profile.
        /// </summary>
        /// <param name="graph"></param>
        private void DrawUserProfileIcon(Graphics graph)
        {
            if (!this.IsTemplate && (this.RefersToHKeyCurrentUser || this.RefersToUserProfile || this.RequestUserInteraction))
            {
                Rectangle rect = new Rectangle(this.Width - _rightBorder - _helpIconWidth - _rightBorder - _configurationStateIconWidth - _rightBorder - _userProfileIconWidth, _topBorder, _userProfileIconWidth, _userProfileIconHeigth);
                graph.DrawImage(_userProfileIcon, rect);
            }
        }

        /// <summary>
        /// Draw an arrow at bottom right of the control, allowing the user to clic on it to expand/colapse the control.
        /// </summary>
        /// <param name="e">Settings of the Paint Event.</param>
        private void DrawUpDownArrow(Graphics graph)
        {
            if (!this.IsTemplate && !this.ExpandCollapseInProgress)
            {
                Rectangle rect = new Rectangle(this.Width - _rightBorder - _arrowUpDownHeight, this.Height - _topBorder - _arrowUpDownHeight, _arrowUpDownWidth, _arrowUpDownHeight);
                graph.DrawImage(_arrow, rect);
            }
        }

        /// <summary>
        /// Draw the description for this CustomAction.
        /// </summary>
        /// <param name="e">Settings of the Paint Event.</param>
        private void DrawDescription(Graphics graph)
        {
            if (this.IsTemplate)
            {
                RectangleF rect = new RectangleF(_leftBorder * 2 + _actionIconWidth, _topBorder, this.Width - _rightBorder - _leftBorder * 2 - _actionIconWidth - _helpIconWidth - _rightBorder - _configurationStateIconWidth, System.Math.Max(this.Height, _minimalHeight) - _topBorder * 2);
                graph.DrawString(this.Text, this.Font, _foreColorBrush, rect);
            }
            else
            {
                RectangleF rect = new RectangleF(_leftBorder * 2 + _actionIconWidth, _topBorder, this.Width - _rightBorder - _leftBorder * 2 - _actionIconWidth - _helpIconWidth - _rightBorder - _configurationStateIconWidth, System.Math.Max(this.Height, _minimalHeight) - _topBorder * 2);
                if (this.ConfigurationState != ConfigurationStates.Configured)
                {
                    graph.DrawString(this.GetUnconfiguratedDescription(), this.Font, _foreColorBrush, rect);
                }
                else
                {
                    graph.DrawString(this.GetConfiguratedDescription(), this.Font, _foreColorBrush, rect);
                }
            }
        }

        /// <summary>
        /// Determine whether the cursor is over the Help Icon or not.
        /// </summary>
        /// <param name="e">MouseEventArgs of the OnMouseMove event.</param>
        /// <returns>True if the cursor is over the Help Icon, false otherwise.</returns>
        private bool IsCursorOverHelpIcon(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(this.Width - _rightBorder - _helpIconWidth, _topBorder, _helpIconWidth, _helpIconHeight);
            return rect.Contains(e.Location);
        }

        /// <summary>
        /// Determine whether the cursor is over the Confiuration State Icon or not.
        /// </summary>
        /// <param name="e">MouseEventArgs of the OnMouseMove event.</param>
        /// <returns>True if the cursor is over the Confiuration State Icon, false otherwise.</returns>
        private bool IsCursorOverConfigurationStateIcon(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(this.Width - _rightBorder - _helpIconWidth - _rightBorder - _configurationStateIconWidth, _topBorder, _configurationStateIconWidth, _configurationStateIconHeigth);
            return rect.Contains(e.Location);
        }

        /// <summary>
        /// Determine whether the cursor is over the User Profile Icon or not.
        /// </summary>
        /// <param name="e">MouseEventArgs of the OnMouseMove event.</param>
        /// <returns>True if the cursor is over the User Profile Icon, false otherwise.</returns>
        private bool IsCursorOverUserProfileIcon(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(this.Width - _rightBorder - _helpIconWidth - _rightBorder - _configurationStateIconWidth - _rightBorder - _userProfileIconWidth, _topBorder, _userProfileIconWidth, _userProfileIconHeigth);
            return rect.Contains(e.Location);
        }

        /// <summary>
        /// etermine whether the cursor is over the UpDownArrow Icon or not.
        /// </summary>
        /// <param name="e">MouseEventArgs of the OnMouseMove event.</param>
        /// <returns>True if the cursor is over the UpDownArrow Icon, false otherwise.</returns>
        private bool IsCursorOverUpDownArrow(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(this.Width - _rightBorder - _arrowUpDownHeight, this.Height - _topBorder - _arrowUpDownHeight, _arrowUpDownWidth, _arrowUpDownHeight);
            return rect.Contains(e.Location);
        }

        private void ExpandOrCollapse()
        {
            this.Height = this.IsExpanded ? this.CollapsedHeigth : this.ExpandedHeigth;
            this.ExpandCollapseInProgress = false;
            //int _heigth = this.IsExpanded ? this.CollapsedHeigth : this.ExpandedHeigth;
            //Transitions.Transition trans = new Transitions.Transition(new Transitions.TransitionType_CriticalDamping(800));
            //trans.add(this, "Height", (object)_heigth);
            //trans.TransitionCompletedEvent += new EventHandler<Transitions.Transition.Args>(TransitionCompletedEvent);
            //this.ExpandCollapseInProgress = true;
            //trans.run();
            this.IsExpanded = !this.IsExpanded;
            if (this.IsExpanded)
                this.OnExpand();
        }

        /// <summary>
        /// Change the property <see cref="RefersToHKeyCurrentUser"/> accordingly to the <see cref="Hive"/> property.
        /// </summary>
        public void UpdateHiveNotificationStatus(RegistryHelper.RegistryHive hive)
        {
            switch (hive)
            {
                case RegistryHelper.RegistryHive.HKey_Local_Machine:
                    this.RefersToHKeyCurrentUser = false;
                    break;
                case RegistryHelper.RegistryHive.HKey_Current_User:
                    this.RefersToHKeyCurrentUser = true;
                    break;
                default:
                    this.RefersToHKeyCurrentUser = false;
                    break;
            }
            this.Invalidate();
        }

        /// <summary>
        /// Change the property <see cref="RefersToUserProfile"/> accordingly to the <see cref="evidence"/> param.
        /// </summary>
        /// <param name="evidence">A string that may potentially make reference to the user profile.</param>
        protected void UpdateUserProfileNotificationStatus(string evidence)
        {
            this.RefersToUserProfile = HasReferenceToUserProfile(evidence);
            this.Invalidate();
        }

        /// <summary>
        /// Change the property <see cref="RequestUserInteraction"/> accordingly the parameter.
        /// </summary>
        /// <param name="requested">True if the action require a user interaction.</param>
        protected void UpdateUserInteractionStatus(bool requested)
        {
            this.RequestUserInteraction = requested;
            this.Invalidate();
        }

        /// <summary>
        /// Returns a localized string depending of the current culture.
        /// Never throws exception.
        /// </summary>
        /// <param name="unlocalizedString">String to localized.</param>
        /// <returns>The localized string.</returns>
        public string GetLocalizedString(string unlocalizedString)
        {
            string result = string.Empty;

            try
            {
                result = resMan.GetString(unlocalizedString);
                if (!String.IsNullOrEmpty(result))
                    return result;
            }
            catch (Exception) { }

            return "Missing_Localized_String_For(" + ((unlocalizedString != null) ? unlocalizedString : "null") + ")";
        }

        /// <summary>
        /// Get a localized string describing the configuration state of this action when it is not configured.
        /// </summary>
        /// <returns></returns>
        protected string GetUnconfiguratedDescription()
        {
            return this.GetLocalizedString("ThisActionIs") + " " + GetLocalizedString(this.ConfigurationState.ToString()) + ".";
        }

        /// <summary>
        /// Called when the control expand the display. So that inherited controls will be able to set the focus on the first editable control.
        /// </summary>
        protected virtual void OnExpand() { }

        //private void TransitionCompletedEvent(object sender, Transitions.Transition.Args e)
        //{
        //    this.ExpandCollapseInProgress = false;
        //    this.Invalidate();
        //}

        #endregion (Private Methods)

        protected virtual void OnChange(EventArgs e)
        {
            EventHandler handler = Change;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public delegate void ExpandInProgressEventHandler(GenericAction action);
        public event ExpandInProgressEventHandler ExpandInProgress;

        public delegate void CollapseInProgressEventHandler(GenericAction action);
        public event CollapseInProgressEventHandler CollapseInProgress;

        public event EventHandler Change;
    }
}
