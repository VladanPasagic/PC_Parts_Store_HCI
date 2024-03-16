using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface IUpdateSettingsCommand
{
    Task ExecuteAsync(Setting setting);
}
