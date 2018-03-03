namespace Wsus_Package_Publisher
{
    partial class RuleMsiFeatureInstalledForProduct
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleMsiFeatureInstalledForProduct));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            this.chkBxAllProductsRequired = new System.Windows.Forms.CheckBox();
            this.chkBxAllFeaturesRequired = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lstBxFeatures = new System.Windows.Forms.ListBox();
            this.btnAddFeatures = new System.Windows.Forms.Button();
            this.btnRemoveFeature = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            // 
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
            // 
            // chkBxAllProductsRequired
            // 
            resources.ApplyResources(this.chkBxAllProductsRequired, "chkBxAllProductsRequired");
            this.chkBxAllProductsRequired.Name = "chkBxAllProductsRequired";
            this.chkBxAllProductsRequired.UseVisualStyleBackColor = true;
            // 
            // chkBxAllFeaturesRequired
            // 
            resources.ApplyResources(this.chkBxAllFeaturesRequired, "chkBxAllFeaturesRequired");
            this.chkBxAllFeaturesRequired.Name = "chkBxAllFeaturesRequired";
            this.chkBxAllFeaturesRequired.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lstBxFeatures
            // 
            resources.ApplyResources(this.lstBxFeatures, "lstBxFeatures");
            this.lstBxFeatures.FormattingEnabled = true;
            this.lstBxFeatures.Name = "lstBxFeatures";
            this.lstBxFeatures.SelectedIndexChanged += new System.EventHandler(this.lstBxFeatures_SelectedIndexChanged);
            // 
            // btnAddFeatures
            // 
            resources.ApplyResources(this.btnAddFeatures, "btnAddFeatures");
            this.btnAddFeatures.Name = "btnAddFeatures";
            this.btnAddFeatures.UseVisualStyleBackColor = true;
            this.btnAddFeatures.Click += new System.EventHandler(this.btnAddFeatures_Click);
            // 
            // btnRemoveFeature
            // 
            resources.ApplyResources(this.btnRemoveFeature, "btnRemoveFeature");
            this.btnRemoveFeature.Name = "btnRemoveFeature";
            this.btnRemoveFeature.UseVisualStyleBackColor = true;
            this.btnRemoveFeature.Click += new System.EventHandler(this.btnRemoveFeature_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // RuleMsiFeatureInstalledForProduct
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveFeature);
            this.Controls.Add(this.btnAddFeatures);
            this.Controls.Add(this.lstBxFeatures);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkBxAllFeaturesRequired);
            this.Controls.Add(this.chkBxAllProductsRequired);
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleMsiFeatureInstalledForProduct";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
        private System.Windows.Forms.CheckBox chkBxAllProductsRequired;
        private System.Windows.Forms.CheckBox chkBxAllFeaturesRequired;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lstBxFeatures;
        private System.Windows.Forms.Button btnAddFeatures;
        private System.Windows.Forms.Button btnRemoveFeature;
        private System.Windows.Forms.Label label1;
    }
}
