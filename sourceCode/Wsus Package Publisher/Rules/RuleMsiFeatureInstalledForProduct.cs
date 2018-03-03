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
    internal partial class RuleMsiFeatureInstalledForProduct : GenericRule
    {
        internal struct FeatureProduct
        {
            internal string Feature { get; set; }
            internal Guid Product { get; set; }
        }
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleMsiFeatureInstalledForProduct).Assembly);

        public RuleMsiFeatureInstalledForProduct()
            : base()
        {
            InitializeComponent();

            txtBxDescription.Text = resMan.GetString("DescriptionRuleMsiFeatureInstalledForProduct");
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531042.aspx";
            AdjustHelpLinkLocation();
        }

        #region {methods - Méthodes}

        private bool ValidateData()
        {
            return (lstBxFeatures.Items.Count != 0);
        }

        internal override string GetRtfFormattedRule()
        {
            RichTextBox rTxtBx = new RichTextBox();

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "<lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">\r\n");
            }

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "<bar:");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "MsiFeatureInstalledForProduct");

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " AllFeaturesRequired");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, AllFeaturesRequired.ToString().ToLower());
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " AllProductsRequired");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, AllProductsRequired.ToString().ToLower());
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            foreach (FeatureProduct pair in FeatureProductPair)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Feature");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, pair.Feature);
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Product");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, pair.Product.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }            

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "/>");

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\r\n");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "</lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">");
            }

            return rTxtBx.Rtf;
        }

        internal override GenericRule Clone()
        {
            RuleMsiFeatureInstalledForProduct clone = new RuleMsiFeatureInstalledForProduct();

            clone.FeatureProductPair = this.FeatureProductPair;
            clone.AllFeaturesRequired = this.AllFeaturesRequired;
            clone.AllProductsRequired = this.AllProductsRequired;
            clone.ReverseRule = this.ReverseRule;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("MsiFeatureInstalledForProduct");
        }

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            //foreach (KeyValuePair<string, string> pair in attributes)
            //{
            //    switch (pair.Key)
            //    {
            //        case "Namespace":
            //            this.Namespace = pair.Value;
            //            break;
            //        case "WqlQuery":
            //            this.WqlQuery = pair.Value;
            //            break;
            //        default:
            //            UnsupportedAttributes.Add(pair.Key, pair.Value);
            //            break;
            //    }
            //}
        }

        #endregion {methods - Méthodes}

        #region {Properties - Propriétés}

        internal List<FeatureProduct> FeatureProductPair { get; set; }

        internal bool AllFeaturesRequired
        {
            get { return chkBxAllFeaturesRequired.Checked; }
            set { chkBxAllFeaturesRequired.Checked = value; }
        }

        internal bool AllProductsRequired
        {
            get { return chkBxAllProductsRequired.Checked; }
            set { chkBxAllProductsRequired.Checked = value; }
        }

        /// <summary>
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        internal override string XmlElementName
        {
            get { return "MsiFeatureInstalledForProduct"; }
        }

        #endregion {Properties - Propriétés}

        #region {Response to events - Réponse aux évènements}

        private void lstBxFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddFeatures_Click(object sender, EventArgs e)
        {
            FrmFeatureForProduct featureProduct = new FrmFeatureForProduct();

            if (featureProduct.ShowDialog(this) == DialogResult.OK)
            {
                FeatureProduct featureProductPair = new FeatureProduct();
                featureProductPair.Feature = featureProduct.FeatureName;
                featureProductPair.Product = featureProduct.ProductGuid;
                FeatureProductPair.Add(featureProductPair);
            }
            btnOk.Enabled = ValidateData();
            btnRemoveFeature.Enabled = (FeatureProductPair.Count !=0);
        }

        private void btnRemoveFeature_Click(object sender, EventArgs e)
        {
            if (lstBxFeatures.SelectedIndex != -1)
                lstBxFeatures.Items.RemoveAt(lstBxFeatures.SelectedIndex);

            btnOk.Enabled = ValidateData();
            btnRemoveFeature.Enabled = (FeatureProductPair.Count != 0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        #endregion {Response to events - Réponse aux évènements}
    }

    internal partial class FrmFeatureForProduct : Form
    {
        private System.Text.RegularExpressions.Regex regExp = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$");

        public FrmFeatureForProduct()
        {
            InitializeComponent();
        }

        #region {Methods - Méthodes}

        internal bool ValidateData()
        {
            return (!string.IsNullOrEmpty(txtBxFeatureName.Text) && !string.IsNullOrEmpty(txtBxProductGuid.Text) && regExp.IsMatch(txtBxProductGuid.Text));
        }

        #endregion {methods - Méthodes}

        #region {Properties - Propriétes}

        /// <summary>
        /// Get or Set the Feature's Name.
        /// </summary>
        internal string FeatureName
        {
            get { return txtBxFeatureName.Text; }
            set { txtBxFeatureName.Text = value; }

        }

        /// <summary>
        /// Get or Set the Guid of the product.
        /// </summary>
        internal Guid ProductGuid
        {
            get { return new Guid(txtBxProductGuid.Text); }
            set { txtBxProductGuid.Text = value.ToString(); }

        }

        #endregion {Properties - Propriétes}

        #region {Response to events - Réponse aux évènements}

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void txtBxFeatureName_TextChanged(object sender, System.EventArgs e)
        {
            btnOk.Enabled = ValidateData();
        }

        private void ProductGuid_TextChanged(object sender, System.EventArgs e)
        {
            txtBxProductGuid.Text = txtBxProductGuid.Text.TrimStart(new char[] { '{' });
            txtBxProductGuid.Text = txtBxProductGuid.Text.TrimEnd(new char[] { '}' });

            btnOk.Enabled = ValidateData();
        }

        #endregion {Response to events - Réponse aux évènements}
    }

}
