namespace RoomHub.Api.Features.Bookings;

public static class BookingsEndpoints
{
    public static IEndpointRouteBuilder MapBookingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bookings").WithTags("Bookings");
        GetBooking.Endpoint.Map(group);
        CancelBooking.Endpoint.Map(group);
        return app;
    }
}
