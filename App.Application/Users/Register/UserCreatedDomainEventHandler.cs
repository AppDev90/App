using App.Domain.Users.Event;
using MediatR;

namespace App.Application.Users.Register;

internal class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var id = notification.UserId;
        return Task.CompletedTask;
    }
}
