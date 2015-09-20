using GoodsDeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoodsDeliverySystem.Shipments
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] != null)
            {
                UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                if (userType != UtilityClass.UserTypes.Manager)
                    Response.Redirect("/Account/Login.aspx");
            }
            else
                Response.Redirect("/Account/Login.aspx");

            if (!IsPostBack)
            {
                DateTime now = DateTime.Now;
                txtStartDate.Text = now.ToString("yyyy") + "-" + now.ToString("MM") + "-01";
                txtEndDate.Text = now.ToString("yyyy-MM-dd");
                LoadRevenueReport();
            }

            var startDate = DateTime.Parse(txtStartDate.Text);
            var endDate = DateTime.Parse(txtEndDate.Text);
            lblStartDate.Text = startDate.ToString("MM/dd/yyyy");
            lblEndDate.Text = endDate.ToString("MM/dd/yyyy");

        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            LoadRevenueReport();
            gvShipmentTypes.DataBind();
            gvShipmentLocations.DataBind();
            gvStatusUpdates.DataBind();
        }

        private void LoadRevenueReport()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GoodsDeliveryConnectionString"].ConnectionString);
                var command = new SqlCommand();
                command.CommandText = "ReportRevenue";
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("startDate", txtStartDate.Text);
                command.Parameters.AddWithValue("endDate", txtEndDate.Text);

                command.Connection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader[0] == DBNull.Value)
                    {
                        lblRevenue.Text = "$0";
                    }
                    else { 
                        lblRevenue.Text = "$" + reader[0];
                    }
                }
                else
                {
                    lblRevenue.Text = "$0";
                }

                reader.Close();
                command.Connection.Close();
            }
            catch (Exception)
            {
                lblRevenue.Text = "$0";
            }
        }
    }
}