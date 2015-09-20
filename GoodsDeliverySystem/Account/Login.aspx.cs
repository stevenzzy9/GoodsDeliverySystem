using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using GoodsDeliverySystem.Models;
using System.Configuration;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
         * Pre:
         * Post: Attempts to log the user into the system using the
         *       input username and password.  Redirects upon valid login
         */
        public void LogIn(object sender, EventArgs e)
        {
            //get user type
            UtilityClass.UserTypes userType = UtilityClass.UserTypes.Customer;
            if (UserType.SelectedValue.Equals("Staff"))
                userType = UtilityClass.UserTypes.Staff;
            else if (UserType.SelectedValue.Equals("Manager"))
                userType = UtilityClass.UserTypes.Manager;

            UserCredentials credentials = new UserCredentials(UserName.Text, Password.Text, userType, true);
            bool loggedIn = credentials.Login();

            if (loggedIn)
            {
                //save user type and username
                Session["userType"] = userType;
                Session["username"] = credentials.username;

                //check if the user needs to change their password
                if (DbInterfacePerson.NeedToChangePassword(UserName.Text))
                    Response.Redirect("ChangePassword.aspx");

                //direct to appropriate menu
                if (userType == UtilityClass.UserTypes.Manager)
                    Response.Redirect("ManagerMenu.aspx");
                else if (userType == UtilityClass.UserTypes.Staff)
                    Response.Redirect("StaffMenu.aspx");
                else if (userType == UtilityClass.UserTypes.Customer)
                    Response.Redirect("CustomerMenu.aspx");
            }
            else
            {
                FailureText.Text = "Invalid username, password, or user type.";
                ErrorMessage.Visible = true;
            }
        }
    }
}