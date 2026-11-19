using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class Handler(BookingRepository bookings) : IQueryHandler<Query, Booking>
{
    public Result<Booking> Handle(Query query)
    {
        var booking = bookings.Get(query.Id);
        return booking is null
            ? Result<Booking>.Failure(new Error(ErrorType.NotFound, "The booking does not exist."))
            : Result<Booking>.Success(booking);
    }
}
