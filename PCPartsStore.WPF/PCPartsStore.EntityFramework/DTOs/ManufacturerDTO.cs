namespace PCPartsStore.EntityFramework.DTOs;

public partial class ManufacturerDTO
{
    public int ManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();
}
