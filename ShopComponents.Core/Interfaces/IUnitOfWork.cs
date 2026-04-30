namespace ShopComponents.Core.Interfaces;

public interface IUnitOfWork
{
    IVentaRepository Ventas { get; }
    IFacturaRepository Facturas { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}