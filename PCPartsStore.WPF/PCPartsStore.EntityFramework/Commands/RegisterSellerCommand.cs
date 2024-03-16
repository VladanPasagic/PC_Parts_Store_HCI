using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework.Commands;

public class RegisterSellerCommand : IRegisterSellerCommand
{
    private const string SuccessfullAccountCreation = "SuccessfulAccountCreation";
    private const string UsernameAlreadyExists = "UsernameAlreadyExists";
    private readonly PcStoreContextFactory _contextFactory;

    public RegisterSellerCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<(bool, string)> ExecuteAsync(Seller seller)
    {
        using var context = _contextFactory.Create();

        SellerDTO? sellerDTO = context.Sellers.FirstOrDefault(s => s.Username == seller.Username);
        if (sellerDTO == null)
        {
            SellerDTO sellerDto = new()
            {
                Username = seller.Username,
                PasswordHash = seller.PasswordHash,
                IsAdmin = false,
                IsActive = true
            };
            context.Sellers.Add(sellerDto);

            context.Settings.Add(new SettingDTO()
            {
                Seller = sellerDto,
                Theme = 0,
                Language = 0
            });

            await context.SaveChangesAsync();
            return (true, SuccessfullAccountCreation);
        }
        else
        {
            return (false, UsernameAlreadyExists);
        }
    }
}
