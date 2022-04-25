using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Products.EventHandlers;

public class AssetUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Product>>
{
    private readonly ILogger<AssetUpdatedEventHandler> _logger;

    public AssetUpdatedEventHandler(ILogger<AssetUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Product> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}