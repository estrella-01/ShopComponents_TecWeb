using ShopComponents.Core.Interfaces;

namespace ShopComponents.Core.Interfaces;

public interface IUnitOfWork
{
    IVentaRepository Ventas { get; }
    IFacturaRepository Facturas { get; }
    IInventarioRepository Inventarios { get; }
    IProformaRepository Proformas { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}