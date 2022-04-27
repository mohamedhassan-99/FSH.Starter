using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets;

public class CreateAssetRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProjectId { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateAssetRequestHandler : IRequestHandler<CreateAssetRequest, Guid>
{
    private readonly IRepository<Asset> _repository;
    private readonly IFileStorageService _file;

    public CreateAssetRequestHandler(IRepository<Asset> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateAssetRequest request, CancellationToken cancellationToken)
    {
        string assetImagePath = await _file.UploadAsync<Asset>(request.Image, FileType.Image, cancellationToken);

        var asset = new Asset(request.Name, request.Description, request.Rate, request.BrandId, request.CategoryId, request.ProjectId, assetImagePath);

        // Add Domain Events to be raised after the commit
        asset.DomainEvents.Add(EntityCreatedEvent.WithEntity(asset));

        await _repository.AddAsync(asset, cancellationToken);

        return asset.Id;
    }
}