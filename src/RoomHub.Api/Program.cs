using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Features.Bookings;
using RoomHub.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Temporary in-memory persistence. EF Core + SQLite is assignment step 5.
builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<BookingRepository>();
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRoomEndpoints();
app.MapBookingEndpoints();

app.Run();
