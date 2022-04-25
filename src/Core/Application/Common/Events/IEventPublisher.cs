using FSH.Starter.Shared.Events;

namespace FSH.Starter.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}