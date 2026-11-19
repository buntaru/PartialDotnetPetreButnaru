using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Features.Shared;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/{id:guid}/cancel", Handle);

    private static IResult Handle(Guid id, ICommandHandler<Command, Booking> handler)
    {
        var result = handler.Handle(new Command(id));
        return result.IsSuccess ? Results.NoContent() : HttpErrorMapping.ToProblem(result.Error!);
    }
}
