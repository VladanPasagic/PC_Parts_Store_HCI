using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class AddSingleItemCommand : CommandBase
{
    private const string SelectArticleFirst = "SelectArticleFirstError";
    private const string NoMoreArticlesLeft = "NoMoreArticlesLeftError";
    private readonly SellerViewModel _sellerViewModel;

    public AddSingleItemCommand(SellerViewModel sellerViewModel)
    {
        _sellerViewModel = sellerViewModel;
    }

    public override void Execute(object? parameter)
    {
        var article = _sellerViewModel.SelectedArticle;

        if (article == null)
        {
            _sellerViewModel.ErrorMessage = LocalizedStrings.Instance[SelectArticleFirst];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel);
            return;
        }

        if (article.Quantity == 0)
        {
            _sellerViewModel.ErrorMessage = LocalizedStrings.Instance[NoMoreArticlesLeft];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel);
            return;
        }

        var item = _sellerViewModel.Items.FirstOrDefault(i => i.ArticleId == article.ArticleId);

        if (item == null)
        {
            _sellerViewModel.Items.Add(new ItemOO()
            {
                Article = article,
                ArticleId = article.ArticleId,
                Quantity = 1,
                Price = article.Price,
            });
        }
        else
        {
            item.Quantity++;
        }

        _sellerViewModel.Price += article.Price;

        article.Quantity--;
    }
}
