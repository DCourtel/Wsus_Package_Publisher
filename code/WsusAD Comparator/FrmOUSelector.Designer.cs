namespace WsusADComparator
{
    partial class FrmOUSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOUSelector));
            this.chkBxSearchAll = new System.Windows.Forms.CheckBox();
            this.trvOU = new System.Windows.Forms.TreeView();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkBxSearchAll
            // 
            resources.ApplyResources(this.chkBxSearchAll, "chkBxSearchAll");
            this.chkBxSearchAll.Checked = true;
            this.chkBxSearchAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxSearchAll.Name = "chkBxSearchAll";
            this.chkBxSearchAll.UseVisualStyleBackColor = true;
            this.chkBxSearchAll.CheckedChanged += new System.EventHandler(this.chkBxSearchAll_CheckedChanged);
            // 
            // trvOU
            // 
            resources.ApplyResources(this.trvOU, "trvOU");
            this.trvOU.CheckBoxes = true;
            this.trvOU.Name = "trvOU";
            this.trvOU.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvOU_AfterCheck);
            this.trvOU.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.trvOU_AfterCollapse);
            this.trvOU.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvOU_AfterExpand);
            this.trvOU.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvOU_KeyPressedChanged);
            this.trvOU.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trvOU_KeyPressedChanged);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FrmOUSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.trvOU);
            this.Controls.Add(this.chkBxSearchAll);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Name = "FrmOUSelector";
            this.Shown += new System.EventHandler(this.FrmOUSelector_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBxSearchAll;
        private System.Windows.Forms.TreeView trvOU;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}