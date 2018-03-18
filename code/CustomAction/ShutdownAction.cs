using System;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class ShutdownAction : GenericAction
    {
        public ShutdownAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 120;
            this.ActionIcon = Properties.Resources.Shutdown;
            this.Text = GetLocalizedString("DescriptionShutdownAction");
            this.ConfigurationState = ConfigurationStates.Configured;
        }

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'FullPath' textbox.
        /// </summary>
        protected override void OnExpand()
        {
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        public void ValidateData()
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

            _result += "</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("Shutdown");
        }

        #endregion Methods
    }
}