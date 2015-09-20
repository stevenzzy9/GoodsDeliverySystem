using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.DatabaseAccess;
using GoodsDeliverySystem.Models;

namespace GoodsDeliverySystem.Shipments
{
    public partial class AwaitingLocalDelivery : System.Web.UI.Page
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
            if (gvShipments.SelectedIndex >= 0)
                ConfirmationPanel.Visible = true;
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            if (gvShipments.SelectedIndex >= 0)
            {
                int id = Convert.ToInt32(gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text);

                //update
                DbInterfaceShipment.ChangeStatusToAwaitingLocalDelivery(id);

                //refresh gridview
                gvShipments.DataBind();
                ConfirmationPanel.Visible = false;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ConfirmationPanel.Visible = false;
        }
    }
}