using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface ICreateArticleCommand
{
    Task ExecuteAsync(Article article);
}
