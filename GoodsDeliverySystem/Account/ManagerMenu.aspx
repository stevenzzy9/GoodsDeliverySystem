<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerMenu.aspx.cs" Inherits="GoodsDeliverySystem.Account.ManagerMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-12">
            <section id="managerMenuForm">
                <div class="form-horizontal">
                    <h1>Menu</h1>
                    <div class="control-column">
                        <div class="form-group">
                            <h3>Drivers</h3>
                            <a href="../Transportation/HireDriver.aspx">Hire Driver</a><br>
                            <a href="../Transportation/FireDriver.aspx">Fire/Edit Driver</a>
                        </div>
                        <div class="form-group">
                            <h3>Reports</h3>
                            <a href="../Shipments/Reports.aspx">Shipment Statistics</a>
                        </div>
                    </div>
                    <div class="control-column">
                        <div class="form-group">
                            <h3>Trucks</h3>
                            <a href="../Transportation/AddTruck.aspx">Add Truck</a><br />
                            <a href="../Transportation/EditTruck.aspx">Edit/Delete Trucks</a>
                        </div>
                        <div class="form-group">
                            <h3>Users</h3>
                            <a href="RegisterStaffManager.aspx">Add Staff or Manager</a><br />
                            <a href="ModifyUserPermissions.aspx">Modify Permissions</a>
                        </div>

                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
