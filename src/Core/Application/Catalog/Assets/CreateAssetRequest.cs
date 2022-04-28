using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets;

public class CreateAssetRequest : IRequest<Guid>
{
    public string Name { get; private set; } = default!;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public string? Barcode { get; set; }
    public string? QrCode { get; set; }
    public string? Model { get; set; }
    public string? Vendor { get; set; }
    public decimal? Rate { get; private set; }
    public string? ImagePath { get; private set; }
    public Guid? BrandId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? ProjectId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public ICollection<Tag> Tags { get; set; }
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category Category { get; private set; } = default!;
    public virtual Project Project { get; private set; } = default!;
    public virtual Department Department { get; private set; } = default!;

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

        var asset = new Asset(
            request.Name,
            request.Summary,
            request.Description,
            request.Location,
            request.Longitude,
            request.Latitude,
            request.Barcode,
            request.QrCode,
            request.Model,
            request.Vendor,
            request.Rate,
            assetImagePath,
            request.BrandId,
            request.CategoryId,
            request.ProjectId,
            request.DepartmentId);

        // Add Domain Events to be raised after the commit
        asset.DomainEvents.Add(EntityCreatedEvent.WithEntity(asset));

        await _repository.AddAsync(asset, cancellationToken);

        return asset.Id;
    }
}