using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                LanguageKey=x.LanguageKey              
            }));
            using T110Context context= new();
            context.Add(product);
            context.SaveChanges();
        }

        public List<Product> GetAllWithInclude(Expression<Func<Product, bool>> filters, string lang)
        {

            using T110Context context = new();

             var products = context.Products.Where(x=>!x.IsDeleted).Where(x=>!x.IsDeleted).
                Include(x=>x.ProductRecords.Where(x => x.LanguageKey == lang)).
                Include(x=>x.Category).
                ThenInclude(x=>x.CategoryRecords).AsQueryable();
            if(filters != null)
            {
                products = products.Where(filters);
            }
            return products.ToList();
        }

        public Product GetByIdWithInclude(Expression<Func<Product, bool>>? filters, string lang)
        {
            using T110Context context = new();
            var product = context.Products.
              Where(x => !x.IsDeleted).
              Include(x => x.ProductRecords.Where(x=>x.LanguageKey==lang)).
              Include(x => x.Category).
              ThenInclude(x => x.CategoryRecords).
              FirstOrDefault(filters);

           
            return product;

        }

        public List<Product> SearchProducts( string? searchTerm,string? langKey)
        {
            using T110Context context = new();
            var products = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products
                    .Include(p=>p.ProductRecords).
                    Where(p => p.ProductRecords.
                    Any(pr => pr.Name.ToUpper().Contains(searchTerm.ToUpper()) && pr.LanguageKey == langKey));
            }

            //if (categoryId.HasValue)
            //{
            //    products = products.Where(c => c.CategoryId == categoryId);
            //}
            //if (minPrice.HasValue && maxPrice.HasValue)
            //{

            //    products = products.Where(c => (c.Discount != null && c.Discount > 0) ?
            //    (c.Discount >= minPrice && c.Discount <= maxPrice) :
            //    (c.Price >= minPrice && c.Price <= maxPrice)
            //    );
            //}
            return products.ToList();

        }
    }
}
