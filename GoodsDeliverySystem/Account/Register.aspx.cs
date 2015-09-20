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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
         * Pre:  The entered email address must be unique
         * Post: The new user is registered in the system and redirected
         */
        public void RegisterUser(object sender, EventArgs e)
        {

            Page.Validate();
            if (!Page.IsValid) return;

            Customer customer = new Customer(FirstName.Text, LastName.Text, String.Format("{0}-{1}-{2}",PhoneAreaCode.Text,
                                             Phone2.Text, Phone3.Text), Email.Text, Password.Text, UtilityClass.UserTypes.Customer);

            if (!DbInterfacePerson.EmailExists(customer.email))
            {
                customer.Register();

                Response.Redirect("/Default.aspx");
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
            args.IsValid = PhoneAreaCode.Text.Length == 3 && Phone2.Text.Length == 3 && Phone3.Text.Length == 4 && phoneRegex.IsMatch(String.Format("{0}{1}{2}",PhoneAreaCode.Text, Phone2.Text, Phone3.Text));
        }
    }
}