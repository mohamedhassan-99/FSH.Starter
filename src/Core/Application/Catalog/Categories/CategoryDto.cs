namespace FSH.Starter.Application.Catalog.Categories;

public class CategoryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}