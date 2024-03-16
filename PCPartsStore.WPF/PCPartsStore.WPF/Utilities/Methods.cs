using PCPartsStore.WPF.ViewModels.Interfaces;

namespace PCPartsStore.WPF.Utilities;

public static class Methods
{
    public static async Task WaitAndRemoveErrorMessage(IErrorViewModel errorViewModel)
    {
        await Task.Delay(3000);
        errorViewModel.ErrorMessage = null;
    }
}
