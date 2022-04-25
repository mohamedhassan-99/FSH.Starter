using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Products.EventHandlers;

public class AssetDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Product>>
{
    private readonly ILogger<AssetDeletedEventHandler> _logger;

    public AssetDeletedEventHandler(ILogger<AssetDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Product> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}