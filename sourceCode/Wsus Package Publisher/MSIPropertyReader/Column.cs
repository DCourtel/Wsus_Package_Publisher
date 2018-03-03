using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher.MSIPropertyReader
{
    public sealed class Column
    {
        private string _name;
        private string _nullable;
        private Nullable<int> _minValue;
        private Nullable<int> _maxValue;
        private string _keyTable;
        private Nullable<Int16> _keyColumn;
        private string _category;
        private string _set;
        private string _description;
        private int _order;
        private List<string> _values = new List<string>();

        public Column(string name, string nullable, Nullable<int> minValue, Nullable<int> maxValue, string keyTable, Nullable<Int16> keyColumn, string category, string set, string description)
        {
            Name = name;
            Nullable = nullable;
            MinValue = minValue;
            MaxValue = maxValue;
            KeyTable = keyTable;
            KeyColumn = KeyColumn;
            Category = category;
            Set = set;
            Description = description;
        }

        #region {Properties - Propriétés}

        /// <summary>
        /// Get or Set the Name of this Column.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Identifies whether the column may contain a Null value.
        /// This column may have one of the following values : 
        /// Y : Yes, the column may have a Null value.
        /// N : No, the column may not have a Null value.
        /// </summary>
        public string Nullable
        {
            get { return _nullable; }
            set
            {
                if (value.ToUpper() == "Y" || value.ToUpper() == "N")
                    _nullable = value.ToUpper();
            }
        }

        /// <summary>
        /// This field applies to columns having numeric value. The field contains the minimum permissible value.
        /// This can be the minimum value for an integer or the minimum value for a date or version string.
        /// </summary>
        public Nullable<int> MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        /// <summary>
        /// This field applies to columns having numeric value. The field is the maximum permissible value.
        /// This may be the maximum value for an integer or the maximum value for a date or version string.
        /// </summary>
        public Nullable<int> MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        /// <summary>
        /// This field applies to columns that are external keys.
        /// The field identified in Column must link to the column number specified by KeyColumn in the table named in KeyTable.
        /// This can be a list of tables separated by semicolons.
        /// </summary>
        public string KeyTable
        {
            get { return _keyTable; }
            set { _keyTable = value; }
        }

        /// <summary>
        /// This field applies to table columns that are external keys.
        /// The field identified in Column must link to the column number specified by KeyColumn in the table named in KeyTable.
        /// The permissible range of the KeyColumn field is 1-32.
        /// </summary>
        public Nullable<Int16> KeyColumn
        {
            get { return _keyColumn; }
            set { _keyColumn = value; }
        }

        /// <summary>
        /// This is the type of data contained by the database field specified by the Table and Column columns of the _Validation table.
        /// If this is a type having a numeric value, such as Integer, DoubleInteger or Time/Date, then enter null into this field and specify the value's range using the MinValue and MaxValue columns.
        /// Use the Category column to specify the non-numeric data types described in Column Data Types.
        /// </summary>
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// This is a list of permissible values for this field separated by semicolons.
        /// This field is usually used for enumerations.
        /// </summary>
        public string Set
        {
            get { return _set; }
            set { _set = value; }
        }

        /// <summary>
        /// A description of the data that is stored in the column.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// The order of the column within the table.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// List of values contains in this Columns. This Property is fill by the 'GetAllMSIValueFromTable' method of the MsiReader Class.
        /// </summary>
        public List<string> Values
        {
            get { return _values; }
        }

        #endregion {Properties - Propriétés}


        #region {Methods - Méthodes}

        public override string ToString()
        {
            return Name;
        }

        #endregion {Methods - Méthodes}


    }
}
