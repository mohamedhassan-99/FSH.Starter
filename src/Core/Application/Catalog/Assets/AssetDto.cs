namespace FSH.Starter.Application.Catalog.Assets;

public class AssetDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public string? ImagePath { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid DepartmentId { get; set; }
    public IList<Guid> TagsIds { get; set; }
    public string BrandName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
    public string ProjectName { get; set; } = default!;
    public string DepartmentName { get; set; } = default!;
    public IList<string> TagsNames { get; set; } = default!;
}