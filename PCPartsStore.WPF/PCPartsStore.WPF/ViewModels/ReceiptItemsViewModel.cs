using PCPartsStore.Domain.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class ReceiptItemsViewModel : ViewModelBase
{
    public ObservableCollection<Item> Items { get; } = [];


    public ICommand CloseModalCommand { get; }

    public ReceiptItemsViewModel(ICommand closeModalCommand, Receipt receipt)
    {
        CloseModalCommand = closeModalCommand;
        foreach (var item in receipt.Items)
        {
            Items.Add(item);
        }
    }
}
