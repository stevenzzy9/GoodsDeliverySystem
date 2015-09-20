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
    public partial class AssignLocalDriverToTruck : System.Web.UI.Page
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

        protected void gvTrucks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show message if there aren't any drivers available
            int row = gvTrucks.SelectedIndex;

            //set id for the selected truck
            if (row > -1)
            {
                TruckId.Text = gvTrucks.Rows[gvTrucks.SelectedIndex].Cells[1].Text;

                Drivers.Items.Clear();
                Drivers.Items.Add(new ListItem("", ""));
                Drivers.DataBind();

                if (Drivers.Items.Count == 1)
                {
                    SelectDriver.Text = "No available drivers";
                    SelectDriver.Visible = true;
                }
                else
                    SelectDriver.Visible = false;

                ConfirmationPanel.Visible = true;
                MessagePanel.Visible = false;
            }
        }

        /*
         * Pre:
         * Post: Assign selected driver to selected truck
         */
        protected void OK_Click(object sender, EventArgs e)
        {
            bool result = DbInterfaceDriver.AssignDriverToTruck(Convert.ToInt32(Drivers.SelectedValue), Convert.ToInt32(TruckId.Text));

            gvTrucks.DataBind();
            ConfirmationPanel.Visible = false;
            MessagePanel.Visible = true;

            if (result)
                Message.Text = "The driver was added to the truck";
            else
                Message.Text = "The driver could not be added to the truck";
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            TruckId.Text = "";
            ConfirmationPanel.Visible = false;
            MessagePanel.Visible = false;
        }
    }
}