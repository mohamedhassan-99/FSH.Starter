using System.ComponentModel;

namespace FSH.Starter.Application.Catalog.Categories;

public interface ICategoryGeneratorJob : IScopedService
{
    [DisplayName("Generate Random Category example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all radom categories created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}