namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed record Command(string? Name, int Capacity);
