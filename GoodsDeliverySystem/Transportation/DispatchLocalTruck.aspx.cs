using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.DatabaseAccess;
using GoodsDeliverySystem.Models;

namespace GoodsDeliverySystem.Transportation
{
    public partial class DispatchLocalTruck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //make sure the user is logged in
                if (Session["userType"] != null)
                {
                    UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                    if (userType != UtilityClass.UserTypes.Staff)
                        Response.Redirect("/Account/Login.aspx");
                }
                else
                    Response.Redirect("/Account/Login.aspx");
            }
        }

        protected void gvShipments_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = gvShipments.SelectedIndex;

            //set id fo the selected truck
            if (row > -1)
            {
                TruckId.Text = gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text;
                ConfirmationPanel.Visible = true;
                MessagePanel.Visible = false;
            }
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            bool result = DbInterfaceTruck.DispatchTruck(Convert.ToInt32(TruckId.Text));

            gvShipments.DataBind();
            MessagePanel.Visible = true;
            ConfirmationPanel.Visible = false;

            if (result)
                Message.Text = "The truck was dispatched";
            else
                Message.Text = "The truck could not be dispatched";
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ConfirmationPanel.Visible = false;
            TruckId.Text = "";
        }
    }
}