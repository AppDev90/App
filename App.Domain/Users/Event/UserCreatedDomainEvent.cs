using App.Domain.Abstraction;

namespace App.Domain.Users.Event;

public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
