using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets;

public class UpdateAssetRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
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
    public IList<Guid> TagsIds { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid DepartmentId { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
    public FileUploadRequest? Image { get; set; }
}

public class UpdateAssetRequestHandler : IRequestHandler<UpdateAssetRequest, Guid>
{
    private readonly IRepository<Asset> _repository;
    private readonly IStringLocalizer<UpdateAssetRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public UpdateAssetRequestHandler(IRepository<Asset> repository, IStringLocalizer<UpdateAssetRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateAssetRequest request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = asset ?? throw new NotFoundException(string.Format(_localizer["asset.notfound"], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentAssetImagePath = asset.ImagePath;
            if (!string.IsNullOrEmpty(currentAssetImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentAssetImagePath));
            }

            asset = asset.ClearImagePath();
        }

        string? assetImagePath = request.Image is not null
            ? await _file.UploadAsync<Asset>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedAsset = asset.Update(
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
        asset.DomainEvents.Add(EntityUpdatedEvent.WithEntity(asset));

        await _repository.UpdateAsync(updatedAsset, cancellationToken);

        return request.Id;
    }
}