using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SistemaDbContext _context;

    public IVentaRepository Ventas { get; }
    public IFacturaRepository Facturas { get; }

    public UnitOfWork(
        SistemaDbContext context,
        IVentaRepository ventas,
        IFacturaRepository facturas)
    {
        _context = context;
        Ventas = ventas;
        Facturas = facturas;
    }

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    public Task BeginTransactionAsync()
        => _context.Database.BeginTransactionAsync();

    public Task CommitAsync()
        => _context.Database.CommitTransactionAsync();

    public Task RollbackAsync()
        => _context.Database.RollbackTransactionAsync();
}