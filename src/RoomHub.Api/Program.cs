using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Features.Bookings;
using RoomHub.Api.Repositories;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Shared;

var builder = WebApplication.CreateBuilder(args);

// Temporary in-memory persistence. EF Core + SQLite is assignment step 5.
builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<BookingRepository>();
builder.Services.AddSingleton(TimeProvider.System);

// Register one CQRS handler per feature.
builder.Services.AddScoped<ICommandHandler<RoomHub.Api.Features.Rooms.CreateRoom.Command, Room>,
    RoomHub.Api.Features.Rooms.CreateRoom.Handler>();
builder.Services.AddScoped<IQueryHandler<RoomHub.Api.Features.Rooms.GetRoom.Query, Room>,
    RoomHub.Api.Features.Rooms.GetRoom.Handler>();
builder.Services.AddScoped<ICommandHandler<RoomHub.Api.Features.Bookings.CreateBooking.Command, Booking>,
    RoomHub.Api.Features.Bookings.CreateBooking.Handler>();
builder.Services.AddScoped<IQueryHandler<RoomHub.Api.Features.Bookings.GetBooking.Query, Booking>,
    RoomHub.Api.Features.Bookings.GetBooking.Handler>();
builder.Services.AddScoped<ICommandHandler<RoomHub.Api.Features.Bookings.CancelBooking.Command, Booking>,
    RoomHub.Api.Features.Bookings.CancelBooking.Handler>();

var app = builder.Build();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRoomEndpoints();
app.MapBookingEndpoints();

app.Run();
