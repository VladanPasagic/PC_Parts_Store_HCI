using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Queries;

public interface IGetAllReceiptsQuery
{
    Task<IEnumerable<Receipt>> ExecuteAsync();
}
