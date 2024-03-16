using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface IUpdateArticleCommand
{
    Task ExecuteAsync(Article article);
}
