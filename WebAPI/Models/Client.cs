using System;
using static WebAPI.Enums.Enums;

namespace WebAPI.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ContactDetails ContactDetails { get; set; }
    }

    public class ClientRequest
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ContactDetails ContactDetails { get; set; }
    }
}
