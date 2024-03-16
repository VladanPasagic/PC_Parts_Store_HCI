using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface IUpdateCategoryCommand
{
    public Task ExecuteAsync(Category category);
}
