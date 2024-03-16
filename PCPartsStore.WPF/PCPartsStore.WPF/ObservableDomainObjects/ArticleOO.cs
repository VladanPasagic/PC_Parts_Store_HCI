using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Utilities;

namespace PCPartsStore.WPF.ObservableDomainObjects;

public class ArticleOO : ObservableObject
{
    private Article _article;

    public ArticleOO()
    {
        _article = new();
    }

    public ArticleOO(Article article)
    {
        _article = new();
        Name = article.Name;
        ArticleId = article.ArticleId;
        CategoryId = article.CategoryId;
        Manufacturer = new(article.Manufacturer);
        Category = new(article.Category);
        ManufacturerId = article.ManufacturerId;
        ManufacturingYear = article.ManufacturingYear;
        Price = article.Price;
        Quantity = article.Quantity;
    }

    public int ArticleId
    {
        get => _article.ArticleId;
        set
        {
            _article.ArticleId = value;
            OnPropertyChanged(nameof(ArticleId));
        }
    }

    public string Name
    {
        get => _article.Name;
        set
        {
            _article.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public int ManufacturerId
    {
        get => _article.ManufacturerId;
        set
        {
            _article.ManufacturerId = value;
            OnPropertyChanged(nameof(ManufacturerId));
        }
    }

    public int CategoryId
    {
        get => _article.CategoryId;
        set
        {
            _article.CategoryId = value;
            OnPropertyChanged(nameof(CategoryId));
        }
    }

    public int ManufacturingYear
    {
        get => _article.ManufacturingYear;
        set
        {
            _article.ManufacturingYear = value;
            OnPropertyChanged(nameof(ManufacturingYear));
        }
    }

    public int Quantity
    {
        get => _article.Quantity;
        set
        {
            _article.Quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    public decimal Price
    {
        get => _article.Price;
        set
        {
            _article.Price = value;
            OnPropertyChanged(nameof(Price));
        }
    }

    public virtual CategoryOO Category
    {
        get => new(_article.Category);
        set
        {
            _article.Category = Converters.ConvertToCategory(value);
            OnPropertyChanged(nameof(Category));
        }
    }

    public virtual ManufacturerOO Manufacturer
    {
        get => new(_article.Manufacturer);
        set
        {
            _article.Manufacturer = Converters.ConvertToManufacturer(value);
            OnPropertyChanged(nameof(Manufacturer));
        }
    }

}
