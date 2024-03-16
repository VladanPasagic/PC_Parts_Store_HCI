using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using System.Windows.Media;
using PCPartsStore.WPF.ViewModels.Interfaces;
using PCPartsStore.WPF.Stores;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.WPF.Commands;

public class ChangeToDarkThemeCommand : CommandBase
{
    private readonly IThemeViewModel _themeViewModel;

    public ChangeToDarkThemeCommand(IThemeViewModel themeViewModel)
    {
        _themeViewModel = themeViewModel;
    }

    public override void Execute(object? parameter)
    {
        var settings = SettingsStore.Instance!.CurrentSetting;

        if (settings!.Theme != (int)Themes.Dark)
        {
            settings.Theme = (int)Themes.Dark;
            SettingsStore.Instance!.Update(settings);
        }
        PrimaryColor primaryColor = PrimaryColor.Amber;
        Color primary = SwatchHelper.Lookup[(MaterialDesignColor)primaryColor];

        SecondaryColor secondaryColor = SecondaryColor.Indigo;
        Color secondary = SwatchHelper.Lookup[(MaterialDesignColor)secondaryColor];

        PaletteHelper helper = new();
        var theme = helper.GetTheme();
        theme.SetPrimaryColor(primary);
        theme.SetSecondaryColor(secondary);
        theme.SetBaseTheme(Theme.Dark);
        helper.SetTheme(theme);

        _themeViewModel.LightThemeChecked = false;
        _themeViewModel.DarkThemeChecked = true;
        _themeViewModel.GreyThemeChecked = false;

    }
}
