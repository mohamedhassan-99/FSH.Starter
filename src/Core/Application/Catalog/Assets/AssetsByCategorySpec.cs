namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByCategorySpec : Specification<Asset>
{
    public AssetsByCategorySpec(Guid brandId) =>
        Query.Where(p => p.BrandId == brandId);
}
