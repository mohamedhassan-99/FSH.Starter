using FSH.Starter.Application.Catalog.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public class DeleteDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteDepartmentRequest(Guid id) => Id = id;
}
public class DeleteDepartmentRequestHandler : IRequestHandler<DeleteDepartmentRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Department> _departmentRepo;
    private readonly IReadRepository<Asset> _assetRepo;
    private readonly IStringLocalizer<DeleteDepartmentRequestHandler> _localizer;

    public DeleteDepartmentRequestHandler(IRepositoryWithEvents<Department> departmentRepo, IReadRepository<Asset> assetRepo, IStringLocalizer<DeleteDepartmentRequestHandler> localizer) =>
        (_departmentRepo, _assetRepo, _localizer) = (departmentRepo, assetRepo, localizer);

    public async Task<Guid> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (await _assetRepo.AnyAsync(new AssetsByDepartmentSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["department.cannotbedeleted"]);
        }

        var department = await _departmentRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = department ?? throw new NotFoundException(_localizer["department.notfound"]);

        await _departmentRepo.DeleteAsync(department, cancellationToken);

        return request.Id;
    }
}
