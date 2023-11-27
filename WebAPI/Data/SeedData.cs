using System;
using System.Collections.Generic;
using WebAPI.Models;
using static WebAPI.Enums.Enums;

namespace WebAPI.Data
{
    public class SeedData : ISeedData
    {
        public List<Address> addresses { get; set; }

        public List<Car> cars { get; set; }

        public List<Client> clients { get; set; }

        public SeedData()
        {
            SeedClients();
            SeedAddresses();
            SeedCars();
        }

        public void SeedClients()
        {
            clients = new List<Client>
            {
                new Client
                {
                    Id = new Guid("95FFF557-DC35-447D-BFD4-34D15E191BBD"),
                    Title = Title.Mr.ToString(),
                    FirstName = "Tim",
                    LastName = "Smith",
                    Email = "Tim.Smith66@mailinator.com",
                    ContactDetails = new ContactDetails
                    {
                        PhoneType = PhoneType.Home.ToString(),
                        PhoneNumber = "01132687542"
                    }
                },
                new Client
                {
                    Id = new Guid("F82A6929-CD9C-42E0-AEF1-4BBD260B54CD"),
                    Title = Title.Mr.ToString(),
                    FirstName = "James",
                    LastName = "Lee",
                    Email = "James7612@mailinator.com",
                    ContactDetails = new ContactDetails
                    {
                        PhoneType = PhoneType.Mobile.ToString(),
                        PhoneNumber = "07959437865"
                    }
                },
                new Client
                {
                    Id = new Guid("661CA7F8-988C-44E7-9F6B-09D0EDC24C25"),
                    Title = Title.Dr.ToString(),
                    FirstName = "James",
                    LastName = "Lee",
                    Email = "James7612@mailinator.com",
                    ContactDetails = new ContactDetails
                    {
                        PhoneType = PhoneType.Home.ToString(),
                        PhoneNumber = "01132986677"
                    }
                }
            };
        }

        public void SeedAddresses()
        {
            addresses = new List<Address>
            {
                new Address
                {
                    Id = new Guid("DC432071-4AF9-4216-A807-0DFBFA1D8BE7"),
                    ClientId = new Guid("95FFF557-DC35-447D-BFD4-34D15E191BBD"),
                    AddressLine1 = "10 Stamford Avenue",
                    AddressLine2 = "",
                    Postcode = "LS11 5YY"
                },
                new Address
                {
                    Id = new Guid("9E84C373-87C2-410C-9C31-92610FF466B2"),
                    ClientId = new Guid("661CA7F8-988C-44E7-9F6B-09D0EDC24C25"),
                    AddressLine1 = "12 Stamford Avenue",
                    AddressLine2 = "",
                    Postcode = "LS11 5YY"
                },
                new Address
                {
                    Id = new Guid("AEBD9432-BF4A-4A38-AEB7-F7C73B27FBB2"),
                    ClientId = new Guid("F82A6929-CD9C-42E0-AEF1-4BBD260B54CD"),
                    AddressLine1 = "24 Stamford Avenue",
                    AddressLine2 = "",
                    Postcode = "LS11 5YS"
                }
            };
        }

        public void SeedCars()
        {
            cars = new List<Car>
            {
                new Car
                {
                    Id = new Guid("0897210F-C1D1-4C99-B1FD-5690CCFC5CEE"),
                    ClientId = new Guid("661CA7F8-988C-44E7-9F6B-09D0EDC24C25"),
                    Make = "Ford",
                    Model = "Focus",
                    Registration = "YY19 HJU"
                },
                new Car
                {
                    Id = new Guid("4E1E3990-FCE8-4408-89F9-6A7BBC3C22CB"),
                    ClientId = new Guid("95FFF557-DC35-447D-BFD4-34D15E191BBD"),
                    Make = "Ford",
                    Model = "Fiesta",
                    Registration = "DY15 HGU"
                },
                new Car
                {
                    Id = new Guid("584E611D-5A15-4A42-BFB3-62A030739C41"),
                    ClientId = new Guid("F82A6929-CD9C-42E0-AEF1-4BBD260B54CD"),
                    Make = "Mercedes",
                    Model = "Fiesta",
                    Registration = "DY15 HGU"
                }
            };
        }
    }
}
