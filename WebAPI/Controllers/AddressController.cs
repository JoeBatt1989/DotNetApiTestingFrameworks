using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly List<Address> addresses;

        private readonly List<Client> clients;

        public AddressController(SeedData seedData)
        {
            this.addresses = seedData.addresses;
            this.clients = seedData.clients;
        }

        [HttpGet]
        public IActionResult GetAddresses()
        {
            return Ok(addresses);
        }

        [HttpGet("{clientId}", Name = "GetAddress")]
        public IActionResult GetAddress(Guid clientId)
        {
            var exists = addresses.Any(a => a.ClientId == clientId);

            if (exists)
            {
                var address = addresses.Find(a => a.ClientId.Equals(clientId));
                return Ok(address);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult PostAddress([FromBody] AddressRequest request)
        {
            var exists = addresses.Any(a => a.ClientId == request.ClientId);

            if (!exists)
            {
                var requestError = IsRequestValid(request);

                if (requestError == null)
                {
                    var address = CreateAddress(request);
                    this.addresses.Add(address);
                    return Ok(address);
                }
                else
                {
                    return requestError;
                }
            }
            else
            {
                return BadRequest("Address already exists for this client");
            }
        }

        [HttpPut("{clientId}")]
        public IActionResult PutAddress(Guid clientId, [FromBody] AddressRequest request)
        {
            var exists = addresses.Any(a => a.ClientId == clientId);

            if (exists)
            {
                var requestError = IsRequestValid(request);

                if (requestError == null)
                {
                    var address = addresses.Find(a => a.ClientId.Equals(request.ClientId));
                    address.AddressLine1 = request.AddressLine1;
                    address.AddressLine2 = request.AddressLine2;
                    address.Postcode = request.Postcode;
                    return Ok(address);
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

        [HttpDelete("{clientId}")]
        public IActionResult DeleteAddress(Guid clientId)
        {
            var exists = addresses.Any(a => a.ClientId == clientId);

            if (exists)
            {
                addresses.RemoveAll(a => a.ClientId == clientId);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private IActionResult IsRequestValid(AddressRequest request)
        {
            if (string.IsNullOrEmpty(request.AddressLine1) || string.IsNullOrEmpty(request.Postcode))
            {
                return BadRequest("Please enter all mandatory address details");
            }

            var exists = clients.Any(a => a.Id == request.ClientId);

            if (!exists)
            {
                return NotFound("Client does not exist");
            }

            return null;
        }

        private Address CreateAddress(AddressRequest request)
        {
            Address address = new Address();
            address.Id = Guid.NewGuid();
            address.ClientId = request.ClientId;
            address.AddressLine1 = request.AddressLine1;
            address.AddressLine2 = request.AddressLine2;
            address.Postcode = request.Postcode;
            return address;
        }
    }
}
