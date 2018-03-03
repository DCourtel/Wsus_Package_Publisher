using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class CatalogItem
    {
        internal enum CatalogItemTypes
        {
            Root,
            Vendor,
            Product,
            Update
        }

        internal CatalogItem()
        {
            ItemType = CatalogItemTypes.Root;
        }

        internal CatalogItem(CatalogVendor vendor)
        {
            ItemType = CatalogItemTypes.Vendor;
            Vendor = vendor;
        }

        internal CatalogItem(CatalogProduct product)
        {
            ItemType = CatalogItemTypes.Product;
            Product = product;
        }

        internal CatalogItem(CatalogUpdate update)
        {
            ItemType = CatalogItemTypes.Update;
            Update = update;
        }

        internal CatalogItemTypes ItemType { get; private set; }
        internal CatalogVendor Vendor { get; private set; }
        internal CatalogProduct Product { get; private set; }
        internal CatalogUpdate Update { get; private set; }

        public override string ToString()
        {
            return ItemType.ToString();
        }
    }
}
