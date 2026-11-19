using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed record Response(Guid Id, Guid RoomId, string BookedBy,
    DateTime StartUtc, DateTime EndUtc, int Attendees, string Status);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/{id:guid}", Handle);

    private static IResult Handle(Guid id, BookingRepository bookings)
    {
        var b = bookings.Get(id);
        if (b is null)
            return HttpErrorMapping.ToProblem(new Error(ErrorType.NotFound, "The booking does not exist."));

        return Results.Ok(new Response(b.Id, b.RoomId, b.BookedBy,
            b.StartUtc, b.EndUtc, b.Attendees, b.Status.ToString()));
    }
}
