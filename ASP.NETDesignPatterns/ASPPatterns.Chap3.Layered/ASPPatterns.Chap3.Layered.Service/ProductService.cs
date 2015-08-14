using System;

namespace ASPPatterns.Chap3.Layered.Service
{
    public class ProductService
    {
        private readonly Model.ProductService _productService;

        public ProductService(Model.ProductService productService)
        {
            _productService = productService;
        }

        public ProductListResponse GetAllProductsFor(ProductListRequest productListRequest)
        {
            var productListResponse = new ProductListResponse();
            try
            {
                var products = _productService.GetAllProductsFor(productListRequest.CustomerType);
                productListResponse.Products = products.ConvertToProductListViewModel();
                productListResponse.Success = true;
            }
            catch (Exception exception)
            {
                //TODO LOG EXCEPTION
                productListResponse.Success = false;
                productListResponse.Message = "An error occured!";
            }
            return productListResponse;
        }
    }
}
