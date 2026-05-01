using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SistemaDbContext _context;
    private IDbContextTransaction? _transaction;

    public IVentaRepository Ventas { get; }
    public IFacturaRepository Facturas { get; }
    public IInventarioRepository Inventarios { get; }
    public IProformaRepository Proformas { get; }

    public UnitOfWork(
        SistemaDbContext context,
        IVentaRepository ventas,
        IFacturaRepository facturas,
        IInventarioRepository inventarios,
        IProformaRepository proformas)
    {
        _context = context;
        Ventas = ventas;
        Facturas = facturas;
        Inventarios = inventarios;
        Proformas = proformas;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
        => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitAsync()
    {
        await _transaction!.CommitAsync();
        await _transaction.DisposeAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction!.RollbackAsync();
        await _transaction.DisposeAsync();
    }
}