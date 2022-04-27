using System.Reflection;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Infrastructure.Persistence.Context;
using FSH.Starter.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class ProjectSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ProjectSeeder> _logger;

    public ProjectSeeder(ISerializerService serializerService, ILogger<ProjectSeeder> logger, ApplicationDbContext db)
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
            _logger.LogInformation("Started to Seed Projects.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string projectData = await File.ReadAllTextAsync(path + "/Catalog/Projects.json", cancellationToken);
            var projects = _serializerService.Deserialize<List<Project>>(projectData);

            if (projects != null)
            {
                foreach (var project in projects)
                {
                    await _db.Projects.AddAsync(project, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Projects.");
        }
    }
}