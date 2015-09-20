using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class Driver : Person
    {
        public int id { get; private set; }
        public Address address { get; set; }
        public string licenseNum { get; private set; }
        public string licenseState { get; private set; }
        public DateTime licenseExpiration { get; set; }
        public UtilityClass.DriverStatus status { get; set; }
        public string remark { get; set; }
        public bool localDriver { get; set; }

        //new driver
        public Driver(string firstName, string lastName, string phone, string email, Address address, string licenseNum,
                      string licenseState, DateTime licenseExpiration, UtilityClass.DriverStatus status, string remark,
                      bool localDriver)
            : base(firstName, lastName, phone, email)
        {
            this.address = address;
            this.licenseNum = licenseNum;
            this.licenseState = licenseState;
            this.licenseExpiration = licenseExpiration;
            this.status = status;
            this.remark = remark;
            this.localDriver = localDriver;
        }

        //existing driver
        public Driver(int id, string firstName, string lastName, string phone, string email, Address address, string licenseNum,
                      string licenseState, DateTime licenseExpiration, UtilityClass.DriverStatus status, string remark, bool localDriver)     
            : base(firstName, lastName, phone, email)
        {
            this.id = id;
            this.address = address;
            this.licenseNum = licenseNum;
            this.licenseState = licenseState;
            this.licenseExpiration = licenseExpiration;
            this.status = status;
            this.remark = remark;
            this.localDriver = localDriver;
        }

        /*
         * Pre:  The license expiration must be in the future
         * Post: The license information is updated
         * @param licenseNum is the drivers license number
         * @param licenseState is the state on the license
         * @param licenseExpiration is the expiration date of the license
         * @returns true if the license was updated
         */
        public bool UpdateDriversLicense(string licenseNum, string licenseState, DateTime licenseExpiration)
        {
            bool updated = true;

            if (DateTime.Today < licenseExpiration)
            {
                this.licenseNum = licenseNum;
                this.licenseState = licenseState;
                this.licenseExpiration = licenseExpiration;

                //TODO - also need to update in database
            }
            else
                updated = false;

            return updated;
        }
    }
}