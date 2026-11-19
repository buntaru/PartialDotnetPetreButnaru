using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Repositories;

public sealed class BookingRepository
{
    private readonly List<Booking> _bookings = [];
    private readonly object _lock = new();

    public void Add(Booking booking) { lock (_lock) { _bookings.Add(booking); } }
    public Booking? Get(Guid id) { lock (_lock) { return _bookings.FirstOrDefault(b => b.Id == id); } }
    // Global list is needed for R9 across rooms. Replace it with a targeted query in EF Core.
    public List<Booking> GetAll() { lock (_lock) { return _bookings.ToList(); } }
}
