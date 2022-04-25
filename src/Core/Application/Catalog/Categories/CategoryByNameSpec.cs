namespace FSH.Starter.Application.Catalog.Categories;

public class CategoryByNameSpec : Specification<Category>, ISingleResultSpecification
{
    public CategoryByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}