using FSH.Starter.Domain.Common.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.Starter.Application.Catalog.Assets;

public class CreateAssetRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public string? Barcode { get; set; }
    public string? QrCode { get; set; }
    public string? Model { get; set; }
    public string? Vendor { get; set; }
    public decimal? Rate { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? DepartmentId { get; set; }
    public IList<Guid>? TagsIds { get; set; }


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
            request.TagsIds,
            request.BrandId,
            request.CategoryId,
            request.ProjectId,
            request.DepartmentId
            );

        // Add Domain Events to be raised after the commit
        asset.DomainEvents.Add(EntityCreatedEvent.WithEntity(asset));

        await _repository.AddAsync(asset, cancellationToken);

        return asset.Id;
    }
}