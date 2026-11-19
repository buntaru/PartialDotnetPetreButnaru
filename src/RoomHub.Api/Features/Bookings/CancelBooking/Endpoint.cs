using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/{id:guid}/cancel", Handle);

    private static IResult Handle(
        Guid id,
        BookingRepository bookings,
        TimeProvider timeProvider)
    {
        var booking = bookings.Get(id);

        if (booking is null)
            return HttpErrorMapping.ToProblem(
                new Error(ErrorType.NotFound,
                    "The booking does not exist."));

        var result = booking.Cancel(timeProvider);

        if (!result.IsSuccess)
            return HttpErrorMapping.ToProblem(result.Error!);

        return Results.NoContent();
    }
}
