namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByProjectSpec : Specification<Asset>
{
    public AssetsByProjectSpec(Guid projectId) =>
        Query.Where(p => p.ProjectId == projectId);
}
