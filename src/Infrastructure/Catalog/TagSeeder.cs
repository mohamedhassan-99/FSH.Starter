using System.Reflection;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Infrastructure.Persistence.Context;
using FSH.Starter.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class TagSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<TagSeeder> _logger;

    public TagSeeder(ISerializerService serializerService, ILogger<TagSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Projects.Any())
        {
            _logger.LogInformation("Started to Seed Tags.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string tagData = await File.ReadAllTextAsync(path + "/Catalog/Tags.json", cancellationToken);
            var tags = _serializerService.Deserialize<List<Tag>>(tagData);

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    await _db.Tags.AddAsync(tag, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Tags.");
        }
    }
}