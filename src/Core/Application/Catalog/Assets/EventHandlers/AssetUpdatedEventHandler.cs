using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets.EventHandlers;

public class AssetUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Asset>>
{
    private readonly ILogger<AssetUpdatedEventHandler> _logger;

    public AssetUpdatedEventHandler(ILogger<AssetUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Asset> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}