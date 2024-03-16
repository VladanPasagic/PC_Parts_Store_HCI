using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Queries;

public interface IGetAllProductsQuery
{
    Task<IEnumerable<Article>> Execute();
}
