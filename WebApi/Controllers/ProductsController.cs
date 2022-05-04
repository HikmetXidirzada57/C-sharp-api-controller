using Business.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductsController>
        //[HttpGet("{lang}")]
        //public IEnumerable<Product> Get(string? lang)
        //{
        //    return  _productService.GetProducts(lang);
        //}
        // GET: api/<ProductsController>
        [HttpGet("{lang}/{searchTerm}")]
        public  JsonResult Get(string? lang,string? searchTerm)
        {
            var result = new JsonResult(new { });

            if (string.IsNullOrEmpty(lang)) return result ; 
            result.Value = new
            {
                succes = true,
                products = _productService.SearchProducts(searchTerm: searchTerm, langKey: lang)
        };
            return result;
        }
        // GET api/<ProductsController>/5
        //[HttpGet("{id}/{lang}")]
        //public Product Get(int? id,string lang)
        //{
        //    return _productService.GetById(id,lang);
        //}

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] ProductDTO product)
        {
            _productService.Add(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] ProductDTO product, int id)
        {
            _productService.Update(product, id);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
