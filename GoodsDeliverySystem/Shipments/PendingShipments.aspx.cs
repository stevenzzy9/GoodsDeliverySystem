using GoodsDeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoodsDeliverySystem.Shipments
{
    public partial class PendingShipments : System.Web.UI.Page
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

        protected void gvPendingShipments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = gvPendingShipments.SelectedIndex;
            Response.Redirect(Request.RawUrl);
            gvPendingShipments.SelectedIndex = index;
        }

        protected void btnAccept_Click(object sender, CommandEventArgs e)
        {
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
            try
            {
                connection.Open();
                string storedProc = "ShipmentAccept";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", e.CommandArgument);

                cmd.ExecuteNonQuery();

                String email = GetEmail(Convert.ToInt32(e.CommandArgument));

                var trackingNum = GetTrackingNumber(int.Parse(e.CommandArgument.ToString()));

                UtilityClass.SendEmail(email, "Shipment Accepted", "Thank you for using the La Crosse Parcel Service!  " +
                                            "Your shipment has been accepted. " +
                                           "Your tracking number is " + trackingNum +
                                           ".  Please visit 138.49.101.81/Account/Login to view your shipment status.");

                gvPendingShipments.DataBind();
            }
            catch
            {

            }
        }

        private string GetTrackingNumber(int i)
        {
            string trackingNumber = null;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
            try
            {
                connection.Open();
                string storedProc = "GetTrackingNum";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", i);


                adapter.Fill(table);
                trackingNumber = table.Rows[0]["trackingNumber"].ToString();
            }
            catch (Exception e)
            {
            }

            connection.Close();

            return trackingNumber;

        }

        private string GetEmail(int i)
        {
            string email = null;
            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
            try
            {
                connection.Open();
                string storedProc = "GetShipmentEmail";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@shipmentId", i);


                adapter.Fill(table);
                email = table.Rows[0]["Email"].ToString();
            }
            catch (Exception e)
            {
            }

            connection.Close();

            return email;

        }

        protected void btnDeny_Click(object sender, CommandEventArgs e)
        {
            SqlConnection connection = new
                 SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
            try
            {
                connection.Open();
                string storedProc = "ShipmentDeny";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", e.CommandArgument);

                cmd.ExecuteNonQuery();

                String email = GetEmail(Convert.ToInt32(e.CommandArgument));

                var trackingNum = GetTrackingNumber(int.Parse(e.CommandArgument.ToString()));

                UtilityClass.SendEmail(email, "Shipment Denied", "Thank you for using the La Crosse Parcel Service!  " +
                                            "Unfortunately, your shipment has been denied. " +
                                           "Please visit 138.49.101.81/Account/Login to view your shipment status.");

                gvPendingShipments.DataBind();
            }
            catch
            {

            }
        }
    }
}