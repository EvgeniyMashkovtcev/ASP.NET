using WebAppGB.Dto;
using WebAppGB.Models;

namespace WebAppGB.Abstraction
{
    public interface IProductRepository
    {
        IEnumerable<ProductDto> GetAllProducts();
        int AddProduct(ProductDto productDto);
        void DeleteProduct(int id);
        void UpdateProductPrice(int productId, decimal newPrice);
    }
}
