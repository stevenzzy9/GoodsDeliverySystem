/*******************************************************
 * This class is responsible for database interactions
 * regarding shipments
 *******************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.Models;

namespace GoodsDeliverySystem.DatabaseAccess
{
    public class DbInterfaceShipment
    {
        /* 
         * Pre:
         * Post: The shipment information is added to the database
         * @returns the id of the shipment
         */
        public static int AddToDatabase(Shipment shipment)
        {
            int id = -1;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "ShipmentNew";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customerId", shipment.customer.id);
                cmd.Parameters.AddWithValue("@length", shipment.length);
                cmd.Parameters.AddWithValue("@width", shipment.width);
                cmd.Parameters.AddWithValue("@height", shipment.height);
                cmd.Parameters.AddWithValue("@weight", shipment.weight);
                cmd.Parameters.AddWithValue("@cost", shipment.cost);
                cmd.Parameters.AddWithValue("@itemValue", shipment.itemValue);
                cmd.Parameters.AddWithValue("@shippingSpeedId", shipment.shippingSpeed);
                cmd.Parameters.AddWithValue("@firstName", shipment.firstName);
                cmd.Parameters.AddWithValue("@lastName", shipment.lastName);
                cmd.Parameters.AddWithValue("@street", shipment.deliveryAddress.street);
                cmd.Parameters.AddWithValue("@city", shipment.deliveryAddress.city);
                cmd.Parameters.AddWithValue("@state", shipment.deliveryAddress.state);
                cmd.Parameters.AddWithValue("@zip", shipment.deliveryAddress.zip);
                cmd.Parameters.AddWithValue("@nameOnCard", shipment.creditCard.nameOnCard);
                cmd.Parameters.AddWithValue("@cardType", shipment.creditCard.cardType);
                cmd.Parameters.AddWithValue("@cardNum", shipment.creditCard.cardNum);
                cmd.Parameters.AddWithValue("@securityCode", shipment.creditCard.securityCode);
                cmd.Parameters.AddWithValue("@expirationMonth", shipment.creditCard.expirationMonth);
                cmd.Parameters.AddWithValue("@expirationYear", shipment.creditCard.expirationYear);
                cmd.Parameters.AddWithValue("@billStreet", shipment.creditCard.billingAddress.street);
                cmd.Parameters.AddWithValue("@billCity", shipment.creditCard.billingAddress.city);
                cmd.Parameters.AddWithValue("@billState", shipment.creditCard.billingAddress.state);
                cmd.Parameters.AddWithValue("@billZip", shipment.creditCard.billingAddress.zip);

                if (shipment.shipmentType != null)
                    cmd.Parameters.AddWithValue("@shipmentTypeId", shipment.shipmentType.id);
                else
                    cmd.Parameters.AddWithValue("@shipmentTypeId", -1);

                if (shipment.insuranceType != null)
                    cmd.Parameters.AddWithValue("@insuranceTypeId", shipment.insuranceType.id);
                else
                    cmd.Parameters.AddWithValue("@insuranceTypeId", -1);

                adapter.Fill(table);

                //get id
                if (table.Rows.Count == 1)
                    id = Convert.ToInt32(table.Rows[0]["Id"]);
            }
            catch (Exception e)
            {
                id = -1;
            }

            connection.Close();

            return id;
        }

        /* 
         * Pre:
         * Post: Retrieves the shipment information neded for truck placement
         * @returns the info of the shipment
         */
        public static Shipment GetShipmentForTruck(int shipmentId)
        {
            Shipment shipment = null;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "ShipmentSelectTruckInfo";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", shipmentId);

                adapter.Fill(table);

                //get shipment info
                if (table.Rows.Count == 1)
                {
                    double length = Convert.ToDouble(table.Rows[0]["Length"]);
                    double width = Convert.ToDouble(table.Rows[0]["Width"]);
                    double height = Convert.ToDouble(table.Rows[0]["Height"]);
                    double weight = Convert.ToDouble(table.Rows[0]["Weight"]);
                    string street = table.Rows[0]["ShippingStreet"].ToString();
                    string city = table.Rows[0]["ShippingCity"].ToString();
                    string state = table.Rows[0]["ShippingState"].ToString();
                    int zip = Convert.ToInt32(table.Rows[0]["ShippingZip"]);
                    string username = table.Rows[0]["Username"].ToString();
                    string password = table.Rows[0]["Password"].ToString();
                    string firstName = table.Rows[0]["FirstName"].ToString();
                    string lastName = table.Rows[0]["LastName"].ToString();
                    string phone = table.Rows[0]["Phone"].ToString();
                    int customerId = Convert.ToInt32(table.Rows[0]["PersonId"]);
                    string email = table.Rows[0]["Email"].ToString();

                    UtilityClass.ShippingSpeed speed = (UtilityClass.ShippingSpeed)table.Rows[0]["ShippingSpeedId"];
                    Customer customer = new Customer(customerId, firstName, lastName, phone, email, password, UtilityClass.UserTypes.Customer);
                    Address address = new Address(street, city, state, zip);

                    shipment = new Shipment(length, width, height, weight, address, speed);
                    shipment.customer = customer;
                }
            }
            catch (Exception e)
            {
                shipment = null;
            }

            connection.Close();

            return shipment;
        }

        /* 
         * Pre:
         * Post: The shipment information is added to the database
         * @returns the id of the shipment
         */
        public static Path[] GetPaths()
        {
            Path[] paths;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "PathsSelect";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                adapter.Fill(table);

                //loop through results and get number of paths
                int numPaths = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (Convert.ToInt32(table.Rows[i]["PathId"]) > numPaths)
                        numPaths = Convert.ToInt32(table.Rows[i]["PathId"]);
                }

                //make array for paths
                paths = new Path[numPaths];
                for (int i = 0; i < numPaths; i++)
                    paths[i] = new Path(new List<Road>());

                //add paths to array
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int pathId = Convert.ToInt32(table.Rows[i]["PathId"]);
                    string city = table.Rows[i]["City"].ToString();
                    int time = Convert.ToInt32(table.Rows[i]["TimeToCity"]);

                    Road path = new Road(city, time);
                    paths[pathId - 1].roads.Add(path);
                }
            }
            catch (Exception e)
            {
                paths = null;
            }

            connection.Close();

            return paths;
        }

        /* 
         * Pre:
         * Post: The shipment information is added to the database
         * @returns the id of the shipment
         */
        public static void RunStatusUpdates()
        {
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "RunStatusUpdates";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                adapter.Fill(table);

            }
            catch (Exception e)
            {
            }

            connection.Close();
        }

        /* 
         * Pre:
         * Post: Change status of all shipments on the same truck to awaiting local delivery
         */
        public static void ChangeStatusToAwaitingLocalDelivery(int shipmentId)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "ShipmentUpdateToAwaitingLocalDelivery";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", shipmentId);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
            }

            connection.Close();
        }

        /* 
         * Pre:
         * Post: Change status of the shipment to delivered and update the truck status
         *       if it was the last shipment on the truck
         */
        public static void ChangeStatusToDelivered(int shipmentId)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "ShipmentUpdateToDelivered";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", shipmentId);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
            }

            connection.Close();
        }
    }
}