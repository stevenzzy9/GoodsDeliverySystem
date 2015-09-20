<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserShipments.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.UserShipments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
    <div class="col-md-8">
        <section id="pendingShipmentForm" >
            <div class="form-horizontal">
                <h3>Your Shipments</h3>
                <br />
                <div class="form-group">
                    <div class="col-md-10">
                        <asp:GridView ID="gvShipments" runat="server" AllowSorting="True" RowStyle-Wrap="false" 
                            AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="gdsDataSource1" CssClass="table table-bordered" AllowPaging="True" >
                            <AlternatingRowStyle BackColor="#EFEFEF" />
                            <RowStyle Wrap="False" />
                            <SelectedRowStyle BackColor="#CDCDEF"/>
                            <Columns>
                                <asp:TemplateField HeaderText="Shipment Details">
                                    <ItemTemplate>
                                        <a href="ViewShipment.aspx?shipment=<%# Eval("Id") %>" >View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" >
                                    <ItemTemplate>
                                        <%# (Eval("Approved") == DBNull.Value) ? "Denied" : (bool)Eval("Approved") ? "Approved" : "Pending" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Confirmation Number" SortExpression="licenseNum" Visible="false" />
                                <asp:BoundField DataField="trackingNumber" HeaderText="Tracking Number" SortExpression="trackingNumber" />
                                <asp:BoundField DataField="username" HeaderText="User" SortExpression="username" />
                                <asp:BoundField DataField="speed" HeaderText="Shipping Speed" SortExpression="speed" />
                                <asp:BoundField DataField="length" HeaderText="Length (inches)" SortExpression="length" />
                                <asp:BoundField DataField="width" HeaderText="Width (inches)" SortExpression="width" />
                                <asp:BoundField DataField="height" HeaderText="Height (inches)" SortExpression="height" />
                                <asp:BoundField DataField="weight" HeaderText="Weight (pounds)" SortExpression="weight" />
                                <asp:BoundField DataField="typeName" HeaderText="Type" SortExpression="typeName" />
                                <asp:BoundField DataField="insuranceType" HeaderText="Insurance" SortExpression="insuranceType" />
                                <asp:BoundField DataField="itemValue" HeaderText="Value" SortExpression="itemValue" />
                                <asp:TemplateField HeaderText="Ship To" >
                                    <ItemTemplate>
                                        <%# Eval("FirstName") %> <%# Eval("LastName") %><br /><%# Eval("ShippingStreet") %><br /><%# Eval("ShippingCity") %>, <%# Eval("ShippingState") %> <%# Eval("ShippingZip") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit Card Info" >
                                    <ItemTemplate>
                                        <%# ("****-****-****-" + Eval("CardNumber").ToString().Substring(12)) %><br /><%# Eval("CardType") %>, Expires <%# Eval("ExpirationMonth") %>/<%# Eval("ExpirationYear") %><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billing Info" >
                                    <ItemTemplate>
                                        <%# Eval("NameOnCard") %><br /><%# Eval("BillingStreet") %><br /><%# Eval("BillingCity") %>, <%# Eval("BillingState") %> <%# Eval("BillingZip") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="UserShipments" SelectCommandType="StoredProcedure" >
                            <SelectParameters>
                                <asp:SessionParameter SessionField="username" Name="username" ConvertEmptyStringToNull="false" DefaultValue="" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
        </section>
    </div>
</div>
</asp:Content>
