using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppGB.Data;
using WebAppGB.Models;

namespace WebAppGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> AddProduct(string name, string description, decimal price)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                if (storageContext.Products.Any(p => p.Name == name))
                    return StatusCode(409);

                var product = new Product() { Name = name, Description = description, Price = price };
                storageContext.Products.Add(product);
                storageContext.SaveChanges();
                return Ok(product.Id);
            }
        }
        [HttpGet("get_all_product")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> list;
            using (StorageContext storageContext = new StorageContext())
            {
                list = storageContext.Products.Select(p => new Product { Name = p.Name, Description = p.Description, Price = p.Price }).ToList();
                return Ok(list);
            }
        }
        [HttpDelete("delete_group")]
        public ActionResult DeleteGroup(int groupId)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var group = storageContext.ProductGroup.Find(groupId);
                if (group == null)
                    return NotFound();

                storageContext.ProductGroup.Remove(group);
                storageContext.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("delete_product/{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var product = storageContext.Products.Find(productId);
                if (product == null)
                    return NotFound();

                storageContext.Products.Remove(product);
                storageContext.SaveChanges();
                return Ok();
            }
        }

        [HttpPut("update_price/{productId}")]
        public ActionResult UpdatePrice(int productId, decimal newPrice)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var product = storageContext.Products.Find(productId);
                if (product == null)
                    return NotFound();

                product.Price = newPrice;
                storageContext.SaveChanges();
                return Ok();
            }
        }
    }
}
