using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface IRegisterSellerCommand
{
    public Task<(bool, string)> ExecuteAsync(Seller seller);
}
