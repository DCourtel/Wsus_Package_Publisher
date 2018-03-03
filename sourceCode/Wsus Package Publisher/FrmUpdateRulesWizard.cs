using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateRulesWizard : Form
    {
        RulesGroup _masterGroupUpdateLevel = new RulesGroup();
        RulesGroup _masterGroupPackageLevel = new RulesGroup();
        RulesGroup _currentGroupUpdateLevel;
        RulesGroup _currentGroupPackageLevel;
        List<GenericRule> _allSupportedRules = new List<GenericRule>();
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateRulesWizard).Assembly);

        internal FrmUpdateRulesWizard()
        {
            Logger.EnteringMethod("FrmUpdateRulesWizard");
            InitializeComponent();
            grpDspUpdateLevel.SelectionChange += new GroupDisplayer.SelectionChangeEventHandler(grpDsp1_SelectionChange);
            grpDspUpdateLevel.EditionRequest += new GroupDisplayer.EditionRequestEventHandler(grpDsp1_EditionRequest);
            grpDspUpdateLevel.RuleEditionRequest += new GroupDisplayer.RuleEditionRequestEventHandler(grpDsp1_RuleEditionRequest);

            grpDspPackageLevel.SelectionChange += new GroupDisplayer.SelectionChangeEventHandler(grpDsp1_SelectionChange);
            grpDspPackageLevel.EditionRequest += new GroupDisplayer.EditionRequestEventHandler(grpDsp1_EditionRequest);
            grpDspPackageLevel.RuleEditionRequest += new GroupDisplayer.RuleEditionRequestEventHandler(grpDsp1_RuleEditionRequest);
            _currentGroupUpdateLevel = _masterGroupUpdateLevel;
            _currentGroupPackageLevel = _masterGroupPackageLevel;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    _allSupportedRules.Add(new RuleMsiProductInstalled());
                    _allSupportedRules.Add(new RuleProcessorArchitecture());
                    _allSupportedRules.Add(new RuleWindowsVersion());
                    _allSupportedRules.Add(new RuleWindowsLanguage());
                    _allSupportedRules.Add(new RuleFileExists());
                    _allSupportedRules.Add(new RuleFileExistsPrependRegSz());
                    _allSupportedRules.Add(new RuleFileVersion());
                    _allSupportedRules.Add(new RuleFileVersionPrependRegSZ());
                    _allSupportedRules.Add(new RuleFileCreated());
                    _allSupportedRules.Add(new RuleFileCreatedPrependRegSz());
                    _allSupportedRules.Add(new RuleFileModified());
                    _allSupportedRules.Add(new RuleFileSize());
                    _allSupportedRules.Add(new RuleRegKeyExists());
                    _allSupportedRules.Add(new RuleRegValueExists());
                    _allSupportedRules.Add(new RuleRegDword());
                    _allSupportedRules.Add(new RuleRegExpandSz());
                    _allSupportedRules.Add(new RuleRegSz());
                    _allSupportedRules.Add(new RuleRegSzToVersion());
                    _allSupportedRules.Add(new RuleWmiQuery());
                    _allSupportedRules.Add(new RuleMsiPatchInstalledForProduct());
                    cmbBxRules.Items.AddRange(_allSupportedRules.ToArray());
                    cmbBxRules.SelectedIndex = 0;
                }));
            thread.Start();

            cmbBxRules.Focus();

            _masterGroupUpdateLevel.IsSelected = true;
            _masterGroupPackageLevel.IsSelected = true;
            grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
        }

        internal void InitializeUpdateLevelwithXml(string xmlUpdateLevel)
        {
            Logger.EnteringMethod();
            _masterGroupUpdateLevel.Reset();
            if (!string.IsNullOrEmpty(xmlUpdateLevel))
            {
                Logger.Write("XML at UpdateLevel : " + xmlUpdateLevel);
                _masterGroupUpdateLevel = ParseXml(_masterGroupUpdateLevel, xmlUpdateLevel, true);
            }

            grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
        }

        internal void InitializePackageLevelwithXml(string xmlPackageLevel)
        {
            _masterGroupPackageLevel.Reset();
            if (!string.IsNullOrEmpty(xmlPackageLevel))
            {
                Logger.Write("XML at PackageLevel : " + xmlPackageLevel);
                _masterGroupPackageLevel = ParseXml(_masterGroupPackageLevel, xmlPackageLevel, true);
            }

            grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
        }

        internal void InitializeWithXml(string newRule, string oldRules, bool isUpdateLevel)
        {
            Logger.EnteringMethod();
            if (!string.IsNullOrEmpty(newRule))
                if (!string.IsNullOrEmpty(oldRules))
                {
                    Logger.Write("New Rule : " + newRule + "\r\nOld Rules : " + oldRules);
                    //"<msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/>"
                    //"<lar:Not><msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/></lar:Not>"
                    if (oldRules.Contains("<msiar:MsiProductInstalled ProductCode=\""))
                    {
                        if (oldRules.IndexOf("<msiar:MsiProductInstalled ProductCode=\"") == oldRules.LastIndexOf("<msiar:MsiProductInstalled ProductCode=\""))
                        {
                            string oldMsiCode = oldRules.Substring(oldRules.IndexOf("<msiar:MsiProductInstalled ProductCode=\"") + "<msiar:MsiProductInstalled ProductCode=\"".Length, 38);
                            string newMsiCode = newRule.Substring(newRule.IndexOf("<msiar:MsiProductInstalled ProductCode=\"") + "<msiar:MsiProductInstalled ProductCode=\"".Length, 38);
                            oldRules = oldRules.Replace(oldMsiCode, newMsiCode);
                            Logger.Write("Old Rules have been modified to : " + oldRules);
                        }
                    }
                    if (isUpdateLevel)
                        InitializeUpdateLevelwithXml(oldRules);
                    else
                        InitializePackageLevelwithXml(oldRules);
                }
                else

                    if (isUpdateLevel)
                        InitializeUpdateLevelwithXml(newRule);
                    else
                        InitializePackageLevelwithXml(newRule);
        }

        internal bool EmptyRuleAtPackageLevel
        {
            get { return chkBxEmptyInstallableItemRule.Checked; }
        }

        internal bool IsAlreadyInitialized { get; set; }

        private RulesGroup ParseXml(RulesGroup group, string xml, bool mergeGroup)
        {
            Logger.EnteringMethod(xml);
            bool reverseRule = false;
            bool thisGroup = true;
            XmlNamespaceManager namespaceMng = new XmlNamespaceManager(new System.Xml.NameTable());
            namespaceMng.AddNamespace("lar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/LogicalApplicabilityRules.xsd");
            namespaceMng.AddNamespace("bar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/BaseApplicabilityRules.xsd");
            namespaceMng.AddNamespace("msiar", "http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/MsiApplicabilityRules.xsd");

            XmlParserContext context = new XmlParserContext(null, namespaceMng, null, XmlSpace.Default);
            XmlTextReader xmlReader = new XmlTextReader(xml, XmlNodeType.Element, context);

            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlReader.Read();
            while (!xmlReader.EOF)
            {
                switch (xmlReader.Prefix)
                {
                    case "lar":
                        switch (xmlReader.LocalName)
                        {
                            case "Not":
                                reverseRule = (xmlReader.NodeType != XmlNodeType.EndElement);
                                xmlReader.Read();
                                break;
                            case "And":
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    if (thisGroup && mergeGroup)
                                    {
                                        group.GroupType = RulesGroup.GroupLogicalOperator.And;
                                        thisGroup = false;
                                        xmlReader.Read();
                                    }
                                    else
                                    {
                                        RulesGroup tempGroup = new RulesGroup();
                                        group.AddGroup(ParseXml(tempGroup, xmlReader.ReadOuterXml(), true));
                                    }
                                }
                                else
                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                        return group;
                                break;
                            case "Or":
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    if (thisGroup && mergeGroup)
                                    {
                                        group.GroupType = RulesGroup.GroupLogicalOperator.Or;
                                        thisGroup = false;
                                        xmlReader.Read();
                                    }
                                    else
                                    {
                                        RulesGroup tempGroup = new RulesGroup();
                                        group.AddGroup(ParseXml(tempGroup, xmlReader.ReadOuterXml(), true));
                                    }
                                }
                                else
                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                        return group;
                                break;
                            default:
                                xmlReader.Read();
                                break;
                        }
                        break;
                    case "bar":
                    case "msiar":
                        foreach (GenericRule rule in _allSupportedRules)
                        {
                            if (xmlReader.LocalName == rule.XmlElementName)
                            {
                                GenericRule tempRule = GetSelectedForm(rule);
                                tempRule.ReverseRule = reverseRule;
                                tempRule.InitializeWithAttributes(GetAttributes(xmlReader));
                                group.AddRule(tempRule);
                                break;
                            }
                        }
                        xmlReader.Read();
                        break;
                    default:
                        xmlReader.Read();
                        break;
                }
            }
            return group;
        }

        private Dictionary<string, string> GetAttributes(XmlTextReader reader)
        {
            Logger.EnteringMethod();
            Dictionary<string, string> attributes = new Dictionary<string, string>();

            while (reader.MoveToNextAttribute())
            {
                if (!reader.Name.StartsWith("xmlns:"))
                {
                    Logger.Write("Adding Attributes : " + reader.Name + " = " + reader.Value);
                    attributes.Add(reader.Name, reader.Value);
                }
            }

            return attributes;
        }

        private void grpDsp1_RuleEditionRequest(GenericRule ResquestingRule)
        {
            Logger.EnteringMethod(ResquestingRule.ToString());
            EditRule(ResquestingRule);
        }

        private void grpDsp1_EditionRequest(GroupDisplayer sender)
        {
            Logger.EnteringMethod();
            sender.InnerGroup.Edit();
            if (sender.Equals(grpDspUpdateLevel))
                grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            else
                grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
        }

        private void grpDsp1_SelectionChange(GroupDisplayer sender)
        {
            Logger.EnteringMethod();
            int total = sender.SelectedRules.Count + sender.SelectedGroups.Count;

            btnEdit.Enabled = (total == 1);
            btnDelete.Enabled = (total > 0);
            btnSaveRules.Enabled = (total > 0);

            if (sender.SelectedGroups.Count == 1)
            {
                btnAddAndGroup.Enabled = true;
                btnAddOrGroup.Enabled = true;
                btnAddRule.Enabled = true;
                btnLoadRules.Enabled = true;
                if (sender.Equals(grpDspPackageLevel))
                    _currentGroupPackageLevel = sender.SelectedGroups[0];
                else
                    _currentGroupUpdateLevel = sender.SelectedGroups[0];
            }
            else
            {
                btnAddAndGroup.Enabled = false;
                btnAddOrGroup.Enabled = false;
                btnAddRule.Enabled = false;
                btnLoadRules.Enabled = false;
            }
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            GenericRule frmRule;

            frmRule = GetSelectedForm(cmbBxRules.SelectedItem);
            Form frm = new Form();
            frm.KeyPreview = true;
            frm.KeyDown += new KeyEventHandler(frm_KeyDown);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Controls.Add(frmRule);
            frm.Size = new Size(frmRule.Width + 20, frmRule.Height + 2 * SystemInformation.CaptionHeight);
            frmRule.Dock = DockStyle.Fill;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Logger.Write("Adding Rule : " + frm.GetType().ToString());
                if (rdBtnUpdateLevel.Checked)
                    _currentGroupUpdateLevel.AddRule(frmRule);
                else
                    _currentGroupPackageLevel.AddRule(frmRule);
            }

            frm.Hide();
            frm.KeyDown -= new KeyEventHandler(frm_KeyDown);
            frm = null;
            if (rdBtnUpdateLevel.Checked)
                grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            else
                grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
        }

        private GenericRule GetSelectedForm(Object obj)
        {
            Logger.EnteringMethod(obj.ToString());
            Type ruleType = obj.GetType();
            Logger.Write("Rule Type : " + ruleType.FullName);
            System.Reflection.Assembly assembly = ruleType.Assembly;
            return (GenericRule)assembly.CreateInstance(ruleType.FullName);
        }

        internal string GetXmlFormattedUpdateLevelRule()
        {
            Logger.EnteringMethod();
            return _masterGroupUpdateLevel.GetXmlFormattedRule();
        }

        internal string GetXmlFormattedPackageLevelRule()
        {
            Logger.EnteringMethod();
            return _masterGroupPackageLevel.GetXmlFormattedRule();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (rdBtnUpdateLevel.Checked)
            {
                if (grpDspUpdateLevel.SelectedRules.Count == 1)
                    EditRule(grpDspUpdateLevel.SelectedRules[0]);
                if (grpDspUpdateLevel.SelectedGroups.Count == 1)
                    grpDspUpdateLevel.SelectedGroups[0].Edit();
                grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            }
            else
            {
                if (grpDspPackageLevel.SelectedRules.Count == 1)
                    EditRule(grpDspPackageLevel.SelectedRules[0]);
                if (grpDspPackageLevel.SelectedGroups.Count == 1)
                    grpDspPackageLevel.SelectedGroups[0].Edit();
                grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
            }
        }

        private void EditRule(GenericRule editedRule)
        {
            Logger.EnteringMethod(editedRule.GetXmlFormattedRule());
            GenericRule backup = editedRule.Clone();
            Form frm = new Form();
            frm.KeyPreview = true;
            frm.KeyDown += new KeyEventHandler(frm_KeyDown);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Size = new Size(editedRule.Width + 20, editedRule.Height + 2 * SystemInformation.CaptionHeight);
            editedRule.Dock = DockStyle.Fill;
            frm.Controls.Add(editedRule);
            if (frm.ShowDialog() == DialogResult.Cancel)
                if (rdBtnUpdateLevel.Checked)
                    ReplaceRule(editedRule, backup, _masterGroupUpdateLevel);
                else
                    ReplaceRule(editedRule, backup, _masterGroupPackageLevel);
            frm.Hide();
            frm.KeyDown -= new KeyEventHandler(frm_KeyDown);
            frm = null;
        }

        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            Form ruleForm = (Form)sender;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ruleForm.Controls[0].Controls["btnOk"].Enabled)
                        (ruleForm.Controls[0].Controls["btnOk"] as Button).PerformClick();
                    break;
                case Keys.Escape:
                    (ruleForm.Controls[0].Controls["btnCancel"] as Button).PerformClick();
                    break;
                default:
                    break;
            }
        }

        private bool ReplaceRule(GenericRule editedRule, GenericRule backupRule, RulesGroup groupToSearchInto)
        {
            Logger.EnteringMethod();
            Logger.Write("Edited Rule : " + editedRule.GetXmlFormattedRule());
            Logger.Write("Backup Rule : " + backupRule.GetXmlFormattedRule());
            if (groupToSearchInto.InnerRules.ContainsKey(editedRule.Id))
            {
                groupToSearchInto.InnerRules.Remove(editedRule.Id);
                groupToSearchInto.AddRule(backupRule);
                if (rdBtnUpdateLevel.Checked)
                    grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
                else
                    grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
                return true;
            }
            else
                foreach (RulesGroup group in groupToSearchInto.InnerGroups.Values)
                {
                    if (ReplaceRule(editedRule, backupRule, group))
                        break;
                }
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            List<GenericRule> selectedRules;
            List<RulesGroup> selectedGroup;

            if (rdBtnUpdateLevel.Checked)
            {
                selectedRules = grpDspUpdateLevel.SelectedRules;
                selectedGroup = grpDspUpdateLevel.SelectedGroups;

                foreach (GenericRule rule in selectedRules)
                    DeleteRule(rule, _masterGroupUpdateLevel);
                foreach (RulesGroup group in selectedGroup)
                    DeleteGroup(group, _masterGroupUpdateLevel);
                grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            }
            else
            {
                selectedRules = grpDspPackageLevel.SelectedRules;
                selectedGroup = grpDspPackageLevel.SelectedGroups;
                foreach (GenericRule rule in selectedRules)
                    DeleteRule(rule, _masterGroupPackageLevel);
                foreach (RulesGroup group in selectedGroup)
                    DeleteGroup(group, _masterGroupPackageLevel);
                grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
            }
        }

        private bool DeleteRule(GenericRule ruleToDelete, RulesGroup groupToSearchInto)
        {
            Logger.EnteringMethod();
            if (groupToSearchInto.InnerRules.ContainsKey(ruleToDelete.Id))
            {
                Logger.Write("Deleting Rule : " + ruleToDelete.GetXmlFormattedRule());
                groupToSearchInto.InnerRules.Remove(ruleToDelete.Id);
                return true;
            }
            else
                foreach (RulesGroup group in groupToSearchInto.InnerGroups.Values)
                {
                    if (DeleteRule(ruleToDelete, group))
                        break;
                }
            return true;
        }

        private bool DeleteGroup(RulesGroup groupToDelete, RulesGroup groupToSearchInto)
        {
            Logger.EnteringMethod();
            if (groupToDelete.Id == _masterGroupUpdateLevel.Id || groupToDelete.Id == _masterGroupPackageLevel.Id)
                return false;

            if (groupToSearchInto.InnerGroups.ContainsKey(groupToDelete.Id))
            {
                Logger.Write("Deleting Group : " + groupToDelete.Id.ToString());
                groupToSearchInto.InnerGroups.Remove(groupToDelete.Id);
                return true;
            }
            else
                foreach (RulesGroup group in groupToSearchInto.InnerGroups.Values)
                {
                    if (DeleteGroup(groupToDelete, group))
                        break;
                }
            return true;
        }

        private void btnAddAndGroup_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            RulesGroup addedGroup = new RulesGroup();

            if (rdBtnUpdateLevel.Checked)
            {
                _currentGroupUpdateLevel.IsSelected = false;
                addedGroup.GroupType = RulesGroup.GroupLogicalOperator.And;
                addedGroup.IsSelected = true;
                _currentGroupUpdateLevel.AddGroup(addedGroup);
                grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            }
            else
            {
                _currentGroupPackageLevel.IsSelected = false;
                addedGroup.GroupType = RulesGroup.GroupLogicalOperator.And;
                addedGroup.IsSelected = true;
                _currentGroupPackageLevel.AddGroup(addedGroup);
                grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
            }
        }

        private void btnAddOrGroup_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            RulesGroup addedGroup = new RulesGroup();

            if (rdBtnUpdateLevel.Checked)
            {
                _currentGroupUpdateLevel.IsSelected = false;
                addedGroup.GroupType = RulesGroup.GroupLogicalOperator.Or;
                addedGroup.IsSelected = true;
                _currentGroupUpdateLevel.AddGroup(addedGroup);
                grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
            }
            else
            {
                _currentGroupPackageLevel.IsSelected = false;
                addedGroup.GroupType = RulesGroup.GroupLogicalOperator.Or;
                addedGroup.IsSelected = true;
                _currentGroupPackageLevel.AddGroup(addedGroup);
                grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
            }
        }

        private void btnSaveRules_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            List<GenericRule> selectedRules;
            List<RulesGroup> selectedGroup;

            if (rdBtnUpdateLevel.Checked)
            {
                selectedRules = grpDspUpdateLevel.SelectedRules;
                selectedGroup = grpDspUpdateLevel.SelectedGroups;
            }
            else
            {
                selectedRules = grpDspPackageLevel.SelectedRules;
                selectedGroup = grpDspPackageLevel.SelectedGroups;
            }


            FilterOutSelectedRules(selectedRules, selectedGroup);
            string xmlToSave = string.Empty;
            foreach (GenericRule rule in selectedRules)
            {
                xmlToSave += rule.GetXmlFormattedRule();
            }
            foreach (RulesGroup group in selectedGroup)
            {
                xmlToSave += group.GetXmlFormattedRule();
            }
