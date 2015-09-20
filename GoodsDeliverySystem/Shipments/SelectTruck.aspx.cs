using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoodsDeliverySystem.Models;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Shipments
{
    public partial class SelectTruck : System.Web.UI.Page
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

        /*
         * Find truck for selected shipment
         */
        protected void gvShipments_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearPage();

            if (gvShipments.SelectedIndex >= 0)
            {
                //get shipment
                Shipment shipment = DbInterfaceShipment.GetShipmentForTruck(Convert.ToInt32(gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text));

                //get all paths from database
                Paths paths = new Paths();

                //get available trucks using selected shipping method
                UtilityClass.ShippingSpeed speed = UtilityClass.ShippingSpeed.Normal;

                if (gvShipments.SelectedRow.Cells[7].Text.Equals("Express"))
                    speed = UtilityClass.ShippingSpeed.Express;
                else if (gvShipments.SelectedRow.Cells[7].Text.Equals("Urgent"))
                    speed = UtilityClass.ShippingSpeed.Urgent;

                List<Truck> trucks = DbInterfaceTruck.GetAvailableTrucks(speed);

                //filter trucks and find only those with room
                if (trucks.Count > 0)
                {
                    List<Truck> temp = new List<Truck>();
                    foreach (Truck truck in trucks)
                    {
                        double length = Convert.ToDouble(gvShipments.SelectedRow.Cells[2].Text);
                        double width = Convert.ToDouble(gvShipments.SelectedRow.Cells[3].Text);
                        double height = Convert.ToDouble(gvShipments.SelectedRow.Cells[4].Text);
                        double volume = length * width * height;
                        double weight = Convert.ToDouble(gvShipments.SelectedRow.Cells[5].Text);

                        if (truck.TruckHasRoom(volume, weight))
                            temp.Add(truck);
                    }

                    trucks = temp;
                }

                //filter trucks and find only those going to the destination
                List<Truck> currentlyGoingToLoc = new List<Truck>();
                if (trucks.Count > 0)
                {
                    foreach (Truck truck in trucks)
                    {
                        //find those going to location - needs to include cities that don't already have deliveries going to them
                        if (truck.GoingToLocation(paths, shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state))
                            currentlyGoingToLoc.Add(truck);
                    }
                }

                //if there are already trucks going to the delivery location, find the best option
                if (currentlyGoingToLoc.Count > 0)
                {
                    trucks = currentlyGoingToLoc;
                }
                //if there aren't any trucks going to the delivery location, see if there are any that
                //can alter their route
                else
                {
                    List<Truck> temp = new List<Truck>();
                    foreach (Truck truck in trucks)
                    {
                        if (truck.AlterRoute(paths, shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state))
                            temp.Add(truck);
                    }

                    trucks = temp;
                }

                //if there is more than one truck left, give staff member option
                if (trucks.Count > 1)
                {
                    //add each truck to drop down
                    ExistingTrucks.Items.Clear();
                    ExistingTrucks.Items.Add(new ListItem("", ""));

                    foreach (Truck truck in trucks)
                        ExistingTrucks.Items.Add(new ListItem(truck.licensePlateNum, truck.id.ToString()));

                    ChooseTruckPanel.Visible = true;
                }
                //if there is one truck left, place shipment on truck
                else if (trucks.Count == 1)
                {
                    TruckPlacement.Text = "The shipment will be placed on the truck with license number " + trucks[0].licensePlateNum + ".  Click OK to confirm.";
                    TruckId.Text = trucks[0].id.ToString();
                    OneTruckPanel.Visible = true;
                }
                //if there is not a truck left, start a new one (if there is one available)
                else
                {
                    trucks = DbInterfaceTruck.GetTrucksByStatus(UtilityClass.TruckStatus.Idle);

                    //add each available truck to dropdown
                    if (trucks.Count > 0)
                    {
                        NewTrucks.Items.Clear();
                        Drivers.Items.Clear();
                        NewTrucks.Items.Add(new ListItem("", ""));
                        Drivers.Items.Add(new ListItem("", ""));

                        //add available trucks to dropdown
                        foreach (Truck truck in trucks)
                            NewTrucks.Items.Add(new ListItem(truck.licensePlateNum, truck.id.ToString()));

                        //add available drivers to dropdown
                        List<Driver> drivers = DbInterfaceDriver.GetAvailableDrivers();
                        if (drivers.Count > 0)
                        {
                            foreach (Driver driver in drivers)
                                Drivers.Items.Add(new ListItem(driver.firstName + " " + driver.lastName, driver.id.ToString()));
                        }
                        else
                        {
                            SelectDriver.Text = "No drivers are currently available.";
                            SelectDriver.Visible = true;
                        }

                        ChooseNewTruckPanel.Visible = true;
                    }
                    //let user know there are no trucks available
                    else
                    {
                        Message.Text = "There are currently no trucks available for this shipment";
                        MessagePanel.Visible = true;
                    }
                }
            }
        }

        /*
         * Pre:
         * Post: Start a new delivery truck and add the selected shipment
         *       to that truck
         */
        protected void OkNew_Click(object sender, EventArgs e)
        {
            SelectNewTruck.Visible = false;
            SelectDriver.Visible = false;

            //change status and shipping speed of truck, set truck path, associate shipment
            //with truck, update shipment status
            if (NewTrucks.SelectedIndex > 0 && Drivers.SelectedIndex > 0)
            {
                //get shipment
                int shipmentId = Convert.ToInt32(gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text);
                Shipment shipment = DbInterfaceShipment.GetShipmentForTruck(shipmentId);
                
                //find new path
                Paths paths = new Paths();
                int pathId = paths.GetIdOfShortestPath(new List<string> { shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state });

                DbInterfaceTruck.StartNewTruckWithShipment(shipmentId, Convert.ToInt32(NewTrucks.SelectedValue), 
                                                           shipment.shippingSpeed, pathId, Convert.ToInt32(Drivers.SelectedValue));

                //show confirmation
                Message.Text = "The shipment was successfully added to the truck";
                gvShipments.DataBind();
                ChooseNewTruckPanel.Visible = false;
                MessagePanel.Visible = true;

                //get delivery date range
                DateTime firstDate = DateTime.Today.AddDays(5); //normal
                DateTime lastDate = DateTime.Today.AddDays(7);
                if (shipment.shippingSpeed == UtilityClass.ShippingSpeed.Express)
                {
                    firstDate = DateTime.Today.AddDays(3);
                    lastDate = DateTime.Today.AddDays(5);
                }
                else if (shipment.shippingSpeed == UtilityClass.ShippingSpeed.Urgent)
                {
                    firstDate = DateTime.Today.AddDays(1);
                    lastDate = DateTime.Today.AddDays(3);
                }

                //send email
                UtilityClass.SendEmail(shipment.customer.email, "Package Ready for Shipment", "Thank you for using the La Crosse Parcel Service!  " +
                                       "Your shipment has been placed on a truck for delivery and can be expected to arrive between " +
                                       firstDate.ToShortDateString() + " and " + lastDate.ToShortDateString() +
                                       ".  Please visit 138.49.101.81/Account/Login to view status updates.");
            }
            else if (NewTrucks.SelectedIndex <= 0)
            {
                SelectNewTruck.Visible = true;
            }
            else if (Drivers.SelectedIndex <= 0)
            {
                SelectDriver.Text = "Please select a driver";
                SelectDriver.Visible = true;
            }
        }

        /*
         * Pre:
         * Post: Add the shipment to the available truck
         */
        protected void OK_Click(object sender, EventArgs e)
        {
            if (!TruckId.Text.Equals(""))
            {
                int truckId = Convert.ToInt32(TruckId.Text);
                int shipmentId = Convert.ToInt32(gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text);

                //get shipment
                Shipment shipment = DbInterfaceShipment.GetShipmentForTruck(shipmentId);

                //get all paths from database
                Paths paths = new Paths();

                //get truck
                Truck truck = DbInterfaceTruck.GetTruckById(truckId);

                //if the truck is already going to the needed location, add the shipment
                if (truck.GoingToLocation(paths, shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state))
                    DbInterfaceTruck.AddShipmentToTruck(shipmentId, truckId);
                //otherwise alter the route and then add the shipment
                else
                {
                    truck.AlterRoute(paths, shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state);
                    DbInterfaceTruck.CommitRouteChangeAndAddShipment(shipmentId, truckId, truck.pathId);
                }

                //show confirmation
                Message.Text = "The shipment was successfully added to the truck";
                gvShipments.DataBind();
                OneTruckPanel.Visible = false;
                MessagePanel.Visible = true;

                //get delivery date range
                DateTime firstDate = DateTime.Today.AddDays(5); //normal
                DateTime lastDate = DateTime.Today.AddDays(7);
                if (shipment.shippingSpeed == UtilityClass.ShippingSpeed.Express)
                {
                    firstDate = DateTime.Today.AddDays(3);
                    lastDate = DateTime.Today.AddDays(5);
                }
                else if (shipment.shippingSpeed == UtilityClass.ShippingSpeed.Urgent)
                {
                    firstDate = DateTime.Today.AddDays(1);
                    lastDate = DateTime.Today.AddDays(3);
                }

                //send email
                UtilityClass.SendEmail(shipment.customer.email, "Package Ready for Shipment", "Thank you for using the La Crosse Parcel Service!  " +
                                       "Your shipment has been placed on a truck for delivery and can be expected to arrive between " +
                                       firstDate.ToShortDateString() + " and " + lastDate.ToShortDateString() +
                                       ".  Please visit 138.49.101.81/Account/Login to view status updates.");
            } 
        }

        /*
         * Pre:
         * Post: Add the shipment to the selected truck
         */
        protected void OkExisting_Click(object sender, EventArgs e)
        {
            SelectTruckExisting.Visible = false;

            if (ExistingTrucks.SelectedIndex > 0)
            {
                int truckId = Convert.ToInt32(ExistingTrucks.SelectedValue);
                int shipmentId = Convert.ToInt32(gvShipments.Rows[gvShipments.SelectedIndex].Cells[1].Text);

                //get shipment
                Shipment shipment = DbInterfaceShipment.GetShipmentForTruck(shipmentId);

                //get all paths from database
                Paths paths = new Paths();

                //get truck
                Truck truck = DbInterfaceTruck.GetTruckById(truckId);

                //if the truck is already going to the needed location, add the shipment
                if (truck.GoingToLocation(paths, shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state))
                    DbInterfaceTruck.AddShipmentToTruck(shipmentId, truckId);
                //otherwise alter the truck's route and add the shipment
                else
                {
                    truck.AlterRoute(paths, shipment.deliveryAddress.city + ", " + shipment.deliveryAddress.state);
                    DbInterfaceTruck.CommitRouteChangeAndAddShipment(shipmentId, truckId, truck.pathId);
                }

                //show confirmation
                Message.Text = "The shipment was successfully added to the truck";
                gvShipments.DataBind();
                ChooseTruckPanel.Visible = false;
                MessagePanel.Visible = true;

                //get delivery date range
                DateTime firstDate = DateTime.Today.AddDays(5); //normal
                DateTime lastDate = DateTime.Today.AddDays(7);
                if (shipment.shippingSpeed == UtilityClass.ShippingSpeed.Express)
                {
                    firstDate = DateTime.Today.AddDays(3);
                    lastDate = DateTime.Today.AddDays(5);
                }
                else if (shipment.shippingSpeed == UtilityClass.ShippingSpeed.Urgent)
                {
                    firstDate = DateTime.Today.AddDays(1);
                    lastDate = DateTime.Today.AddDays(3);
                }

                //send email
                UtilityClass.SendEmail(shipment.customer.email, "Package Ready for Shipment", "Thank you for using the La Crosse Parcel Service!  " +
                                       "Your shipment has been placed on a truck for delivery and can be expected to arrive between " +
                                       firstDate.ToShortDateString() + " and " + lastDate.ToShortDateString() +
                                       ".  Please visit 138.49.101.81/Account/Login to view status updates.");
            }
            else
                SelectTruckExisting.Visible = true;
        }

        /*
         * Pre:
         * Post: Cancel adding the shipment to a new truck
         */
        protected void CancelNew_Click(object sender, EventArgs e)
        {
            clearPage();
        }

        /*
         * Pre:
         * Post: Cancel adding the shipment to a truck
         */
        protected void Cancel_Click(object sender, EventArgs e)
        {
            clearPage();
        }

        /*
         * Pre:
         * Post: Clear extra panels and error messages
         */
        private void clearPage()
        {
            //hide update panels
            MessagePanel.Visible = false;
            OneTruckPanel.Visible = false;
            ChooseNewTruckPanel.Visible = false;
            ChooseTruckPanel.Visible = false;

            //clear errors
            SelectNewTruck.Visible = false;
            SelectDriver.Visible = false;
            SelectTruckExisting.Visible = false;
        }

    }
}