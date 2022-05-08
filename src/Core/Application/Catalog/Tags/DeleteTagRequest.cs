using FSH.Starter.Application.Catalog.Assets;

namespace FSH.Starter.Application.Catalog.Tags;

public class DeleteTagRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteTagRequest(Guid id) => Id = id;
}

public class DeleteTagRequestHandler : IRequestHandler<DeleteTagRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Tag> _tagRepo;
    private readonly IReadRepository<Asset> _assetRepo;
    private readonly IStringLocalizer<DeleteTagRequestHandler> _localizer;

    public DeleteTagRequestHandler(IRepositoryWithEvents<Tag> tagRepo, IReadRepository<Asset> assetRepo, IStringLocalizer<DeleteTagRequestHandler> localizer) =>
        (_tagRepo, _assetRepo, _localizer) = (tagRepo, assetRepo, localizer);

    public async Task<Guid> Handle(DeleteTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepo.GetByIdAsync(request.Id, cancellationToken);
        
        if (await _assetRepo.AnyAsync(new AssetsByTagSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["tag.cannotbedeleted"]);
        }

        _ = tag ?? throw new NotFoundException(_localizer["tags.notfound"]);

        await _tagRepo.DeleteAsync(tag, cancellationToken);

        return request.Id;
    }
}