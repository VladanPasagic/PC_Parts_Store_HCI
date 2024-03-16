using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface ILoginSellerCommand
{
    (bool success, string errorMsg, Seller? seller) Execute(Seller seller);
}
