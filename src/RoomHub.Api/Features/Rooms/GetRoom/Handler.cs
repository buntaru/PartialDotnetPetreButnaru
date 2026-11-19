using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class Handler(RoomRepository rooms) : IQueryHandler<Query, Room>
{
    public Result<Room> Handle(Query query)
    {
        var room = rooms.Get(query.Id);
        return room is null
            ? Result<Room>.Failure(new Error(ErrorType.NotFound, "The room does not exist."))
            : Result<Room>.Success(room);
    }
}
