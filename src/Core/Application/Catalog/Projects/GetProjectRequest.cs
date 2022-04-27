namespace FSH.Starter.Application.Catalog.Projects;

public class GetProjectRequest : IRequest<ProjectDto>
{
    public Guid Id { get; set; }

    public GetProjectRequest(Guid id) => Id = id;
}

public class ProjectByIdSpec : Specification<Project, ProjectDto>, ISingleResultSpecification
{
    public ProjectByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetProjectRequestHandler : IRequestHandler<GetProjectRequest, ProjectDto>
{
    private readonly IRepository<Project> _repository;
    private readonly IStringLocalizer<GetProjectRequestHandler> _localizer;

    public GetProjectRequestHandler(IRepository<Project> repository, IStringLocalizer<GetProjectRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<ProjectDto> Handle(GetProjectRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Project, ProjectDto>)new ProjectByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["projects.notfound"], request.Id));
}