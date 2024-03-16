using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface ICreateManufacturerCommand
{
    Task<bool> ExecuteAsync(Manufacturer manufacturer);
}
