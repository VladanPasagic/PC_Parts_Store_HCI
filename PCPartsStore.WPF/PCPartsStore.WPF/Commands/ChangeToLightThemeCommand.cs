using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Media;

namespace PCPartsStore.WPF.Commands;

public class ChangeToLightThemeCommand : CommandBase
{
    private readonly IThemeViewModel _themeViewModel;

    public ChangeToLightThemeCommand(IThemeViewModel themeViewModel)
    {
        _themeViewModel = themeViewModel;
    }

    public override void Execute(object? parameter)
    {
        var settings = SettingsStore.Instance!.CurrentSetting;

        if (settings!.Theme == (int)Themes.Light)
        {
            settings.Theme = (int)Themes.Light;
            SettingsStore.Instance!.Update(settings);
        }
        PrimaryColor primaryColor = PrimaryColor.DeepPurple;
        Color primary = SwatchHelper.Lookup[(MaterialDesignColor)primaryColor];

        SecondaryColor secondaryColor = SecondaryColor.Teal;
        Color secondary = SwatchHelper.Lookup[(MaterialDesignColor)secondaryColor];

        PaletteHelper helper = new();
        var theme = helper.GetTheme();
        theme.SetPrimaryColor(primary);
        theme.SetSecondaryColor(secondary);
        theme.SetBaseTheme(Theme.Light);
        helper.SetTheme(theme);

        _themeViewModel.LightThemeChecked = true;
        _themeViewModel.DarkThemeChecked = false;
        _themeViewModel.GreyThemeChecked = false;
    }
}
