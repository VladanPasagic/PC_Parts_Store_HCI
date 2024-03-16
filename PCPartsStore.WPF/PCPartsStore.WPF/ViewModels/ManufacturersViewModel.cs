using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace PCPartsStore.WPF.ViewModels;

public class ManufacturersViewModel : ViewModelBase, IModalViewModel, IErrorViewModel, ILoadingViewModel, ISearchViewModel
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

    public ObservableCollection<ManufacturerOO> Manufacturers { get; set; } = [];

    public CrudButtonControlViewModel Crud { get; set; }

    public ManufacturerOO? SelectedManufacturer { get; set; }

    private ManufacturerDetailsFormViewModel? _form;

    public ManufacturerDetailsFormViewModel? ManufacturerDetailsFormViewModel
    {
        get => _form;
        set
        {
            _form = value!;
            OnPropertyChanged(nameof(ManufacturerDetailsFormViewModel));
        }
    }

    public SearchFieldControlViewModel SearchFieldControl { get; }

    public ManufacturersViewModel()
    {
        ManufacturerStore.Instance!.ManufacturerRead += ManufacturerStore_ManufacturersRead;
        ManufacturerStore.Instance!.ManufacturerCreated += ManufacturerStore_ManufacturerCreated;
        ManufacturerStore.Instance!.ManufacturerUpdated += ManufacturerStore_ManufacturerUpdated;
        ManufacturerStore.Instance!.ManufacturerDeleted += ManufacturerStore_ManufacturerDeleted;

        new LoadManufacturersCommand().Execute(null);

        Crud = new(new OpenManufacturerModalCommand(this, false), new DeleteManufacturerCommand(this), new OpenManufacturerModalCommand(this, true));
        SearchFieldControl = new(new SearchCommand(this), new QuitSearchCommand(this));

    }

    private void ManufacturerStore_ManufacturerDeleted(int index)
    {
        ManufacturerOO? manufacturer = Manufacturers.FirstOrDefault(m => m.ManufacturerId == index);

        if (manufacturer != null)
        {
            Manufacturers.Remove(manufacturer);
        }
    }

    private void ManufacturerStore_ManufacturerUpdated(ManufacturerOO manufacturer)
    {
        IsOpen = false;

        ManufacturerOO? manufacturerModel = Manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturer.ManufacturerId);
        if (manufacturerModel != null)
        {
            manufacturerModel.ManufacturerId = manufacturer.ManufacturerId;
            manufacturerModel.Name = manufacturer.Name;
            manufacturerModel.Country = manufacturer.Country;
        }
    }

    private void ManufacturerStore_ManufacturerCreated(ManufacturerOO manufacturer)
    {
        IsOpen = false;
        Manufacturers.Add(manufacturer);
    }

    public override void Dispose()
    {
        ManufacturerStore.Instance!.ManufacturerRead -= ManufacturerStore_ManufacturersRead;
        ManufacturerStore.Instance!.ManufacturerCreated -= ManufacturerStore_ManufacturerCreated;
        ManufacturerStore.Instance!.ManufacturerUpdated -= ManufacturerStore_ManufacturerUpdated;
        ManufacturerStore.Instance!.ManufacturerDeleted -= ManufacturerStore_ManufacturerDeleted;

        base.Dispose();
    }

    private void ManufacturerStore_ManufacturersRead()
    {
        Manufacturers.Clear();

        foreach (var manufacturer in ManufacturerStore.Instance!.Manufacturers)
        {
            Manufacturers.Add(manufacturer);
        }
    }

    public void Search(string searchString)
    {
        var manufacturers = Manufacturers.Where(m => m.Name.ToLower().Contains(searchString.ToLower())).ToList();
        Manufacturers.Clear();
        foreach (var manufacturer in manufacturers)
        {
            Manufacturers.Add(manufacturer);
        }
    }

    public void QuitSearch()
    {
        SearchFieldControl.SearchText = string.Empty;
        Manufacturers.Clear();

        foreach (var manufacturer in ManufacturerStore.Instance!.Manufacturers)
        {
            Manufacturers.Add(manufacturer);
        }
    }
}
