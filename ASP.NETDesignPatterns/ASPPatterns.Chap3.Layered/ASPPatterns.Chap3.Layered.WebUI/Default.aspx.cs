using System;
using System.Collections.Generic;
using ASPPatterns.Chap3.Layered.Model;
using ASPPatterns.Chap3.Layered.Repository;
using ASPPatterns.Chap3.Layered.Presentation;
using ASPPatterns.Chap3.Layered.Service;
using StructureMap;
using System.Web.UI;
namespace ASPPatterns.Chap3.Layered.WebUI
{
    public partial class Default : Page, IProductListView
    {
        private ProductListPresenter _presenter;

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new ProductListPresenter(this, ObjectFactory.GetInstance<Service.ProductService>());
            ddlCustomerType.SelectedIndexChanged += DdlCustomerTypeSelectedIndexChanged;
        }

        void DdlCustomerTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter.Display();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
                _presenter.Display();
        }

        public void Display(IList<ProductViewModel> products)
        {
            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        public CustomerType CustomerType
        {
            get
            {
                return (CustomerType)Enum.ToObject(typeof(CustomerType),
                    int.Parse(this.ddlCustomerType.SelectedValue));
            }
        }

        public string ErrorMessage
        {
            set
            {
                lblErrorMessage.Text =
                    String.Format("<p><strong>Error</strong><br/>{0}<p/>", value);
            }
        }
    }
}