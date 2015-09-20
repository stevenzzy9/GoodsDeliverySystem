using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;
using GoodsDeliverySystem.DatabaseAccess;
using System.Text.RegularExpressions;

namespace GoodsDeliverySystem.Account
{
    public partial class RegisterStaffManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] != null)
            {
                UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                if (userType != UtilityClass.UserTypes.Manager)
                    Response.Redirect("/Account/Login.aspx");
            }
            else
                Response.Redirect("/Account/Login.aspx");
        }

        /*
         * Pre:  The entered email address must be unique
         * Post: The new user is registered in the system and redirected
         */
        public void RegisterUser(object sender, EventArgs e)
        {

            Page.Validate();
            if (!Page.IsValid) return;

            bool isManager = UserType.SelectedIndex == 1;
            String phone = String.Format("{0}-{1}-{2}", PhoneAreaCode.Text, Phone2.Text, Phone3.Text);
            string password = UtilityClass.MakeRandomPassWord();

            Staff staff = new Staff(FirstName.Text, LastName.Text, phone, Email.Text, password);

            if (!DbInterfacePerson.EmailExists(staff.email))
            {
                staff.Register();

                UtilityClass.SendEmail(staff.email, "New Goods Delivery System Account", "You have been registered " +
                                       "for the Goods Delivery System.  Your password is " + password + 
                                       ".  You will change this password upon logging into the system.");

                MainForm.Visible = false;
                Confirmation.Visible = true;
            }
            else
            {
                FailureText.Text = "There is already an account associated with this email.";
                ErrorMessage.Visible = true;
            }
        }

        protected void PhoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex phoneRegex = new Regex(@"\d{10}");
            args.IsValid = PhoneAreaCode.Text.Length == 3 && Phone2.Text.Length == 3 && Phone3.Text.Length == 4 && phoneRegex.IsMatch(String.Format("{0}{1}{2}", PhoneAreaCode.Text, Phone2.Text, Phone3.Text));
        }
    }
}