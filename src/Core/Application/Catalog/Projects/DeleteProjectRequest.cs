using FSH.Starter.Application.Catalog.Assets;

namespace FSH.Starter.Application.Catalog.Projects;

public class DeleteProjectRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteProjectRequest(Guid id) => Id = id;
}

public class DeleteProjectRequestHandler : IRequestHandler<DeleteProjectRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Project> _projectRepo;
    private readonly IReadRepository<Asset> _assetRepo;
    private readonly IStringLocalizer<DeleteProjectRequestHandler> _localizer;

    public DeleteProjectRequestHandler(IRepositoryWithEvents<Project> projectRepo, IReadRepository<Asset> assetRepo, IStringLocalizer<DeleteProjectRequestHandler> localizer) =>
        (_projectRepo, _assetRepo, _localizer) = (projectRepo, assetRepo, localizer);

    public async Task<Guid> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        if (await _assetRepo.AnyAsync(new AssetsByProjectSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["project.cannotbedeleted"]);
        }

        var project = await _projectRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = project ?? throw new NotFoundException(_localizer["projects.notfound"]);

        await _projectRepo.DeleteAsync(project, cancellationToken);

        return request.Id;
    }
}