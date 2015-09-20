/*******************************************************
 * This class is responsible for database interactions
 * regarding people and users
 *******************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GoodsDeliverySystem.DatabaseAccess
{
    public partial class DbInterfacePerson
    {
        /*
         * Pre:  An existing user may not have the same email address
         * Post: The new user is added to the system
         * @returns the user id if the user is successfully added and -1 otherwise
         */
        public static int RegisterUser(string firstName, string lastName, string phone, string email, string username, 
                                       string password, UtilityClass.UserTypes userType, bool changePassword)
        {
            int userId = -1;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "UserRegister";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@userType", userType);
                cmd.Parameters.AddWithValue("@needPasswordReset", changePassword);

                adapter.Fill(table);

                if (table.Rows.Count == 1)
                    userId = Convert.ToInt32(table.Rows[0]["UserId"]);
            }
            catch (Exception e)
            {
                userId = -1;
            }

            connection.Close();

            return userId;
        }

        /*
         * Pre:
         * Post: Determines whether or not a correct username and password were entered
         * @param username is the entered username
         * @param password is the encoded password
         * @returns true if the login information is valid and false otherwise
         */
        public static bool Login(string username, string password, UtilityClass.UserTypes userType)
        {
            bool loggedIn = false;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "UserLogin";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@userType", userType);

                adapter.Fill(table);

                if (table.Rows.Count == 1)
                    loggedIn = true;
            }
            catch (Exception e)
            {
               
            }

            connection.Close();

            return loggedIn;
        }

        /*
         * Pre:
         * Post: The user type of the user with the input username is updated
         * @param username is the username of the user to update
         * @param newUserType is the new user type/role of the user
         * @returns true if the update is successful and false otherwise
         */
        public static bool UpdateUserType(string username, UtilityClass.UserTypes newUserType)
        {
            bool success = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "UserChangeRole";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@newRole", newUserType);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
                success = false;
            }

            connection.Close();

            return success;
        }

        /*
         * Pre:
         * Post: Determines whether a user already exists with the input email address
         * @param email is the email address to look for
         * @returns true if the email address exists and false otherwise
         */
        public static bool EmailExists(string email)
        {
            bool exists = false;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "UserSelectByEmail";
                
                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@email", email);

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


        /*
         * Pre:  
         * Post: Update the random password with inputing email
         * @param email
         * @returns 
         */
        public static void retrieveCredentials(string email)
        {
            String randomPassword = UtilityClass.MakeRandomPassWord();
            String content = "Your new password is " + randomPassword;
            UtilityClass.SendEmail(email,"New password",content);
            
            string encodeNewPassword = UtilityClass.GetMd5Hash(randomPassword);
          
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "UpdateRandomPassword";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@randomPassword", encodeNewPassword);

                adapter.Fill(table);

                
            }
            catch (Exception e)
            {
               
            }

            connection.Close();
        }

        public static void userUpdatePassword(string username, string newPassword)
        {
            string encodeUserNewPassword = UtilityClass.GetMd5Hash(newPassword);
          
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "ChangePassword";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@newPassword", encodeUserNewPassword);

                adapter.Fill(table);
            }
            catch (Exception e)
            {
               
            }

            connection.Close();
        }


        /* Pre:
         * Post: 
         * @returns true if the account username and password match and false otherwise
         */
        public static bool MatchPassword(string username, string oldpassword)
        {
            string encodeOldPassword = UtilityClass.GetMd5Hash(oldpassword);

            bool match = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "MatchPassword";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@oldPassword", encodeOldPassword);

                adapter.Fill(table);

                if (table.Rows.Count < 1)
                    match = false;
            }
            catch (Exception e)
            {
               // match = true;
            }

            connection.Close();

            return match;
        }

        /*
         * Pre:
         * Post: Determine whether or not the user with the input username needs
         *       to update their password
         * @returns true if the password must be updated and false otherwise
         */
        public static bool NeedToChangePassword(string username)
        {
            bool need = true;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "NeedToChangePassword";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
               
                adapter.Fill(table);
                
                Byte needPasswordReset = Convert.ToByte(table.Rows[0]["NeedsPasswordReset"]);

                if (needPasswordReset == 1)
                {
                    need = true;
                }
                else 
                {
                    need = false;
                } 
            }
            catch (Exception e)
            {
                // match = true;
            }

            connection.Close();

            return need;
        }

        /*
         * Pre:  Customer exists with input username
         * Post: The customer information is retrieved based on the input username
         */
        public static Customer GetCustomer(string username)
        {
            Customer customer = null;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

            try
            {
                connection.Open();
                string storedProc = "UserSelect";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    int id = Convert.ToInt32(table.Rows[0]["UserId"]);
                    string firstName = table.Rows[0]["FirstName"].ToString();
                    string lastName = table.Rows[0]["LastName"].ToString();
                    string email = table.Rows[0]["Email"].ToString();
                    string phone = table.Rows[0]["Phone"].ToString();
                    string password = table.Rows[0]["Password"].ToString();
                    int userType = Convert.ToInt32(table.Rows[0]["UserType"]);

                    customer = new Customer(id, firstName, lastName, phone, email, password, (UtilityClass.UserTypes)userType);
                }
            }
            catch (Exception e)
            {
                customer = null;
            }

            connection.Close();

            return customer;
        }
    }
}