namespace FSH.Starter.Application.Catalog.Tags;
public class TagDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Color { get; set; }

}
