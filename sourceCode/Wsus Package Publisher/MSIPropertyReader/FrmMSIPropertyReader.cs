using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsiReader;

namespace Wsus_Package_Publisher
{
    public partial class FrmMSIPropertyReader : Form
    {
        MsiReader.MsiReader reader = new MsiReader.MsiReader();

        public FrmMSIPropertyReader()
        {
            Logger.EnteringMethod("FrmMSIPropertyReader");
            InitializeComponent();
        }

        private void btnLoadMSIFile_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            MsiReader.MsiReader msiReader = new MsiReader.MsiReader();

            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Logger.Write("Will load " + openFileDialog1.FileName);
                reader.MsiFilePath = openFileDialog1.FileName;
                dtGrvProperties.Rows.Clear();
                dtGrvProperties.Columns.Clear();
                cmbBxTables.Items.Clear();

                SortedDictionary<string, Table> tables = reader.GetAllMSITables();

                foreach (KeyValuePair<string, Table> pair in tables)
                {
                    if (pair.Value.IsOrdered)
                        cmbBxTables.Items.Add(pair.Value);
                }
                cmbBxTables.Focus();
                foreach (Table item in cmbBxTables.Items)
                {
                    if (item.Name == "Property")
                    {
                        cmbBxTables.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void cmbBxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            dtGrvProperties.Rows.Clear();
            dtGrvProperties.Columns.Clear();

            if (cmbBxTables.SelectedIndex != -1 && cmbBxTables.SelectedItem != null)
            {
                Table table = (Table)cmbBxTables.SelectedItem;

                table = reader.GetAllMSIValueFromTable(table);

                SortedDictionary<int, Column> columns = new SortedDictionary<int, Column>();

                foreach (Column column in table.Columns)
                {
                    columns.Add(column.Order, column);
                }

                foreach (KeyValuePair<int, Column> pair in columns)
                {
                    int index = dtGrvProperties.Columns.Add(pair.Value.Name, pair.Value.Name);
                    dtGrvProperties.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                DataGridViewRow productCodeRow = null;

                for (int i = 0; i < columns[1].Values.Count; i++)
                {
                    int index = dtGrvProperties.Rows.Add();
                    DataGridViewRow row = dtGrvProperties.Rows[index];

                    foreach (KeyValuePair<int, Column> pair in columns)
                    {
                        if (pair.Value.Values.Count != 0)
                            row.Cells[pair.Value.Name].Value = pair.Value.Values[i];
                    }
                    if (row.Cells[0].Value.ToString().ToLower() == "productcode")
                        productCodeRow = row;
                }
                dtGrvProperties.Sort(dtGrvProperties.Columns[0], ListSortDirection.Ascending);
                if(productCodeRow != null)
                {
                    dtGrvProperties.ClearSelection();
                    productCodeRow.Selected = true;
                    dtGrvProperties.FirstDisplayedScrollingRowIndex = productCodeRow.Index;
                }
            }
        }
    }
}
