using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Media;

namespace PCPartsStore.WPF.Commands;

public class ChangeToGreyThemeCommand : CommandBase
{
    private readonly IThemeViewModel _themeViewModel;

    public ChangeToGreyThemeCommand(IThemeViewModel themeViewModel)
    {
        _themeViewModel = themeViewModel;
    }

    public override void Execute(object? parameter)
    {
        var settings = SettingsStore.Instance!.CurrentSetting;

        if (settings!.Theme != (int)Themes.Grey)
        {
            settings.Theme = (int)Themes.Grey;
            SettingsStore.Instance!.Update(settings);
        }
        PrimaryColor primaryColor = PrimaryColor.Red;
        Color primary = SwatchHelper.Lookup[(MaterialDesignColor)primaryColor];

        SecondaryColor secondaryColor = SecondaryColor.Green;
        Color secondary = SwatchHelper.Lookup[(MaterialDesignColor)secondaryColor];

        PaletteHelper helper = new();
        var theme = helper.GetTheme();
        theme.SetPrimaryColor(primary);
        theme.SetSecondaryColor(secondary);
        theme.Paper = SwatchHelper.Lookup[((MaterialDesignColor)PrimaryColor.BlueGrey)];
        helper.SetTheme(theme);

        _themeViewModel.LightThemeChecked = false;
        _themeViewModel.DarkThemeChecked = false;
        _themeViewModel.GreyThemeChecked = true;
    }
}
