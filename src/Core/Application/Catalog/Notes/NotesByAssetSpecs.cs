using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class NotesByAssetSpecs : Specification<Note>
{
    public NotesByAssetSpecs(Guid assetId)
    {
        Query.Where(s => s.AssetId == assetId);
    }
}


 