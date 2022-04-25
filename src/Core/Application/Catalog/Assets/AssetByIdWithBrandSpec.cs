namespace FSH.Starter.Application.Catalog.Assets;

public class AssetByIdWithBrandSpec : Specification<Asset, AssetDetailsDto>, ISingleResultSpecification
{
    public AssetByIdWithBrandSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Brand);
}