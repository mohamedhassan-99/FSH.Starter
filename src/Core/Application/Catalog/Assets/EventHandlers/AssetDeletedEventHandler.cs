using FSH.Starter.Domain.Common.Events;

namespace FSH.Starter.Application.Catalog.Assets.EventHandlers;

public class AssetDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Asset>>
{
    private readonly ILogger<AssetDeletedEventHandler> _logger;

    public AssetDeletedEventHandler(ILogger<AssetDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Asset> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}