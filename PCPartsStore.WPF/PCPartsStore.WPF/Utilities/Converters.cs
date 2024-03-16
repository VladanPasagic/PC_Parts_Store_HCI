using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.ObservableDomainObjects;

namespace PCPartsStore.WPF.Utilities;

public static class Converters
{
    public static Article ConvertToArticle(ArticleOO article)
    {
        return new Article()
        {
            Name = article.Name,
            ArticleId = article.ArticleId,
            CategoryId = article.CategoryId,
            Manufacturer = ConvertToManufacturer(article.Manufacturer),
            Category = ConvertToCategory(article.Category),
            ManufacturerId = article.ManufacturerId,
            ManufacturingYear = article.ManufacturingYear,
            Price = article.Price,
            Quantity = article.Quantity,
        };
    }

    public static Category ConvertToCategory(CategoryOO category)
    {
        return new Category()
        {
            Name = category.Name,
            CategoryId = category.CategoryId,
        };
    }

    public static Manufacturer ConvertToManufacturer(ManufacturerOO manufacturer)
    {
        return new Manufacturer()
        {
            Name = manufacturer.Name,
            ManufacturerId = manufacturer.ManufacturerId,
            Country = manufacturer.Country,
        };
    }
}
