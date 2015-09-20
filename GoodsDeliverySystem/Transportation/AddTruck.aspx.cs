using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;
using GoodsDeliverySystem.DatabaseAccess;
using System.Text.RegularExpressions;

namespace GoodsDeliverySystem.Transportation
{
    public partial class AddTruck : System.Web.UI.Page
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
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            lblTruckExists.Visible = false;

            if (!DbInterfaceTruck.TruckExists(LicensePlateNum.Text, LicensePlateState.SelectedValue))
            {
                Truck truck = new Truck(LicensePlateNum.Text, LicensePlateState.Text,  Convert.ToInt32(Model.SelectedValue), 
                                        Convert.ToDouble(FuelTankSize.Text), Convert.ToDouble(Odometer.Text), Convert.ToDouble(Length.Text), 
                                        Convert.ToDouble(Width.Text), Convert.ToDouble(Height.Text), Convert.ToDouble(Weight.Text), 
                                        Remarks.Text, LocalTruck.Checked);

                if (truck.AddTruckToDatabase())
                {
                    MainForm.Visible = false;
                    Confirmation.Visible = true;
                }
            }
            else
                lblTruckExists.Visible = true;
        }

        /*
         * Pre:
         * Post: Validate that the length field is a number between 150 and 480
         */
        protected void LengthValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(Length.Text, out num))
            {
                if (num < 150 || num > 480)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the width field is a number between 60 and 102
         */
        protected void WidthValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(Width.Text, out num))
            {
                if (num < 60 || num > 102)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the height field is a number between 60 and 168
         */
        protected void HeightValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(Height.Text, out num))
            {
                if (num < 60 || num > 168)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the weight field is a number between 2000 and 40000
         */
        protected void WeightValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(Weight.Text, out num))
            {
                if (num < 2000 || num > 40000)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the odometer field is a number between 0 and 100,000
         */
        protected void OdometerValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(Odometer.Text, out num))
            {
                if (num < 0 || num > 100000)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the fuel tank size field is a number between 25 and 200
         */
        protected void FuelTankValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(FuelTankSize.Text, out num))
            {
                if (num < 25 || num > 200)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        //"[A-Z]{3}-\d{3}"
        /*
         * Pre:
         * Post: Validates that the three phone number fields are filled in correctly
         */
        protected void LicensePlateNum_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex reg = new Regex(@"^\d{3}-[A-Z]{3}$");

            if (LicensePlateState.SelectedValue == "MN" || LicensePlateState.SelectedValue == "WI")
                reg = new Regex(@"^\d{3}-[A-Z]{3}$");
            else if (LicensePlateState.SelectedValue == "IN")
                reg = new Regex(@"^[A-Z0-9]{4,8}$");
            else if (LicensePlateState.SelectedValue == "OH")
                reg = new Regex(@"^\d{3}[A-Z]{4}$");
            else if (LicensePlateState.SelectedValue == "IL")
                reg = new Regex(@"^\d{1}[A-Z]{6}$");

            args.IsValid = !LicensePlateNum.Text.Equals("") && reg.IsMatch(LicensePlateNum.Text);
        }
    }
}