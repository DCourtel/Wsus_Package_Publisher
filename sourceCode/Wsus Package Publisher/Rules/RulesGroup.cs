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
    internal partial class RulesGroup
    {
        internal enum GroupLogicalOperator { And, Or}
        private bool _isSelected = false;
        private Dictionary<Guid, GenericRule> _innerRules = new Dictionary<Guid, GenericRule>();
        private Dictionary<Guid, RulesGroup> _innerGroups = new Dictionary<Guid, RulesGroup>();
        private Guid _guid = Guid.NewGuid();
        private GroupLogicalOperator _groupType = GroupLogicalOperator.And;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RulesGroup).Assembly);

        #region (Methods - méthodes)

        internal void AddRule(GenericRule ruleToAdd)
        {
                _innerRules.Add(ruleToAdd.Id, ruleToAdd);
        }

        internal void AddGroup(RulesGroup groupToAdd)
        {
            _innerGroups.Add(groupToAdd.Id, groupToAdd);
        }

        /// <summary>
        /// Clear Inner rules and Inner Groups. Set Group Type to 'And'.
        /// </summary>
        internal void Reset()
        {
            _innerRules.Clear();
            _innerGroups.Clear();
            GroupType = GroupLogicalOperator.And;
        }

        internal string GetXmlFormattedRule()
        {
            string result = string.Empty;

            if ((InnerRules.Count + InnerGroups.Count) > 1)
            {
                switch (GroupType)
                {
                    case GroupLogicalOperator.And:
                        result += "<lar:And>\r\n";
                        break;
                    case GroupLogicalOperator.Or:
                        result += "<lar:Or>\r\n";
                        break;
                    default:
                        break;
                }
            }

            foreach (GenericRule rule in InnerRules.Values)
                result += rule.GetXmlFormattedRule();

            foreach (RulesGroup group in InnerGroups.Values)
                result += group.GetXmlFormattedRule();

            if ((InnerRules.Count + InnerGroups.Count) > 1)
            {
                switch (GroupType)
                {
                    case GroupLogicalOperator.And:
                        result += "</lar:And>\r\n";
                        break;
                    case GroupLogicalOperator.Or:
                        result += "</lar:Or>\r\n";
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
        
        internal void Edit()
        {
            DialogResult result = DialogResult.No;

            switch (GroupType)
            {
                case GroupLogicalOperator.And:
                    result = MessageBox.Show(resMan.GetString("EditGroupTypeOr"), "", MessageBoxButtons.YesNo);
                    break;
                case GroupLogicalOperator.Or:
                    result = MessageBox.Show(resMan.GetString("EditGroupTypeAnd"), "", MessageBoxButtons.YesNo);
                    break;
                default:
                    break;
            }

            if (result == DialogResult.Yes)
                if (GroupType == GroupLogicalOperator.And)
                    GroupType = GroupLogicalOperator.Or;
                else
                    GroupType = GroupLogicalOperator.And;
        }

        #endregion
        
        #region (Properties - Propiétés)

        internal bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        internal Guid Id
        {
            get { return _guid; }
        }

        internal Dictionary<Guid, GenericRule> InnerRules
        {
            get { return _innerRules; }
            set { _innerRules = value; }
        }

        internal Dictionary<Guid, RulesGroup> InnerGroups
        {
            get { return _innerGroups; }
            set { _innerGroups = value; }
        }

        internal GroupLogicalOperator GroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }

        #endregion
    }
}