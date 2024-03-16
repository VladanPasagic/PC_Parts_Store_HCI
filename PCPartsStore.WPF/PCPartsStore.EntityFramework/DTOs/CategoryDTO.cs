namespace PCPartsStore.EntityFramework.DTOs;

public partial class CategoryDTO
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();

}
