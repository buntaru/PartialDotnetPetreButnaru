using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Shared;

public interface IQueryHandler<in TQuery, TResponse>
{
    Result<TResponse> Handle(TQuery query);
}
