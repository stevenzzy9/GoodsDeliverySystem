using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            if(!NewPassword.Text.Equals(ConfirmPassword.Text)){
                NotMatchText.Text = "The new password and confirm password must be same";
                ErrorMessage.Visible = true;
                return;
            }

            //if the entered username and old password don't match a user in the system display an error
            //something like this:
            //FailureText.Text = "The entered username and password do not exist in the system.";
            //ErrorMessage.Visible = true;
            if (DbInterfacePerson.MatchPassword(Username.Text, OldPassword.Text))
            {
                DbInterfacePerson.userUpdatePassword(Username.Text,NewPassword.Text);
                Response.Redirect("Login.aspx");
            }
            else {
                FailureText.Text = "The entered username and password do not exist in the system.";
                ErrorMessage.Visible = true;
            }
            

        }

        /*
         * Pre:
         * Post: Validate that the new password field matches the confirm password field
         */
        protected void PasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //set args.IsValid = false if they don't match
        }
    }
}