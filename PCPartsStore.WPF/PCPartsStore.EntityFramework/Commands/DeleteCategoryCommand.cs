using Microsoft.EntityFrameworkCore;
using PCPartsStore.Domain.Commands;

namespace PCPartsStore.EntityFramework.Commands;

public class DeleteCategoryCommand : IDeleteCategoryCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public DeleteCategoryCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(int id)
    {
        using var context = _contextFactory.Create();

        context.Categories.Remove(new DTOs.CategoryDTO()
        {
            CategoryId = id
        });

        await context.SaveChangesAsync();
    }
}
