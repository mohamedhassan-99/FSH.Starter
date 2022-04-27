using System.ComponentModel;

namespace FSH.Starter.Application.Catalog.Projects;

public interface IProjectGeneratorJob : IScopedService
{
    [DisplayName("Generate Random Project example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all random Projects created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}