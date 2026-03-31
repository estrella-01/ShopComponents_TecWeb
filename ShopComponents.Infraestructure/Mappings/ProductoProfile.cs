using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infrastructure.Mappings;

public class ProductoProfile : Profile
{
    public ProductoProfile()
    {
        CreateMap<Producto, ProductoDto>();
        CreateMap<ProductoDto, Producto>();
    }
}