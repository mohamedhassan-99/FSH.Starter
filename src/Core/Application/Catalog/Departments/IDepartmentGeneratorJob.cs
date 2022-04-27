using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public interface IDepartmentGeneratorJob : IScopedService
{
    [DisplayName("Generate Random Department example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all radom departments created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}
