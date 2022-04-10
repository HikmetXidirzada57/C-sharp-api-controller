﻿using Business.Abstract;
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
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productService.GetProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int? id)
        {
            return _productService.GetById(id);
        }

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
