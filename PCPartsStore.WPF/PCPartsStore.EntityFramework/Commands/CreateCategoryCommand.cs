using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class CreateCategoryCommand : ICreateCategoryCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public CreateCategoryCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> ExecuteAsync(Category category)
    {
        using var context = _contextFactory.Create();

        context.Categories.Add(new DTOs.CategoryDTO()
        {
            Name = category.Name,
        });

        int entries = await context.SaveChangesAsync();
        return entries > 0;
    }
}
