using Microsoft.AspNetCore.Mvc;
using WebAppGB.Data;
using WebAppGB.Models;

namespace WebAppGeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> AddProductGroup(string name, string description)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                if (storageContext.ProductGroup.Any(p => p.Name == name))
                    return StatusCode(409);

                var productGroup = new ProductGroup() { Name = name, Description = description };
                storageContext.ProductGroup.Add(productGroup);
                storageContext.SaveChanges();
                return Ok($"Добавлена группа с ID = {productGroup.Id}");
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductGroup>> GetAllProductGroups()
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var list = storageContext.ProductGroup.ToList();
                return Ok(list);
            }
        }
        [HttpDelete]
        public ActionResult DeleteProductGroup(int id)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var productGroup = storageContext.ProductGroup.FirstOrDefault(p => p.Id == id);
                if (productGroup != null)
                {
                    storageContext.ProductGroup.Remove(productGroup);
                    storageContext.SaveChanges();
                    return Ok();
                }
                return StatusCode(404, "Нет группы с таким ID");
            }
        }
    }
}
