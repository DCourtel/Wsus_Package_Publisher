using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPP.Management
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataGridViewDataAttribute : Attribute
    {
        public DataGridViewDataAttribute(bool visible, float width, bool displayedByDefault, bool canBeHide)
        {
            this.Visible = visible;
            this.Width = width;
            this.DisplayedByDefault = displayedByDefault;
            this.CanBeHide = canBeHide;
        }

        /// <summary>
        /// Gets or Sets if this property is displayed
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or Sets the Width of the DataGridView Column
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or Sets if this DataGridView Column is displayed by default
        /// </summary>
        public bool DisplayedByDefault { get; set; }

        /// <summary>
        /// Gets or Sets if this DataGridView can be hide
        /// </summary>
        public bool CanBeHide { get; set; }
    }
}
