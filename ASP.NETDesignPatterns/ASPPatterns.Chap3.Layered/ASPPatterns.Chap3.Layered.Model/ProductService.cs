using System.Collections.Generic;

namespace ASPPatterns.Chap3.Layered.Model
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Product> GetAllProductsFor(CustomerType customerType)
        {
            var discountStrategy = DiscountFactory.GetDiscountStrategyFor(customerType);
            var products = _productRepository.FindAll();
            products.Apply(discountStrategy);
            return products;
        }
    }
}
