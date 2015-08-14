using System.Collections.Generic;
using System.Web;

namespace ASPPatterns.Chap2.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheStorage _cacheStorage;

        public ProductService(IProductRepository productRepository, ICacheStorage cacheStorage)
        {
            _productRepository = productRepository;
            _cacheStorage = cacheStorage;
        }

        public IList<Product> GetAllProductsIn(int categoryId)
        {
            var storageKey = string.Format("products_in_category_id_{0}", categoryId);
            IList<Product> products = _cacheStorage.Retrieve<List<Product>>(storageKey);
            if (products == null)
            {
                products = _productRepository.GetAllProductsIn(categoryId);
               _cacheStorage.Store(storageKey, products);
            }
            return products;
        }
    }
}