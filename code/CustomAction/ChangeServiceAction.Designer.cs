namespace CustomActions
{
    partial class ChangeServiceAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeServiceAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxServiceName = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdBtnDisable = new System.Windows.Forms.RadioButton();
            this.rdBtnManual = new System.Windows.Forms.RadioButton();
            this.rdBtnAutomatic = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxServiceName
            // 
            resources.ApplyResources(this.txtBxServiceName, "txtBxServiceName");
            this.txtBxServiceName.BackColor = System.Drawing.Color.Orange;
            this.txtBxServiceName.Name = "txtBxServiceName";
            this.txtBxServiceName.TextChanged += new System.EventHandler(this.txtBxServiceName_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdBtnDisable);
            this.groupBox1.Controls.Add(this.rdBtnManual);
            this.groupBox1.Controls.Add(this.rdBtnAutomatic);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // rdBtnDisable
            // 
            resources.ApplyResources(this.rdBtnDisable, "rdBtnDisable");
            this.rdBtnDisable.Name = "rdBtnDisable";
            this.rdBtnDisable.UseVisualStyleBackColor = true;
            this.rdBtnDisable.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rdBtnManual
            // 
            resources.ApplyResources(this.rdBtnManual, "rdBtnManual");
            this.rdBtnManual.Name = "rdBtnManual";
            this.rdBtnManual.UseVisualStyleBackColor = true;
            this.rdBtnManual.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rdBtnAutomatic
            // 
            resources.ApplyResources(this.rdBtnAutomatic, "rdBtnAutomatic");
            this.rdBtnAutomatic.Checked = true;
            this.rdBtnAutomatic.Name = "rdBtnAutomatic";
            this.rdBtnAutomatic.TabStop = true;
            this.rdBtnAutomatic.UseVisualStyleBackColor = true;
            this.rdBtnAutomatic.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // ChangeServiceAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxServiceName);
            this.Controls.Add(this.label1);
            this.Name = "ChangeServiceAction";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxServiceName;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdBtnDisable;
        private System.Windows.Forms.RadioButton rdBtnManual;
        private System.Windows.Forms.RadioButton rdBtnAutomatic;
    }
}
