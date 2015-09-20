using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class Shipment
    {
        public int id { get; private set; }
        public Customer customer { get; set; }
        public double length { get; private set; }
        public double width { get; private set; }
        public double height { get; private set; }
        public double weight { get; private set; }
        public ShipmentType shipmentType { get; private set; }
        public InsuranceType insuranceType { get; private set; }
        public double itemValue { get; private set; }
        public double cost { get; private set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public Address deliveryAddress { get; private set; }
        public CreditCard creditCard { get; private set; }
        public bool approved { get; private set; }
        public UtilityClass.ShippingSpeed shippingSpeed { get; private set; }

        //new shipment
        public Shipment(Customer customer, double length, double width, double height, double weight, ShipmentType shipmentType,
                        InsuranceType insuranceType, double itemValue, double cost, string firstName, string lastName, 
                        Address deliveryAddress, CreditCard creditCard, bool approved, UtilityClass.ShippingSpeed shippingSpeed)
        {
            this.customer = customer;
            this.length = length;
            this.width = width;
            this.height = height;
            this.weight = weight;
            this.shipmentType = shipmentType;
            this.insuranceType = insuranceType;
            this.itemValue = itemValue;
            this.cost = cost;
            this.firstName = firstName;
            this.lastName = lastName;
            this.deliveryAddress = deliveryAddress;
            this.creditCard = creditCard;
            this.approved = approved;
            this.shippingSpeed = shippingSpeed;
        }

        //used when info is only needed for truck data
        public Shipment(double length, double width, double height, double weight, Address deliveryAddress, UtilityClass.ShippingSpeed shippingSpeed)
        {
            this.length = length;
            this.width = width;
            this.height = height;
            this.weight = weight;
            this.deliveryAddress = deliveryAddress;
            this.shippingSpeed = shippingSpeed;
        }

        /*
         * Pre:  All shipment information must be valid
         * Post: The shipment is added to the database
         * @returns the id of the shipment
         */
        public int AddToDatabase()
        {
            id = DbInterfaceShipment.AddToDatabase(this);

            return id;
        }
    }
}