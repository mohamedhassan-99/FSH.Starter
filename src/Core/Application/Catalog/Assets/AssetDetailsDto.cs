using FSH.Starter.Application.Catalog.Brands;
using FSH.Starter.Application.Catalog.Categories;

namespace FSH.Starter.Application.Catalog.Assets;

public class AssetDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public string? ImagePath { get; set; }
    public BrandDto Brand { get; set; } = default!;
    public CategoryDto Category { get; set; } = default!;
}