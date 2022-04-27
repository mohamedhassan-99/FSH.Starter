using FSH.Starter.Application.Identity.Roles;
using FSH.Starter.Application.Identity.Users;

namespace FSH.Starter.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Brand> _brandRepo;
    private readonly IReadRepository<Category> _categoryRepo;
    private readonly IReadRepository<Project> _projectRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<Asset> _assetRepo;
    private readonly IStringLocalizer<GetStatsRequestHandler> _localizer;

    public GetStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Brand> brandRepo, IReadRepository<Category> categoryRepo, IReadRepository<Product> productRepo, IReadRepository<Project> projectRepo, IReadRepository<Asset> assetRepo, IStringLocalizer<GetStatsRequestHandler> localizer)
    {
        _userService = userService;
        _roleService = roleService;
        _brandRepo = brandRepo;
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
        _projectRepo = projectRepo;
        _assetRepo = assetRepo;
        _localizer = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            AssetCount = await _assetRepo.CountAsync(cancellationToken),
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            BrandCount = await _brandRepo.CountAsync(cancellationToken),
            CategoryCount = await _categoryRepo.CountAsync(cancellationToken),
            ProjectCount = await _projectRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken)
        };

        int selectedYear = DateTime.Now.Year;
        double[] assetsFigure = new double[13];
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];
        double[] categoriesFigure = new double[13];
        double[] projectsFigure = new double[13];
        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01);
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

            var assetSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Asset>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);
            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var categorySpec = new AuditableEntitiesByCreatedOnBetweenSpec<Category>(filterStartDate, filterEndDate);
            var projectSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Project>(filterStartDate, filterEndDate);

            assetsFigure[i - 1] = await _assetRepo.CountAsync(assetSpec, cancellationToken);
            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
            brandsFigure[i - 1] = await _brandRepo.CountAsync(brandSpec, cancellationToken);
            categoriesFigure[i - 1] = await _categoryRepo.CountAsync(categorySpec, cancellationToken);
            projectsFigure[i - 1] = await _projectRepo.CountAsync(projectSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Assets"], Data = assetsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Products"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Brands"], Data = brandsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Categories"], Data = categoriesFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Projects"], Data = categoriesFigure });

        return stats;
    }
}