using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class AdminViewModel : ViewModelBase, ILanguageViewModel, IThemeViewModel
{
    public ICommand ProductMenuItemCommand { get; }

    public ICommand SellerMenuItemCommand { get; }

    public ICommand CategoryMenuItemCommand { get; }

    public ICommand ManufacturerMenuItemCommand { get; }

    public ViewModelBase CurrentViewModel => AdminNavigationStore.Instance.CurrentViewModel!;

    public ICommand ChangeLanguageSerbianCommand { get; }

    public ICommand ChangeLanguageEnglishCommand { get; }

    public ICommand ChangeLightThemeCommand { get; }

    public ICommand ChangeDarkThemeCommand { get; }

    public ICommand LogoutMenuItemCommand { get; }

    public ICommand ReceiptMenuItemCommand { get; }


    private bool _EnglishLanguageChecked;

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

    public ICommand ChangeGreyThemeCommand { get; }

    public AdminViewModel()
    {
        SellerMenuItemCommand = new SellerMenuItemCommand();
        CategoryMenuItemCommand = new CategoryMenuItemCommand();
        ReceiptMenuItemCommand = new ReceiptMenuItemCommand();
        ChangeLanguageEnglishCommand = new ChangeLanguageEnglishCommand(this);
        ChangeLanguageSerbianCommand = new ChangeLanguageSerbianCommand(this);
        ManufacturerMenuItemCommand = new ManufacturerMenuItemCommand();
        ProductMenuItemCommand = new ProductMenuItemCommand();
        ChangeDarkThemeCommand = new ChangeToDarkThemeCommand(this);
        ChangeLightThemeCommand = new ChangeToLightThemeCommand(this);
        ChangeGreyThemeCommand = new ChangeToGreyThemeCommand(this);
        LogoutMenuItemCommand = new LogoutMenuItemCommand();

        AdminNavigationStore.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
        AdminNavigationStore.Instance.CurrentViewModel = new ProductsViewModel();

        SettingsStore.Instance!.SettingsRead += Instance_SettingsRead;

        new LoadSettingsCommand().Execute(null);

    }

    private void Instance_SettingsRead(Domain.Models.Setting settings)
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

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
