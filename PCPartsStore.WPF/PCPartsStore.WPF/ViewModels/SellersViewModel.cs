using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class SellersViewModel : ViewModelBase, IErrorViewModel, IModalViewModel, ILoadingViewModel, ISearchViewModel
{
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

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    private string? _errorMessage;

    public SellersViewModel()
    {
        SellerStore.Instance!.SellerCreated += SellerStore_SellerCreated;
        SellerStore.Instance!.SellersRead += SellerStore_SellersRead;
        SellerStore.Instance!.SellerDeactivated += SellerStore_SellerDeactivated;

        new LoadSellersCommand().Execute(null);

        OpenSellerModalCommand = new OpenSellerModalCommand(this);
        DeactivateSellerCommand = new DeactivateSellerCommand(this);
        SearchFieldControl = new(new SearchCommand(this), new QuitSearchCommand(this));
    }

    private void SellerStore_SellerDeactivated(Seller seller)
    {
        Seller? sellerModel = Sellers.FirstOrDefault(s => s.SellerId == seller.SellerId);
        if (sellerModel != null)
        {
            Sellers.Remove(sellerModel);
        }

        Sellers.Add(seller);
    }

    private void SellerStore_SellersRead()
    {
        Sellers.Clear();

        foreach (var seller in SellerStore.Instance!.Sellers)
        {
            Sellers.Add(seller);
        }
    }

    public override void Dispose()
    {
        SellerStore.Instance!.SellerCreated -= SellerStore_SellerCreated;
        SellerStore.Instance!.SellersRead -= SellerStore_SellersRead;
        SellerStore.Instance!.SellerDeactivated -= SellerStore_SellerDeactivated;

        base.Dispose();
    }

    private void SellerStore_SellerCreated(Seller seller)
    {
        IsOpen = false;
        Sellers.Add(seller);
    }

    public void Search(string searchString)
    {
        var sellers = Sellers.Where(s => s.Username.ToLower().Contains(searchString.ToLower())).ToList();
        Sellers.Clear();
        foreach (var seller in sellers)
        {
            Sellers.Add(seller);
        }
    }

    public void QuitSearch()
    {
        SearchFieldControl.SearchText = string.Empty;
        Sellers.Clear();

        foreach (var seller in SellerStore.Instance!.Sellers)
        {
            Sellers.Add(seller);
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

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

    public ObservableCollection<Seller> Sellers { get; set; } = [];

    public Seller? SelectedSeller { get; set; }

    public ICommand OpenSellerModalCommand { get; }

    public ICommand DeactivateSellerCommand { get; }

    private SellerRegistrationDetailsFormViewModel? _form;
    public SellerRegistrationDetailsFormViewModel? SellerRegistrationDetailsFormViewModel
    {
        get => _form;
        set
        {
            _form = value;
            OnPropertyChanged(nameof(SellerRegistrationDetailsFormViewModel));
        }
    }

    public SearchFieldControlViewModel SearchFieldControl { get; }
}
