using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface ICreateCategoryCommand
{
    Task<bool> ExecuteAsync(Category category);
}
