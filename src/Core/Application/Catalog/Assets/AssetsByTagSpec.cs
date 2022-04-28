using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Assets;

public class AssetsByTagSpec : Specification<Asset>
{
    public AssetsByTagSpec(Guid brandId) =>
        Query.Where(p => p.BrandId == brandId);
}
