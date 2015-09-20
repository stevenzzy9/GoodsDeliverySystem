<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AwaitingLocalDelivery.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.AwaitingLocalDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-11">
            <section id="selectTruckForm">
                <div class="form-horizontal">
                    <h3>Update Shipment Status: Awaiting Local Delivery</h3>
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
                                                      There are no shipments in transit.
                                                  </EmptyDataTemplate>
                                                  <Columns>
                                                      <asp:BoundField DataField="Id" HeaderText="Confirmation Number" SortExpression="Id" />
                                                      <asp:TemplateField HeaderText="Ship To">
                                                          <ItemTemplate>
                                                              <%# Eval("FirstName") %> <%# Eval("LastName") %><br />
                                                              <%# Eval("ShippingStreet") %><br />
                                                              <%# Eval("ShippingCity") %>, <%# Eval("ShippingState") %> <%# Eval("ShippingZip") %>
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:BoundField DataField="LicensePlateNum" HeaderText="Truck License Plate #" SortExpression="LicensePlateNum" />
                                                      <asp:BoundField DataField="Driver" HeaderText="Driver" SortExpression="Driver" />
                                                  </Columns>
                                              </asp:GridView>
                                             <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ShipmentsInTransit" SelectCommandType="StoredProcedure"></asp:SqlDataSource>          
                                        </div>                 
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                           <br />
                             <asp:UpdatePanel ID="ConfirmationPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Confirmation Required</h4>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="col-md-offset-1 control-label">Is this shipment awaiting local delivery?</asp:Label><br />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
        </div>
    </div>
           
</asp:Content>
