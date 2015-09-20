using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class Staff : Person
    {
        public bool isManager { get; set; }
        public UserCredentials credentials { get; private set; }

        public Staff(string firstName, string lastName, string phone, string email, string password)
            : base(firstName, lastName, phone, email)
        {
            SetCredentials(email, password);
        }

        /*
         * Pre:  email must be a valid email address
         * Post: The staff member's user credentials are set
         * @param email is the staff member's email address
         * @param password is the staff memeber's unencrypted password
         */
        private void SetCredentials(string email, string password)
        {
            credentials = new UserCredentials(email, password, UtilityClass.UserTypes.Staff, true);
        }

        /*
         * Pre:  The customer's email address may not already exist in the system
         * Post: The customer is registered in the system as a new user
         * @returns true if the registration was successful and false otherwise
         */
        public bool Register()
        {
            bool result = true;
            UtilityClass.UserTypes type = UtilityClass.UserTypes.Staff;

            if (isManager)
                type = UtilityClass.UserTypes.Manager;

            int userId = DbInterfacePerson.RegisterUser(firstName, lastName, phone, email, credentials.username,
                                                    credentials.password, type, true);

            if (userId != -1)
                id = userId;
            else
                result = false;

            return result;
        }
    }
}