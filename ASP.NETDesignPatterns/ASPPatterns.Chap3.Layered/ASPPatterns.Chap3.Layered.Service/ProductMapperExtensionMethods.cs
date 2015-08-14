using System.Collections.Generic;
using System.Linq;
using ASPPatterns.Chap3.Layered.Model;

namespace ASPPatterns.Chap3.Layered.Service
{
    public static class ProductMapperExtensionMethods
    {
        public static IList<ProductViewModel> ConvertToProductListViewModel(this IList<Product> products)
        {
            return products.Select(product => product.ConvertToProductViewModel()).ToList();
        }

        public static ProductViewModel ConvertToProductViewModel(this Product product)
        {
            var productViewModel = new ProductViewModel
                                       {
                                           Name = product.Name,
                                           ProductId = product.Id,
                                           RRP = string.Format("{0:C}", product.Price.RecommendedRetailPrice),
                                           SellingPrice = string.Format("{0:C}", product.Price.SellingPrice)
                                       };
            if (product.Price.Discount > 0)
                productViewModel.Discount = string.Format("{0:C}", product.Price.Discount);
            if (product.Price.Savings < 1 && product.Price.Savings > 0)
                productViewModel.Savings = product.Price.Savings.ToString("#%");
            return productViewModel;
        }
    }
}
