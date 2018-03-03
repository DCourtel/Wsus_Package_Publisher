namespace Wsus_Package_Publisher
{
    partial class FrmDeleteObsoleteFolders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDeleteObsoleteFolders));
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkLstBxFoldersToDelete = new System.Windows.Forms.CheckedListBox();
            this.prgBarDelete = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkLstBxFoldersToDelete
            // 
            resources.ApplyResources(this.chkLstBxFoldersToDelete, "chkLstBxFoldersToDelete");
            this.chkLstBxFoldersToDelete.CheckOnClick = true;
            this.chkLstBxFoldersToDelete.FormattingEnabled = true;
            this.chkLstBxFoldersToDelete.Name = "chkLstBxFoldersToDelete";
            this.chkLstBxFoldersToDelete.Sorted = true;
            // 
            // prgBarDelete
            // 
            resources.ApplyResources(this.prgBarDelete, "prgBarDelete");
            this.prgBarDelete.Name = "prgBarDelete";
            // 
            // FrmDeleteObsoleteFolders
            // 
            this.AcceptButton = this.btnDelete;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.prgBarDelete);
            this.Controls.Add(this.chkLstBxFoldersToDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label1);
            this.Name = "FrmDeleteObsoleteFolders";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox chkLstBxFoldersToDelete;
        private System.Windows.Forms.ProgressBar prgBarDelete;
    }
}