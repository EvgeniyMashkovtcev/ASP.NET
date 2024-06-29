using WebAppGB.Dto;
using WebAppGB.Models;

namespace WebAppGB.Abstraction
{
    public interface IProductGroupRepository
    {
        IEnumerable<ProductGroupDto> GetAllProductsGroups();
        int AddProductGroup(ProductGroupDto productGroupDto);
        void DeleteProductGroup(int id);
    }
}
