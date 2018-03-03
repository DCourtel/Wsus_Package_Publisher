using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class frmCatalogDifferenceViewer : Form
    {
        private List<CatalogSubscription> _updatedCatalogs = new List<CatalogSubscription>();
        private CatalogSubscription _displayedCatalog;
        private bool _programaticallyChangeSelection = false;

        internal frmCatalogDifferenceViewer(List<CatalogSubscription> updatedCatalogs)
        {
            InitializeComponent();
            _updatedCatalogs = updatedCatalogs;
        }

        private void ClearDisplay()
        {
            txtBxVendor.Text = string.Empty;
            txtBxProduct.Text = string.Empty;
            txtBxTitle.Text = string.Empty;
            txtBxDescription.Text = string.Empty;
        }

        private void DisplayUpdateInformation(CatalogUpdate updateToDisplay)
        {
            if (!string.IsNullOrEmpty(updateToDisplay.VendorName))
                txtBxVendor.Text = updateToDisplay.VendorName;
            if (!string.IsNullOrEmpty(updateToDisplay.ProductName))
                txtBxProduct.Text = updateToDisplay.ProductName;
            if (!string.IsNullOrEmpty(updateToDisplay.Title))
                txtBxTitle.Text = updateToDisplay.Title;
            if (!string.IsNullOrEmpty(updateToDisplay.Description))
                txtBxDescription.Text = updateToDisplay.Description;
        }

        private void frmCatalogDifferenceViewer_Shown(object sender, EventArgs e)
        {
            ClearDisplay();
            cmbBxCatalog.Items.Clear();
            cmbBxDeletedUpdates.Items.Clear();
            cmbBxAddedUpdates.Items.Clear();
            foreach (CatalogSubscription catalog in _updatedCatalogs)
            {
                cmbBxCatalog.Items.Add(catalog);
            }
        }

        private void cmbBxCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearDisplay();
            cmbBxDeletedUpdates.Items.Clear();
            cmbBxAddedUpdates.Items.Clear();

            if (cmbBxCatalog.SelectedIndex != -1 && cmbBxCatalog.SelectedItem != null)
            {
                _displayedCatalog = (CatalogSubscription)cmbBxCatalog.SelectedItem;
                foreach (CatalogUpdate deletedUpdate in _displayedCatalog.DeletedUpdates)
                {
                    cmbBxDeletedUpdates.Items.Add(deletedUpdate);
                }
                foreach (CatalogUpdate addedUpdate in _displayedCatalog.AddedUpdates)
                {
                    cmbBxAddedUpdates.Items.Add(addedUpdate);
                }
            }
        }

        private void cmbBxDeletedUpdates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_programaticallyChangeSelection)
            {
                _programaticallyChangeSelection = true;
                cmbBxAddedUpdates.SelectedIndex = -1;
                ClearDisplay();

                if (cmbBxDeletedUpdates.SelectedIndex != -1 && cmbBxDeletedUpdates.SelectedItem != null)
                    DisplayUpdateInformation((CatalogUpdate)cmbBxDeletedUpdates.SelectedItem);

                _programaticallyChangeSelection = false;
            }
        }

        private void cmbBxAddedUpdates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_programaticallyChangeSelection)
            {
                _programaticallyChangeSelection = true;
                cmbBxDeletedUpdates.SelectedIndex = -1;
                ClearDisplay();

                if (cmbBxAddedUpdates.SelectedIndex != -1 && cmbBxAddedUpdates.SelectedItem != null)
                    DisplayUpdateInformation((CatalogUpdate)cmbBxAddedUpdates.SelectedItem);

                _programaticallyChangeSelection = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }



    }
}
