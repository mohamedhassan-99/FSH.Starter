using FSH.Starter.Application.Common.Exporters;

namespace FSH.Starter.Application.Catalog.Assets;

public class ExportAssetsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class ExportAssetsRequestHandler : IRequestHandler<ExportAssetsRequest, Stream>
{
    private readonly IReadRepository<Asset> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportAssetsRequestHandler(IReadRepository<Asset> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportAssetsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportAssetsWithBrandsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportAssetsWithBrandsSpecification : EntitiesByBaseFilterSpec<Asset, AssetExportDto>
{
    public ExportAssetsWithBrandsSpecification(ExportAssetsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Brand)
            .Where(p => p.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}
public class ExportAssetsWithCategoriesSpecification : EntitiesByBaseFilterSpec<Asset, AssetExportDto>
{
    public ExportAssetsWithCategoriesSpecification(ExportAssetsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Category)
            .Where(p => p.CategoryId.Equals(request.CategoryId!.Value), request.CategoryId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}