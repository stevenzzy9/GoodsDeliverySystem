using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using GoodsDeliverySystem.Models;
using GoodsDeliverySystem.DatabaseAccess;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GoodsDeliverySystem.Shipments
{
    public partial class RegisterShipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //make sure the user is logged in
                if (Session["userType"] != null)
                {
                    UtilityClass.UserTypes userType = (UtilityClass.UserTypes)Session["userType"];

                    if (userType != UtilityClass.UserTypes.Customer)
                        Response.Redirect("/Account/Login.aspx");
                }
                else
                    Response.Redirect("/Account/Login.aspx");

                //populate year dropdown for credit card
                for (int i = 0; i < 5; i++)
                    Year.Items.Add(new ListItem((DateTime.Today.Year + i).ToString(), (DateTime.Today.Year + i).ToString()));

                //add onchange event to insurance dropdown
                Insurance.Attributes["onchange"] = "window.updateCostOptions()";
                Weight.Attributes["onchange"] = "window.updateCostOptions()";
                ShippingSpeed.Attributes["onchange"] = "window.updateCostOptions()";
                Cost.Text = "$0.00";
            }
            else
                calculateShipping();
        }

        /*
         * Pre:
         * Post: If the entered data is valid, move on to the delivery information
         */
        protected void ToDeliveryInfo_Click(object sender, EventArgs e)
        {
            Page.Validate("Shipment");

            if (Page.IsValid)
            {
                ShipmentInfo.Visible = false;
                DeliveryInfo.Visible = true;
            }
        }

        /*
         * Pre:
         * Post: If the delivery information is valid, show the payment section
         */
        protected void ToPaymentInfo_Click(object sender, EventArgs e)
        {
            Page.Validate("Delivery");

            if (Page.IsValid)
            {
                DeliveryInfo.Visible = false;
                PaymentInfo.Visible = true;
            }
        }

        /*
         * Pre:
         * Post: The delivery information is hidden and the shipment information is shown
         */
        protected void BackToShipmentInfo_Click(object sender, EventArgs e)
        {
            ShipmentInfo.Visible = true;
            DeliveryInfo.Visible = false;
        }

        /*
         * Pre:
         * Post: If the payment information is valid, submit the shipment
         */
        protected void Submit_Click(object sender, EventArgs e)
        {
            Page.Validate("Payment");

            //collect information and enter into database
            if (Page.IsValid)
            {
                double length = Convert.ToDouble(Length.Text);
                double width = Convert.ToDouble(Width.Text);
                double height = Convert.ToDouble(Height.Text);
                double weight = Convert.ToDouble(Weight.Text);
                double cost = Convert.ToDouble(Cost.Text.Substring(1));
                Customer customer = DbInterfacePerson.GetCustomer(Session["username"].ToString());
                Address deliveryAddress = GetDeliveryAddress();
                CreditCard card = GetCreditCard();

                double value = 0;
                if (ItemValue.Text.Length > 0)
                    value = Convert.ToDouble(ItemValue.Text);

                ShipmentType type = null;
                if (ShipmentType.SelectedIndex > 0)
                    type = new ShipmentType(Convert.ToInt32(ShipmentType.SelectedValue), ShipmentType.SelectedItem.Text);

                UtilityClass.ShippingSpeed speed = UtilityClass.ShippingSpeed.Normal;
                if (ShippingSpeed.SelectedIndex == 1)
                    speed = UtilityClass.ShippingSpeed.Express;
                else if (ShippingSpeed.SelectedIndex == 2)
                    speed = UtilityClass.ShippingSpeed.Urgent;

                InsuranceType insurance = null;
                if (Insurance.SelectedIndex > 0)
                    insurance = new InsuranceType(Convert.ToInt32(Insurance.SelectedValue), Insurance.SelectedItem.Text);

                Shipment shipment = new Shipment(customer, length, width, height, weight, type, insurance,
                                                 value, cost, FirstName.Text, LastName.Text, deliveryAddress, card, 
                                                 false, speed);

                //if successfully added, show confirmation message
                int id = shipment.AddToDatabase();
                if (id != -1)
                {
                    string trackingNum = GetTrackingNumber(id);
                    ConfirmLabel.Text = "Thank you for using the La Crosse Parcel Service! Your shipment has been registered and is being processed. You will be notified when your shipment is accepted";
                    MainPage.Visible = false;
                    Confirmation.Visible = true;


                    UtilityClass.SendEmail(customer.email, "Shipment in Processing", "Thank you for using the La Crosse Parcel Service!  " +
                                           "Your tracking number is " + trackingNum + 
                                           ".  Please visit 138.49.101.81/Account/Login to view your shipment status.");
                }
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

        /*
         * Pre:  Delivery address information must be complete and valid
         * Post: An address object is made out of the delivery address information
         */
        private Address GetDeliveryAddress()
        {
            int zip = Convert.ToInt32(Zip.Text);

            Address address = new Address(Street.Text, City.Text, State.SelectedValue, zip);

            return address;
        }

        /*
         * Pre:  Credit card information must be complete and valid
         * Post: A credit card object is made out of the input information
         */
        private CreditCard GetCreditCard()
        {
            string cardNum = Regex.Replace(CardNumber.Text, "[^0-9]", "");
            int securityCode = Convert.ToInt32(SecurityCode.Text);
            int expirationMonth = Convert.ToInt32(Month.SelectedValue);
            int expirationYear = Convert.ToInt32(Year.SelectedValue);

            //billing address
            int zip = Convert.ToInt32(ZipPay.Text);
            Address address = new Address(StreetPay.Text, CityPay.Text, StatePay.SelectedValue, zip);

            CreditCard card = new CreditCard(NameOnCard.Text, CardType.SelectedValue, cardNum, securityCode, 
                                             expirationMonth, expirationYear, address);

            return card;
        }

        protected void BackToDeliveryInfo_Click(object sender, EventArgs e)
        {
            PaymentInfo.Visible = false;
            DeliveryInfo.Visible = true;
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
                if (num <= 0 || num > 120)
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
                if (num <= 0 || num > 120)
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
                if (num <= 0 || num > 120)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the weight field is a number between 0 and 150
         */
        protected void WeightValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Double.TryParse(Weight.Text, out num))
            {
                if (num <= 0 || num > 150)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Validate that the item value field is a number between 0 and 25,000
         */
        protected void ValueValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            double num;

            if (Insurance.SelectedIndex > 0)
            {
                if (ItemValue.Text.Length > 0 && Double.TryParse(ItemValue.Text, out num))
                {
                    if (num < .01 || num > 25000)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
                else if (ItemValue.Text.Length == 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
                args.IsValid = false;
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
         * Post: Validates that the zip code field contains 5 digits
         */
        protected void ZipPayValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex zipRegex = new Regex(@"\d{5}");
            args.IsValid = ZipPay.Text.Length == 5 && zipRegex.IsMatch(ZipPay.Text);
        }

        /*
         * Pre:
         * Post: Make sure the security code is 3 or 4 digits
         */
        protected void SecurityCodeValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex codeRegex3 = new Regex(@"\d{3}");
            Regex codeRegex4 = new Regex(@"\d{4}");
            args.IsValid = (codeRegex3.IsMatch(SecurityCode.Text) || codeRegex4.IsMatch(SecurityCode.Text)) && SecurityCode.Text.Length < 5;
        }

        /*
         * Pre:
         * Post: Make sure the selected expiration date is not in the past
         */
        protected void ExpirationValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Month.SelectedIndex > 0 && Year.SelectedIndex > 0)
            {
                //find earliest acceptable expiration
                int month = DateTime.Today.Month;
                int year = DateTime.Today.Year;
                DateTime earliestAcceptableDate = Convert.ToDateTime(month + "/1/" + year);

                //get selected expiration
                month = Convert.ToInt32(Month.SelectedValue);
                year = Convert.ToInt32(Year.Text);
                DateTime actualExpiration = Convert.ToDateTime(month + "/1/" + year);

                if (earliestAcceptableDate > actualExpiration)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = false;
        }

        /*
         * Pre:
         * Post: Make sure the card number is valid
         */
        protected void CardNumValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex codeRegex;
            bool isMatch = true;
            int temp;

            //make sure string contains only numbers, spaces, and dashes
            for (int i = 0; i < CardNumber.Text.Length; i++)
            {
                if (!CardNumber.Text[i].Equals(' ') && !CardNumber.Text[i].Equals('-') && 
                    !Int32.TryParse(CardNumber.Text[i].ToString(), out temp))
                {
                    args.IsValid = false;
                    return;
                }
            }

            //get rid of spaces and dashes
            string cardNum = Regex.Replace(CardNumber.Text, "[^0-9]", "");

            //make sure card numbers are correct length and contain required digits
            if (CardType.SelectedValue.Equals("Visa"))
            {
                codeRegex = new Regex(@"^4[0-9]{15}$");
                isMatch = codeRegex.IsMatch(cardNum);
            }
            else if (CardType.SelectedValue.Equals("MasterCard"))
            {
                codeRegex = new Regex(@"^5[1-5][0-9]{14}$");
                isMatch = codeRegex.IsMatch(cardNum);
            }
            else if (CardType.SelectedValue.Equals("Discover"))
            {
                codeRegex = new Regex(@"^6(?:011|5[0-9]{2})[0-9]{12}$");
                isMatch = codeRegex.IsMatch(cardNum);
            }
            else //card type is empty so don't validate this yet
            {
                args.IsValid = true;
                return;
            }

            args.IsValid = isMatch && Mod10Check(cardNum);
        }

        /*
         * Pre:
         * Post: Uses Luhn algorithm to determine whether card number is valid
         */
        public static bool Mod10Check(string creditCardNumber)
        {
            // check whether input string is null or empty
            if (string.IsNullOrEmpty(creditCardNumber))
                return false;

            //// 1.	Starting with the check digit double the value of every other digit 
            //// 2.	If doubling of a number results in a two digits number, add up
            ///   the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);


            //// If the final sum is divisible by 10, then the credit card number
            //   is valid. If it is not divisible by 10, the number is invalid.            
            return sumOfDigits % 10 == 0;
        }

        /*
         * Pre:
         * Post: Calculate cost
         */
        private void calculateShipping()
        {
            int weight;
            double cost = 0.0;

            if (Int32.TryParse(Weight.Text, out weight))
            {
                if (weight <= 0)
                    cost = 0;
                else if (weight < 5)
                    cost = 5.75;
                else if (weight < 10)
                    cost = 10.75;
                else if (weight < 20)
                    cost = 17.95;
                else if (weight < 30)
                    cost = 24.95;
                else if (weight < 45)
                    cost = 28.95;
                else if (weight < 70)
                    cost = 35.95;
                else if (weight < 90)
                    cost = 67.95;
                else if (weight < 110)
                    cost = 93.95;
                else if (weight >= 110)
                    cost = 110.95;
            }

            //add arbitary insurance cost
            if (Insurance.SelectedIndex == 1)
                cost += 2.0;
            else if (Insurance.SelectedIndex == 2)
                cost += 5.0;

            if (cost == 0.0)
                Cost.Text = "$0.00";
            else
                Cost.Text = "$" + cost;
        }
    }
}