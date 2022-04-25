using System.Reflection;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Infrastructure.Persistence.Context;
using FSH.Starter.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class CategorySeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<CategorySeeder> _logger;

    public CategorySeeder(ISerializerService serializerService, ILogger<CategorySeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Categories.Any())
        {
            _logger.LogInformation("Started to Seed Categories.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string categoryData = await File.ReadAllTextAsync(path + "/Catalog/Categories.json", cancellationToken);
            var categories = _serializerService.Deserialize<List<Category>>(categoryData);

            if (categories != null)
            {
                foreach (var category in categories)
                {
                    await _db.Categories.AddAsync(category, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Categories.");
        }
    }
}