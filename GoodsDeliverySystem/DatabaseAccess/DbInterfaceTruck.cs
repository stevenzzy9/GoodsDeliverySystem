/*******************************************************
 * This class is responsible for database interactions
 * regarding trucks
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
    public class DbInterfaceTruck
    {
        /*
         * Pre:  A truck may not exist with the same license plate
         * Post: The new truck is added to the system
         * @returns true if the truck is successfully added and false otherwise
         */
        public static bool AddNewTruck(string licensePlateNum, string licensePlateState, double length, double width,
                                       double height, double maxWeight, int modelId, double fuelTankSize,
                                       double odometerReading, string remark, bool localTruck)
        {
            bool success = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckNew";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@licensePlateNum", licensePlateNum);
                cmd.Parameters.AddWithValue("@licensePlateState", licensePlateState);
                cmd.Parameters.AddWithValue("@length", length);
                cmd.Parameters.AddWithValue("@width", width);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@maxWeight", maxWeight);
                cmd.Parameters.AddWithValue("@modelId", modelId);
                cmd.Parameters.AddWithValue("@fuelTankSize", fuelTankSize);
                cmd.Parameters.AddWithValue("@odometerReading", odometerReading);
                cmd.Parameters.AddWithValue("@remark", remark);
                cmd.Parameters.AddWithValue("@localTruck", localTruck);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
                success = false;
            }

            connection.Close();

            return success;
        }

        /* Pre:
         * Post: Determines whether or not a truck exists with the input license plate number and state
         * @returns true if the truck exists and false otherwise
         */
        public static bool TruckExists(string licensePlateNum, string licensePlateState)
        {
            bool exists = false;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckSelectByLicensePlate";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@licenseNum", licensePlateNum);
                cmd.Parameters.AddWithValue("@licenseState", licensePlateState);

                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                    exists = true;
            }
            catch (Exception e)
            {
                exists = true;
            }

            connection.Close();

            return exists;
        }

        /* Pre:
         * Post: Retrieves a list of all trucks being loaded with the input shipping speed
         * @returns a list of available trucks
         */
        public static List<Truck> GetAvailableTrucks(UtilityClass.ShippingSpeed speed)
        {
            List<Truck> trucks = new List<Truck>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckSelectAvailableByShipSpeed";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shippingSpeedId", speed);

                adapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(table.Rows[i]["Id"]);
                    string licenseNum = table.Rows[i]["LicensePlateNum"].ToString();
                    string licenseState = table.Rows[i]["LicensePlateState"].ToString();
                    double length = Convert.ToDouble(table.Rows[i]["Length"]);
                    double width = Convert.ToDouble(table.Rows[i]["Width"]);
                    double height = Convert.ToDouble(table.Rows[i]["Height"]);
                    double weight = Convert.ToDouble(table.Rows[i]["MaxWeight"]);
                    int modelId = Convert.ToInt32(table.Rows[i]["ModelId"]);
                    double fuelTankSize = Convert.ToDouble(table.Rows[i]["FuelTankSize"]);
                    double odometer = Convert.ToDouble(table.Rows[i]["OdometerReading"]);
                    string remark = table.Rows[i]["Remark"].ToString();

                    //find if truck is for local deliveries
                    bool local = false;
                    if (!table.Rows[i]["LocalTruck"].ToString().Equals(""))
                        local = Convert.ToBoolean(table.Rows[i]["LocalTruck"]);

                    //get truck status 
                    UtilityClass.TruckStatus status = UtilityClass.TruckStatus.Idle;
                    if (!table.Rows[i]["StatusId"].ToString().Equals("") && Convert.ToInt32(table.Rows[i]["StatusId"]) >= 1)
                        status = (UtilityClass.TruckStatus)table.Rows[i]["StatusId"];

                    //get current truck path if there is one
                    int pathId = -1;
                    if (!table.Rows[i]["PathId"].Equals(""))
                        pathId = Convert.ToInt32(table.Rows[i]["PathId"]);

                    Truck truck = new Truck(id, licenseNum, licenseState, modelId, fuelTankSize, odometer, length,
                                            width, height, weight, remark, local);
                    truck.status = status;
                    truck.speed = speed;
                    if (pathId > 0) truck.pathId = pathId;

                    trucks.Add(truck);
                }
            }
            catch (Exception e)
            {
                trucks = null;
            }

            connection.Close();

            return trucks;
        }

        /* Pre:
         * Post: Retrieves a list of all idle trucks
         * @returns a list of available trucks
         */
        public static Truck GetTruckById(int id)
        {
            Truck truck = null;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckSelectById";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@truckId", id);

                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    string licenseNum = table.Rows[0]["LicensePlateNum"].ToString();
                    string licenseState = table.Rows[0]["LicensePlateState"].ToString();
                    double length = Convert.ToDouble(table.Rows[0]["Length"]);
                    double width = Convert.ToDouble(table.Rows[0]["Width"]);
                    double height = Convert.ToDouble(table.Rows[0]["Height"]);
                    double weight = Convert.ToDouble(table.Rows[0]["MaxWeight"]);
                    int modelId = Convert.ToInt32(table.Rows[0]["ModelId"]);
                    double fuelTankSize = Convert.ToDouble(table.Rows[0]["FuelTankSize"]);
                    double odometer = Convert.ToDouble(table.Rows[0]["OdometerReading"]);
                    string remark = table.Rows[0]["Remark"].ToString();
                    int status = Convert.ToInt32(table.Rows[0]["StatusId"]);

                    //find if truck is for local deliveries
                    bool local = false;
                    if (!table.Rows[0]["LocalTruck"].ToString().Equals(""))
                        local = Convert.ToBoolean(table.Rows[0]["LocalTruck"]);

                    int pathId = -1;
                    if (!table.Rows[0]["PathId"].ToString().Equals(""))
                        pathId = Convert.ToInt32(table.Rows[0]["PathId"]);

                    truck = new Truck(id, licenseNum, licenseState, modelId, fuelTankSize, odometer, length,
                                       width, height, weight, remark, local);
                    truck.status = (UtilityClass.TruckStatus)status;
                    if (pathId != -1) truck.pathId = pathId;
                }
            }
            catch (Exception e)
            {
                truck = null;
            }

            connection.Close();

            return truck;
        }

        /* Pre:
         * Post: Retrieves a list of all idle trucks
         * @returns a list of available trucks
         */
        public static List<Truck> GetTrucksByStatus(UtilityClass.TruckStatus status)
        {
            List<Truck> trucks = new List<Truck>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckSelectByStatus";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@statusId", status);

                adapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(table.Rows[i]["Id"]);
                    string licenseNum = table.Rows[i]["LicensePlateNum"].ToString();
                    string licenseState = table.Rows[i]["LicensePlateState"].ToString();
                    double length = Convert.ToDouble(table.Rows[i]["Length"]);
                    double width = Convert.ToDouble(table.Rows[i]["Width"]);
                    double height = Convert.ToDouble(table.Rows[i]["Height"]);
                    double weight = Convert.ToDouble(table.Rows[i]["MaxWeight"]);
                    int modelId = Convert.ToInt32(table.Rows[i]["ModelId"]);
                    double fuelTankSize = Convert.ToDouble(table.Rows[i]["FuelTankSize"]);
                    double odometer = Convert.ToDouble(table.Rows[i]["OdometerReading"]);
                    string remark = table.Rows[i]["Remark"].ToString();

                    //find if truck is for local deliveries
                    bool local = false;
                    if (!table.Rows[i]["LocalTruck"].ToString().Equals(""))
                        local = Convert.ToBoolean(table.Rows[i]["LocalTruck"]);

                    Truck truck = new Truck(id, licenseNum, licenseState, modelId, fuelTankSize, odometer, length,
                                            width, height, weight, remark, local);
                    truck.status = status;

                    trucks.Add(truck);
                }
            }
            catch (Exception e)
            {
                trucks = null;
            }

            connection.Close();

            return trucks;
        }

        /* Pre:
         * Post: Retrieves a list of all shipments loaded onto the input truck
         * @returns a list of shipments
         */
        public static List<Shipment> GetTruckShipments(int truckId)
        {
            List<Shipment> shipments = new List<Shipment>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckSelectShipments";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@truckId", truckId);

                adapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    double length = Convert.ToDouble(table.Rows[i]["Length"]);
                    double width = Convert.ToDouble(table.Rows[i]["Width"]);
                    double height = Convert.ToDouble(table.Rows[i]["Height"]);
                    double weight = Convert.ToDouble(table.Rows[i]["MaxWeight"]);
                    string street = table.Rows[i]["ShippingStreet"].ToString();
                    string city = table.Rows[i]["ShippingCity"].ToString();
                    string state = table.Rows[i]["ShippingState"].ToString();
                    int zip = Convert.ToInt32(table.Rows[i]["ShippingZip"]);
                    UtilityClass.ShippingSpeed speed = (UtilityClass.ShippingSpeed)table.Rows[0]["ShippingSpeedId"];

                    Address address = new Address(street, city, state, zip);
                    Shipment shipment = new Shipment(length, width, height, weight, address, speed);

                    shipments.Add(shipment);
                }
            }
            catch (Exception e)
            {
            }

            connection.Close();

            return shipments;
        }

        /* Pre:
        * Post: Starts a new delivery truck with a new shipment
        */
        public static void StartNewTruckWithShipment(int shipmentId, int truckId, UtilityClass.ShippingSpeed speed, int pathId, int driverId)
        {
            List<Shipment> shipments = new List<Shipment>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "NewDeliveryTruckWithPackage";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", shipmentId);
                cmd.Parameters.AddWithValue("@truckId", truckId);
                cmd.Parameters.AddWithValue("@shippingSpeedId", speed);
                cmd.Parameters.AddWithValue("@pathId", pathId);
                cmd.Parameters.AddWithValue("@driverId", driverId);

                adapter.Fill(table);
            }
            catch (Exception e)
            {

            }

            connection.Close();
        }

        /* 
         * Pre:
        * Post: Adds a shipment to a truck that is already being loaded
        */
        public static void AddShipmentToTruck(int shipmentId, int truckId)
        {
            List<Shipment> shipments = new List<Shipment>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "AddPackageToTruck";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", shipmentId);
                cmd.Parameters.AddWithValue("@truckId", truckId);

                adapter.Fill(table);
            }
            catch (Exception e)
            {

            }

            connection.Close();
        }

        /* Pre:
        * Post: Changes the route of a truck and adds a new shipment
        */
        public static void CommitRouteChangeAndAddShipment(int shipmentId, int truckId, int pathId)
        {
            List<Shipment> shipments = new List<Shipment>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "AddPackageToTruckAndChangeRoute";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", shipmentId);
                cmd.Parameters.AddWithValue("@truckId", truckId);
                cmd.Parameters.AddWithValue("@pathId", pathId);

                adapter.Fill(table);
            }
            catch (Exception e)
            {

            }

            connection.Close();
        }

        /*
         * Pre:  
         * Post: Dispatch the truck with the input id
         */
        public static bool DispatchTruck(int truckId)
        {
            bool result = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "TruckDispatch";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@truckId", truckId);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
                result = false;
            }

            connection.Close();

            return result;
        }
    }
}