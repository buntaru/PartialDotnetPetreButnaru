using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class Handler(RoomRepository rooms, BookingRepository bookings, TimeProvider timeProvider)
    : ICommandHandler<Command, Booking>
{
    public Result<Booking> Handle(Command command)
    {
        // API contract: missing room must return 404 before any booking validation.
        var room = rooms.Get(command.RoomId);
        if (room is null)
            return Result<Booking>.Failure(new Error(ErrorType.NotFound, "The room does not exist."));

        var result = Booking.Create(room, command.BookedBy, command.StartUtc, command.EndUtc,
            command.Attendees, bookings.GetAll(), timeProvider);
        if (!result.IsSuccess) return result;

        bookings.Add(result.Value!);
        return result;
    }
}
