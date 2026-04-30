using ShopComponents.Core.Entities;

public interface IProformaRepository
{
    Task InsertAsync(Proforma proforma);
}