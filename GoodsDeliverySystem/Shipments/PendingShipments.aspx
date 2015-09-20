<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PendingShipments.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.PendingShipments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-8">
            <section id="pendingShipmentForm">
                <div class="form-horizontal">
                    <h3>Pending Shipments</h3>
                    <br />
                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:GridView ID="gvPendingShipments" runat="server" AllowSorting="True" RowStyle-Wrap="false"
                                AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="gdsDataSource1" CssClass="table table-bordered"
                                OnSelectedIndexChanged="gvPendingShipments_SelectedIndexChanged" AllowPaging="True">
                                <AlternatingRowStyle BackColor="#EFEFEF" />
                                <RowStyle Wrap="False" />
                                <SelectedRowStyle BackColor="#CDCDEF" />
                                <EmptyDataTemplate>
                                    There are no pending shipments.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Options">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnCommand="btnAccept_Click" CommandName="Accept" CommandArgument='<%# Eval("Id") %>' Text="Accept" /><br />
                                            <asp:LinkButton runat="server" OnCommand="btnDeny_Click" CommandName="Deny" CommandArgument='<%# Eval("Id") %>' Text="Deny" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id" HeaderText="Confirmation Number" SortExpression="Id" />
                                    <asp:BoundField DataField="username" HeaderText="User" SortExpression="username" />
                                    <asp:BoundField DataField="speed" HeaderText="Shipping Speed" SortExpression="speed" />
                                    <asp:BoundField DataField="length" HeaderText="Length (inches)" SortExpression="length" />
                                    <asp:BoundField DataField="width" HeaderText="Width (inches)" SortExpression="width" />
                                    <asp:BoundField DataField="height" HeaderText="Height (inches)" SortExpression="height" />
                                    <asp:BoundField DataField="weight" HeaderText="Weight (pounds)" SortExpression="weight" />
                                    <asp:BoundField DataField="typeName" HeaderText="Type" SortExpression="typeName" />
                                    <asp:BoundField DataField="insuranceType" HeaderText="Insurance" SortExpression="insuranceType" />
                                    <asp:BoundField DataField="itemValue" HeaderText="Value" SortExpression="itemValue" />
                                    <asp:TemplateField HeaderText="Ship To">
                                        <ItemTemplate>
                                            <%# Eval("FirstName") %> <%# Eval("LastName") %><br />
                                            <%# Eval("ShippingStreet") %><br />
                                            <%# Eval("ShippingCity") %>, <%# Eval("ShippingState") %> <%# Eval("ShippingZip") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Card Info">
                                        <ItemTemplate>
                                            <%# ("****-****-****-" + Eval("CardNumber").ToString().Substring(12)) %><br />
                                            <%# Eval("CardType") %>, Expires <%# Eval("ExpirationMonth") %>/<%# Eval("ExpirationYear") %><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Billing Info">
                                        <ItemTemplate>
                                            <%# Eval("NameOnCard") %><br />
                                            <%# Eval("BillingStreet") %><br />
                                            <%# Eval("BillingCity") %>, <%# Eval("BillingState") %> <%# Eval("BillingZip") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ShipmentPending" SelectCommandType="StoredProcedure" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
