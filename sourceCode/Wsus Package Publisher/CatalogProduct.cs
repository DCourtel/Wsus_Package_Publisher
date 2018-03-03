using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class CatalogProduct
    {
        private List<CatalogUpdate> _updates = new List<CatalogUpdate>();

        internal CatalogProduct(string productName)
        {
            ProductName = productName;
        }

        #region (Internal Properties - PRopriétés Internes)

        internal List<CatalogUpdate> Updates
        {
            get { return _updates; }
        }

        internal string ProductName { get; set; }

        #endregion (Internal Properties - PRopriétés Internes)

        #region (Internal Methods - Méthodes Internes)

        internal void AddUpdate(CatalogUpdate updateToAdd)
        {
            Updates.Add(updateToAdd);
        }

        #endregion (Internal Methods - Méthodes Internes)
                
        #region (Public Methods - Méthodes Publique)

        public override string ToString()
        {
            return ProductName;
        }

        #endregion (Public Methods - Méthodes Publique)

    }
}