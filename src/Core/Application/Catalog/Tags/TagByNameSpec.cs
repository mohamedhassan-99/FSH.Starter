namespace FSH.Starter.Application.Catalog.Tags;

public class TagByNameSpec : Specification<Tag>, ISingleResultSpecification
{
    public TagByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}