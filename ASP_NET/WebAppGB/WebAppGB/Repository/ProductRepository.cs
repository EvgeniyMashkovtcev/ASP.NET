﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebAppGB.Abstraction;
using WebAppGB.Data;
using WebAppGB.Dto;
using WebAppGB.Models;

namespace WebAppGB.Repository
{
    public class ProductRepository(StorageContext storageContext, IMapper _mapper, IMemoryCache _memoryCache) : IProductRepository
    {
        public int AddProduct(ProductDto productDto)
        {
            if (storageContext.Products.Any(p => p.Name == productDto.Name))
                throw new Exception("Есть продукт с таким именем");

            var entity = _mapper.Map<Product>(productDto);
            storageContext.Products.Add(entity);
            storageContext.SaveChanges();
            _memoryCache.Remove("products");
            return entity.Id;
        }

        public void DeleteProduct(int id)
        {
            var product = storageContext.Products.Find(id);
            if (product == null)
                throw new Exception("Продукт не найден");

            storageContext.Products.Remove(product);
            storageContext.SaveChanges();
            _memoryCache.Remove("products");
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductDto> listDto)) return listDto;
            listDto = storageContext.Products.Select(_mapper.Map<ProductDto>).ToList();
            _memoryCache.Set("products", listDto, TimeSpan.FromMinutes(30));
            return listDto;
        }
    }
}
