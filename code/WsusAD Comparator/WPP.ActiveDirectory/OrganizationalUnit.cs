using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPP.ActiveDirectory
{
    public class OrganizationalUnit
    {
        private List<OrganizationalUnit> _childs = new List<OrganizationalUnit>();

        public OrganizationalUnit(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public List<OrganizationalUnit> Childs { get { return this._childs; } }

        public int ComputerCount { get; set; }
    }
}
