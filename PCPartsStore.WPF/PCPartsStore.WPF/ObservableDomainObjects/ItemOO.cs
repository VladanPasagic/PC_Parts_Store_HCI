using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Utilities;

namespace PCPartsStore.WPF.ObservableDomainObjects;

public class ItemOO : ObservableObject
{
    private Item _item;

    public ItemOO()
    {
        _item = new();
    }

    public ItemOO(Item item)
    {
        _item = new();
        Quantity = item.Quantity;
        Price = item.Price;
        ArticleId = item.ArticleId;
        ReceiptId = item.ReceiptId;
        Receipt = item.Receipt;
        Article = new(item.Article);
    }

    public int Quantity
    {
        get => _item.Quantity;
        set
        {
            _item.Quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    public int ReceiptId
    {
        get => _item.ReceiptId;
        set
        {
            _item.ReceiptId = value;
            OnPropertyChanged(nameof(ReceiptId));
        }
    }

    public int ArticleId
    {
        get => _item.ArticleId;
        set
        {
            _item.ArticleId = value;
            OnPropertyChanged(nameof(ArticleId));
        }
    }

    public decimal Price
    {
        get => _item.Price;
        set
        {
            _item.Price = value;
            OnPropertyChanged(nameof(Price));
        }
    }

    public virtual ArticleOO Article
    {
        get => new(_item.Article);
        set
        {
            _item.Article = Converters.ConvertToArticle(value);
            OnPropertyChanged(nameof(Article));
        }
    }

    public virtual Receipt Receipt
    {
        get => _item.Receipt;
        set
        {
            _item.Receipt = value;
            OnPropertyChanged(nameof(Receipt));
        }
    }
}
