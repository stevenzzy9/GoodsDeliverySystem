<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DispatchLocalTruck.aspx.cs" Inherits="GoodsDeliverySystem.Transportation.DispatchLocalTruck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-11">
            <section id="selectTruckForm">
                <div class="form-horizontal">
                    <h3>Dispatch Local Trucks</h3>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="col-md-10">
                                            <asp:GridView ID="gvShipments" runat="server" AllowSorting="True" RowStyle-Wrap="false"
                                                AutoGenerateColumns="False" DataKeyNames="TruckId" DataSourceID="gdsDataSource1" CssClass="table table-bordered" AllowPaging="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvShipments_SelectedIndexChanged">
                                                <AlternatingRowStyle BackColor="#EFEFEF" />
                                                <RowStyle Wrap="False" />
                                                <SelectedRowStyle BackColor="#CDCDEF" />
                                                <EmptyDataTemplate>
                                                    There are no local trucks to dispatch.
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="TruckId" HeaderText="Truck Id" SortExpression="TruckId" ItemStyle-CssClass="hiddenCol" HeaderStyle-CssClass="hiddenCol" />
                                                    <asp:BoundField DataField="LicensePlateNum" HeaderText="License Plate #" SortExpression="LicensePlateNum">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ModelName" HeaderText="Model" SortExpression="ModelName">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Driver" SortExpression="FirstName">
                                                        <ItemTemplate>
                                                            <%# Eval("FirstName") %> <%# Eval("LastName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="TruckSelectLocalToDispatch" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="ConfirmationPanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <h4>Confirmation Required</h4>
                                    <div class="form-group" style="margin-left: 30px">
                                        <asp:Label ID="Confirmation" runat="server" Visible="true" Text="Would you like to dispatch the selected truck?" />
                                        <asp:Label ID="TruckId" runat="server" Visible="false" />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-2">
                                            <asp:Button ID="OK" runat="server" Text="Dispatch" CssClass="btn btn-default" OnClick="OK_Click" />
                                            <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="Cancel_Click" />
                                        </div>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="MessagePanel" runat="server" Visible="false">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label ID="Message" runat="server" CssClass="text-danger">The truck was dispatched</asp:Label>
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
