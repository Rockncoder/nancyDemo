using System;
using Nancy;

namespace nancyDemo
{
    public sealed class VehicleModule: NancyModule
    {
        public VehicleModule(): base("/vehicles")
        {
            Head("/", it => $"HEAD Method at Vehicles!!! // UTC: {DateTime.UtcNow}");

            Get("/", it => GetAllVehicles());
            
            Get("/{vehicleid}", it => GetVehicle(it));

            Post("/{vehicleid}", it => $"POST Method at Vehicles!!! // UTC: {DateTime.UtcNow}");

            Delete("/{vehicleid}", it => $"DELETE Method at Vehicles!!! // UTC: {DateTime.UtcNow}");

            Put("/{vehicleid}", it => $"PUT Method at Vehicles!!! // UTC: {DateTime.UtcNow}");

            Patch("/{vehicleid}", it => $"PATCH Method at Vehicles!!! // UTC: {DateTime.UtcNow}");

            Options("/", it => $"OPTIONS Method at Vehicles!!! // UTC: {DateTime.UtcNow}");
        }

        private static string GetAllVehicles()
        {
            return $"GET Method all vehicles at Vehicles!!! // UTC: {DateTime.UtcNow}";
        }
        
        private static string GetVehicle(dynamic it)
        {
            var vehicleId = (string) it.vehicleid;
            return $"GET Method vehicle {vehicleId} at Vehicles!!! // UTC: {DateTime.UtcNow}";
        }
    }
}