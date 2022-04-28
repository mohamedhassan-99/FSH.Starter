namespace FSH.Starter.Application.Catalog.Tags;

public class SearchTagsRequest : PaginationFilter, IRequest<PaginationResponse<TagDto>>
{
}

public class TagsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Tag, TagDto>
{
    public TagsBySearchRequestSpec(SearchTagsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchTagsRequestHandler : IRequestHandler<SearchTagsRequest, PaginationResponse<TagDto>>
{
    private readonly IReadRepository<Tag> _repository;

    public SearchTagsRequestHandler(IReadRepository<Tag> repository) => _repository = repository;

    public async Task<PaginationResponse<TagDto>> Handle(SearchTagsRequest request, CancellationToken cancellationToken)
    {
        var spec = new TagsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}