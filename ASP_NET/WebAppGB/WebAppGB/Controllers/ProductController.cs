using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using WebAppGB.Abstraction;
using WebAppGB.Data;
using WebAppGB.Dto;
using WebAppGB.Models;
using WebAppGB.Repository;

namespace WebAppGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public ActionResult<int> AddProduct(ProductDto productDto)
        {
            try
            {
                var id = _productRepository.AddProduct(productDto);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(409);
            }
        }
        [HttpGet("get_all_product")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_productRepository.GetAllProducts());
            
        }
        [HttpDelete("delete_group/{groupId}")]
        public ActionResult DeleteGroup(int groupId)
        {
            try
            {
                _productRepository.DeleteProduct(groupId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("delete_product/{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            try
            {
                _productRepository.DeleteProduct(productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("update_price/{productId}")]
        public ActionResult UpdatePrice(int productId, decimal newPrice)
        {
            try
            {
                _productRepository.UpdateProductPrice(productId, newPrice);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("export_csv")]
        public IActionResult ExportProductsToCsv()
        {
            var products = _productRepository.GetAllProducts();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("ProductGroupId, ProductName, ProductPrice, Description");

            foreach (var product in products)
            {
                csvBuilder.AppendLine($"{product.ProductGroupId},{product.Name},{product.Price},{product.Description}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var csvFileName = "products.csv";
            return File(buffer, "text/csv", csvFileName);
        }
    }

}
