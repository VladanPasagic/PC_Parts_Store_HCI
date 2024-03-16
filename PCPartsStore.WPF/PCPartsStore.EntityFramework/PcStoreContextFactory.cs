using Microsoft.EntityFrameworkCore;

namespace PCPartsStore.EntityFramework
{
    public class PcStoreContextFactory
    {
        private readonly DbContextOptions _options;
        public PcStoreContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public PcStoreContext Create()
        {
            return new PcStoreContext(_options);
        }
    }
}
