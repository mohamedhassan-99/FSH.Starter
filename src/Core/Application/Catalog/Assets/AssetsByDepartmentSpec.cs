using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByDepartmentSpec : Specification<Asset>
{
    public AssetsByDepartmentSpec(Guid brandId) =>
        Query.Where(p => p.BrandId == brandId);
}
