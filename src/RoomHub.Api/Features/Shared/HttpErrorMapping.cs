using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Shared;

public static class HttpErrorMapping
{
    public static IResult ToProblem(Error error) => Results.Problem(
        statusCode: error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        },
        title: error.Type.ToString(),
        detail: error.Message);
}
