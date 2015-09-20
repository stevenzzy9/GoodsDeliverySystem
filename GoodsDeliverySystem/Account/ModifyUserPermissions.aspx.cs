using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace GoodsDeliverySystem.Account
{
    public partial class ModifyUserPermissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO uncomment after testing
            if (Session["userType"] != null)
            {
                UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                if (userType != UtilityClass.UserTypes.Manager)
                    Response.Redirect("/Account/Login.aspx");
            }
            else
                Response.Redirect("/Account/Login.aspx");

            Error.Style["Display"] = "none";
            Success.Style["Display"] = "none";
            Warning.Style["Display"] = "none";

        }

        /*
         * Pre:
         * Post: The information of the selected user is shown
         */
        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show selected name, email, and current role
            if (gvUsers.SelectedIndex >= 0)
            {
                SelectedName.Text = gvUsers.Rows[gvUsers.SelectedIndex].Cells[2].Text + " " + gvUsers.Rows[gvUsers.SelectedIndex].Cells[3].Text;
                SelectedEmail.Text = gvUsers.Rows[gvUsers.SelectedIndex].Cells[5].Text;

                if (gvUsers.Rows[gvUsers.SelectedIndex].Cells[6].Text.Equals("Manager"))
                    rblRole.SelectedIndex = 1;
                else if (gvUsers.Rows[gvUsers.SelectedIndex].Cells[6].Text.Equals("Staff"))
                    rblRole.SelectedIndex = 0;

                rblRole.Visible = true;
            }
        }

        /*
         * Pre: 
         * Post: The user role of the selected user is updated
         */
        protected void Update_Click(object sender, EventArgs e)
        {
            if (!SelectedName.Text.Equals("") && !SelectedEmail.Text.Equals("") && rblRole.SelectedIndex != -1)
            {
                SqlConnection connection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);

                try
                {
                    connection.Open();
                    string storedProc = "UserUpdateRole";

                    SqlCommand cmd = new SqlCommand(storedProc, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", SelectedEmail.Text);
                    cmd.Parameters.AddWithValue("@role", rblRole.SelectedIndex + 1);

                    cmd.ExecuteNonQuery();

                    Success.Style["Display"] = "inline";

                }
                catch
                {
                    Error.Style["Display"] = "inline";
                }
            }
            else
            {
                Warning.Style["Display"] = "inline";
            }
            gvUsers.DataBind();
        }
    }
}