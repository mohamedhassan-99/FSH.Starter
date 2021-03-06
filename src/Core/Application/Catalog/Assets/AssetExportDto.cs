namespace FSH.Starter.Application.Catalog.Assets;

public class AssetExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Rate { get; set; } = default!;
    public string BrandName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
    public string ProjectName { get; set; } = default!;
    public string DepartmentName { get; set; } = default!;
    public IList<string> TagsNames { get; set; } = default!;
}