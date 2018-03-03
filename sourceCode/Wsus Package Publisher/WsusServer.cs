using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class WsusServer : IEquatable<WsusServer>, ICloneable  
    {
        private string _name = "";
        private bool _isLocal = false;
        private int _port = 80;
        private bool _useSSL = false;
        private bool _ignoreCertErrors = false;
        private int _deadLineDaysSpan = 7;
        private int _deadLineHour = 16;
        private int _deadLineMinute = 31;
        private FrmSettings.MakeVisibleInWsusPolicy _visibility = FrmSettings.MakeVisibleInWsusPolicy.Never;
        private List<MetaGroup> _metaGroups = new List<MetaGroup>();

        internal WsusServer()
        {
            Logger.EnteringMethod();
        }

        internal WsusServer(string name, bool isLocal, int port, bool useSSL, int deadLineDaysSpan, int hour, int minute)
        {
            Logger.EnteringMethod(name + ", " + isLocal.ToString() + ", " + port.ToString() + ", " + useSSL.ToString() + ", " + deadLineDaysSpan.ToString() + ", " + hour.ToString() + ", " + minute.ToString());
            Name = name;
            IsLocal = isLocal;
            Port = port;
            UseSSL = useSSL;
            DeadLineDaysSpan = deadLineDaysSpan;
            DeadLineHour = hour;
            DeadLineMinute = minute;
        }

        /// <summary>
        /// Get or Set the name of the server.
        /// </summary>
        internal string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Logger.EnteringMethod(value.ToString());
                    _name = value;
                }
            }
        }

        /// <summary>
        /// Get or Set if the application should connect to the local machine.
        /// </summary>
        internal bool IsLocal
        {
            get { return _isLocal; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _isLocal = value;
            }
        }

        /// <summary>
        /// Get or Set communication port for this server.
        /// </summary>
        internal int Port
        {
            get { return _port; }
            set
            {
                if (value > 0 && value <= 65535)
                {
                    Logger.EnteringMethod(value.ToString());
                    _port = value;
                }
            }
        }

        /// <summary>
        /// Get or Set if the communication use SSL.
        /// </summary>
        internal bool UseSSL
        {
            get { return _useSSL; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _useSSL = value;
            }
        }

        /// <summary>
        /// Get or Set if WPP should ignore Certification Validation Errors.
        /// </summary>
        internal bool IgnoreCertificateErrors
        {
            get { return _ignoreCertErrors; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _ignoreCertErrors = value;
            }
        }

        /// <summary>
        /// Get or Set the number of days betwwen today and the DeadLine for udpate installation.
        /// </summary>
        internal int DeadLineDaysSpan
        {
            get { return _deadLineDaysSpan; }
            set
            {
                if (value >= 0 && value <= 365)
                {
                    Logger.EnteringMethod(value.ToString());
                    _deadLineDaysSpan = value;
                }
            }
        }

        /// <summary>
        /// Get or Set the hour of the DeadLine.
        /// </summary>
        internal int DeadLineHour
        {
            get { return _deadLineHour; }
            set
            {
                if (value >= 0 && value <= 23)
                {
                    Logger.EnteringMethod(value.ToString());
                    _deadLineHour = value;
                }
            }
        }

        internal int DeadLineMinute
        {
            get { return _deadLineMinute; }
            set
            {
                if (value >= 0 && value <= 59)
                {
                    Logger.EnteringMethod(value.ToString());
                    _deadLineMinute = value;
                }
            }
        }

        internal FrmSettings.MakeVisibleInWsusPolicy VisibleInWsusConsole
        {
            get { return _visibility; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _visibility = value;
            }
        }

        internal List<MetaGroup> MetaGroups
        {
            get { return _metaGroups; }
            private set { _metaGroups = value; }
        }

        internal bool IsValid()
        {
            return (!String.IsNullOrEmpty(Name) && Port > 0 && Port < 65536 &&
                DeadLineDaysSpan >= 0 && DeadLineDaysSpan <= 365 &&
                DeadLineHour >= 0 && DeadLineHour <= 23 &&
                DeadLineMinute >= 0 && DeadLineMinute <= 59);
        }

        public override string ToString()
        {
            if (IsLocal)
                return Name + " (Local)";
            else
                return Name + " (" + Port.ToString() + ")";
        }

        public bool Equals(WsusServer other)
        {
            if (other == null)
                return false;
            if (this.Name == other.Name && this.Port == other.Port && this.UseSSL == other.UseSSL && this.IsLocal == other.IsLocal)
                return true;
            return false;
        }

        public int GetHashCode(WsusServer server)
        {
            string fingerPrint = server.Name + server.Port.ToString() + server.UseSSL.ToString() + server.IsLocal.ToString();
            return fingerPrint.GetHashCode();
        }

        public object Clone()
        {
            WsusServer clone = new WsusServer(this.Name, this.IsLocal, this.Port, this.UseSSL, this.DeadLineDaysSpan, this.DeadLineHour, this.DeadLineMinute);
            clone.VisibleInWsusConsole = this.VisibleInWsusConsole;
            clone.IgnoreCertificateErrors = this.IgnoreCertificateErrors;
            clone.MetaGroups = this.MetaGroups;

            return clone;
        }
    }
}
