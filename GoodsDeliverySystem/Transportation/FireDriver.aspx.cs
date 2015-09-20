using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;
using System.Text.RegularExpressions;

namespace GoodsDeliverySystem.Transportation
{
    public partial class FireDriver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fire.Attributes.Add("onclick", "return confirm('Do you want to fire this driver?');");
            if (Session["userType"] != null)
            {
                UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                if (userType != UtilityClass.UserTypes.Manager)
                    Response.Redirect("/Account/Login.aspx");
            }
            else
                Response.Redirect("/Account/Login.aspx");

            if (gvDrivers.SelectedIndex >= 0)
            {
                gvDrivers.Rows[gvDrivers.SelectedIndex].BackColor = Color.FromArgb(200, 200, 249);
            }
            else
            {
                Fire.Enabled = false;
            }
        }

        protected void gvDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvDrivers.Rows)
            {
                row.BackColor = Color.White;
            }
            gvDrivers.Rows[gvDrivers.SelectedIndex].BackColor = Color.FromArgb(200, 200, 249);
            Fire.Enabled = true;
        }



        /*
        * Pre:
       * Post: check the status of the Driver .  when the driver can be fired, result is result. Otherwise the result is false.
       */

        protected static bool CheckDriverStatus(int Id) {

            bool result = true;
            int status = 0;

            DataTable table = new DataTable();
            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
            try
            {
                connection.Open();
                string storedProc = "SearchTheDriverStatus";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);


                adapter.Fill(table);
                status = Convert.ToInt32(table.Rows[0]["Status"]);
                if (status == 1)
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
            }
            connection.Close();

            return result;

        }

        protected void Fire_Click(object sender, EventArgs e)
        {

            if (gvDrivers.SelectedIndex < 0)
            {
                return;
            }
            
            var Driver_ID = gvDrivers.Rows[gvDrivers.SelectedIndex].Cells[1].Text;

            if (!FireDriver.CheckDriverStatus(int.Parse(Driver_ID)))
            {
                lblReason.Text = "The driver is currently in transit.";
                Error.Style["Display"] = "inline";
                return;
            }

            SqlConnection connection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
            try
            {
                connection.Open();
                string storedProc = "DriverDelete";

                SqlCommand cmd = new SqlCommand(storedProc, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Driver_ID", Driver_ID);

                cmd.ExecuteNonQuery();

                Success.Style["Display"] = "inline";

            }
            catch
            {
                Error.Style["Display"] = "inline";
            }
            gvDrivers.DataBind();

        }

        /*
        * Pre:
       * Post: Validates that the three phone number fields are filled in correctly
       */
        protected void LicenseNum_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex reg = new Regex(@"^[a-zA-Z]\d{13}$");

            if (gvDrivers.EditIndex < 0)
            {
                // No row is being edited
                return;
            }

            var LicenseState = (DropDownList) gvDrivers.Rows[gvDrivers.EditIndex].FindControl("state");
            var LicenseNum = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("licenseNum");

            if (LicenseState.SelectedValue == "WI")
                reg = new Regex(@"^[a-zA-Z]\d{13}$");
            else if (LicenseState.SelectedValue == "MN")
                reg = new Regex(@"^[a-zA-Z]\d{12}$");
            else if (LicenseState.SelectedValue == "IN")
                reg = new Regex(@"^\d{9}$");
            else if (LicenseState.SelectedValue == "OH")
                reg = new Regex(@"^\d{8}$");
            else if (LicenseState.SelectedValue == "IL")
                reg = new Regex(@"^[a-zA-Z]\d{12}$");

            args.IsValid = !LicenseNum.Text.Equals("") && reg.IsMatch(LicenseNum.Text);
        }

        protected void gvDrivers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var Driver_ID = gvDrivers.Rows[e.RowIndex].Cells[2].Text;

            if (!FireDriver.CheckDriverStatus(int.Parse(Driver_ID)))
            {
                lblReason.Text = "The driver is currently in transit.";
                Error.Style["Display"] = "inline";
                e.Cancel = true;
                return;
            }
        }
    }
}