using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;

namespace Wsus_Package_Publisher
{
    public abstract partial class GenericRule : UserControl
    {
        private bool _isSelected = false;
        private System.Guid _guid;
        private LinkLabel lnkLblHelp = new LinkLabel();
        private Dictionary<string, string> _unsupportedAttributes = new Dictionary<string, string>();
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleFileCreated).Assembly);

        internal GenericRule()
            : base()
        {
            this.Controls.Add(lnkLblHelp);
            InitializeComponent();
            _guid = System.Guid.NewGuid();
            lnkLblHelp.Text = resMan.GetString("Help");
            AdjustHelpLinkLocation();
            lnkLblHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lnkLblHelp.Click += new System.EventHandler(lnkLblHelp_Click);
            HelpLink = "http://technet.microsoft.com/en-us/library/bb531004.aspx";
        }

        #region (Properties - Propriétés)

        internal bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        internal System.Guid Id
        {
            get { return _guid; }
        }

        internal abstract bool ReverseRule { get; set; }

        internal abstract string XmlElementName
        {
            get;
        }

        internal Dictionary<string, string> UnsupportedAttributes
        {
            get { return _unsupportedAttributes; }
        }

        internal string HelpLink { get; set; }

        #endregion

        #region (Methods - Méthodes)

        internal void print(RichTextBox rTxtBx, System.Drawing.Font font, Color color, string text)
        {
            rTxtBx.SelectionFont = font;
            rTxtBx.SelectionColor = color;
            rTxtBx.SelectedText += text;
        }

        internal abstract string GetRtfFormattedRule();

        internal string GetXmlFormattedRule()
        {
            RichTextBox rTxtBxTemp = new RichTextBox();
            rTxtBxTemp.Rtf = GetRtfFormattedRule();
            return rTxtBxTemp.Text;
        }

        internal abstract void InitializeWithAttributes(Dictionary<string, string> attributes);

        internal abstract GenericRule Clone();

        public abstract override string ToString();

        internal void AdjustHelpLinkLocation()
        {
            lnkLblHelp.Location = new Point(7, this.Height - lnkLblHelp.Height);
        }

        private void lnkLblHelp_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo browser = new System.Diagnostics.ProcessStartInfo(HelpLink);

            try
            {
                System.Diagnostics.Process.Start(browser);
            }
            catch (System.Exception) { };
        }

        #endregion

    }
}