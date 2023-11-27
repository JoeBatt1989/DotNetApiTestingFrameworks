using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ContactDetails
    {
        public string PhoneType { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
