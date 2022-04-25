using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets.EventHandlers;

public class AssetCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Asset>>
{
    private readonly ILogger<AssetCreatedEventHandler> _logger;

    public AssetCreatedEventHandler(ILogger<AssetCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Asset> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}