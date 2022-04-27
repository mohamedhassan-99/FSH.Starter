namespace FSH.Starter.Application.Catalog.Assets;

public class GetAssetRequest : IRequest<AssetDetailsDto>
{
    public Guid Id { get; set; }

    public GetAssetRequest(Guid id) => Id = id;
}

public class GetAssetRequestHandler : IRequestHandler<GetAssetRequest, AssetDetailsDto>
{
    private readonly IRepository<Asset> _repository;
    private readonly IStringLocalizer<GetAssetRequestHandler> _localizer;

    public GetAssetRequestHandler(IRepository<Asset> repository, IStringLocalizer<GetAssetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AssetDetailsDto> Handle(GetAssetRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Asset, AssetDetailsDto>)new AssetByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["asset.notfound"], request.Id));
}