namespace FSH.Starter.Domain.Catalog;

public class Project : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public Project(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public Project Update(string? name, string? description, bool? isDeleted = null)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}