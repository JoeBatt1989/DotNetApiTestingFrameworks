using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly List<Car> cars;

        private readonly List<Client> clients;

        private const string registrtionRegex = @"(^[A-Z]{2}[0-9]{2} [A-Z]{3}$)|(^[A-Z][0-9]{1,3} [A-Z]{3}$)|(^[A-Z]{3} [0-9]{1,3}[A-Z]$)|(^[0-9]{1,4} [A-Z]{1,2}$)|(^[0-9]{1,3} [A-Z]{1,3}$)|(^[A-Z]{1,2} [0-9]{1,4}$)|(^[A-Z]{1,3} [0-9]{1,3}$)";

        public CarController(SeedData seedData)
        {
            this.cars = seedData.cars;
            this.clients = seedData.clients;
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            return Ok(cars);
        }

        [HttpGet("{clientId}", Name = "GetCar")]
        public IActionResult GetCar(Guid clientId)
        {
            var exists = cars.Any(a => a.ClientId == clientId);

            if (exists)
            {
                var clientCars = cars.FindAll(c => c.ClientId.Equals(clientId));
                return Ok(clientCars);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult PostCar([FromBody] CarRequest request)
        {
            var requestError = IsRequestValid(request);

            if (requestError == null)
            {
                var car = CreateCar(request);
                this.cars.Add(car);
                return Ok(car);
            }
            else
            {
                return requestError;
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutCar(Guid id, [FromBody] CarRequest request)
        {
            var exists = cars.Any(c => c.Id == id);

            if (exists)
            {
                var requestError = IsRequestValid(request);

                if (requestError == null)
                {
                    var car = cars.Find(c => c.Id.Equals(id));
                    car.Make = request.Make;
                    car.Model = request.Model;
                    car.Registration = request.Registration;

                    return Ok(car);
                }
                else
                {
                    return requestError;
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{clientId}", Name = "DeleteCars")]
        public IActionResult DeleteAllCars(Guid clientId)
        {
            var exists = cars.Any(a => a.ClientId == clientId);

            if (exists)
            {
                cars.RemoveAll(c => c.ClientId == clientId);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{clientId}/{id}", Name = "DeleteCar")]
        public IActionResult DeleteSpecificCar(Guid clientId, Guid id)
        {
            var car = cars.Find(c => c.Id.Equals(id));
            if (car != null && car.ClientId.Equals(clientId))
            {
                cars.RemoveAll(c => c.ClientId == clientId && c.Id == id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private IActionResult IsRequestValid(CarRequest request)
        {
            if (string.IsNullOrEmpty(request.Make) || string.IsNullOrEmpty(request.Model) || string.IsNullOrEmpty(request.Registration))
            {
                return BadRequest("Please enter all mandatory car details");
            }

            if (!IsValidRegistration(request.Registration))
            {
                return BadRequest("Registration is not valid format");
            }

            var exists = clients.Any(a => a.Id == request.ClientId);

            if (!exists)
            {
                return NotFound("Client does not exist");
            }

            return null;
        }

        private bool IsValidRegistration(string registration) => registration != null && Regex.IsMatch(registration, registrtionRegex);

        private Car CreateCar(CarRequest request)
        {
            Car car = new Car();
            car.Id = Guid.NewGuid();
            car.ClientId = request.ClientId;
            car.Make = request.Make;
            car.Model = request.Model;
            car.Registration = request.Registration;
            return car;
        }
    }
}
