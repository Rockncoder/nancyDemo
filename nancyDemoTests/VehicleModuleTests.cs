using System;
using nancyDemo;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace nancyDemoTests
{
    public class VehicleModuleTests
    {
        [Fact]
        public void Verify_Get_All_Vehicles_Route()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new VehicleModule()));

            // Act
            var response = browser.Get("/vehicles");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }
        
        [Fact]
        public void Verify_Get_One_Vehicle_Route()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new VehicleModule()));

            // Act
            var response = browser.Get("/vehicles/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }
        
        [Fact]
        public void Verify_Post_One_Vehicle_Route()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new VehicleModule()));

            // Act
            var response = browser.Post("/vehicles/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}