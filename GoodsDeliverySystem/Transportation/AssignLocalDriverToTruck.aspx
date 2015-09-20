<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignLocalDriverToTruck.aspx.cs" Inherits="GoodsDeliverySystem.Transportation.AssignLocalDriverToTruck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-11">
            <section id="selectTruckForm">
                <div class="form-horizontal">
                    <h3>Assign Drivers to Local Trucks</h3>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-md-10">
                                    <asp:GridView ID="gvTrucks" runat="server" AllowSorting="True" RowStyle-Wrap="false"
                                        AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="gdsDataSource1" CssClass="table table-bordered" AllowPaging="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvTrucks_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="#EFEFEF" />
                                        <RowStyle Wrap="False" />
                                        <SelectedRowStyle BackColor="#CDCDEF" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Truck Id" SortExpression="Id" ItemStyle-CssClass="hiddenCol" HeaderStyle-CssClass="hiddenCol" />
                                            <asp:BoundField DataField="LicensePlateNum" HeaderText="License Plate #" SortExpression="LicensePlateNum" />
                                            <asp:BoundField DataField="ModelName" HeaderText="Model" SortExpression="ModelName">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Driver" HeaderText="Driver" SortExpression="Driver">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="TrucksViewLocal" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="ConfirmationPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Select a Driver</h4>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" AssociatedControlID="Drivers" runat="server" CssClass="control-label col-md-1">Driver</asp:Label><asp:Label ID="TruckId" runat="server" Visible="false" />
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="Drivers" CssClass="form-control" AppendDataBoundItems="True" DataSourceID="gdsDataSource2" DataTextField="DriverName" DataValueField="Id">
                                                <asp:ListItem Text="" Value="" Selected="True" />
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="gdsDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="DriversSelectAvailableLocal" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            <asp:Label ID="SelectDriver" runat="server" CssClass="text-danger" Visible="false">Please select a driver</asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-2">
                                            <asp:Button ID="OK" runat="server" Text="Ok" CssClass="btn btn-default" OnClick="OK_Click" />
                                            <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="Cancel_Click" />
                                        </div>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="MessagePanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label ID="Message" runat="server" CssClass="text-danger">The driver was assigned to the truck</asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
            </section>
        </div>
    </div>
</asp:Content>
