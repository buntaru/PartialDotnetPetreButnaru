using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed record Request(string? Name, int Capacity);
public sealed record Response(Guid Id, string Name, int Capacity);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/", Handle);

    private static IResult Handle(Request request, RoomRepository rooms)
    {
        var result = Room.Create(request.Name, request.Capacity);
        if (!result.IsSuccess) return HttpErrorMapping.ToProblem(result.Error!);

        var room = result.Value!;
        rooms.Add(room);
        return Results.Created($"/api/rooms/{room.Id}", new Response(room.Id, room.Name, room.Capacity));
    }
}
