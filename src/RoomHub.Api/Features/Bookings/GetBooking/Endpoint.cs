using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Features.Shared;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed record Response(Guid Id, Guid RoomId, string BookedBy,
    DateTime StartUtc, DateTime EndUtc, int Attendees, string Status);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/{id:guid}", Handle);

    private static IResult Handle(Guid id, IQueryHandler<Query, Booking> handler)
    {
        var result = handler.Handle(new Query(id));
        if (!result.IsSuccess) return HttpErrorMapping.ToProblem(result.Error!);
        var booking = result.Value!;
        return Results.Ok(new Response(booking.Id, booking.RoomId, booking.BookedBy,
            booking.StartUtc, booking.EndUtc, booking.Attendees, booking.Status.ToString()));
    }
}
