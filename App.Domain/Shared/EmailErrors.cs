using App.Domain.Abstraction;

namespace Bookify.Domain.Shared;

public static class EmailErrors
{
    public static readonly Error Empty = new(
        "Email.Empty",
        "The Email is empty.");

    public static readonly Error InvalidFormat = new(
        "Email.InvalidFormat",
        "Email format is invalid.");
}