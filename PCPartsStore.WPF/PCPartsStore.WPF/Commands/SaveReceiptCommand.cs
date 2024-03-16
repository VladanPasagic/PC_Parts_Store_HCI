using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class SaveReceiptCommand : AsyncCommandBase
{
    private const string ReceiptEmpty = "ReceiptEmpty";
    private SellerViewModel _sellerViewModel;

    public SaveReceiptCommand(SellerViewModel sellerViewModel)
    {
        _sellerViewModel = sellerViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        if (_sellerViewModel.Items.Count == 0)
        {
            _sellerViewModel.ErrorMessage = LocalizedStrings.Instance[ReceiptEmpty];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel);
            return;
        }
        else
        {
            await ReceiptStore.Instance!.Create(new Receipt()
            {
                TotalPrice = _sellerViewModel.Price,
                Items = _sellerViewModel.Items.Select(i => new Item()
                {
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ArticleId = i.ArticleId,
                }).ToList(),
                SellerId = SellerStore.Instance!.ActiveSeller!.SellerId
            });
        }
    }
}
