using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Queries;

public interface IGetAllCategoriesQuery
{
    Task<IEnumerable<Category>> ExecuteAsync();
}
