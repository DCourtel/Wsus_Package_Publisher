using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPP.ActiveDirectory;
using WPP.Wsus;
using WPP.Management;
using System.Data;

namespace WsusADComparator
{
    public class Model
    {
        private string _login = String.Empty;
        private ActiveDirectory _ad = new ActiveDirectory();
        private IWsusServices _wsusServices = null;
        private WsusServer _wsusServer = null;
        private DataTable _computersTable;
        private Localization _localization = Localization.GetInstance();
        private System.Threading.Thread _searcherThread;

        public Model(DataTable computerTable)
        {
            //this._wsusServer = String.IsNullOrEmpty(wsusServerName) ? new WsusServer() : new WsusServer(wsusServerName, wsusServerPort, useSSL);
            this._computersTable = computerTable;

            this.ADComputerFound = 0;
            this.WsusComputerFound = 0;
            this.InitializeDataTable(this._computersTable);
        }

        #region (properties)

        /// <summary>
        /// Gets or Sets the number of computer found in Active Directory.
        /// </summary>
        public int ADComputerFound { get; private set; }

        /// <summary>
        /// Gets or Sets the number of computer found in Wsus.
        /// </summary>
        public int WsusComputerFound { get; private set; }

        #endregion (properties)

        #region (methods)

        /// <summary>
        /// Gets the name of the Active Directory domaine of this computer
        /// </summary>
        /// <returns>The name of the Active Directory domaine of this computer.</returns>
        public string GetDomaineName()
        {
            return this._ad.GetDomainName();
        }

        internal void ClearDataTable()
        {
            this._computersTable.Clear();
        }

        internal void SearchComputers(string domainName)
        {
            List<WppComputer> adComputers = new List<WppComputer>();
            this.ADComputerFound = 0;
            this.WsusComputerFound = 0;
            adComputers = this._ad.GetAdComputers(domainName);
            this.ADComputerFound = adComputers.Count;
            this.BeginAsynchronousSearch(adComputers);
        }

        internal void SearchComputers(List<OrganizationalUnit> OUList)
        {
            List<WppComputer> adComputers = new List<WppComputer>();
            this.ADComputerFound = 0;
            this.WsusComputerFound = 0;
            adComputers = this._ad.GetAdComputers(OUList);
            this.ADComputerFound = adComputers.Count;
            this.BeginAsynchronousSearch(adComputers);
        }

