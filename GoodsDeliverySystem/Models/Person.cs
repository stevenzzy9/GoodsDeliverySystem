using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class Person
    {
        public int id { get; protected set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string phone { get; private set; }
        public string email { get; private set; }

        //existing person
        public Person(int id, string firstName, string lastName, string phone, string email)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
        }

        //new person
        public Person(string firstName, string lastName, string phone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
        }
    }
}