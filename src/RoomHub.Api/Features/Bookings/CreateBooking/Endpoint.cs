using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Features.Shared;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed record Request(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);
public sealed record Response(Guid Id, Guid RoomId, string Status);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/{roomId:guid}/bookings", Handle);

    private static IResult Handle(Guid roomId, Request request, ICommandHandler<Command, Booking> handler)
    {
        var result = handler.Handle(new Command(roomId, request.BookedBy, request.StartUtc,
            request.EndUtc, request.Attendees));
        if (!result.IsSuccess) return HttpErrorMapping.ToProblem(result.Error!);
        var booking = result.Value!;
        return Results.Created($"/api/bookings/{booking.Id}",
            new Response(booking.Id, booking.RoomId, booking.Status.ToString()));
    }
}
