using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Utilities;

namespace PCPartsStore.WPF.Stores;

class CategoryStore
{
    public static CategoryStore? Instance { get; private set; }

    private readonly IGetAllCategoriesQuery _getAllCategoriesQuery;
    private readonly ICreateCategoryCommand _createCategoryCommand;
    private readonly IUpdateCategoryCommand _updateCategoryCommand;
    private readonly IDeleteCategoryCommand _deleteCategoryCommand;

    private readonly List<CategoryOO> _categories = [];

    public static void CreateInstance(IGetAllCategoriesQuery getAllCategoriesQuery, ICreateCategoryCommand createCategoryCommand,
        IUpdateCategoryCommand updateCategoryCommand, IDeleteCategoryCommand deleteCategoryCommand)
    {
        Instance ??= new(getAllCategoriesQuery, createCategoryCommand, updateCategoryCommand, deleteCategoryCommand);
    }

    private CategoryStore(IGetAllCategoriesQuery getAllCategoriesQuery, ICreateCategoryCommand createCategoryCommand,
        IUpdateCategoryCommand updateCategoryCommand, IDeleteCategoryCommand deleteCategoryCommand)
    {
        _getAllCategoriesQuery = getAllCategoriesQuery;
        _createCategoryCommand = createCategoryCommand;
        _updateCategoryCommand = updateCategoryCommand;
        _deleteCategoryCommand = deleteCategoryCommand;
    }

    public IEnumerable<CategoryOO> Categories => _categories;

    public event Action? CategoriesRead;
    public event Action<CategoryOO>? CategoryCreated;
    public event Action<CategoryOO>? CategoryUpdated;
    public event Action<int>? CategoryDeleted;

    public async Task Read()
    {
        IEnumerable<Category> categories = await _getAllCategoriesQuery.ExecuteAsync();

        _categories.Clear();

        foreach (var category in categories)
        {
            _categories.Add(new CategoryOO(category));
        }

        CategoriesRead?.Invoke();
    }

    public async Task Create(CategoryOO category)
    {
        await _createCategoryCommand.ExecuteAsync(Converters.ConvertToCategory(category));

        _categories.Add(category);

        CategoryCreated?.Invoke(category);
    }

    public async Task Update(CategoryOO category)
    {
        await _updateCategoryCommand.ExecuteAsync(Converters.ConvertToCategory(category));

        int currentIndex = _categories.FindIndex(c => c.CategoryId == category.CategoryId);

        if (currentIndex != -1)
        {
            _categories[currentIndex] = category;
        }

        CategoryUpdated?.Invoke(category);
    }

    public async Task Delete(int id)
    {
        await _deleteCategoryCommand.ExecuteAsync(id);

        int currentIndex = _categories.FindIndex(c => c.CategoryId == id);

        if (currentIndex == -1)
        {
            return;
        }

        _categories.RemoveAt(currentIndex);

        CategoryDeleted?.Invoke(id);
    }
}
