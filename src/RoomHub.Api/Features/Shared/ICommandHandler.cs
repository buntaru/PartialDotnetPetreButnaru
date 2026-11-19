using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Shared;

public interface ICommandHandler<in TCommand, TResponse>
{
    Result<TResponse> Handle(TCommand command);
}
