using ASPPatterns.Chap3.Layered.Service;

namespace ASPPatterns.Chap3.Layered.Presentation
{
    public class ProductListPresenter
    {
        private readonly IProductListView _productListView;
        private readonly ProductService _productService;

        public ProductListPresenter(IProductListView productListView, ProductService productService)
        {
            _productListView = productListView;
            _productService = productService;
        }

        public void Display()
        {
            var productListRequest = new ProductListRequest {CustomerType = _productListView.CustomerType};
            var productListResponse = _productService.GetAllProductsFor(productListRequest);
            if (productListResponse.Success)
                _productListView.Display(productListResponse.Products);
            else
                _productListView.ErrorMessage = productListResponse.Message;
        }
    }
}