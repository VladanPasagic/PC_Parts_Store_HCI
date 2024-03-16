using Microsoft.EntityFrameworkCore;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework.Queries;

public class GetAllCategoriesQuery : IGetAllCategoriesQuery
{
    private readonly PcStoreContextFactory _contextFactory;

    public GetAllCategoriesQuery(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Category>> ExecuteAsync()
    {
        using var context = _contextFactory.Create();

        IEnumerable<CategoryDTO> categoryDTOs = await context.Categories.ToListAsync();

        return categoryDTOs.Select(c => new Category()
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
        });
    }
}
