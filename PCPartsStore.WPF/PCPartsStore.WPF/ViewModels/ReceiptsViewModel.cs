using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class ReceiptsViewModel : ViewModelBase, IErrorViewModel, IModalViewModel, ILoadingViewModel, ISearchViewModel
{
    public ObservableCollection<Receipt> Receipts { get; set; } = [];

    public ReceiptItemsViewModel? _modal;
    public ReceiptItemsViewModel? ReceiptItemsViewModel
    {
        get => _modal;
        set
        {
            _modal = value;
            OnPropertyChanged(nameof(ReceiptItemsViewModel));
        }
    }

    private bool _isOpen = false;

    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            OnPropertyChanged(nameof(IsOpen));
        }
    }

    private bool _isLoading = false;

    private string? _errorMessage;

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }
    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

    public SearchFieldControlViewModel SearchFieldControl { get; }

    public ICommand ViewReceiptCommand { get; set; }

    public Receipt? SelectedReceipt { get; set; }

    public ReceiptsViewModel()
    {
        SearchFieldControl = new SearchFieldControlViewModel(new SearchCommand(this), new QuitSearchCommand(this));
        ViewReceiptCommand = new ViewReceiptCommand(this);
        ReceiptStore.Instance!.ReceiptsRead += ReceiptsViewModel_ReceiptsRead;

        new LoadReceiptsCommand().Execute(null);

    }

    private void ReceiptsViewModel_ReceiptsRead()
    {
        foreach (var receipt in ReceiptStore.Instance!.Receipts)
        {
            Receipts.Add(receipt);
        }
    }

    public void Search(string searchString)
    {
        if (searchString.All(char.IsDigit))
        {
            var receipts = Receipts.Where(p => p.ReceiptId == int.Parse(searchString)).ToList();
            Receipts.Clear();
            foreach (var receipt in receipts)
            {
                Receipts.Add(receipt);
            }
        }
    }

    public void QuitSearch()
    {
        SearchFieldControl.SearchText = string.Empty;
        Receipts.Clear();

        foreach (var receipt in ReceiptStore.Instance!.Receipts)
        {
            Receipts.Add(receipt);
        }
    }
}
