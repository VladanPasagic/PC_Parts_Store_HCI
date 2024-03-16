using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace PCPartsStore.WPF.Commands;

class ChangeLanguageSerbianCommand : CommandBase
{
    private readonly ILanguageViewModel _languageViewModel;

    public ChangeLanguageSerbianCommand(ILanguageViewModel languageViewModel)
    {
        _languageViewModel = languageViewModel;
    }

    public override void Execute(object? parameter)
    {
        var settings = SettingsStore.Instance!.CurrentSetting!;
        if (settings.Language == (int)Languages.English)
        {
            settings.Language = (int)Languages.Serbian;
            SettingsStore.Instance!.Update(settings);
        }
        LocalizeDictionary.Instance.Culture = new CultureInfo("sr-Latn-BA");
        _languageViewModel.EnglishLanguageChecked = false;
        _languageViewModel.SerbianLanguageChecked = true;
    }
}
