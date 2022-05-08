using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByDepartmentSpec : Specification<Asset>
{
    public AssetsByDepartmentSpec(Guid departmentId) =>
        Query.Where(p => p.DepartmentId == departmentId);
}
