using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Utilities;
using System.Security.Policy;

namespace PCPartsStore.WPF.Stores;

public class ProductStore
{
    public static ProductStore? Instance { get; private set; }

    private readonly IGetAllProductsQuery _getAllProductsQuery;
    private readonly ICreateArticleCommand _createArticleCommand;
    private readonly IUpdateArticleCommand _updateArticleCommand;
    private readonly IDeleteArticleCommand _deleteArticleCommand;

    private readonly List<ArticleOO> _products = [];

    public IEnumerable<ArticleOO> Products => _products;

    public event Action? ProductsRead;
    public event Action<ArticleOO>? ProductCreated;
    public event Action<ArticleOO>? ProductUpdated;
    public event Action<int>? ProductDeleted;

    public static void CreateInstance(IGetAllProductsQuery getAllProductsQuery, ICreateArticleCommand createArticleCommand,
        IUpdateArticleCommand updateArticleCommand, IDeleteArticleCommand deleteArticleCommand)
    {
        Instance ??= new(getAllProductsQuery, createArticleCommand, updateArticleCommand, deleteArticleCommand);
    }

    private ProductStore(IGetAllProductsQuery getAllProductsQuery, ICreateArticleCommand createArticleCommand,
        IUpdateArticleCommand updateArticleCommand, IDeleteArticleCommand deleteArticleCommand)
    {
        _getAllProductsQuery = getAllProductsQuery;
        _createArticleCommand = createArticleCommand;
        _updateArticleCommand = updateArticleCommand;
        _deleteArticleCommand = deleteArticleCommand;
    }

    public async Task Read()
    {
        IEnumerable<Article> articles = await _getAllProductsQuery.Execute();

        _products.Clear();

        foreach (var article in articles)
        {
            _products.Add(new ArticleOO(article));
        }

        ProductsRead?.Invoke();
    }

    public async Task Delete(int id)
    {
        await _deleteArticleCommand.ExecuteAsync(id);

        int currentIndex = _products.FindIndex(p => p.ArticleId == id);

        if (currentIndex == -1)
        {
            return;
        }

        _products.RemoveAt(currentIndex);

        ProductDeleted?.Invoke(id);
    }

    public async Task Create(ArticleOO article)
    {
        await _createArticleCommand.ExecuteAsync(Converters.ConvertToArticle(article));

        _products.Add(article);

        ProductCreated?.Invoke(article);
    }

    public async Task Update(ArticleOO article)
    {
        await _updateArticleCommand.ExecuteAsync(Converters.ConvertToArticle(article));

        int currentIndex = _products.FindIndex(p => p.ArticleId == article.ArticleId);

        if (currentIndex != -1)
        {
            _products[currentIndex] = article;
        }

        ProductUpdated?.Invoke(article);
    }
}
