using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        IProductDal _dal;

        public ProductManager(IProductDal dal)
        {
            _dal = dal;
        }

        //public List<Product> SearchProducts(int? categoryId,decimal? minPrice,decimal? maxPrice)
        //{
        //    return _dal.SearchProducts(categoryId,minPrice,maxPrice);
        //}
        
        public List<Product> GetProducts()
        {
            return _dal.GetAllWithInclude();
        }
        public Product? GetById(int? id)
        {
            if (id == null) return null;
           return _dal.Get(c=>c.Id == id);
           
        }

        public void Add(ProductDTO product)
        {
            _dal.AddProductWithLang(product); 
        }

        public void Update(ProductDTO product,int id)
        {
          Product selectedProduct= _dal.Get(c=>c.Id==id);
            selectedProduct.Price= product.Price;
            selectedProduct.Discount= product.Discount;
            selectedProduct.CategoryId= product.CategoryId;
            _dal.Update(selectedProduct);
        }

        public void Delete(int? id)
        {
            if (id == null) return;
            var product = _dal.Get(c => c.Id == id);
            product.IsDeleted = true;
            _dal.Update(product);
        }

    }
}
