using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateBooking;

public sealed record Request(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);
public sealed record Response(Guid Id, Guid RoomId, string Status);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/{roomId:guid}/bookings", Handle);

    private static IResult Handle(
        Guid roomId, Request request, RoomRepository rooms,
        BookingRepository bookings, TimeProvider timeProvider)
    {
        // The contract requires 404 for an unknown room before request validation.
        var room = rooms.Get(roomId);
        if (room is null)
            return HttpErrorMapping.ToProblem(new Error(ErrorType.NotFound, "The room does not exist."));

        var result = Booking.Create(room, request.BookedBy, request.StartUtc, request.EndUtc,
            request.Attendees, bookings.GetAll(), timeProvider);
        if (!result.IsSuccess) return HttpErrorMapping.ToProblem(result.Error!);

        var booking = result.Value!;
        bookings.Add(booking);
        return Results.Created($"/api/bookings/{booking.Id}",
            new Response(booking.Id, booking.RoomId, booking.Status.ToString()));
    }
}
