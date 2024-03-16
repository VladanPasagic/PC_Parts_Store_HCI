using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class SellerViewModel : ViewModelBase, ILanguageViewModel, IThemeViewModel, IErrorViewModel, ILoadingViewModel, IModalViewModel, ISearchViewModel
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

    private string? _errorMessage;

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

    public ICommand ChangeLanguageSerbianCommand { get; }

    public ICommand ChangeLanguageEnglishCommand { get; }

    public ICommand ChangeLightThemeCommand { get; }

    public ICommand ChangeDarkThemeCommand { get; }

    public ICommand ChangeGreyThemeCommand { get; }

    public ICommand LogoutMenuItemCommand { get; }

    private bool _EnglishLanguageChecked;


    public ObservableCollection<ArticleOO> Products { get; set; } = [];

    public ObservableCollection<ItemOO> Items { get; set; } = [];

    public ItemOO? SelectedItem { get; set; }

    public ArticleOO? SelectedArticle { get; set; }


    private decimal _price;
    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged(nameof(Price));
        }
    }

    public bool EnglishLanguageChecked
    {
        get => _EnglishLanguageChecked;
        set
        {
            _EnglishLanguageChecked = value;
            OnPropertyChanged(nameof(EnglishLanguageChecked));
        }
    }

    private bool _SerbianLanguageChecked;

    public bool SerbianLanguageChecked
    {
        get => _SerbianLanguageChecked;
        set
        {
            _SerbianLanguageChecked = value;
            OnPropertyChanged(nameof(SerbianLanguageChecked));
        }
    }

    private bool _lightThemeChecked;
    public bool LightThemeChecked
    {
        get => _lightThemeChecked;
        set
        {
            _lightThemeChecked = value;
            OnPropertyChanged(nameof(LightThemeChecked));
        }
    }

    private bool _darkThemeChecked;
    public bool DarkThemeChecked
    {
        get => _darkThemeChecked;
        set
        {
            _darkThemeChecked = value;
            OnPropertyChanged(nameof(DarkThemeChecked));
        }
    }

    public ICommand AddSingleItemCommand { get; }

    private ItemQuantityFormViewModel? _quantityFormViewModel;
    public ItemQuantityFormViewModel? ItemQuantityFormViewModel
    {
        get => _quantityFormViewModel;
        set
        {
            _quantityFormViewModel = value;
            OnPropertyChanged(nameof(ItemQuantityFormViewModel));
        }
    }

    public ICommand SaveReceiptCommand { get; }

    public ICommand RemoveFromReceiptCommand { get; }

    public ICommand AddItemsCommand { get; }

    public SearchFieldControlViewModel SearchFieldControl { get; }

    private bool _greyThemeChecked;
    public bool GreyThemeChecked
    {
        get => _greyThemeChecked;
        set
        {
            _greyThemeChecked = value;
            OnPropertyChanged(nameof(GreyThemeChecked));
        }
    }

    public SellerViewModel()
    {
        AddSingleItemCommand = new AddSingleItemCommand(this);
        SaveReceiptCommand = new SaveReceiptCommand(this);
        AddItemsCommand = new AddItemsCommand(this);
        RemoveFromReceiptCommand = new RemoveFromReceiptCommand(this);
        SearchFieldControl = new SearchFieldControlViewModel(new SearchCommand(this), new QuitSearchCommand(this));
        ChangeLanguageEnglishCommand = new ChangeLanguageEnglishCommand(this);
        ChangeLanguageSerbianCommand = new ChangeLanguageSerbianCommand(this);
        ChangeDarkThemeCommand = new ChangeToDarkThemeCommand(this);
        ChangeGreyThemeCommand = new ChangeToGreyThemeCommand(this);
        ChangeLightThemeCommand = new ChangeToLightThemeCommand(this);
        LogoutMenuItemCommand = new LogoutMenuItemCommand();
        ProductStore.Instance!.ProductsRead += ProductsStore_ProductsRead;

        ReceiptStore.Instance!.ReceiptCreated += SellerViewModel_ReceiptCreated;

        new LoadProductsCommand().Execute(null);

        SettingsStore.Instance!.SettingsRead += SellerViewModel_SettingsRead;

        new LoadSettingsCommand().Execute(null);
    }

    private void SellerViewModel_SettingsRead(Domain.Models.Setting settings)
    {
        if (settings.Language == 0)
        {
            new ChangeLanguageEnglishCommand(this).Execute(null);
        }
        else if (settings.Language == 1)
        {
            new ChangeLanguageSerbianCommand(this).Execute(null);
        }

        if (settings.Theme == 0)
        {
            new ChangeToLightThemeCommand(this).Execute(null);
        }
        else if (settings.Theme == 1)
        {
            new ChangeToDarkThemeCommand(this).Execute(null);
        }
        else if (settings.Theme == 2)
        {
            new ChangeToGreyThemeCommand(this).Execute(null);
        }
    }

    private void SellerViewModel_ReceiptCreated()
    {
        Items.Clear();
        Price = 0;
    }

    private void ProductsStore_ProductsRead()
    {
        Products.Clear();

        foreach (var item in ProductStore.Instance!.Products)
        {
            Products.Add(item);
        }
    }

    public void Search(string searchString)
    {
        var products = Products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
        Products.Clear();
        foreach (var article in products)
        {
            Products.Add(article);
        }
    }

    public void QuitSearch()
    {
        SearchFieldControl.SearchText = string.Empty;
        Products.Clear();
        foreach (var article in ProductStore.Instance!.Products)
        {
            Products.Add(article);
        }
    }
}
