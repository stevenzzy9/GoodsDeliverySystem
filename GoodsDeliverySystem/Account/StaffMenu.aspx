<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffMenu.aspx.cs" Inherits="GoodsDeliverySystem.Account.StaffMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-12">
            <section id="staffMenuForm">
                <div class="form-horizontal">
                    <h1>Menu</h1>
                    <div class="control-column">
                        <div class="form-group">
                            <h3>Shipments</h3>
                            <a href="../Shipments/PendingShipments.aspx">Pending Shipments</a><br />
                            <a href="../Shipments/SelectTruck.aspx">Select Truck for Shipment</a><br />
                            <a href="../Shipments/ReceiveShipment.aspx">Receive Shipments</a><br />
                            <a href="../Shipments/AwaitingLocalDelivery.aspx">Change Status to 'Awaiting Local Delivery'</a><br />
                            <a href="../Shipments/Delivered.aspx">Change Status to 'Delivered'</a>
                        </div>
                    </div>
                    <div class="control-column">
                        <div class="form-group">
                            <h3>Trucks</h3>
                            <a href="../Transportation/ViewTrucks.aspx">View Truck Statuses</a><br />
                            <a href="../Transportation/DispatchTruck.aspx">Dispatch Trucks</a><br />
                            <a href="../Transportation/DispatchLocalTruck.aspx">Dispatch Local Trucks</a><br />
                            <a href="../Transportation/AssignLocalDriverToTruck">Assign Drivers to Local Trucks</a>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
