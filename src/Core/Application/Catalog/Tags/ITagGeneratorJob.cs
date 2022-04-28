using System.ComponentModel;

namespace FSH.Starter.Application.Catalog.Tags;

public interface ITagGeneratorJob : IScopedService
{
    [DisplayName("Generate Random Tag example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all Random Tags created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}