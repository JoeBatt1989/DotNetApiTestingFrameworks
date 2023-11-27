using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAPI.Data;
using WebAPI.Models;
using static WebAPI.Enums.Enums;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly List<Client> clients;

        private readonly List<Address> addresses;

        private readonly List<Car> cars;

        public ClientController(SeedData seedData)
        {
            this.clients = seedData.clients;

            this.addresses = seedData.addresses;

            this.cars = seedData.cars;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return Ok(clients);
        }

        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult Get(Guid id)
        {
            var client = clients.Find(c => c.Id.Equals(id));

            if (client.Id != null)
            {
                return Ok(client);
            }
            else
            {
                return NotFound("Client not found");
            }
        }

        [HttpPost]
        public IActionResult PostClient([FromBody] ClientRequest request)
        {
            var requestError = IsRequestValid(request);

            if (requestError == null)
            {
                var client = this.CreateClient(request);
                this.clients.Add(client);
                return Ok(client);
            }
            else
            {
                return requestError;
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutClient(Guid id, [FromBody] ClientRequest request)
        {
            var requestError = IsRequestValid(request);

            if (requestError == null)
            {
                var client = clients.Find(c => c.Id.Equals(id));
                client.FirstName = request.FirstName;
                client.LastName = request.LastName;
                client.Email = request.Email;
                client.ContactDetails.PhoneType = request.ContactDetails.PhoneType;
                client.ContactDetails.PhoneNumber = request.ContactDetails.PhoneNumber;
                return Ok(client);
            }
            else
            {
                return requestError;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(Guid id)
        {
            var client = clients.Find(c => c.Id.Equals(id));

            if (client == null || client.Id == null)
            {
                return NotFound("Client not found");
            }
            else
            {
                clients.RemoveAll(c => c.Id == id);
                addresses.RemoveAll(a => a.ClientId == id);
                cars.RemoveAll(c => c.ClientId == id);
                return Ok("Client deleted successfully");
            }
        }

        private IActionResult IsRequestValid(ClientRequest request)
        {
            if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.ContactDetails.PhoneNumber))
            {
                return BadRequest("Please enter all mandatory details for client");
            }

            if (Enum.TryParse(request.Title, false, out Title title))
            {
            }
            else
            {
                return BadRequest($"Title {request.Title} is not valid");
            }

            if (Enum.TryParse(request.ContactDetails.PhoneType, false, out PhoneType phoneType))
            {
            }
            else
            {
                return BadRequest($"Phone type {request.ContactDetails.PhoneType} is not valid");
            }

            if (!IsValidEmailAddress(request.Email))
            {
                return BadRequest("Email Address is not valid");
            }

            return null;
        }

        private bool IsValidEmailAddress(string email) => email != null && new EmailAddressAttribute().IsValid(email);

        private Client CreateClient(ClientRequest request)
        {
            Client client = new Client();
            client.Id = Guid.NewGuid();
            client.Title = request.Title;
            client.FirstName = request.FirstName;
            client.LastName = request.LastName;
            client.Email = request.Email;

            ContactDetails contactDetails = new ContactDetails()
            {
                PhoneType = request.ContactDetails.PhoneType,
                PhoneNumber = request.ContactDetails.PhoneNumber
            };

            client.ContactDetails = contactDetails;
            return client;
        }
    }
}
