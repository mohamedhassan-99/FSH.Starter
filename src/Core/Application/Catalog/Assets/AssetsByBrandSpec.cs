namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByBrandSpec : Specification<Asset>
{
    public AssetsByBrandSpec(Guid brandId) =>
        Query.Where(p => p.BrandId == brandId);
}
