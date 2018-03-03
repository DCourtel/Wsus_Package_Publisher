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
    public partial class FrmExportUpdateProgress : Form
    {
        public FrmExportUpdateProgress()
        {
            InitializeComponent();
        }

        internal string Description
        {
            get
            { return lblProgression.Text; }
            set
            {
                lblProgression.Text = value;
                lblProgression.Refresh();
            }
        }

        internal void SetProgressBar(int value)
        {
            prgBrExporting.Value = value;
            prgBrExporting.Refresh();
        }

        internal void SetProgressBarMaxValue(int maxValue)
        {
            prgBrExporting.Maximum = maxValue;
        }

    }
}
