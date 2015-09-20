using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.DatabaseAccess;
using GoodsDeliverySystem.Models;

namespace GoodsDeliverySystem.Shipments
{
    public partial class ReceiveShipment : System.Web.UI.Page
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

        protected void gvShipments_SelectedIndexChanged(object sender, EventArgs e) {
            ReceiveShipmentPanel.Visible = true;
            
        }
        protected void OK_Click(object sender, EventArgs e) {

            if (gvShipments.SelectedIndex >= 0)
            {
                int id = Convert.ToInt32(gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text);
                string status = "Approved for Local Delivery";

                DataTable table = new DataTable();
                SqlConnection connection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

                try
                {
                    connection.Open();
                    string storedProc = "ShipmentUpdateStatus";

                    SqlCommand cmd = new SqlCommand(storedProc, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@shipmentID", id);
                    cmd.Parameters.AddWithValue("@status", status);
                    adapter.Fill(table);


                }
                catch (Exception ex)
                {

                }

                connection.Close();
                gvShipments.DataBind();
                ReceiveShipmentPanel.Visible = false;
                DbInterfaceShipment.RunStatusUpdates();
            }

        }

        protected void Select_Click(object sender, EventArgs e) { 

        }
       
        protected void Cancel_Click(object sender, EventArgs e)
        {
            ReceiveShipmentPanel.Visible = false;
        }


    }
}