using FSH.Starter.Application.Catalog.Brands;
using FSH.Starter.Application.Catalog.Categories;
using FSH.Starter.Application.Catalog.Departments;
using FSH.Starter.Application.Catalog.Projects;
using FSH.Starter.Application.Catalog.Tags;

namespace FSH.Starter.Application.Catalog.Assets;

public class AssetDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Summary { get;   set; }
    public string? Description { get;   set; }
    public string? Location { get;   set; }
    public string? Longitude { get;   set; }
    public string? Latitude { get;   set; }
    public string? Barcode { get;   set; }
    public string? QrCode { get;   set; }
    public string? Model { get;   set; }
    public string? Vendor { get;   set; }
    public decimal? Rate { get;   set; }
    public string? ImagePath { get;   set; }
    public BrandDto Brand { get; set; } = default!;
    public CategoryDto Category { get; set; } = default!;
    public ProjectDto Project { get; set; } = default!;
    public DepartmentDto Department { get; set; } = default!;
    public TagDto Tag { get; set; } = default!;
    public IList<TagDto> Tags { get; set; } = default!;
}