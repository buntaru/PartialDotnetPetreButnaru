namespace RoomHub.Api.Domain.Common;

public sealed record Error(ErrorType Type, string Message);
