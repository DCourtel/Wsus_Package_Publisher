using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal sealed class MetaGroup
    {
        private List<MetaGroup> _innerMetaGroup = new List<MetaGroup>();
        private List<ComputerGroup> _innerComputerGroups = new List<ComputerGroup>();

        internal MetaGroup() { }

        internal MetaGroup(string name)
        {
            Logger.EnteringMethod(name);  
            this.Name = name;
        }

        /// <summary>
        /// Get or Set the name of the MetaGroup.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Get Inner MetaGroups.
        /// </summary>
        internal List<MetaGroup> InnerMetaGroups 
        {
            get { return _innerMetaGroup; }
        }

        /// <summary>
        /// Get Inner Computer Groups.
        /// </summary>
        internal List<ComputerGroup> InnerComputerGroups 
        {
            get { return _innerComputerGroups; } 
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
