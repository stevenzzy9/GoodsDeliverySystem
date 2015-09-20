<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerMenu.aspx.cs" Inherits="GoodsDeliverySystem.Account.CustomerMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-12">
            <section id="customerMenuForm">
                <div class="form-horizontal">
                    <h1>Menu</h1>
                    <div class="control-column">
                        <div class="form-group">
                            <h3>Shipments</h3>
                            <a href="../Shipments/RegisterShipment.aspx">Register Shipment</a><br />
                            <a href="../Shipments/UserShipments.aspx">View My Shipments</a>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
