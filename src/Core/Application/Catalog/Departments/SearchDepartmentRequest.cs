using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public class SearchDepartmentRequest : PaginationFilter, IRequest<PaginationResponse<DepartmentDto>>
{

}

public class DepartmentBySearchRequestSpec : EntitiesByPaginationFilterSpec<Department, DepartmentDto>
{
    public DepartmentBySearchRequestSpec(SearchDepartmentRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}
public class SearchDepartmentRequestHandler : IRequestHandler<SearchDepartmentRequest, PaginationResponse<DepartmentDto>>
{
    private readonly IReadRepository<Department> _repository;

    public SearchDepartmentRequestHandler(IReadRepository<Department> repository) => _repository = repository;

    public async Task<PaginationResponse<DepartmentDto>> Handle(SearchDepartmentRequest request, CancellationToken cancellationToken)
    {
        var spec = new DepartmentBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}