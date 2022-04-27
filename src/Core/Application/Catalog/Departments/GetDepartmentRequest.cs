using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public class GetDepartmentRequest : IRequest<DepartmentDto>
{
    public Guid Id { get; set; }

    public GetDepartmentRequest(Guid id) => Id = id;
}

public class DepartmentByIdSpec : Specification<Department, DepartmentDto>, ISingleResultSpecification
{
    public DepartmentByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetDepartmentRequestHandler : IRequestHandler<GetDepartmentRequest, DepartmentDto>
{
    private readonly IRepository<Department> _repository;
    private readonly IStringLocalizer<GetDepartmentRequestHandler> _localizer;

    public GetDepartmentRequestHandler(IRepository<Department> repository, IStringLocalizer<GetDepartmentRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<DepartmentDto> Handle(GetDepartmentRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Department, DepartmentDto>)new DepartmentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["department.notfound"], request.Id));
}
