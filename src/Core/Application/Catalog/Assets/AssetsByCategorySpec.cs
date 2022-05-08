namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByCategorySpec : Specification<Asset>
{
    public AssetsByCategorySpec(Guid categoryId) =>
        Query.Where(p => p.CategoryId == categoryId);
}
