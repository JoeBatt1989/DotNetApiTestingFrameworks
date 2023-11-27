using System;

namespace WebAPI.Models
{
    public class Address
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Postcode { get; set; }
    }

    public class AddressRequest
    {
        public Guid ClientId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Postcode { get; set; }
    }
}
