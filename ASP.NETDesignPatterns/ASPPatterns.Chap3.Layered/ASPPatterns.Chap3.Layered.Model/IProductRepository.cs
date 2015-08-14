using System.Collections.Generic;

namespace ASPPatterns.Chap3.Layered.Model
{
    public interface IProductRepository
    {
        IList<Product> FindAll();
    }
}
