using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Mappings;

public class ShopProfile : Profile
{
    public ShopProfile()
    {
        CreateMap<Inventario, InventarioDto>().ReverseMap();
        CreateMap<Producto, ProductoDto>().ReverseMap();
        CreateMap<Ventum, VentaDto>().ReverseMap();
        CreateMap<Factura, FacturaDto>().ReverseMap();
        CreateMap<Proforma, ProformaDto>().ReverseMap();
        CreateMap<Detalleproforma, DetalleProformaDto>().ReverseMap();
    }
}