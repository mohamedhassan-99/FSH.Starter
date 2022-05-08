namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsBySearchRequestWithDepartmentsSpec : EntitiesByPaginationFilterSpec<Asset, AssetDto>
{
    public AssetsBySearchRequestWithDepartmentsSpec(SearchAssetsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Department)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}