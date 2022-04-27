namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsBySearchRequestWithProjectsSpec : EntitiesByPaginationFilterSpec<Asset, AssetDto>
{
    public AssetsBySearchRequestWithProjectsSpec(SearchAssetsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Project)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.ProjectId.Equals(request.ProjectId!.Value), request.ProjectId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}