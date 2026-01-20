using GeekShopping.API.Data.Dto;

namespace GeekShopping.API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> FindAll();
        Task<ProductDto> FindById(long id);
        Task<ProductDto> FindByName(string name);
        Task<ProductDto> Create(ProductDto productDto);
        Task<ProductDto> Update(ProductDto productDto);
        Task<bool> Delete(long id);

    }
}
