using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal class CatalogUpdate
    {
        private SoftwareDistributionPackage _sdp;

        internal CatalogUpdate(SoftwareDistributionPackage sdp)
        {
            SDP = sdp;
        }

        public override string ToString()
        {
            return Title;
        }

        #region (Internal Properties - PRopriétés Internes)

        internal SoftwareDistributionPackage SDP
        {
            get { return _sdp; }
            private set { _sdp = value; }
        }

        internal string Title
        {
            get { return GetString(SDP.Title); }
        }

        internal string VendorName
        {
            get { return GetString(SDP.VendorName); }
        }

        internal string ProductName
        {
            get { return GetString(SDP.ProductNames); }
        }

        internal string Description
        {
            get { return GetString(SDP.Description); }
        }

        internal string SupportUrl
        {
            get { return GetString(SDP.SupportUrl); }
        }

        internal string SecurityBulletinId
        {
            get { return GetString(SDP.SecurityBulletinId); }
        }

        internal string SecurityRating
        {
            get { return GetString(SDP.SecurityRating.ToString()); }
        }

        internal string PackageId
        {
            get { return GetString(SDP.PackageId.ToString()); }
        }

        internal IList<Uri> AdditionnalInformationsUrls
        {
            get { return _sdp.AdditionalInformationUrls; }
        }

        internal System.Collections.Specialized.StringCollection CveIds
        {
            get { return _sdp.CommonVulnerabilitiesIds; }
        }

        internal System.Collections.Specialized.StringCollection Languages
        {
            get 
            {
                if (_sdp.InstallableItems != null && _sdp.InstallableItems.Count != 0)
                {
                    return _sdp.InstallableItems[0].Languages;
                }

                return new System.Collections.Specialized.StringCollection(); 
            }
        }

        internal DateTime CreationDate { get { return _sdp.CreationDate; } }
        
        #endregion (Internal Properties - PRopriétés Internes)

        #region(Private Methods - Méthodes privées)

        private string GetString(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
                return inputString;
            return string.Empty;
        }

        private string GetString(System.Collections.Specialized.StringCollection inputString)
        {
            if (inputString != null && inputString.Count != 0 && !string.IsNullOrEmpty(inputString[0]))
                return inputString[0];
            return string.Empty;
        }

        private string GetString(Uri inputString)
        {
            if (inputString != null && !string.IsNullOrEmpty(inputString.ToString()))
                return inputString.ToString();
            return string.Empty;
        }

        #endregion(Private Methods - Méthodes privées)
    }
}
