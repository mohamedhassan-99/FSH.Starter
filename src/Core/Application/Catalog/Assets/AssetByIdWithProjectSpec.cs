namespace FSH.Starter.Application.Catalog.Assets;

public class AssetByIdWithProjectSpec : Specification<Asset, AssetDetailsDto>, ISingleResultSpecification
{
    public AssetByIdWithProjectSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Project);
}