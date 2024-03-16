using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using System.Diagnostics;

namespace PCPartsStore.EntityFramework.Commands;

public class DeactivateSellerCommand : IDeactivateSellerCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public DeactivateSellerCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Seller seller)
    {
        using var context = _contextFactory.Create();
        var sellerDTO = context.Sellers.FirstOrDefault(s => s.SellerId == seller.SellerId);
        Trace.WriteLine(sellerDTO);
        if (sellerDTO == null)
        {
            return;
        }

        sellerDTO.IsActive = false;
        context.Sellers.Update(sellerDTO);
        await context.SaveChangesAsync();
    }
}
