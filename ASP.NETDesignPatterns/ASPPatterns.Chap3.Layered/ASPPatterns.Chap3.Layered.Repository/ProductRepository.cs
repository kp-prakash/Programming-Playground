using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASPPatterns.Chap3.Layered.Model;

namespace ASPPatterns.Chap3.Layered.Repository
{
    public class ProductRepository:IProductRepository
    {
        public IList<Model.Product> FindAll()
        {
            var products = new ShopDataContext().Products
                .Select(prod => new Model.Product
                                    {
                                        Id = prod.ProductId,
                                        Name = prod.ProductName,
                                        Price = new Price(prod.RRP, prod.SellingPrice)
                                    });
            return products.ToList();
        }
    }
}
