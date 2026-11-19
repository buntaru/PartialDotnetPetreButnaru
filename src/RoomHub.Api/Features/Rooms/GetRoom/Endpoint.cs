using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Shared;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed record Response(Guid Id, string Name, int Capacity);

public static class Endpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/{id:guid}", Handle);

    private static IResult Handle(Guid id, IQueryHandler<Query, Room> handler)
    {
        var result = handler.Handle(new Query(id));
        if (!result.IsSuccess) return HttpErrorMapping.ToProblem(result.Error!);
        var room = result.Value!;
        return Results.Ok(new Response(room.Id, room.Name, room.Capacity));
    }
}
