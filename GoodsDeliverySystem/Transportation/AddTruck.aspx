<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTruck.aspx.cs" Inherits="GoodsDeliverySystem.Transportation.AddTruck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-12">
            <section id="addTruckForm">
                <div class="form-horizontal">
                    <h3>Add Truck</h3>
                    <asp:UpdatePanel ID="FullPage" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="MainForm" runat="server">
                                <ContentTemplate>
                                    <div class="control-column">
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Model" CssClass="col-md-3 control-label">Model</asp:Label>
                                            <div class="col-md-9">
                                                <asp:DropDownList runat="server" ID="Model" CssClass="form-control" AppendDataBoundItems="true" DataSourceID="GdsDataSource1" DataTextField="ModelName" DataValueField="Id">
                                                    <asp:ListItem Value="" Text="" Selected="True" />
                                                </asp:DropDownList>
                                                <asp:SqlDataSource runat="server" ID="GdsDataSource1" ConnectionString='<%$ ConnectionStrings:GoodsDeliveryConnectionString %>' SelectCommand="SELECT * FROM [TruckModels] ORDER BY [ModelName]"></asp:SqlDataSource>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Model"
                                                    CssClass="text-danger" ErrorMessage="The model field is required." />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Length" CssClass="col-md-3 control-label">Length</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Length" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                <div class="float-left text-box-post-label">inches</div>
                                                <br />
                                                <asp:CustomValidator ID="LengthValidator" runat="server" ControlToValidate="Length"
                                                    OnServerValidate="LengthValidator_ServerValidate" ErrorMessage="<br/>Length must be a number between 150 and 480." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Width" CssClass="col-md-3 control-label">Width</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Width" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                <div class="float-left text-box-post-label">inches</div>
                                                <br />
                                                <asp:CustomValidator ID="WidthValidator" runat="server" ControlToValidate="Width"
                                                    OnServerValidate="WidthValidator_ServerValidate" ErrorMessage="<br/>Width must be a number between 60 and 102." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Height" CssClass="col-md-3 control-label">Height</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Height" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                <div class="float-left text-box-post-label">inches</div>
                                                <br />
                                                <asp:CustomValidator ID="HeightValidator" runat="server" ControlToValidate="Height"
                                                    OnServerValidate="HeightValidator_ServerValidate" ErrorMessage="<br/>Height must be a number between 60 and 168." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Weight" CssClass="col-md-3 control-label">Maximum Weight</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Weight" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                <div class="float-left text-box-post-label">pounds</div>
                                                <br />
                                                <asp:CustomValidator ID="WeightValidator" runat="server" ControlToValidate="Weight"
                                                    OnServerValidate="WeightValidator_ServerValidate" ErrorMessage="<br/>Weight capacity must be a number between 2000 and 40,000." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="control-column">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="Odometer" CssClass="col-md-3 control-label">Odometer Reading</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Odometer" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                <div class="float-left text-box-post-label">miles</div>
                                                <br />
                                                <asp:CustomValidator ID="OdometerValidator" runat="server" ControlToValidate="Odometer"
                                                    OnServerValidate="OdometerValidator_ServerValidate" ErrorMessage="<br/>The odometer reading must be a number between 0 and 100,000." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" AssociatedControlID="FuelTankSize" CssClass="col-md-3 control-label">Fuel Tank Size</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="FuelTankSize" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                <div class="float-left text-box-post-label">gallons</div>
                                                <br />
                                                <asp:CustomValidator ID="FuelTankValidator" runat="server" ControlToValidate="FuelTankSize"
                                                    OnServerValidate="FuelTankValidator_ServerValidate" ErrorMessage="<br/>The fuel tank size must be a number between 25 and 200." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LicensePlateNum" CssClass="col-md-3 control-label">License Number</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="LicensePlateNum" CssClass="form-control" MaxLength="10" />
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="LicensePlateNum" OnServerValidate="LicensePlateNum_ServerValidate" ErrorMessage="The license plate number is in an invalid format" CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LicensePlateState" CssClass="col-md-3 control-label">License State</asp:Label>
                                            <div class="col-md-9">
                                                <asp:DropDownList runat="server" ID="LicensePlateState" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Abbreviation" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="" Text="" Selected="True" />
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [States] ORDER BY [Name]"></asp:SqlDataSource>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="LicensePlateState"
                                                    CssClass="text-danger" ErrorMessage="The license plate state field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Remarks" CssClass="col-md-3 control-label">Remarks</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Remarks" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LocalTruck" CssClass="col-md-3 control-label">Local Truck</asp:Label>
                                            <div class="col-md-9" style="margin-top:7px">
                                                <asp:CheckBox ID="LocalTruck" runat="server" Text="" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-2 col-md-9 pull-right">
                                                <asp:Label ID="lblTruckExists" runat="server" CssClass="text-danger" Visible="false">The truck already exists</asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-2 col-md-9 pull-right">
                                                <asp:Button ID="Add" runat="server" Text="Add Truck" CssClass="btn btn-default" OnClick="Add_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="Confirmation" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:Label ID="ConfirmLabel" runat="server" CssClass="confirmation-message">The truck as been successfully added!</asp:Label><br />
                                    <br />
                                    <a href="../Account/ManagerMenu.aspx" class="confirmation-message">Return to Menu</a>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
