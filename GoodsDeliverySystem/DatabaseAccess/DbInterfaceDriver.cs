
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
    public class DbInterfaceDriver
    {
        /*
         * Pre:  An existing Driver may not have the same Driver licenseNumber
         * Post: The new Driver is added to the system
         * @returns true if the Driver is successfully added and false otherwise
         */
        public static bool HireDriver(string firstName, string lastName, string street, string city, string state, int zip, string phone, string email,
                                      string licenseNumber,string licenseState, string licenseExp, string Remark, bool localDriver)
        {
            bool result = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "DriverNew";

                SqlCommand cmd = new SqlCommand(storedProc, connection);
            
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@State", state);
                cmd.Parameters.AddWithValue("@Zip", zip);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@LicenseNum", licenseNumber);
                cmd.Parameters.AddWithValue("@LicenseState", licenseState);
                cmd.Parameters.AddWithValue("@LicenseExpiration", licenseExp);
                cmd.Parameters.AddWithValue("@Remark", Remark);
                cmd.Parameters.AddWithValue("@localDriver", localDriver);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
                result = false;
            }

            connection.Close();

            return result;
        }

        /* Pre:
         * Post: Determines whether or not a Driver exists with the input license number and state
         * @returns true if the Driver exists and false otherwise
         */
        public static bool DriverExists(string licenseNum, string licenseState)
        {
            bool exists = false;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "DriverSelectByLicense";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@licenseNum", licenseNum);
                cmd.Parameters.AddWithValue("@licenseState", licenseState);

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
         * Post: Retrieves available drivers
         * @returns a list of available drivers
         */
        public static List<Driver> GetAvailableDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "DriverSelectAvailable";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                adapter.Fill(table);

                //add drivers to list
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(table.Rows[i]["Id"]);
                    string firstName = table.Rows[i]["FirstName"].ToString();
                    string lastName = table.Rows[i]["LastName"].ToString();
                    string phone = table.Rows[i]["Phone"].ToString();
                    string email = table.Rows[i]["Email"].ToString();
                    string licenseNum = table.Rows[i]["LicenseNum"].ToString();
                    string licenseState = table.Rows[i]["LicenseState"].ToString();
                    DateTime licenseExpiration = Convert.ToDateTime(table.Rows[i]["LicenseExpiration"]);

                    Driver driver = new Driver(id, firstName, lastName, phone, email, null, licenseNum, licenseState, 
                                               licenseExpiration, UtilityClass.DriverStatus.Available, "", false);

                    drivers.Add(driver);
                }
            }
            catch (Exception e)
            {
            }

            connection.Close();

            return drivers;
        }

        /* Pre:
         * Post: Assigns input driver to input truck
         */
        public static bool AssignDriverToTruck(int driverId, int truckId)
        {
            bool result = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "DriverAssignToTruck";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@driverId", driverId);
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
 