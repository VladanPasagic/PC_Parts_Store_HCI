using Microsoft.EntityFrameworkCore;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework.Queries;

public class GetAllManufacturersQuery : IGetAllManufacturersQuery
{
    private readonly PcStoreContextFactory _contextFactory;

    public GetAllManufacturersQuery(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Manufacturer>> Execute()
    {
        using var context = _contextFactory.Create();

        IEnumerable<ManufacturerDTO> manufacturerDTOs = await context.Manufacturers.ToListAsync();

        return manufacturerDTOs.Select(m =>
            new Manufacturer()
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                Country = m.Country,
            });

    }
}
