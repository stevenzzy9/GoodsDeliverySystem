<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HireDriver.aspx.cs" Inherits="GoodsDeliverySystem.Transportation.HireDriver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-12">
            <section id="hireDriverForm">
                <div class="form-horizontal">
                    <h3>Hire Driver</h3>
                    <asp:UpdatePanel ID="FullPage" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="MainForm" runat="server">
                                <ContentTemplate>
                                    <div class="control-column">
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LocalDriver" CssClass="col-md-3 control-label">Local Driver</asp:Label>
                                            <div class="col-md-9" style="margin-top:7px">
                                                <asp:CheckBox ID="LocalDriver" runat="server" Text="" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-3 control-label">First Name</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName"
                                                    CssClass="text-danger" ErrorMessage="The first name field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-3 control-label">Last Name</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastName"
                                                    CssClass="text-danger" ErrorMessage="The last name field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Street" CssClass="col-md-3 control-label">Street</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Street" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Street"
                                                    CssClass="text-danger" ErrorMessage="The street field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-3 control-label">City</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="City" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="City"
                                                    CssClass="text-danger" ErrorMessage="The city field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="State" CssClass="col-md-3 control-label">State</asp:Label>
                                            <div class="col-md-9">
                                                <asp:DropDownList runat="server" ID="State" CssClass="form-control" DataSourceID="GdsDataSource1" DataTextField="Name" DataValueField="Abbreviation" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="" Text="" Selected="True" />
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="GdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [States] ORDER BY [Name]"></asp:SqlDataSource>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="State"
                                                    CssClass="text-danger" ErrorMessage="The state field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Zip" CssClass="col-md-3 control-label">Zip</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Zip" CssClass="form-control" MaxLength="5" TextMode="Number" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Zip"
                                                    CssClass="text-danger" ErrorMessage="The zip code field is required." />
                                                <asp:CustomValidator ID="ZipValidator" runat="server" ControlToValidate="Zip" OnServerValidate="ZipValidator_ServerValidate" ErrorMessage="<br/>The zip code must be exactly 5 digits" CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="control-column">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="PhoneAreaCode" CssClass="col-md-3 control-label">Phone</asp:Label>
                                            <div class="col-md-9">
                                                <div style="float: left; padding-right: .5em;">
                                                    <asp:TextBox runat="server" ID="PhoneAreaCode" CssClass="form-control" Width="50" MaxLength="3" />
                                                </div>
                                                <div style="float: left; padding-right: .5em;">
                                                    <asp:TextBox runat="server" ID="Phone2" CssClass="form-control" Width="50" MaxLength="3" />
                                                </div>
                                                <div style="float: left; padding-right: .5em;">
                                                    <asp:TextBox runat="server" ID="Phone3" CssClass="form-control" Width="60" MaxLength="4" />
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:CustomValidator ID="PhoneValidator" runat="server" ControlToValidate="PhoneAreaCode" OnServerValidate="PhoneValidator_ServerValidate" ErrorMessage="The phone number must be exactly 10 digits" CssClass="text-danger" ValidateEmptyText="true" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-3 control-label">Email</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Email"
                                                    CssClass="text-danger" ErrorMessage="The email field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LicenseNum" CssClass="col-md-3 control-label">License Number</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="LicenseNum" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="LicenseNum"
                                                    CssClass="text-danger" ErrorMessage="The license number field is required." /><br />
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="LicenseNum" OnServerValidate="LicenseNum_ServerValidate" ErrorMessage="The license number is in an invalid format" CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LicenseState" CssClass="col-md-3 control-label">License State</asp:Label>
                                            <div class="col-md-9">
                                                <asp:DropDownList runat="server" ID="LicenseState" CssClass="form-control" DataSourceID="GdsDataSource1" DataTextField="Name" DataValueField="Abbreviation" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="" Text="" Selected="True" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="LicenseState"
                                                    CssClass="text-danger" ErrorMessage="The license state field is required." />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="LicenseExpiration" CssClass="col-md-3 control-label">License Expiration</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="LicenseExpiration" CssClass="form-control" TextMode="Date" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="LicenseExpiration"
                                                    CssClass="text-danger" ErrorMessage="The license expiration number field is required." />
                                                <asp:CustomValidator ID="LicenseExpValidator" runat="server" ControlToValidate="LicenseExpiration" OnServerValidate="LicenseExpValidator_ServerValidate" ErrorMessage="The license is expired." CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Remark" CssClass="col-md-3 control-label">Remark</asp:Label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="Remark" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-md-offset-2 col-md-9 pull-right">
                                                <asp:Button ID="Hire" runat="server" Text="Hire Driver" CssClass="btn btn-default" OnClick="Hire_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-2 col-md-9 pull-right">
                                                <asp:Label ID="lbDriverExists" runat="server" CssClass="text-danger" Visible="false">The Driver already exists</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="Confirmation" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:Label ID="ConfirmLabel" runat="server" CssClass="confirmation-message">The driver has been registered successfully!</asp:Label><br />
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
