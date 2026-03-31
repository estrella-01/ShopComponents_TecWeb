using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;
using ShopComponents.Infraestructure.Data;


namespace ShopComponents.Services.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;
    private readonly SistemaDbContext _context;

    public ProductoService(IProductoRepository repository, SistemaDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task InsertAsync(Producto producto)
    {
        await _repository.InsertAsync(producto);
    }

    public async Task<Producto> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Producto producto)
    {
        await _repository.UpdateAsync(producto);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<object> CrearProforma(int productoId, int cantidad)
    {
        var producto = await _repository.GetByIdAsync(productoId);

        if (producto == null)
            throw new Exception("Producto no existe");

        if (cantidad <= 0)
            throw new Exception("Cantidad inválida");

        var total = producto.Precio * cantidad;

        var proforma = new Proforma
        {
            Fecha = DateTime.Now,
            Total = total
        };

        _context.Proformas.Add(proforma);
        await _context.SaveChangesAsync();

        return new
        {
            ProformaId = proforma.Id,
            Producto = producto.Nombre,
            Cantidad = cantidad,
            Total = total
        };
    }

}

