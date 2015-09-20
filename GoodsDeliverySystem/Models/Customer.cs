using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class Customer : Person
    {
        public UserCredentials credentials { get; private set; }

        //constructor for new customer
        public Customer(string firstName, string lastName, string phone, string email, string password, UtilityClass.UserTypes userType)
            : base(firstName, lastName, phone, email)
        {
            credentials = new UserCredentials(email, password, userType, true);
        }

        //constructor for existing customer
        public Customer(int id, string firstName, string lastName, string phone, string email, string password, UtilityClass.UserTypes userType)
            : base(id, firstName, lastName, phone, email)
        {
            credentials = new UserCredentials(email, password, userType, false);
        }

        /*
         * Pre:  The customer's email address may not already exist in the system
         * Post: The customer is registered in the system as a new user
         * @returns true if the registration was successful and false otherwise
         */
        public bool Register()
        {
            bool result = true;

            int userId = DbInterfacePerson.RegisterUser(firstName, lastName, phone, email, credentials.username, 
                                                   credentials.password, UtilityClass.UserTypes.Customer, false);

            if (userId != -1)
                id = userId;
            else
                result = false;

            return result;
        }
    }
}