namespace RoomHub.Api.Features.Rooms;

public static class RoomsEndpoints
{
    public static IEndpointRouteBuilder MapRoomEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rooms").WithTags("Rooms");
        CreateRoom.Endpoint.Map(group);
        GetRoom.Endpoint.Map(group);
        RoomHub.Api.Features.Bookings.CreateBooking.Endpoint.Map(group);
        return app;
    }
}
