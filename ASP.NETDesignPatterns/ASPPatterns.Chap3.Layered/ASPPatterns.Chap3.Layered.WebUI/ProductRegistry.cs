using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASPPatterns.Chap3.Layered.Model;
using StructureMap.Configuration.DSL;
using ASPPatterns.Chap3.Layered.Repository;

namespace ASPPatterns.Chap3.Layered.WebUI
{
    public class ProductRegistry:Registry
    {
        public ProductRegistry()
        {
            For<IProductRepository>().Use<ProductRepository>();
        }
    }
}
