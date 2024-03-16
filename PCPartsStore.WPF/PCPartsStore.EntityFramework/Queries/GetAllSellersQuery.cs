using Microsoft.EntityFrameworkCore;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework.Queries;

public class GetAllSellersQuery : IGetAllSellersQuery
{
    private readonly PcStoreContextFactory _contextFactory;

    public GetAllSellersQuery(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Seller>> Execute()
    {
        using var context = _contextFactory.Create();

        IEnumerable<SellerDTO> sellerDTOs = await context.Sellers.Where(s => s.IsAdmin == false).ToListAsync();

        return sellerDTOs.Select(m => new Seller()
        {
            SellerId = m.SellerId,
            Username = m.Username,
            IsActive = m.IsActive,
        });
    }
}
