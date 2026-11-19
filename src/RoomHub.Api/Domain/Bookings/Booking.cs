using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Domain.Bookings;

public enum BookingStatus { Active, Cancelled }

public sealed class Booking
{
    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    private Booking() { }

    public static Result<Booking> Create(
        Room room, string? bookedBy, DateTime startUtc, DateTime endUtc,
        int attendees, IEnumerable<Booking> existingBookings, TimeProvider timeProvider)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return Result<Booking>.Failure(new Error(ErrorType.Validation, "BookedBy is required."));
        if (endUtc <= startUtc)
            return Result<Booking>.Failure(new Error(ErrorType.Validation, "The end must be after the start."));
        if (attendees <= 0)
            return Result<Booking>.Failure(new Error(ErrorType.Validation, "Attendees must be positive."));
        if (attendees > room.Capacity)
            return Result<Booking>.Failure(new Error(ErrorType.Validation, "The room does not have enough seats."));
        if (startUtc < timeProvider.GetUtcNow().UtcDateTime)
            return Result<Booking>.Failure(new Error(ErrorType.Validation, "A booking cannot start in the past."));
        var maxDuration = room.Capacity > 10
            ? TimeSpan.FromHours(2)
            : TimeSpan.FromHours(4);

        if (endUtc - startUtc > maxDuration)
            return Result<Booking>.Failure(
                new Error(
                    ErrorType.Validation,
                    $"The maximum booking duration is {maxDuration.TotalHours} hours."));

        // Half-open intervals [start, end): touching intervals are allowed.
        var active = existingBookings.Where(b => b.Status == BookingStatus.Active).ToList();
        if (active.Any(b => b.RoomId == room.Id && b.StartUtc < endUtc && startUtc < b.EndUtc))
            return Result<Booking>.Failure(new Error(ErrorType.Conflict, "The room is occupied in this interval."));

        // R9: the participant may have at most 2 active bookings on a UTC day, across all rooms.
        // Comparison is ordinal after trimming the incoming participant name.
        var participant = bookedBy.Trim();
        if (active.Count(b => string.Equals(b.BookedBy, participant, StringComparison.Ordinal)
                              && b.StartUtc.Date == startUtc.Date) >= 2)
            return Result<Booking>.Failure(new Error(ErrorType.Conflict, "Participant already has 2 active bookings on this UTC day."));

        return Result<Booking>.Success(new Booking
        {
            Id = Guid.NewGuid(), RoomId = room.Id, BookedBy = participant,
            StartUtc = startUtc, EndUtc = endUtc, Attendees = attendees,
            Status = BookingStatus.Active
        });
    }

    public Result<Booking> Cancel(TimeProvider timeProvider)
    {
        if (Status == BookingStatus.Cancelled)
            return Result<Booking>.Failure(
                new Error(ErrorType.Conflict,
                    "Booking is already cancelled."));

        var now = timeProvider.GetUtcNow().UtcDateTime;

        if (StartUtc - now < TimeSpan.FromMinutes(60))
            return Result<Booking>.Failure(
                new Error(ErrorType.Conflict,
                    "Booking can only be cancelled at least 60 minutes before start."));

        Status = BookingStatus.Cancelled;

        return Result<Booking>.Success(this);
    }
}
