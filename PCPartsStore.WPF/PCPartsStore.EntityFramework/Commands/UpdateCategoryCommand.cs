using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class UpdateCategoryCommand : IUpdateCategoryCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public UpdateCategoryCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Category category)
    {
        using var context = _contextFactory.Create();

        var categoryDto = context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);

        categoryDto!.Name = category.Name;

        context.Categories.Update(categoryDto);

        await context.SaveChangesAsync();

    }
}
