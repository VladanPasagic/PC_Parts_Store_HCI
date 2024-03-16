using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface ICreateReceiptCommand
{
    public Task ExecuteAsync(Receipt receipt);
}
