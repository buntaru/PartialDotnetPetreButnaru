using RoomHub.Api.Domain.Common;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed record Response(Guid Id, string Name, int Capacity);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/{id:guid}", Handle);

    private static IResult Handle(Guid id, RoomRepository rooms)
    {
        var room = rooms.Get(id);
        if (room is null)
            return HttpErrorMapping.ToProblem(new Error(ErrorType.NotFound, "The room does not exist."));
        return Results.Ok(new Response(room.Id, room.Name, room.Capacity));
    }
}
