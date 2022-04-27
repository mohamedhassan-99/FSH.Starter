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
