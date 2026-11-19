using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public sealed class Handler(BookingRepository bookings, TimeProvider timeProvider)
    : ICommandHandler<Command, Booking>
{
    public Result<Booking> Handle(Command command)
    {
        var booking = bookings.Get(command.Id);
        if (booking is null)
            return Result<Booking>.Failure(new Error(ErrorType.NotFound, "The booking does not exist."));

        return booking.Cancel(timeProvider);
    }
}
