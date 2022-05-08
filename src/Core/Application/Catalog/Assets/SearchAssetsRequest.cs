namespace FSH.Starter.Application.Catalog.Assets;

public class SearchAssetsRequest : PaginationFilter, IRequest<PaginationResponse<AssetDto>>
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? DepartmentId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class SearchAssetsRequestByBrandHandler : IRequestHandler<SearchAssetsRequest, PaginationResponse<AssetDto>>
{
    private readonly IReadRepository<Asset> _repository;

    public SearchAssetsRequestByBrandHandler(IReadRepository<Asset> repository) => _repository = repository;

    public async Task<PaginationResponse<AssetDto>> Handle(SearchAssetsRequest request, CancellationToken cancellationToken)
    {
        if (request.BrandId.HasValue)
        {
            var spec = new AssetsBySearchRequestWithBrandsSpec(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        }
        else if (request.DepartmentId.HasValue)
        {
            var spec = new AssetsBySearchRequestWithDepartmentsSpec(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        }
        else if (request.CategoryId.HasValue)
        {
            var spec = new AssetsBySearchRequestWithCategoriesSpec(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        }
        else
        {
            var spec = new AssetsBySearchRequestWithProjectsSpec(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        }


    }
}
