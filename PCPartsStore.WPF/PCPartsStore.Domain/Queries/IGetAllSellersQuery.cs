using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Queries;

public interface IGetAllSellersQuery
{
    Task<IEnumerable<Seller>> Execute();
}
