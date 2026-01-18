using AutoMapper;
using GeekShopping.API.Data.Dto;
using GeekShopping.API.Model;

namespace GeekShopping.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
