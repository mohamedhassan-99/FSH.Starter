namespace FSH.Starter.Application.Catalog.Assets;

public class AssetByIdWithCategorySpec : Specification<Asset, AssetDetailsDto>, ISingleResultSpecification
{
    public AssetByIdWithCategorySpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Category);
}