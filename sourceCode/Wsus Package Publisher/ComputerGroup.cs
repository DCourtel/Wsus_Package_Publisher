using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    public sealed class ComputerGroup
    {
        private string _displayName = string.Empty;
        private List<ComputerGroup> _innerComputerGroup = new List<ComputerGroup>();

        public ComputerGroup(string name, Guid id)
        {
            Name = name;
            DisplayName = name;
            ComputerGroupId = id;
        }

        public ComputerGroup(string name, Guid id, string displayName)
        {
            Name = name;
            DisplayName = displayName;
            ComputerGroupId = id;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        #region {Properties - Propriétés}

        internal string Name { get; set; }

        internal Guid ComputerGroupId { get; set; }

        internal string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        internal List<ComputerGroup> InnerComputerGroup { get { return _innerComputerGroup; } }

        #endregion {Properties - Propriétés}
    }
}
