using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Shared;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class Handler(RoomRepository rooms) : ICommandHandler<Command, Room>
{
    public Result<Room> Handle(Command command)
    {
        var result = Room.Create(command.Name, command.Capacity);
        if (!result.IsSuccess) return result;
        rooms.Add(result.Value!);
        return result;
    }
}
