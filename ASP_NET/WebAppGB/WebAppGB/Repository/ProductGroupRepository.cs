using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebAppGB.Abstraction;
using WebAppGB.Data;
using WebAppGB.Dto;
using WebAppGB.Models;

namespace WebAppGB.Repository
{
    public class ProductGroupRepository(StorageContext storageContext, IMapper _mapper, IMemoryCache _memoryCache) : IProductGroupRepository
    {
        public int AddProductGroup(ProductGroupDto productGroupDto)
        {
            if (storageContext.ProductGroup.Any(p => p.Name == productGroupDto.Name))
                throw new Exception("Есть продукт с таким именем");

            var entity = _mapper.Map<ProductGroup>(productGroupDto);
            storageContext.ProductGroup.Add(entity);
            storageContext.SaveChanges();
            _memoryCache.Remove("products");
            return entity.Id;
        }

        public void DeleteProductGroup(int id)
        {
            var productGroup = storageContext.ProductGroup.Find(id);
            if (productGroup == null)
                throw new Exception("Группа продуктов не найдена");

            storageContext.ProductGroup.Remove(productGroup);
            storageContext.SaveChanges();
        }

        public IEnumerable<ProductGroupDto> GetAllProductsGroups()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductGroupDto> listDto)) return listDto;
            listDto = storageContext.Products.Select(_mapper.Map<ProductGroupDto>).ToList();
            _memoryCache.Set("products", listDto, TimeSpan.FromMinutes(30));
            return listDto;
        }
    }
}
