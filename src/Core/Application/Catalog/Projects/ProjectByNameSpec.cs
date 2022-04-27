namespace FSH.Starter.Application.Catalog.Projects;

public class ProjectByNameSpec : Specification<Project>, ISingleResultSpecification
{
    public ProjectByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}