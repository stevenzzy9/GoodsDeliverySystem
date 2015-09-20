<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewTrucks.aspx.cs" Inherits="GoodsDeliverySystem.Transportation.ViewTrucks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-11">
            <section id="selectTruckForm">
                <div class="form-horizontal">
                    <h3>View Trucks</h3>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-md-10">
                                    <asp:GridView ID="gvTrucks" runat="server" AllowSorting="True" RowStyle-Wrap="false"
                                        AutoGenerateColumns="False" DataKeyNames="LicensePlateNum" DataSourceID="gdsDataSource1" CssClass="table table-bordered" AllowPaging="True">
                                        <AlternatingRowStyle BackColor="#EFEFEF" />
                                        <RowStyle Wrap="False" />
                                        <SelectedRowStyle BackColor="#CDCDEF" />
                                        <Columns>
                                            <asp:BoundField DataField="LicensePlateNum" HeaderText="License Plate #" SortExpression="LicensePlateNum" />
                                            <asp:TemplateField HeaderText="Local Truck?" SortExpression="LocalTruck">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Local" runat="server" Checked='<%# Eval("LocalTruck").ToString().Equals("True") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Status" HeaderText="Truck Status" SortExpression="Status">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Speed" HeaderText="Shipping Speed" SortExpression="Speed">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Driver" HeaderText="Driver" SortExpression="Driver">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumShipments" HeaderText="# Shipments" SortExpression="NumShipments">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="TrucksViewStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
            </section>
        </div>
    </div>
</asp:Content>
