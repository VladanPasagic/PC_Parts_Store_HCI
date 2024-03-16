using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace PCPartsStore.WPF.ViewModels;

public class ProductsViewModel : ViewModelBase, IModalViewModel, IErrorViewModel, ILoadingViewModel, ISearchViewModel
{
    public ObservableCollection<ArticleOO> Products { get; set; } = [];

    public CrudButtonControlViewModel Crud { get; }


    private ProductDetailsFormViewModel? _form;

    public ProductDetailsFormViewModel? ProductDetailsFormViewModel
    {
        get => _form;
        set
        {
            _form = value!;
            OnPropertyChanged(nameof(ProductDetailsFormViewModel));
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

    public ArticleOO? SelectedArticle { get; set; }

    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

    public SearchFieldControlViewModel SearchFieldControl { get; }

    public ProductsViewModel()
    {
        ProductStore.Instance!.ProductsRead += ProductsStore_ProductsRead;
        ProductStore.Instance!.ProductCreated += ProductStore_ProductCreated;
        ProductStore.Instance!.ProductDeleted += ProductStore_ProductDeleted;
        ProductStore.Instance!.ProductUpdated += ProductStore_ProductUpdated;

        new LoadProductsCommand().Execute(null);

        Crud = new(new OpenProductModalCommand(this, false), new DeleteProductCommand(this), new OpenProductModalCommand(this, true));
        SearchFieldControl = new(new SearchCommand(this), new QuitSearchCommand(this));
    }

    public override void Dispose()
    {
        ProductStore.Instance!.ProductsRead -= ProductsStore_ProductsRead;
        ProductStore.Instance!.ProductCreated -= ProductStore_ProductCreated;
        ProductStore.Instance!.ProductDeleted -= ProductStore_ProductDeleted;
        ProductStore.Instance!.ProductUpdated -= ProductStore_ProductUpdated;

        base.Dispose();
    }

    private void ProductStore_ProductUpdated(ArticleOO article)
    {
        IsOpen = false;
        ArticleOO? articleModel = Products.FirstOrDefault(a => a.ArticleId == article.ArticleId);

        if (articleModel != null)
        {
            articleModel.Price = article.Price;
            articleModel.Quantity = article.Quantity;
            articleModel.Manufacturer = article.Manufacturer;
            articleModel.Category = article.Category;
            articleModel.ManufacturerId = article.ManufacturerId;
            articleModel.CategoryId = article.CategoryId;
            articleModel.Name = article.Name;
            articleModel.ArticleId = article.ArticleId;
            articleModel.ManufacturingYear = article.ManufacturingYear;
        }
    }

    private void ProductStore_ProductDeleted(int index)
    {
        ArticleOO? article = Products.FirstOrDefault(a => a.ArticleId == index);

        if (article != null)
        {
            Products.Remove(article);
        }
    }

    private void ProductStore_ProductCreated(ArticleOO article)
    {
        IsOpen = false;
        Products.Add(article);
    }

    private void ProductsStore_ProductsRead()
    {
        Products.Clear();

        foreach (var article in ProductStore.Instance!.Products)
        {
            Products.Add(article);
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
