using GoodsDeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoodsDeliverySystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null)
            {
                ulManager.Style["display"] = "none";
                ulCustomer.Style["display"] = "none";
                ulStaff.Style["display"] = "none";
            }
            else if ((UtilityClass.UserTypes)Session["userType"] == UtilityClass.UserTypes.Manager) 
            {
                ulNotLoggedIn.Style["display"] = "none";
                ulCustomer.Style["display"] = "none";
                ulStaff.Style["display"] = "none";
            } 
            else if ((UtilityClass.UserTypes)Session["userType"] == UtilityClass.UserTypes.Staff) 
            {
                ulNotLoggedIn.Style["display"] = "none";
                ulCustomer.Style["display"] = "none";
                ulManager.Style["display"] = "none";
            } 
            else if ((UtilityClass.UserTypes)Session["userType"] == UtilityClass.UserTypes.Customer) 
            {
                ulNotLoggedIn.Style["display"] = "none";
                ulManager.Style["display"] = "none";
                ulStaff.Style["display"] = "none";
            }
            else
            {
                ulManager.Style["display"] = "none";
                ulCustomer.Style["display"] = "none";
                ulStaff.Style["display"] = "none";
            }
        }

        protected void LogOut(object sender, EventArgs e)
        {
            Session["userType"] = null;
            Response.Redirect(Request.RawUrl);
        }
    }
}