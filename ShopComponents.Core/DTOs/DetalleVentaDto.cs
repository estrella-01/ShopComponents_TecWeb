using System;
using System.Collections.Generic;
using System.Text;

namespace ShopComponents.Core.DTOs;

public class DetalleVentaDto
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}