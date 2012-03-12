using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers.Controllers
{
    public class FirstApiController : ApiController
    {
        public string HelloWorld()
        {
            return "Hello World. Time is: " + DateTime.Now.ToString();
        }

        public string PostPerson(Person person)
        {
            return person.ToString();
        }



    }


    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public AddressTypes AddressType { get; set; }
    }

    public enum AddressTypes
    {
        Home = 0,
        Business = 2
    }

    public class Person
    {        
        public string Name { get; set; }
        public string Company { get; set; }
        public DateTime Entered { get; set; }

        public List<Address> Addresses { get; set; }

        public Person()
        {
            Addresses = new List<Address>();
        }

        public override string ToString()
        {
            return Name + "\r\n" + Company + "\r\n" + Entered.ToString("d");
        }

        public static List<Person> SampleData()
        {
            var personList = new List<Person>();
            
            var person = new Person()
            {
                Name = "Rick",
                Company = "West Wind",
                Entered = new DateTime(2012,3,5),
                Addresses = new List<Address>
                {
                    new Address() 
                    {
                         StreetAddress = "32 Kaiea",
                         City = "Paia",
                         AddressType = AddressTypes.Home,
                    },
                    new Address() 
                    {
                         StreetAddress = "33 Kaiea",
                         City = "Paia",
                         AddressType = AddressTypes.Business,
                    }
                }
            };
            personList.Add(person);
            
            person = new Person()
            {
                Name = "Markus",
                Company = "EPS Software",
                Entered = new DateTime(2012,3,6),
                Addresses = new List<Address>
                {
                    new Address() 
                    {
                         StreetAddress = "212 Sub Urbia side",
                         City = "Houston",
                         AddressType = AddressTypes.Home,
                    },
                    new Address() 
                    {
                         StreetAddress = "213 Busy Wack ",
                         City = "Paia",
                         AddressType = AddressTypes.Business,
                    }
                }
            };
            personList.Add(person);

            return personList;
        }
    }


}