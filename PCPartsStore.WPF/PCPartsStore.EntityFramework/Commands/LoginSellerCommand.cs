using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class LoginSellerCommand : ILoginSellerCommand
{
    private const string InvalidUsernamePasswordCombo = "InvalidUsernamePasswordCombo";
    private const string InactiveAccount = "InactiveAccount";
    private readonly PcStoreContextFactory _contextFactory;

    public LoginSellerCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public (bool success, string errorMsg, Seller? seller) Execute(Seller seller)
    {
        using var context = _contextFactory.Create();
        DTOs.SellerDTO? sellerDTO = context.Sellers.FirstOrDefault(s => s.Username.Equals(seller.Username));
        if (sellerDTO is null || !BCrypt.Net.BCrypt.Verify(seller.PasswordHash, sellerDTO.PasswordHash))
        {
            return (false, InvalidUsernamePasswordCombo, null);
        }
        if (sellerDTO.IsActive == false)
        {
            return (false, InactiveAccount, null);
        }

        return (true, string.Empty, new Seller()
        {
            Username = sellerDTO.Username,
            IsActive = true,
            IsAdmin = sellerDTO.IsAdmin,
            SellerId = sellerDTO.SellerId,
        });
    }
}
