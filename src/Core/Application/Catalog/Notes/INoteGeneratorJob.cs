using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public interface INoteGeneratorJob : IScopedService
{
    [DisplayName("Generate Random Note example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all radom Notes created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}
