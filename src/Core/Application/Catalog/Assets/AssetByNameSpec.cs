namespace FSH.Starter.Application.Catalog.Assets;

public class AssetByNameSpec : Specification<Asset>, ISingleResultSpecification
{
    public AssetByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}