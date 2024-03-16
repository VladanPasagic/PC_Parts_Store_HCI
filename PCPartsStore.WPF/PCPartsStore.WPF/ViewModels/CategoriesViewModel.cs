using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace PCPartsStore.WPF.ViewModels;

public class CategoriesViewModel : ViewModelBase, IModalViewModel, IErrorViewModel, ILoadingViewModel, ISearchViewModel
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

    public ObservableCollection<CategoryOO> Categories { get; } = [];

    public CrudButtonControlViewModel Crud { get; }

    public CategoryOO? SelectedCategory { get; set; }


    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

    private CategoryDetailsFormViewModel? _form;
    public CategoryDetailsFormViewModel? CategoryDetailsFormViewModel
    {
        get => _form;
        set
        {
            _form = value!;
            OnPropertyChanged(nameof(CategoryDetailsFormViewModel));
        }
    }

    public SearchFieldControlViewModel SearchFieldControl { get; }

    public CategoriesViewModel()
    {
        CategoryStore.Instance!.CategoriesRead += CategoryStore_CategoriesRead;
        CategoryStore.Instance!.CategoryCreated += CategoryStore_CategoryCreated;
        CategoryStore.Instance!.CategoryUpdated += CategoryStore_CategoryUpdated;
        CategoryStore.Instance!.CategoryDeleted += CategoryStore_CategoryDeleted;

        new LoadCategoriesCommand().Execute(null);

        Crud = new(new OpenCategoryModalCommand(this, false), new DeleteCategoryCommand(this), new OpenCategoryModalCommand(this, true));
        SearchFieldControl = new(new SearchCommand(this), new QuitSearchCommand(this));
    }

    public override void Dispose()
    {
        CategoryStore.Instance!.CategoriesRead -= CategoryStore_CategoriesRead;
        CategoryStore.Instance!.CategoryCreated -= CategoryStore_CategoryCreated;
        CategoryStore.Instance!.CategoryUpdated -= CategoryStore_CategoryUpdated;
        CategoryStore.Instance!.CategoryDeleted -= CategoryStore_CategoryDeleted;

        base.Dispose();
    }

    private void CategoryStore_CategoryDeleted(int index)
    {
        CategoryOO? category = Categories.FirstOrDefault(c => c.CategoryId == index);

        if (category != null)
        {
            Categories.Remove(category);
        }
    }

    private void CategoryStore_CategoriesRead()
    {
        Categories.Clear();

        foreach (var category in CategoryStore.Instance!.Categories)
        {
            Categories.Add(category);
        }
    }

    private void CategoryStore_CategoryCreated(CategoryOO category)
    {
        IsOpen = false;
        Categories.Add(category);
    }

    private void CategoryStore_CategoryUpdated(CategoryOO category)
    {
        IsOpen = false;
        CategoryOO? categoryModel = Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
        if (categoryModel != null)
        {
            categoryModel.CategoryId = category.CategoryId;
            categoryModel.Name = category.Name;
        }
    }

    public void Search(string searchString)
    {
        var categories = Categories.Where(c => c.Name.ToLower().Contains(searchString.ToLower())).ToList();
        Categories.Clear();
        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    public void QuitSearch()
    {
        SearchFieldControl.SearchText = string.Empty;
        Categories.Clear();
        foreach (var category in CategoryStore.Instance!.Categories)
        {
            Categories.Add(category);
        }
    }
}
