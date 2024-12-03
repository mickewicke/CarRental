using System.Net;
using CarRentalApi.Services.RentalService;
using Microsoft.AspNetCore.Mvc;


namespace CarRentalApi.API
{
    public class RentalApi
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/hello", async () =>
            {
                return Results.Ok(new { message = "ok" });
            });

            app.MapPost("/available-vehicles", async ([FromServices] RentalService serviceServices) =>
            {
                var vehicles = await serviceServices.GetAvailabelVehicles();
                return Results.Ok(vehicles);
            });
                
            app.MapPost("/pickup-vehicle", async ([FromServices] RentalService serviceServices, [FromBody] PickupVehicleReq req) =>
            {
                await serviceServices.RegisterVehiclePickup(req);
                return Results.Ok();
            });

            app.MapPost("/return-vehicle", async ([FromServices] RentalService serviceServices, [FromBody] ReturnVehicleReq req) =>
            {
                var receipt = await serviceServices.RegisterVehicleReturn(req);

                return Results.Ok(receipt);
            });

            app.MapPost("/active-rentals", async ([FromServices] RentalService serviceServices) =>
            {
                var rentals = await serviceServices.GetActiveRentals();
                return Results.Ok(rentals);
            });
        }
    }
}


