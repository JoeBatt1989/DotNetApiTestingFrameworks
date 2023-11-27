using System;

namespace WebAPI.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }
    }

    public class CarRequest
    {
        public Guid ClientId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }
    }
}
