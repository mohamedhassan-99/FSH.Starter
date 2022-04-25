using Mapster;

namespace FSH.Starter.Application.Catalog.Assets;

public class GetAssetViaDapperRequest : IRequest<AssetDto>
{
    public Guid Id { get; set; }

    public GetAssetViaDapperRequest(Guid id) => Id = id;
}

public class GetAssetViaDapperRequestHandler : IRequestHandler<GetAssetViaDapperRequest, AssetDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<GetAssetViaDapperRequestHandler> _localizer;

    public GetAssetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetAssetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AssetDto> Handle(GetAssetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.QueryFirstOrDefaultAsync<Asset>(
            $"SELECT * FROM public.\"Assets\" WHERE \"Id\"  = '{request.Id}' AND \"Tenant\" = '@tenant'", cancellationToken: cancellationToken);

        _ = product ?? throw new NotFoundException(string.Format(_localizer["asset.notfound"], request.Id));

        return product.Adapt<AssetDto>();
    }
}