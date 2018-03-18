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
    public partial class StartServiceAction : GenericAction
    {
        public StartServiceAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 120;
            this.ActionIcon = Properties.Resources.StartServicesElement;
            this.Text = GetLocalizedString("DescriptionStartServiceAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the name of the service to start.
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

            _result += "<ServiceName>" + this.ServiceName + "</ServiceName>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("StartTheService") + this.ServiceName + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxServiceName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events

    }
}
