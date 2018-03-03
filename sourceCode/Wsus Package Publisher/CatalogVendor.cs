using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    class CatalogVendor
    {
        private Dictionary<string, CatalogProduct> _products = new Dictionary<string, CatalogProduct>();
        

        internal CatalogVendor(string vendorName)
        {
            VendorName = vendorName;
        }

        #region (Internal Properties - PRopriétés Internes)

        internal Dictionary<string, CatalogProduct> Products
        {
            get { return _products; }
        }

        internal string VendorName
        {
            get;
            set;
        }

        #endregion (Internal Properties - PRopriétés Internes)

        #region (Internal Methods - Méthodes Internes)

        internal void AddProduct(CatalogProduct productToAdd)
        {
            _products.Add(productToAdd.ProductName, productToAdd);
        }

        #endregion (Internal Methods - Méthodes Internes)

        #region (Public Methods - Méthodes Publique)

        public override string ToString()
        {
            return VendorName;
        }

        #endregion (Public Methods - Méthodes Publique)

    }
}
