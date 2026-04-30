using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Mappings;

public class ShopProfile : Profile
{
    public ShopProfile()
    {
        CreateMap<Inventario, InventarioDto>().ReverseMap();

        // (ya deberías tener estos)
        CreateMap<Producto, ProductoDto>().ReverseMap();
        CreateMap<Ventum, VentaDto>().ReverseMap();
        CreateMap<Factura, FacturaDto>().ReverseMap();
    }
}