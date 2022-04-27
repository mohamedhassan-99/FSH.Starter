namespace FSH.Starter.Application.Catalog.Projects;

public class ProjectDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}