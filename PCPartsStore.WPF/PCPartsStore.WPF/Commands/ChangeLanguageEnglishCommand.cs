using WPFLocalizeExtension.Engine;
using System.Globalization;
using PCPartsStore.WPF.ViewModels.Interfaces;
using PCPartsStore.WPF.Stores;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.WPF.Commands;

class ChangeLanguageEnglishCommand : CommandBase
{
    private readonly ILanguageViewModel _languageViewModel;

    public ChangeLanguageEnglishCommand(ILanguageViewModel languageViewModel)
    {
        _languageViewModel = languageViewModel;
    }

    public override void Execute(object? parameter)
    {
        var settings = SettingsStore.Instance!.CurrentSetting!;
        if (settings.Language == (int)Languages.Serbian)
        {
            settings.Language = (int)Languages.English;
            SettingsStore.Instance!.Update(settings);
        }
        LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");
        _languageViewModel.EnglishLanguageChecked = true;
        _languageViewModel.SerbianLanguageChecked = false;
    }
}
