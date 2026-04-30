using Dapper;
using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Infraestructure.Data;
using System.Text;

namespace ShopComponents.Infraestructure.Repositories;

public class VentaRepository : BaseRepository<Ventum>, IVentaRepository
{
    private readonly IDapperContext _dapperContext;

    public VentaRepository(SistemaDbContext context, IDapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<Ventum>> GetAllAsync(VentaFilter? filter = null)
    {
        using var cn = _dapperContext.CreateConnection();

        if (filter is null)
            return await cn.QueryAsync<Ventum>("SELECT * FROM venta");

        var sql = new StringBuilder("SELECT * FROM venta WHERE 1=1");

        if (filter.ClienteId.HasValue)
            sql.Append(" AND ClienteId = @ClienteId");
        if (filter.UsuarioId.HasValue)
            sql.Append(" AND UsuarioId = @UsuarioId");
        if (filter.FechaDesde.HasValue)
            sql.Append(" AND Fecha >= @FechaDesde");
        if (filter.FechaHasta.HasValue)
            sql.Append(" AND Fecha <= @FechaHasta");

        sql.Append(" ORDER BY Id DESC");
        return await cn.QueryAsync<Ventum>(sql.ToString(), filter);
    }

    public async Task<Ventum?> GetByIdAsync(int id)
        => await _context.Venta.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Ventum entity)
        => await _context.Venta.AddAsync(entity);

    public void Update(Ventum entity) => _context.Venta.Update(entity);
    public void Delete(Ventum entity) => _context.Venta.Remove(entity);
}