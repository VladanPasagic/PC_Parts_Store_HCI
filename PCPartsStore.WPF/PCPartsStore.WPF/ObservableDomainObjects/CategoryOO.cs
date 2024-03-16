using PCPartsStore.Domain.Models;

namespace PCPartsStore.WPF.ObservableDomainObjects;

public class CategoryOO : ObservableObject
{
    private Category _category;

    public CategoryOO()
    {
        _category = new();
    }

    public CategoryOO(Category category)
    {
        _category = new();
        CategoryId = category.CategoryId;
        Name = category.Name;
    }

    public string Name
    {
        get => _category.Name;
        set
        {
            _category.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public int CategoryId
    {
        get => _category.CategoryId;
        set
        {
            _category.CategoryId = value;
            OnPropertyChanged(nameof(CategoryId));
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not null and CategoryOO other)
        {
            return other.CategoryId == CategoryId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return CategoryId.GetHashCode();
    }
}
