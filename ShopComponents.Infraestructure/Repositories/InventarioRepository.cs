using Dapper;
using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Infraestructure.Data;
using System.Text;

namespace ShopComponents.Infraestructure.Repositories;

public class InventarioRepository : IInventarioRepository
{
    private readonly SistemaDbContext _context;
    private readonly IDapperContext _dapperContext;

    public InventarioRepository(SistemaDbContext context, IDapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<Inventario>> GetAllAsync(InventarioFilter? filter = null)
    {
        using var cn = _dapperContext.CreateConnection();

        if (filter is null)
            return await cn.QueryAsync<Inventario>("SELECT * FROM inventarios");

        var sql = new StringBuilder("SELECT * FROM inventarios WHERE 1=1");

        if (filter.ProductoId.HasValue)
            sql.Append(" AND ProductoId = @ProductoId");
        if (!string.IsNullOrEmpty(filter.TipoMovimiento))
            sql.Append(" AND TipoMovimiento = @TipoMovimiento");
        if (filter.FechaDesde.HasValue)
            sql.Append(" AND Fecha >= @FechaDesde");
        if (filter.FechaHasta.HasValue)
            sql.Append(" AND Fecha <= @FechaHasta");

        sql.Append(" ORDER BY Id DESC");
        return await cn.QueryAsync<Inventario>(sql.ToString(), filter);
    }

    public async Task<IEnumerable<Inventario>> GetByProductoIdAsync(int productoId)
        => await _context.Inventarios
            .Where(i => i.ProductoId == productoId)
            .ToListAsync();

    public async Task InsertAsync(Inventario inventario)
        => await _context.Inventarios.AddAsync(inventario);
}