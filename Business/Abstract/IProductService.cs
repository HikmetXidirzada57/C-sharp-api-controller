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
        List<Product> GetProducts( string? lang);
        List<Product> SearchProducts(string? searchTerm, string? langKey);
        Product GetById(int? id,string? lang);
        void Add(ProductDTO product);
        void Update(ProductDTO product,int id);
        void Delete(int? id);
    }

}
