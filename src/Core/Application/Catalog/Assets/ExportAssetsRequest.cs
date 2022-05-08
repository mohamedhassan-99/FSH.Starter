using FSH.Starter.Application.Common.Exporters;

namespace FSH.Starter.Application.Catalog.Assets;

public class ExportAssetsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? DepartmentId { get; set; }
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

        if (!request.ProjectId.HasValue && !request.CategoryId.HasValue && !request.DepartmentId.HasValue)
        {
            var spec = new ExportAssetsWithBrandsSpecification(request);
            var list = await _repository.ListAsync(spec, cancellationToken);
            return _excelWriter.WriteToStream(list);
        }
        else if (!request.ProjectId.HasValue && !request.BrandId.HasValue && !request.DepartmentId.HasValue)
        {
            var spec = new ExportAssetsWithCategoriesSpecification(request);
            var list = await _repository.ListAsync(spec, cancellationToken);
            return _excelWriter.WriteToStream(list);
        }
        else if (!request.ProjectId.HasValue && !request.BrandId.HasValue && !request.CategoryId.HasValue)
        {
            var spec = new ExportAssetsWithDepartmentsSpecification(request);
            var list = await _repository.ListAsync(spec, cancellationToken);
            return _excelWriter.WriteToStream(list);
        }
        else
        {
            var spec = new ExportAssetsWithProjectsSpecification(request);
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
    public class ExportAssetsWithDepartmentsSpecification : EntitiesByBaseFilterSpec<Asset, AssetExportDto>
    {
        public ExportAssetsWithDepartmentsSpecification(ExportAssetsRequest request)
            : base(request) =>
            Query
                .Include(p => p.Department)
                .Where(p => p.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue)
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
    public class ExportAssetsWithProjectsSpecification : EntitiesByBaseFilterSpec<Asset, AssetExportDto>
    {
        public ExportAssetsWithProjectsSpecification(ExportAssetsRequest request)
            : base(request) =>
            Query
                .Include(p => p.Project)
                .Where(p => p.ProjectId.Equals(request.ProjectId!.Value), request.ProjectId.HasValue)
                .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
                .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
    }
}