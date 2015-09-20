using GoodsDeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoodsDeliverySystem.Shipments
{
    public partial class ViewShipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //make sure the user is logged in
                if (Session["userType"] != null)
                {
                    UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                    if (userType != UtilityClass.UserTypes.Customer)
                        Response.Redirect("/Account/Login.aspx");
                }
                else
                    Response.Redirect("/Account/Login.aspx");


                if (Request.QueryString["shipment"] == null)
                {
                    Response.Redirect("/Shipments/UserShipments.aspx");
                }
                else
                {
                    int shipmentID;
                    if (!int.TryParse(Request.QueryString["shipment"], out shipmentID))
                    {
                        Response.Redirect("/Shipments/UserShipments.aspx");
                    }
                }
            }
        }
    }
}