<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <br />
  <br />
    <div class="row">
        <div class="col-md-8">
            <section id="pendingShipmentForm" >
                <div class="form-horizontal">
                    <h3>Reports</h3>
                    <br />
                    <div class="form-group">
                        <div class="col-md-10">
                            Start date: <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" />
                            End date: <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" />
                            <asp:Button ID="btnReports" runat="server" OnClick="btnReports_Click" Text="Go" /><br /><br />

                            <h3>
                                <asp:Label ID="lblStartDate" runat="server" /> to <asp:Label ID="lblEndDate" runat="server" />
                            </h3>
                            <br />
                            <h4>Revenue</h4>
                            <h3><asp:Label ID="lblRevenue" runat="server" /></h3>
                            <br />

                            <h4>Shipment Types</h4>

                            <asp:GridView ID="gvShipmentTypes" runat="server" AutoGenerateColumns="true" DataSourceID="gdsShipmentTypes" CssClass="table table-bordered" AllowSorting="true" >
                                <EmptyDataTemplate>
                                    No shipments over this time span.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:SqlDataSource ID="gdsShipmentTypes" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ReportShipments" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter Name="startDate" ControlID="txtStartDate" DbType="Date"/>
                                    <asp:ControlParameter Name="endDate" ControlID="txtEndDate" DbType="Date" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <br /><br />

                            <h4>Shipment Locations</h4>

                            <asp:GridView ID="gvShipmentLocations" runat="server" AutoGenerateColumns="true" DataSourceID="gdsShipmentLocations" CssClass="table table-bordered" AllowSorting="true" AllowPaging="true" >
                                <EmptyDataTemplate>
                                    No shipments over this time span.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:SqlDataSource ID="gdsShipmentLocations" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ReportLocations" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter Name="startDate" ControlID="txtStartDate" DbType="Date"/>
                                    <asp:ControlParameter Name="endDate" ControlID="txtEndDate" DbType="Date" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <br /><br />

                            <h4>Status Updates</h4>

                            <asp:GridView ID="gvStatusUpdates" runat="server" AutoGenerateColumns="true" DataSourceID="gdsStatusUpdates" CssClass="table table-bordered" AllowSorting="true" AllowPaging="true" >
                                <EmptyDataTemplate>
                                    No status updates over this time span.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:SqlDataSource ID="gdsStatusUpdates" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="ReportStatus" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter Name="startDate" ControlID="txtStartDate" DbType="Date"/>
                                    <asp:ControlParameter Name="endDate" ControlID="txtEndDate" DbType="Date" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>