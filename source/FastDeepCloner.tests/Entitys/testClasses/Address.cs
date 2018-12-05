using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner.tests.Entitys.testClasses
{
    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        public Address()
        {

        }

        public Address(string street, int number, int postalCode, string city)
        {
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
        }

        public string fullAddress()
        {
            return $"{this.Street} {this.Number} {this.PostalCode} {this.City}";
        }

        public string addressShort()
        {
            return $"{this.Street} - {this.City}";
        }

        public override string ToString()
        {
            return $"{this.Street} {this.Number} {Environment.NewLine}{this.PostalCode} {this.City}";


        }
        public string line1()
        {
            return $"{this.Street} {this.Number}";


        }
        public string line2()
        {
            return $"{this.PostalCode} {this.City}";


        }
    }
}
