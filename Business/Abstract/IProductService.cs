using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetById(int? id);
        void Add(ProductDTO product);
        void Update(ProductDTO product,int id);
        void Delete(int? id);
    }

}
