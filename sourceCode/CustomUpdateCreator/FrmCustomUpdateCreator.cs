using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUpdateElements;

namespace CustomUpdateCreator
{
    public partial class FrmCustomUpdateCreator : Form
    {
        List<GenericElement> elements = new List<GenericElement>();
        List<VariableElement> variables = new List<VariableElement>();

        public FrmCustomUpdateCreator()
        {
            InitializeComponent();

            customUpdateElementViewer1.ElementDoubleClick += new CustomUpdateElementViewer.CustomUpdateElementViewer.ElementDoubleClickEventHandler(addElement);

            ExecutableElement executableElement = new ExecutableElement();
            customUpdateElementViewer1.AddElement(executableElement);

            ScriptElement scriptElement = new ScriptElement();
            customUpdateElementViewer1.AddElement(scriptElement);

            TextFileElement textFileElement = new TextFileElement();
            customUpdateElementViewer1.AddElement(textFileElement);

            FileElement fileElement = new FileElement();
            customUpdateElementViewer1.AddElement(fileElement);

            FolderElement folderElement = new FolderElement();
            customUpdateElementViewer1.AddElement(folderElement);

            ServiceElement serviceElement = new ServiceElement();
            customUpdateElementViewer1.AddElement(serviceElement);

            RegistryKeyElement RegKeyElement = new RegistryKeyElement();
            customUpdateElementViewer1.AddElement(RegKeyElement);

            RegistryElement RegElement = new RegistryElement();
            customUpdateElementViewer1.AddElement(RegElement);

            VariableElement variableElement = new VariableElement();
            customUpdateElementViewer1.AddElement(variableElement);

            PowerManagementElement powerElement = new PowerManagementElement();
            customUpdateElementViewer1.AddElement(powerElement);

            WaitElement waitElement = new WaitElement();
            customUpdateElementViewer1.AddElement(waitElement);

            KillProcessElement killElement = new KillProcessElement();
            customUpdateElementViewer1.AddElement(killElement);

            ReturnCodeElement returnCodeElement = new ReturnCodeElement();
            customUpdateElementViewer1.AddElement(returnCodeElement);

            lnkLblHelp.Enabled = System.IO.File.Exists("Custom Updates.pdf");
        }

        #region (Public Methods - Méthodes Public)

        public void InitializeFromXML(string XMLActions)
        {
            variables.Clear();
            elements.Clear();
        }

        public string GetXmlActions()
        {
            string result = "<CustomUpdate>\r\n";

            foreach (VariableElement variable in variables)
            {
                result += variable.GetXMLAction() + "\r\n";
            }
            foreach (GenericElement element in elements)
            {
                result += element.GetXMLAction() + "\r\n";
            }

            return result + "</CustomUpdate>";
        }

        #endregion (Public Methods - Méthodes Public)

        #region (Privates Methods - Méthodes privées)

        private GenericElement GetSelectedElement(Object obj)
        {
            Type elementType = obj.GetType();
            System.Reflection.Assembly assembly = elementType.Assembly;
            return (GenericElement)assembly.CreateInstance(elementType.FullName);
        }

        private void UpElement(GenericElement elementToUp)
        {
            int index = elements.IndexOf(elementToUp);
            if (index != -1)
            {
                elements.Remove(elementToUp);
                elements.Insert(index - 1, elementToUp);
            }
            RefreshDisplay();
        }

        private void DownElement(GenericElement elementToDown)
        {
            int index = elements.IndexOf(elementToDown);
            if (index != -1)
            {
                elements.Remove(elementToDown);
                elements.Insert(index + 1, elementToDown);
            }
            RefreshDisplay();
        }

        private void DeleteElement(GenericElement elementToDelete)
        {
            if (elements.Contains(elementToDelete))
            {
                elements.Remove(elementToDelete);
                RefreshDisplay();
                elementToDelete.Dispose();
                elementToDelete = null;
            }
        }

        private void UnSelectAllElement()
        {
            foreach (GenericElement element in elements)
            {
                element.IsSelected = false;
            }
        }

        private void UnSelectAllVariable()
        {
            foreach (VariableElement variable in variables)
            {
                variable.IsSelected = false;
            }
        }

