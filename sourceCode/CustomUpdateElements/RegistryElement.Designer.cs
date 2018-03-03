namespace CustomUpdateElements
{
    partial class RegistryElement
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
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxActions = new System.Windows.Forms.ComboBox();
            this.txtBxValue = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxValueType = new System.Windows.Forms.ComboBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxNewData = new System.Windows.Forms.TextBox();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxVariable = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxHive = new System.Windows.Forms.ComboBox();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxKey = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Action : ";
            // 
            // cmbBxActions
            // 
            this.cmbBxActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxActions.FormattingEnabled = true;
            this.cmbBxActions.Location = new System.Drawing.Point(79, 59);
            this.cmbBxActions.Name = "cmbBxActions";
            this.cmbBxActions.Size = new System.Drawing.Size(371, 21);
            this.cmbBxActions.TabIndex = 0;
            this.cmbBxActions.SelectedIndexChanged += new System.EventHandler(this.cmbBxAction_SelectedIndexChanged);
            // 
            // txtBxValue
            // 
            this.txtBxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxValue.Enabled = false;
            this.txtBxValue.Location = new System.Drawing.Point(78, 139);
            this.txtBxValue.Name = "txtBxValue";
            this.txtBxValue.Size = new System.Drawing.Size(371, 20);
            this.txtBxValue.TabIndex = 3;
            this.txtBxValue.TextChanged += new System.EventHandler(this.txtBxValue_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Value : ";
            // 
            // cmbBxValueType
            // 
            this.cmbBxValueType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxValueType.Enabled = false;
            this.cmbBxValueType.Location = new System.Drawing.Point(78, 194);
            this.cmbBxValueType.Name = "cmbBxValueType";
            this.cmbBxValueType.Size = new System.Drawing.Size(184, 21);
            this.cmbBxValueType.TabIndex = 5;
            this.cmbBxValueType.SelectedIndexChanged += new System.EventHandler(this.cmbBxValueType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Value Type : ";
            // 
            // txtBxNewData
            // 
            this.txtBxNewData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxNewData.Enabled = false;
            this.txtBxNewData.Location = new System.Drawing.Point(79, 165);
            this.txtBxNewData.Name = "txtBxNewData";
            this.txtBxNewData.Size = new System.Drawing.Size(370, 20);
            this.txtBxNewData.TabIndex = 4;
            this.txtBxNewData.TextChanged += new System.EventHandler(this.txtBxNewData_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "New Data : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Variable : ";
            // 
            // cmbBxVariable
            // 
            this.cmbBxVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxVariable.Enabled = false;
            this.cmbBxVariable.Location = new System.Drawing.Point(79, 223);
            this.cmbBxVariable.Name = "cmbBxVariable";
            this.cmbBxVariable.Size = new System.Drawing.Size(183, 21);
            this.cmbBxVariable.TabIndex = 6;
            this.cmbBxVariable.SelectedIndexChanged += new System.EventHandler(this.cmbBxVariable_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(382, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Hive : ";
            // 
            // cmbBxHive
            // 
            this.cmbBxHive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxHive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxHive.FormattingEnabled = true;
            this.cmbBxHive.Items.AddRange(new object[] {
            "HKEY_LOCAL_MACHINE",
            "HKEY_CURRENT_USER"});
            this.cmbBxHive.Location = new System.Drawing.Point(79, 86);
            this.cmbBxHive.Name = "cmbBxHive";
            this.cmbBxHive.Size = new System.Drawing.Size(183, 21);
            this.cmbBxHive.TabIndex = 1;
            this.cmbBxHive.SelectedIndexChanged += new System.EventHandler(this.cmbBxHive_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Key : ";
            // 
            // txtBxKey
            // 
            this.txtBxKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxKey.Location = new System.Drawing.Point(79, 113);
            this.txtBxKey.Name = "txtBxKey";
            this.txtBxKey.Size = new System.Drawing.Size(370, 20);
            this.txtBxKey.TabIndex = 2;
            this.txtBxKey.TextChanged += new System.EventHandler(this.txtBxKey_TextChanged);
            // 
            // RegistryElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBxKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbBxHive);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxActions);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbBxVariable);
            this.Controls.Add(this.txtBxNewData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBxValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBxValueType);
            this.Name = "RegistryElement";
            this.DoubleClick += new System.EventHandler(this.RegistryElement_DoubleClick);
            this.Controls.SetChildIndex(this.cmbBxValueType, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtBxValue, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtBxNewData, 0);
            this.Controls.SetChildIndex(this.cmbBxVariable, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.cmbBxActions, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbBxHive, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtBxKey, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.ComboBox cmbBxActions;
        private System.Windows.Forms.TextBox txtBxValue;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.ComboBox cmbBxValueType;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxNewData;
        private EasyCompany.Controls.ExtendedLabel label4;
        private EasyCompany.Controls.ExtendedLabel label5;
        private System.Windows.Forms.ComboBox cmbBxVariable;
        private System.Windows.Forms.Button btnOk;
        private EasyCompany.Controls.ExtendedLabel label6;
        private System.Windows.Forms.ComboBox cmbBxHive;
        private EasyCompany.Controls.ExtendedLabel label7;
        private System.Windows.Forms.TextBox txtBxKey;
    }
}
