using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;

namespace GoodsDeliverySystem.Account
{
    public partial class CustomerMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] != null)
            {
                UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                if (userType != UtilityClass.UserTypes.Customer)
                    Response.Redirect("/Account/Login.aspx");
            }
            else
                Response.Redirect("/Account/Login.aspx");
        }
    }
}