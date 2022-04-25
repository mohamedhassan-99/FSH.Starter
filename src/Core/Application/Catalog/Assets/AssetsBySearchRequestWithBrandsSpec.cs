namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsBySearchRequestWithBrandsSpec : EntitiesByPaginationFilterSpec<Asset, AssetDto>
{
    public AssetsBySearchRequestWithBrandsSpec(SearchAssetsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Brand)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}