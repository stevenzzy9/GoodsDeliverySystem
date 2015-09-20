<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectTruck.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.SelectTruck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-8">
            <section id="selectTruckForm">
                <div class="form-horizontal">
                    <h3>Select Truck for Shipment</h3>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>


                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="col-md-10">
                                            <asp:GridView ID="gvShipments" runat="server" AllowSorting="True" RowStyle-Wrap="false"
                                                AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="gdsDataSource1" CssClass="table table-bordered" AllowPaging="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvShipments_SelectedIndexChanged">
                                                <AlternatingRowStyle BackColor="#EFEFEF" />
                                                <RowStyle Wrap="False" />
                                                <SelectedRowStyle BackColor="#CDCDEF" />
                                                <EmptyDataTemplate>
                                                    There are no shipments waiting to be loaded on a truck.
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Confirmation Number" SortExpression="id" />
                                                    <asp:BoundField DataField="length" HeaderText="Length (inches)" SortExpression="length" />
                                                    <asp:BoundField DataField="width" HeaderText="Width (inches)" SortExpression="width" />
                                                    <asp:BoundField DataField="height" HeaderText="Height (inches)" SortExpression="height" />
                                                    <asp:BoundField DataField="weight" HeaderText="Weight (pounds)" SortExpression="weight" />
                                                    <asp:BoundField DataField="typeName" HeaderText="Type" SortExpression="typeName" />
                                                    <asp:BoundField DataField="Speed" HeaderText="Shipping Speed" SortExpression="speed" />
                                                    <asp:TemplateField HeaderText="Ship To">
                                                        <ItemTemplate>
                                                            <%# Eval("FirstName") %> <%# Eval("LastName") %><br />
                                                            <%# Eval("ShippingStreet") %><br />
                                                            <%# Eval("ShippingCity") %>, <%# Eval("ShippingState") %> <%# Eval("ShippingZip") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ShipmentNotShipped" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="OneTruckPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Confirmation Required</h4>
                                    <div class="form-group">
                                        <asp:Label ID="TruckPlacement" runat="server" CssClass="col-md-offset-1 control-label"></asp:Label><asp:Label ID="TruckId" runat="server" Visible="false" />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-2">
                                            <asp:Button ID="OK" runat="server" Text="OK" CssClass="btn btn-default" OnClick="OK_Click" />
                                            <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="Cancel_Click" />
                                        </div>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="MessagePanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label ID="Message" runat="server" CssClass="text-danger">There are currently no trucks available for this shipment</asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="ChooseNewTruckPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Confirmation Required</h4>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="col-md-offset-1 control-label">A new truck must be started for this shipment, please select the license plate number of a truck and a driver.</asp:Label><br />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" AssociatedControlID="NewTrucks" runat="server" CssClass="col-md-3 control-label">Truck</asp:Label>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="NewTrucks" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value="" Selected="true" />
                                            </asp:DropDownList>
                                            <asp:Label ID="SelectNewTruck" runat="server" CssClass="text-danger" Visible="false">Please select a truck</asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" AssociatedControlID="Drivers" runat="server" CssClass="col-md-3 control-label">Driver</asp:Label>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="Drivers" CssClass="form-control">
                                                <asp:ListItem Text="" Value="" Selected="True" />
                                            </asp:DropDownList>
                                            <asp:Label ID="SelectDriver" runat="server" CssClass="text-danger" Visible="false">Please select a driver</asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-4">
                                            <asp:Button ID="OkNew" runat="server" Text="OK" CssClass="btn btn-default" OnClick="OkNew_Click" />
                                            <asp:Button ID="CancelNew" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="CancelNew_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="ChooseTruckPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Confirmation Required</h4>
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="col-md-offset-1 control-label">There are multiple trucks available.  Please select one for this shipment.</asp:Label><br />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label5" AssociatedControlID="ExistingTrucks" runat="server" CssClass="col-md-3 control-label">Truck</asp:Label>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ExistingTrucks" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value="" Selected="true" />
                                            </asp:DropDownList>
                                            <asp:Label ID="SelectTruckExisting" runat="server" CssClass="text-danger" Visible="false">Please select a truck</asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-4">
                                            <asp:Button ID="OkExisting" runat="server" Text="OK" CssClass="btn btn-default" OnClick="OkExisting_Click" />
                                            <asp:Button ID="CancelExisting" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="CancelNew_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
