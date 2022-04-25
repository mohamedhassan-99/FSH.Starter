using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets;

public class DeleteAssetRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteAssetRequest(Guid id) => Id = id;
}

public class DeleteAssetRequestHandler : IRequestHandler<DeleteAssetRequest, Guid>
{
    private readonly IRepository<Asset> _repository;
    private readonly IStringLocalizer<DeleteAssetRequestHandler> _localizer;

    public DeleteAssetRequestHandler(IRepository<Asset> repository, IStringLocalizer<DeleteAssetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteAssetRequest request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = asset ?? throw new NotFoundException(_localizer["asset.notfound"]);

        // Add Domain Events to be raised after the commit
        asset.DomainEvents.Add(EntityDeletedEvent.WithEntity(asset));

        await _repository.DeleteAsync(asset, cancellationToken);

        return request.Id;
    }
}