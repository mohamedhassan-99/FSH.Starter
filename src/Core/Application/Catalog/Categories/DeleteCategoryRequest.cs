using FSH.Starter.Application.Catalog.Assets;

namespace FSH.Starter.Application.Catalog.Categories;

public class DeleteCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteCategoryRequest(Guid id) => Id = id;
}

public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _categoryRepo;
    private readonly IReadRepository<Asset> _assetRepo;
    private readonly IStringLocalizer<DeleteCategoryRequestHandler> _localizer;

    public DeleteCategoryRequestHandler(IRepositoryWithEvents<Category> categoryRepo, IReadRepository<Asset> assetRepo, IStringLocalizer<DeleteCategoryRequestHandler> localizer) =>
        (_categoryRepo, _assetRepo, _localizer) = (categoryRepo, assetRepo, localizer);

    public async Task<Guid> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        if (await _assetRepo.AnyAsync(new AssetsByCategorySpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["category.cannotbedeleted"]);
        }

        var category = await _categoryRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(_localizer["category.notfound"]);

        await _categoryRepo.DeleteAsync(category, cancellationToken);

        return request.Id;
    }
}