using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher.MSIPropertyReader
{
    public sealed class Table
    {
        private string _name;
        private List<Column> _columns = new List<Column>();

        public Table()
        {
            IsOrdered = false;
        }

        #region {Properties - Propriétés}

        /// <summary>
        /// Get or Set the name of this Table.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;
            }
        }

        /// <summary>
        /// Get the list of all Columns in this table.
        /// </summary>
        public List<Column> Columns
        {
            get { return _columns; }
        }

        public bool IsOrdered { get; set; }

        #endregion {Properties - Propriétés}

        #region {Methods - Méthodes}

        public override string ToString()
        {
            return Name;
        }

        public Column GetColumn(string columnName)
        {
            Column result = null;

            foreach (Column column in Columns)
            {
                if (column.Name.ToLower() == columnName.ToLower())
                {
                    result = column;
                    break;
                }
            }

            return result;
        }

        #endregion {Methods - Méthodes}

    }
}
