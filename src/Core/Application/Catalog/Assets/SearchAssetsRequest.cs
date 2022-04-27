namespace FSH.Starter.Application.Catalog.Assets;

public class SearchAssetsRequest : PaginationFilter, IRequest<PaginationResponse<AssetDto>>
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? ProjectId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class SearchAssetsRequestHandler : IRequestHandler<SearchAssetsRequest, PaginationResponse<AssetDto>>
{
    private readonly IReadRepository<Asset> _repository;

    public SearchAssetsRequestHandler(IReadRepository<Asset> repository) => _repository = repository;

    public async Task<PaginationResponse<AssetDto>> Handle(SearchAssetsRequest request, CancellationToken cancellationToken)
    {
        var spec = new AssetsBySearchRequestWithCategoriesSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}