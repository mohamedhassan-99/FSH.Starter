namespace FSH.Starter.Application.Catalog.Assets;

public class AssetByIdSpec : Specification<Asset>, ISingleResultSpecification
{
    public AssetByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
        .Include(p => p.Brand)
        .Include(p => p.Category)
        .Include(p => p.Project)
        .Include(p => p.Tags);
}