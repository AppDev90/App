namespace App.Domain.Abstraction
{
    public interface IEntity
    {
        void ClearDomainEvents();
        IReadOnlyList<IDomainEvent> GetDomainEvents();
    }
}