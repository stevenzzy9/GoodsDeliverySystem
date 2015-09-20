using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Account
{
    public partial class RetrieveCredentials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["userType"] != null)
            //{
            //    UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

            //    if (userType != UtilityClass.UserTypes.Manager)
            //        Response.Redirect("/Account/Login.aspx");
            //}
            //else
            //    Response.Redirect("/Account/Login.aspx");
        }

        protected void Submit(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessfulMessage.Visible = false;

            if (!DbInterfacePerson.EmailExists(Email.Text))
            {
                FailureText.Text = "The email address does not exist in the system.";
                ErrorMessage.Visible = true;
            }
            else
            {
                DbInterfacePerson.retrieveCredentials(Email.Text);

                SuccessfulText.Text = "Your temporary password has been sent to your email address on file";
                SuccessfulMessage.Visible = true;
                btnMenu.Visible = true;
            }
        }

        protected void btnMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Account/Login.aspx");
        }
    }
}