        private void BeginAsynchronousSearch(List<WppComputer> computers)
        {
            if (this.SearchComputerProgress != null)
                this.SearchComputerProgress(0, String.Format(this._localization.GetLocalizedString("FoundComputersInAD"), computers.Count));

            System.Threading.SynchronizationContext uiContext = System.Threading.SynchronizationContext.Current;

            this._searcherThread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                if (computers.Count != 0)
                    this.SearchComputersInWsus(computers);
                uiContext.Post(new System.Threading.SendOrPostCallback(UpdateDataTable), computers);

                if (this.SearchComputerFinished != null)
                    this.SearchComputerFinished();
            }));
            this._searcherThread.Start();
        }

        // CSV export

        internal string GetDataForExport()
        {
            string result = this.GetCsvHeader();
            List<System.Reflection.PropertyInfo> exportableProperties = WppComputer.GetDataGridViewProperties();

            foreach (DataRow row in this._computersTable.Rows)
            {
                try
                {
                    WppComputer computer = row.Field<WppComputer>(this._localization.GetLocalizedString("ComputerObj"));

                    foreach (System.Reflection.PropertyInfo exportableProperty in exportableProperties)
                    {
                        string propertyValue = exportableProperty.GetValue(computer, null).ToString();
                        result += propertyValue + ";";
                    }
                    result = result.Substring(0, result.Length - 1) + "\r\n";
                }
                catch (Exception) { }
            }

            return result;
        }

        private string GetCsvHeader()
        {
            string header = String.Empty;

            try
            {
                foreach (System.Reflection.PropertyInfo exportableProperty in WppComputer.GetDataGridViewProperties())
                {
                    header += this._localization.GetLocalizedString(exportableProperty.Name) + ";";
                }
                header = header.Substring(0, header.Length - 1) + "\r\n";
            }
            catch (Exception) { }

            return header;
        }

        // Data table Managment

        internal void UpdateDataTable(object computers)
        {
            List<WppComputer> adComputers = (List<WppComputer>)computers;

            this._computersTable.Clear();
            List<System.Reflection.PropertyInfo> datagridviewProperties = WppComputer.GetDataGridViewProperties();

            foreach (WppComputer adComputer in adComputers)
            {
                Object[] columns = new Object[datagridviewProperties.Count];

                try
                {
                    for (int i = 0; i < datagridviewProperties.Count; i++)
                    {
                        if (datagridviewProperties[i].PropertyType == typeof(DateTime))
                            columns[i] = DateTime.Parse(datagridviewProperties[i].GetValue(adComputer, null).ToString());
                        else if (datagridviewProperties[i].PropertyType == typeof(WppComputer))
                        {
                            columns[i] = (WppComputer)datagridviewProperties[i].GetValue(adComputer, null);
                        }
                        else
                        {
                            string propertyValue = datagridviewProperties[i].GetValue(adComputer, null).ToString();
                            columns[i] = String.Equals(propertyValue, String.Empty) ? null : propertyValue;
                        }
                    }

                    System.Data.DataRow newRow = this._computersTable.NewRow();
                    newRow.ItemArray = columns;
                    this._computersTable.Rows.Add(newRow);
                    adComputer.DatatableIndex = this._computersTable.Rows.IndexOf(newRow);
                }
                catch (Exception) { }
            }
        }

        private void InitializeDataTable(DataTable table)
        {
            foreach (System.Reflection.PropertyInfo exportableProperty in WppComputer.GetDataGridViewProperties())
            {
                table.Columns.Add(this._localization.GetLocalizedString(exportableProperty.Name), exportableProperty.PropertyType);
            }
        }

        // Wsus Queries

        private void SearchComputersInWsus(List<WppComputer> computers)
        {
            try
            {
                if (this.SearchComputerBegin != null)
                    this.SearchComputerBegin(computers.Count);

                this.ConnectToWsus();
                int progress = 0;

                foreach (WppComputer computer in computers)
                {
                    progress++;
                    bool isInWsus = this._wsusServices.IsInWsus(computer.ComputerName);
                    computer.IsInWsus = isInWsus ? this._localization.GetLocalizedString("Yes") : this._localization.GetLocalizedString("No");
                    if (isInWsus)
                        this.WsusComputerFound++;
                    if (this.SearchComputerProgress != null)
                        this.SearchComputerProgress(progress, this._localization.GetLocalizedString("SearchingInWsus"));
                }
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(this._localization.GetLocalizedString("ErrorOccursWhileQueryWsus") + "\r\n" + ex.Message); }
        }

        private void ConnectToWsus()
        {
            if (this._wsusServices == null)
            {
                this._wsusServices = _wsusServer.Connect();
            }
        }

        // Remote computers queries

        internal void GetWsusClientID(List<int> indexes, WPP.Security.Credential credential)
        {
            foreach (int index in indexes)
            {
                WppComputer remoteComputer = this._computersTable.Rows[index].Field<WppComputer>(this._localization.GetLocalizedString("ComputerObj"));
                this._computersTable.Rows[index].SetField<String>(this._localization.GetLocalizedString("WsusClientID"), remoteComputer.GetWsusClientID(credential));
            }
        }

        #endregion (methods)

        #region (Event Delegates - événements)

        public delegate void SearchComputerBeginEventHandler(int total);
        public event SearchComputerBeginEventHandler SearchComputerBegin;

        public delegate void SearchComputerProgressEventHandler(int progress, string message);
        public event SearchComputerProgressEventHandler SearchComputerProgress;

        public delegate void SearchComputerFinishedEventHandler();
        public event SearchComputerFinishedEventHandler SearchComputerFinished;

        #endregion
    }
}
