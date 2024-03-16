using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using System.Diagnostics;

namespace PCPartsStore.EntityFramework.Commands;

public class CreateReceiptCommand : ICreateReceiptCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public CreateReceiptCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Receipt receipt)
    {
        using var context = _contextFactory.Create();

        try
        {
            context.Receipts.Add(new DTOs.ReceiptDTO()
            {
                TotalPrice = receipt.TotalPrice,
                Items = receipt.Items.Select(i => new DTOs.ItemDTO()
                {
                    ArticleId = i.ArticleId,
                    Price = i.Price,
                    Quantity = i.Quantity,
                }).ToList(),
                SellerId = receipt.SellerId,
            });

            foreach (var item in receipt.Items)
            {
                var article = context.Articles.FirstOrDefault(a => a.ArticleId == item.ArticleId);
                article!.Quantity -= item.Quantity;
                context.Articles.Update(article);
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.StackTrace + ex.Message);
        }
    }
}
