using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class ProductDetailsFormViewModel : ViewModelBase, ILoadingViewModel, IErrorViewModel, IModalContentViewModel
{
    public string? Name { get; set; }

    public ObservableCollection<ManufacturerOO> Manufacturers { get; set; } = [];

    private int _manufacturerIndex = -1;
    public int ManufacturerIndex
    {
        get => _manufacturerIndex;
        set
        {
            _manufacturerIndex = value;
            OnPropertyChanged(nameof(ManufacturerIndex));
        }
    }

    private ArticleOO? _article;

    public ObservableCollection<CategoryOO> Categories { get; set; } = [];

    private int _categoryIndex = -1;

    public int CategoryIndex
    {
        get => _categoryIndex;
        set
        {
            _categoryIndex = value;
            OnPropertyChanged(nameof(CategoryIndex));
        }
    }

    public int ManufacturingYear { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public ProductDetailsFormViewModel(ICommand addProductCommand, ICommand closeModalCommand, ICommand editProductCommand, ArticleOO? product = default)
    {
        new LoadManufacturersCommand().Execute(null);
        new LoadCategoriesCommand().Execute(null);

        ManufacturerStore.Instance!.ManufacturerRead += ManufacturerStore_ManufacturersRead;
        CategoryStore.Instance!.CategoriesRead += CategoryStore_CategoriesRead;

        if (product != default)
        {
            _article = product;
            ModalButtonControlViewModel = new(true, addProductCommand, closeModalCommand, editProductCommand);
            Price = product.Price;
            Quantity = product.Quantity;
            ManufacturingYear = product.ManufacturingYear;
            Name = product.Name!;
        }
        else
        {
            ModalButtonControlViewModel = new(false, addProductCommand, closeModalCommand, editProductCommand);
            CategoryIndex = -1;
            ManufacturerIndex = -1;
        }
    }

    public override void Dispose()
    {
        CategoryStore.Instance!.CategoriesRead -= CategoryStore_CategoriesRead;
        ManufacturerStore.Instance!.ManufacturerRead -= ManufacturerStore_ManufacturersRead;

        base.Dispose();
    }

    private void ManufacturerStore_ManufacturersRead()
    {
        Manufacturers.Clear();

        foreach (ManufacturerOO manufacturer in ManufacturerStore.Instance!.Manufacturers)
        {
            Manufacturers.Add(manufacturer);
        }
        if (_article == null)
            return;

        ManufacturerIndex = Manufacturers.IndexOf(new ManufacturerOO()
        {
            ManufacturerId = _article!.ManufacturerId,
        });
    }

    private void CategoryStore_CategoriesRead()
    {
        Categories.Clear();

        foreach (CategoryOO category in CategoryStore.Instance!.Categories)
        {
            Categories.Add(category);
        }
        if (_article == null)
            return;

        CategoryIndex = Categories.IndexOf(new CategoryOO()
        {
            CategoryId = _article!.CategoryId,
        });
    }

    public ModalButtonControlViewModel ModalButtonControlViewModel { get; }

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
}
