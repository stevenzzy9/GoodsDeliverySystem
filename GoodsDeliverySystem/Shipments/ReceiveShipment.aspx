<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveShipment.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.ReceiveShipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
    <br />
    <br />
    <div class="row">
        <div class="col-md-8">
            <section id="selectTruckForm">
                <div class="form-horizontal">
                    <h3>Receive Shipment</h3>
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
                                                      There are no shipments to receive for local delivery.
                                                  </EmptyDataTemplate>
                                                  <Columns>
                                                      <asp:BoundField DataField="Id" HeaderText="Confirmation Number" SortExpression="Id" />
                                                      <asp:BoundField DataField="trackingNumber" HeaderText="Tracking Number" SortExpression="trackingNumber" />
                                                      <asp:BoundField DataField="timestamp" HeaderText="Timestamp" SortExpression="timesstamp" />
                                                      <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                                      <asp:TemplateField HeaderText="Ship To">
                                                          <ItemTemplate>
                                                              <%# Eval("FirstName") %> <%# Eval("LastName") %><br />
                                                              <%# Eval("Street") %><br />
                                                              <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("Zip") %>
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:BoundField DataField="length" HeaderText="Length (inches)" SortExpression="length" />
                                                      <asp:BoundField DataField="width" HeaderText="Width (inches)" SortExpression="width" />
                                                      <asp:BoundField DataField="height" HeaderText="Height (inches)" SortExpression="height" />
                                                      <asp:BoundField DataField="weight" HeaderText="Weight (pounds)" SortExpression="weight" />

                                                  </Columns>
                                              </asp:GridView>
                                             <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ViewLocalShipmentDelivery" SelectCommandType="StoredProcedure"></asp:SqlDataSource>          
                                        </div>                 
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                           <br />

                             <asp:UpdatePanel ID="ReceiveShipmentPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Confirmation Required</h4>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="col-md-offset-1 control-label">Do you want to receive this shipment for local delivery?</asp:Label><br />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-2">
                                            <asp:Button ID="OK" runat="server" Text="Receive" CssClass="btn btn-default" OnClick="OK_Click" />
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
