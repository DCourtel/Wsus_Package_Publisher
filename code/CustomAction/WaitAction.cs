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
    public partial class WaitAction : GenericAction
    {
        public WaitAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 120;
            this.ActionIcon = Properties.Resources.WaitElement;
            this.Text = GetLocalizedString("DescriptionWaitAction");
            base.RefersToUserProfile = false;
            base.ConfigurationState = ConfigurationStates.Configured;
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the amount of seconds to wait.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public int AmountOfTime
        {
            get { return (int)this.nupAmountOfTime.Value; }

            set { this.nupAmountOfTime.Value = System.Math.Max(1, System.Math.Min(600, value)); }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'AmountOfTime' nup.
        /// </summary>
        protected override void OnExpand()
        {
            this.nupAmountOfTime.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            this.ConfigurationState = ConfigurationStates.Configured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<AmountOfTime>" + this.AmountOfTime.ToString() + "</AmountOfTime>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("Wait") + " " + this.AmountOfTime + " " + this.GetLocalizedString("Seconds") + ".";
        }

        #endregion Methods

        #region Events

        private void nupAmountOfTime_ValueChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
