namespace FSH.Starter.Application.Catalog.Projects;

public class SearchProjectsRequest : PaginationFilter, IRequest<PaginationResponse<ProjectDto>>
{
}

public class ProjectsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Project, ProjectDto>
{
    public ProjectsBySearchRequestSpec(SearchProjectsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchProjectsRequestHandler : IRequestHandler<SearchProjectsRequest, PaginationResponse<ProjectDto>>
{
    private readonly IReadRepository<Project> _repository;

    public SearchProjectsRequestHandler(IReadRepository<Project> repository) => _repository = repository;

    public async Task<PaginationResponse<ProjectDto>> Handle(SearchProjectsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ProjectsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}