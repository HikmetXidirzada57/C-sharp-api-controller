using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EFEntityRepositoryBase<Product, T110Context>, IProductDal
    {
        public void AddProductWithLang(ProductDTO productDTO)
        {
            Product product = new()
            {
                Price = productDTO.Price,
                Discount = productDTO.Discount,
                CategoryId = productDTO.CategoryId,
                ProductRecords = new List<ProductRecord>(),
            };
            product.ProductRecords.AddRange(productDTO.ProductRecords.Select(x => new ProductRecord()
            {
                Description = x.Description,
                Name = x.Name,
                LanguageId = x.LanguageId,
            }));
            using T110Context context= new();
            context.Add(product);
            context.SaveChanges();
        }

        public List<Product> GetAllWithInclude()
        {
            using T110Context context = new();
           return context.Products.Where(x=>!x.IsDeleted).Include(x=>x.ProductRecords).Include(x=>x.Category).ThenInclude(x=>x.CategoryRecords).ToList();
             
        }

        //public List<Product> GetAllWithLang()
        //{
        //    using T110Context context = new();
        //    return context.Products.Where(x => !x.IsDeleted).Include(x => x.ProductRecords).Include(x => x.Category).ThenInclude(x => x.CategoryRecords).ToList();
        //}

        public List<Product> SearchProducts(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            using T110Context context = new();
            var products = context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(c => c.CategoryId == categoryId);
            }
            if (minPrice.HasValue && maxPrice.HasValue)
            {

                products = products.Where(c => (c.Discount != null && c.Discount > 0) ?
                (c.Discount >= minPrice && c.Discount <= maxPrice) :
                (c.Price >= minPrice && c.Price <= maxPrice)
                );
            }
            return products.ToList();

        }
    }
}
