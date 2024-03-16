using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Queries;

public interface IGetSettingsQuery
{
    public Setting? Execute(Seller seller);
}
