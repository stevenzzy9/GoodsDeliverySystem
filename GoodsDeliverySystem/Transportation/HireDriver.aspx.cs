using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using GoodsDeliverySystem.Models;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Transportation
{
    public partial class HireDriver : System.Web.UI.Page
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
        }

        /*
         * Pre: 
         * Post: If all entered values are valid, the new driver is added to the system
         */
        protected void Hire_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            Address address = new Address(Street.Text, City.Text, State.SelectedValue, Convert.ToInt32(Zip.Text));
            Driver driver = new Driver(FirstName.Text, LastName.Text, String.Format("{0}-{1}-{2}", PhoneAreaCode.Text,
                                       Phone2.Text, Phone3.Text), Email.Text, address, LicenseNum.Text, LicenseState.SelectedValue,
                                       Convert.ToDateTime(LicenseExpiration.Text), UtilityClass.DriverStatus.Available, Remark.Text,
                                       LocalDriver.Checked);

            lbDriverExists.Visible = false;
            if (!DbInterfaceDriver.DriverExists(LicenseNum.Text, LicenseState.SelectedValue))
            {
                if (DbInterfaceDriver.HireDriver(driver.firstName, driver.lastName, address.street, address.city, address.state, 
                        address.zip, driver.phone, driver.email, driver.licenseNum, driver.licenseState, driver.licenseExpiration.ToString(), 
                        driver.remark, driver.localDriver))
                {
                    MainForm.Visible = false;
                    Confirmation.Visible = true;
                }
            }
            else
            {
                lbDriverExists.Visible = true;
            }
        }

        /*
         * Pre:
         * Post: Validates that the three phone number fields are filled in correctly
         */
        protected void PhoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex phoneRegex = new Regex(@"\d{10}");
            args.IsValid = PhoneAreaCode.Text.Length == 3 && Phone2.Text.Length == 3 && Phone3.Text.Length == 4 && phoneRegex.IsMatch(String.Format("{0}{1}{2}", PhoneAreaCode.Text, Phone2.Text, Phone3.Text));
        }

        /*
         * Pre:
         * Post: Validates that the zip code field contains 5 digits
         */
        protected void ZipValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex zipRegex = new Regex(@"\d{5}");
            args.IsValid = Zip.Text.Length == 5 && zipRegex.IsMatch(Zip.Text);
        }

        

        /*
         * Pre:
         * Post: Validates that the three phone number fields are filled in correctly
         */
        protected void LicenseNum_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex reg = new Regex(@"^[a-zA-Z]\d{13}$");

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

        /*
         * Pre:
         * Post: Validates the the license is not already expired
         */
        protected void LicenseExpValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = DateTime.Today < Convert.ToDateTime(LicenseExpiration.Text);
        }
    }
}