        private void UpdateContextMenu(Guid elementID, bool isVariableElement)
        {
            ctxMnuElement.Items["tlStrpUpElement"].Enabled = (elements.Count != 0) && (elementID != elements[0].ID) || (isVariableElement && elementID != variables[0].ID);
            ctxMnuElement.Items["tlStrpDownElement"].Enabled = (elements.Count != 0) && (elementID != elements[elements.Count - 1].ID) && !isVariableElement;
        }

        private void RefreshDisplay()
        {
            tlpElements.SuspendLayout();
            tlpElements.Controls.Clear();
            tlpElements.Controls.AddRange(variables.ToArray());
            tlpElements.Controls.AddRange(elements.ToArray());
            tlpElements.ResumeLayout();
            tlpElements.PerformLayout();
        }

        #endregion (Privates Methods - Méthodes privées)

        #region (Answers to events - Réponses auw évenements)

        private void addElement(GenericElement sender)
        {
            GenericElement elementToAdd = GetSelectedElement(sender);

            tlpElements.Controls.Add(elementToAdd);
            elementToAdd.Dock = DockStyle.Fill;
            elementToAdd.IsTemplate = false;
            elementToAdd.ElementSelect += new GenericElement.ElementSelectEventHandler(element_OnSelect);
            elementToAdd.ElementRightClick += new GenericElement.ElementRightClickEventHandler(element_RightClick);
            elementToAdd.ElementDoubleClick += new GenericElement.ElementDoubleClickEventHandler(elements_DoubleClick);
            if (elementToAdd.GetType() == typeof(VariableElement))
                variables.Add((VariableElement)elementToAdd);
            else
                elements.Add(elementToAdd);
        }

        private void element_RightClick(GenericElement sender)
        {
            UnSelectAllElement();
            UnSelectAllVariable();
            sender.IsSelected = true;
            UpdateContextMenu(sender.ID, sender.GetType() == typeof(VariableElement));
            Point clientPoint = sender.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            ctxMnuElement.Show(sender, clientPoint);
        }

        private void elements_DoubleClick(GenericElement sender)
        {
            sender.ShowElement(variables);
            txtBxDescription.Text = sender.ActionDescription;
        }

        private void element_OnSelect(GenericElement sender, bool controlKeyPressed)
        {
            int selectedCount = 0;

            foreach (GenericElement element in elements)
            {
                if (element.ID != sender.ID && !controlKeyPressed)
                    element.IsSelected = false;
                if (element.IsSelected)
                    selectedCount++;
            }
            if (selectedCount == 1)
                txtBxDescription.Text = sender.ActionDescription;
            else
                txtBxDescription.Text = string.Empty;
        }

        private void tlpElements_ControlsCountChange(object sender, ControlEventArgs e)
        {
            lblIndication.Visible = (tlpElements.Controls.Count == 0);
        }

        private void ctxMnuElement_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            GenericElement clickedElement = (GenericElement)ctxMnuElement.SourceControl;

            switch (e.ClickedItem.Name)
            {
                case "tlStrpUpElement":
                    UpElement(clickedElement);
                    break;
                case "tlStrpDownElement":
                    DownElement(clickedElement);
                    break;
                case "tlStrpDeleteElement":
                    DeleteElement(clickedElement);
                    break;
                default:
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool variableOK = true;
            bool elementsOk = true;

            if (elements.Count == 0)
            {
                MessageBox.Show("You must add, at least, one element.");
            }
            else
            {
                if (variables.Count != 0)
                {
                    foreach (VariableElement var in variables)
                    {
                        if (var.ConfigurationState != GenericElement.ConfigState.Configured)
                        {
                            variableOK = false;
                            break;
                        }
                    }
                }
                foreach (GenericElement element in elements)
                {
                    if (element.ConfigurationState != GenericElement.ConfigState.Configured)
                    {
                        elementsOk = false;
                    }
                    if(element.GetType() == typeof(ExecutableElement) && (element as ExecutableElement).FilePath.EndsWith(".msi", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (MessageBox.Show("You have include an ExecutableElement with a .MSI file. You cannot 'RUN' a .MSI file, you have to 'OPEN' it with MsiExec.exe.\r\nDo you want to make some modifications ?", String.Empty, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            return;
                    }
                }
                
                if (variableOK && elementsOk)
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                else
                    MessageBox.Show("All Elements and Variables must be correctly configured.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void lnkLblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo("Custom Updates.pdf");
                System.Diagnostics.Process.Start(processInfo);
            }
            catch (Exception) { }
        }

        #endregion (Answers to events - Réponses auw évenements)

    }
}
