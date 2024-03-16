using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Queries;

public interface IGetAllManufacturersQuery
{
    Task<IEnumerable<Manufacturer>> Execute();
}