#if DEBUG
            MessageBox.Show(xmlToSave);
#endif
            SaveFileDialog saveDlgBx = new SaveFileDialog();
            saveDlgBx.CheckPathExists = true;
            saveDlgBx.DefaultExt = ".rules";
            saveDlgBx.InitialDirectory = Environment.CurrentDirectory + "\\Saved Rules";
            saveDlgBx.Filter = "*.rules|*.rules";
            saveDlgBx.Title = resManager.GetString("SaveRules");
            saveDlgBx.ValidateNames = true;
            try
            {
                if (saveDlgBx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Logger.Write("Saving : " + xmlToSave);
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(saveDlgBx.FileName, false);
                    writer.Write(xmlToSave);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                MessageBox.Show(resManager.GetString("FailedToSave" + "\r\n" + ex.Message));
            }
        }

        private void FilterOutSelectedRules(List<GenericRule> selectedRules, List<RulesGroup> selectedGroup)
        {
            Logger.EnteringMethod();
            if (selectedRules.Count != 0)
            {
                foreach (RulesGroup group in selectedGroup)
                {
                    foreach (Guid ruleID in group.InnerRules.Keys)
                    {
                        int index = 0;
                        while (index < selectedRules.Count)
                        {
                            if (selectedRules[index].Id.Equals(ruleID))
                            {
                                selectedRules.RemoveAt(index);
                                break;
                            }
                            else
                                index++;
                        }
                    }
                    FilterOutSelectedRules(selectedRules, group.InnerGroups.Values.ToList<RulesGroup>());
                }
            }
        }

        private void btnLoadRules_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            string xmlToLoad = string.Empty;
            OpenFileDialog openDlgBx = new OpenFileDialog();

            openDlgBx.AddExtension = true;
            openDlgBx.CheckFileExists = true;
            openDlgBx.CheckPathExists = true;
            openDlgBx.DefaultExt = ".Rules";
            openDlgBx.Filter = "*.rules|*.rules";
            openDlgBx.InitialDirectory = Environment.CurrentDirectory + "\\Saved Rules";
            openDlgBx.Multiselect = false;
            openDlgBx.Title = resManager.GetString("OpenSavedRules");
            openDlgBx.ValidateNames = true;

            try
            {
                if (openDlgBx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(openDlgBx.FileName);
                    xmlToLoad = reader.ReadToEnd();
                    Logger.Write("Loading rules from Xml : " + xmlToLoad);
                    reader.Close();
                    RulesGroup selectedGroup;
                    if (rdBtnUpdateLevel.Checked)
                        selectedGroup = grpDspUpdateLevel.SelectedGroups[0];
                    else
                        selectedGroup = grpDspPackageLevel.SelectedGroups[0];
                    RulesGroup tmpGroup = new RulesGroup();
                    RulesGroup newRulesGroup = ParseXml(tmpGroup, xmlToLoad, false);

                    foreach (RulesGroup group in newRulesGroup.InnerGroups.Values)
                    {
                        selectedGroup.AddGroup(group);
                    }
                    foreach (GenericRule rule in newRulesGroup.InnerRules.Values)
                    {
                        selectedGroup.AddRule(rule);
                    }
#if DEBUG
                    MessageBox.Show(newRulesGroup.GetXmlFormattedRule());
#endif
                    if (rdBtnUpdateLevel.Checked)
                        grpDspUpdateLevel.Initialize(_masterGroupUpdateLevel);
                    else
                        grpDspPackageLevel.Initialize(_masterGroupPackageLevel);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                MessageBox.Show(resManager.GetString("FailedToLoad" + "\r\n" + ex.Message));
            }
        }

        private void rdBtnUpdateLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(rdBtnPackageLevel))
                rdBtnUpdateLevel.Checked = !rdBtnPackageLevel.Checked;
            else
                rdBtnPackageLevel.Checked = !rdBtnUpdateLevel.Checked;

            grpDspUpdateLevel.Enabled = rdBtnUpdateLevel.Checked;
            grpDspPackageLevel.Enabled = (rdBtnPackageLevel.Checked && !chkBxEmptyInstallableItemRule.Checked);
        }

        private void chkBxEmptyInstallableItemRule_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBxEmptyInstallableItemRule.Checked)
                rdBtnUpdateLevel.Checked = true;

            rdBtnPackageLevel.Enabled = !chkBxEmptyInstallableItemRule.Checked;
            grpDspPackageLevel.Enabled = (rdBtnPackageLevel.Checked && !chkBxEmptyInstallableItemRule.Checked);
        }
    }
}
