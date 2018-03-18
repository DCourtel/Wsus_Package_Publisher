using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class ChangeServiceAction : GenericAction
    {
        public ChangeServiceAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 150;
            this.ActionIcon = Properties.Resources.ChangeServiceElement;
            this.Text = GetLocalizedString("DescriptionChangeServiceAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the name of the service to stop.
        /// </summary>
        /// <exception cref="NullReferenceException">'ServiceName' can not be set to null.</exception>
        public string ServiceName
        {
            get { return this.txtBxServiceName.Text.Trim(); }

            set
            {
                this.txtBxServiceName.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets the starting mode for this service. Allowed parameters for the setter (Case insensitive): 'Automatic', 'Manual' or 'Disable'. Default is 'Disable'.
        /// </summary>
        public string Mode
        {
            get
            {
                if (this.rdBtnAutomatic.Checked)
                    return "Automatic";
                if (this.rdBtnManual.Checked)
                    return "Manual";
                return "Disable";
            }

            set
            {
                switch (value.ToLower())
                {
                    case "automatic":
                        this.rdBtnAutomatic.Checked = true;
                        break;
                    case "manual":
                        this.rdBtnManual.Checked = true;
                        break;
                    default:
                        this.rdBtnDisable.Checked = true;
                        break;
                }
            }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'ServiceName' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxServiceName.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            if (String.IsNullOrEmpty(this.ServiceName))
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxServiceName.BackColor = Color.Orange;
            }
            else
            {
                this.ConfigurationState = ConfigurationStates.Configured;
                this.txtBxServiceName.BackColor = SystemColors.Window;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<ServiceName>" + this.ServiceName + "</ServiceName>\r\n<Mode>" + this.Mode + "</Mode>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("ChangeTheStartingModeOf") + this.ServiceName + " " + this.GetLocalizedString("Into") + this.GetLocalizedString(this.Mode) + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxServiceName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
