using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface IDeactivateSellerCommand
{
    Task ExecuteAsync(Seller seller);
}
