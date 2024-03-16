using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace PCPartsStore.WPF.Resources;

public class LocalizedStrings
{
    private LocalizedStrings()
    {

    }

    public static LocalizedStrings Instance { get; } = new LocalizedStrings();

    public void SetCulture(string cultureCode)
    {
        CultureInfo cultureInfo = new(cultureCode);
        LocalizeDictionary.Instance.Culture = cultureInfo;
    }

    public string? this[string key]
    {
        get
        {
            var result = LocalizeDictionary.Instance.GetLocalizedObject("PCPartsStore.WPF", "Translation", key, LocalizeDictionary.Instance.Culture);
            return result as string;
        }
    }
}
