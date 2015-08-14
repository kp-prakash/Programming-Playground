using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap2.Service
{
    public class ProductRepository : IProductRepository
    {
        public IList<Product> GetAllProductsIn(int categoryId)
        {
            IList<Product> products = new List<Product>();
            var product1 = new Product {Name = "Product1", CategoryId = 1};
            var product2 = new Product { Name = "Product2", CategoryId = 2};
            var product3 = new Product {Name = "Product3", CategoryId = 3};
            var product4 = new Product { Name = "Product4", CategoryId = 1 };
            var product5 = new Product { Name = "Product5", CategoryId = 2 };
            var product6 = new Product { Name = "Product6", CategoryId = 3 };
            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);
            products.Add(product5);
            products.Add(product6);
            return products;
        }
    }
}
