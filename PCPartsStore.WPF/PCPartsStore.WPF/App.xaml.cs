using Microsoft.EntityFrameworkCore;
using PCPartsStore.EntityFramework;
using PCPartsStore.EntityFramework.Commands;
using PCPartsStore.EntityFramework.DTOs;
using PCPartsStore.EntityFramework.Queries;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;
using System.Configuration;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace PCPartsStore.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string username = "adminUsername";
        private static readonly string password = "adminPassword";
        private static readonly string db = "mysql";
        protected override async void OnStartup(StartupEventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings[db].ConnectionString;

            PcStoreContextFactory pcStoreContextFactory = new(new DbContextOptionsBuilder().UseMySQL(connectionString).Options);

            string adminUser = ConfigurationManager.AppSettings[username]!;

            using (PcStoreContext context = pcStoreContextFactory.Create())
            {
                if (!context.Sellers.Any(s => s.Username.Equals(adminUser)))
                {
                    SellerDTO sellerDTO = new()
                    {
                        Username = adminUser,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(ConfigurationManager.AppSettings[password]),
                        IsAdmin = true,
                        IsActive = true,
                    };
                    context.Sellers.Add(sellerDTO);
                    context.Settings.Add(new SettingDTO()
                    {
                        Seller = sellerDTO,
                        Theme = 0,
                        Language = 0,
                    });
                    await context.SaveChangesAsync();
                }
            }

            SellerStore.CreateInstance(new LoginSellerCommand(pcStoreContextFactory), new RegisterSellerCommand(pcStoreContextFactory),
                new GetAllSellersQuery(pcStoreContextFactory), new DeactivateSellerCommand(pcStoreContextFactory));
            ProductStore.CreateInstance(new GetAllProductsQuery(pcStoreContextFactory), new CreateArticleCommand(pcStoreContextFactory),
                new UpdateArticleCommand(pcStoreContextFactory), new DeleteArticleCommand(pcStoreContextFactory));
            CategoryStore.CreateInstance(new GetAllCategoriesQuery(pcStoreContextFactory), new CreateCategoryCommand(pcStoreContextFactory),
                new UpdateCategoryCommand(pcStoreContextFactory), new DeleteCategoryCommand(pcStoreContextFactory));
            ManufacturerStore.CreateInstance(new GetAllManufacturersQuery(pcStoreContextFactory), new CreateManufacturerCommand(pcStoreContextFactory),
                new UpdateManufacturerCommand(pcStoreContextFactory), new DeleteManufacturerCommand(pcStoreContextFactory));
            ReceiptStore.CreateInstance(new GetAllReceiptsQuery(pcStoreContextFactory), new CreateReceiptCommand(pcStoreContextFactory));
            SettingsStore.CreateInstance(new GetSettingsQuery(pcStoreContextFactory), new UpdateSettingsCommand(pcStoreContextFactory));


            NavigationStore.Instance.CurrentViewModel = new LoginViewModel();

            MainWindow mainWindow = new()
            {
                DataContext = new MainViewModel()
            };

            mainWindow.Show();

            base.OnStartup(e);
        }
    }

}
