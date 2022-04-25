namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsBySearchRequestWithCategoriesSpec : EntitiesByPaginationFilterSpec<Asset, AssetDto>
{
    public AssetsBySearchRequestWithCategoriesSpec(SearchAssetsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Category)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.CategoryId.Equals(request.CategoryId!.Value), request.CategoryId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}