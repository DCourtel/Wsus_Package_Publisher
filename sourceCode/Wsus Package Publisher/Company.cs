using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class Company
    {
        private string _companyName = string.Empty;
        private Guid _id = Guid.NewGuid();
        private Dictionary<Guid, Product> _products = new Dictionary<Guid, Product>();

        /// <summary>
        /// Create a new instance of Company with the name 'companyName'.
        /// </summary>
        /// <param name="companyName">Name of th Company.</param>
        internal Company(Guid id, string companyName)
        {
            CompanyName = companyName;
            ID = id;
        }

        /// <summary>
        /// Get or Set the name of the Company
        /// </summary>
        internal string CompanyName
        {
            get { return _companyName; }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _companyName = value;
            }
        }

        internal Guid ID
        {
            get { return _id; }
            private set
            {
                _id = value;
            }
        }
        
        /// <summary>
        /// Return the list of all products for this Company.
        /// </summary>
        internal Dictionary<Guid, Product> Products
        {
            get { return _products; }
        }

        /// <summary>
        /// Add a Product to this Company
        /// </summary>
        /// <param name="productName">Name of the Product, must be unique for the Company</param>
        internal void AddProduct(Guid id, string productName)
        {
            if (!string.IsNullOrEmpty(productName) && !_products.ContainsKey(id))
            {
                Product newProductInstance = new Product(id, productName, this);

                _products.Add(id, newProductInstance);
                if (ProductAdded != null)
                    ProductAdded(this, newProductInstance);
                newProductInstance.NoMoreUpdatesForThisProduct += new Product.NoMoreUpdatesForThisProductEventHandler(ProductRunOutofUpdates);
                newProductInstance.UpdateRefeshed += new Product.UpdateRefeshedEventHandler(UpdateRefeshed);
            }
        }

        private void ProductRunOutofUpdates(Product productWithoutUpdate)
        {
            RemoveProduct(productWithoutUpdate.ID);
        }

        private void UpdateRefeshed(Product product)
        {
            if (ProductRefreshed != null)
                ProductRefreshed(this, product);
        }

        /// <summary>
        /// Remove a Product in this Company.
        /// </summary>
        /// <param name="productName">Name of the Product to remove.</param>
        internal void RemoveProduct(Guid productId)
        {
            WsusWrapper _wsus = WsusWrapper.GetInstance();

            if (_products.ContainsKey(productId))
            {
                Product productToRemove = _products[productId];
                _products.Remove(productId);
                if (ProductRemoved != null)
                    ProductRemoved(this, productToRemove);
                if (Products.Count == 0 && NoMoreProductsForThisCompany != null && !_wsus.CategoryExists(this.ID))
                    NoMoreProductsForThisCompany(this);
            }
        }

        /// <summary>
        /// Get the number of Product for this Company
        /// </summary>
        /// <returns>Number of Product.</returns>
        internal int GetProductsCount()
        {
            return _products.Count;
        }

        /// <summary>
        /// Remove all Product for this Company.
        /// </summary>
        internal void ClearProductsList()
        {
            _products.Clear();
            if (NoMoreProductsForThisCompany != null)
                NoMoreProductsForThisCompany(this);
        }

        public override string ToString()
        {
            return CompanyName;
        }

        public delegate void NoMoreProductsForThisCompanyEventHandler(Company companyWithoutProducts);
        public event NoMoreProductsForThisCompanyEventHandler NoMoreProductsForThisCompany;

        public delegate void ProductAddedEventHandler(Company company, Product product);
        public event ProductAddedEventHandler ProductAdded;

        public delegate void ProductRemovedEventHandler(Company company, Product product);
        public event ProductRemovedEventHandler ProductRemoved;

        public delegate void ProductRefreshedEventHandler(Company company, Product product);
        public event ProductRefreshedEventHandler ProductRefreshed;
    }
}
