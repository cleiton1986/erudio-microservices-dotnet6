using AutoMapper;
using GeeKShooping.Infra;
using GeekShopping.API.Data.Dto;
using GeekShopping.API.Model;
using GeekShopping.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.API.Repository
{
    public class ProductRepository : Notification, IProductRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;
        public ProductRepository(
            MySQLContext context,
            IMapper mapper 
            )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> FindAll()
        {
            var productsList =  await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productsList);
        }

        public async Task<ProductDto> FindByName(string name)
        {

            var productDto = new ProductDto();

            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    Notify("name inválido", "FindByName");
                    return productDto;
                }

                var productsList = await _context.Products.ToListAsync();

                var products = await _context.Products
                                     .Where(p => p.Name.ToUpper().Contains(name.ToUpper()))
                                     .FirstOrDefaultAsync() ?? new Product();
                productDto = _mapper.Map<ProductDto>(products);
            }
            catch (Exception ex)
            {

                Notification.Notify($"Erro ao buscar Por nome. : {ex.Message} ", "FindByName");
            }


            return productDto;
        }

        public async Task<ProductDto> FindById(long id)
        {
         
            var productDto = new ProductDto();

            try
            {
                if (id <= 0)
                {
                    Notify("Id inválido", "FindById");
                    return productDto;
                }

                var products = await _context.Products
                                     .Where(p => p.Id == id)
                                     .FirstOrDefaultAsync() ?? new Product();
                productDto =  _mapper.Map<ProductDto>(products);
            }
            catch (Exception ex)
            {

                Notification.Notify($"Erro ao buscar Por Id. : {ex.Message} ", "FindById");
            }
           

            return productDto;
        }
        public async Task<ProductDto> Create(ProductDto productDto)
        {
   
            try
            {
                var product = _mapper.Map<Product>(productDto);

                if (product.Validate())
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    productDto = _mapper.Map<ProductDto>(product);
                }
              
            }
            catch (Exception ex)
            {

                Notify($"Erro ao criar Produto. : {ex.Message} ", "Create");
            }

            return productDto;
        }
        public async Task<ProductDto> Update(ProductDto productDto)
        {

            try
            {
                var product = _mapper.Map<Product>(productDto);
                if (product.Validate()) { 
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                    productDto = _mapper.Map<ProductDto>(product);
                }

            }
            catch (Exception ex)
            {

                Notify($"Erro ao atualizar Produto. : {ex.Message} ", "Update");
            }

            return productDto;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var products = await _context.Products
                                             .Where(p => p.Id == id)
                                             .FirstOrDefaultAsync() ?? new Product();

                if (products.Id <= 0) return false;

                _context.Products.Remove(products);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Notify($"Erro ao deletar Produto. :{ex.Message} ", "Delete");

            }

            return false;
        }



    }
}
