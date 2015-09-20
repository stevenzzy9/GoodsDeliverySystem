<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterStaffManager.aspx.cs" Inherits="GoodsDeliverySystem.Account.RegisterStaffManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h3>Register</h3>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel runat="server" ID="MainForm">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                        <p class="text-danger">
                                            <asp:Literal runat="server" ID="FailureText" />
                                        </p>
                                    </asp:PlaceHolder>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName"
                                                CssClass="text-danger" ErrorMessage="The first name field is required." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastName"
                                                CssClass="text-danger" ErrorMessage="The last name field is required." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" AssociatedControlID="UserType" CssClass="col-md-2 control-label">User Type</asp:Label>
                                        <div class="col-md-10">
                                            <asp:DropDownList runat="server" ID="UserType" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Staff" />
                                                <asp:ListItem Value="2" Text="Manager" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" AssociatedControlID="PhoneAreaCode" CssClass="col-md-2 control-label">Phone</asp:Label>
                                        <div class="col-md-10">
                                            <div style="float: left; padding-right: .5em;">
                                                <asp:TextBox runat="server" ID="PhoneAreaCode" CssClass="form-control" Width="50" MaxLength="3" />
                                            </div>
                                            <div style="float: left; padding-right: .5em;">
                                                <asp:TextBox runat="server" ID="Phone2" CssClass="form-control" Width="50" MaxLength="3" />
                                            </div>
                                            <div style="float: left; padding-right: .5em;">
                                                <asp:TextBox runat="server" ID="Phone3" CssClass="form-control" Width="60" MaxLength="4" />
                                            </div>
                                            <div class="col-md-10">
                                                <asp:CustomValidator ID="PhoneValidator" runat="server" ControlToValidate="PhoneAreaCode" OnServerValidate="PhoneValidator_ServerValidate" ErrorMessage="<br/>Phone number must be included and exactly 10 digits" CssClass="text-danger" ValidateEmptyText="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Email"
                                                CssClass="text-danger" ErrorMessage="The email field is required." /><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"
                                                CssClass="text-danger" ErrorMessage="Must be a valid email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-2 col-md-10">
                                            <asp:Button ID="Button1" runat="server" OnClick="RegisterUser" Text="Register" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="Confirmation" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:Label ID="ConfirmLabel" runat="server" CssClass="confirmation-message">The new user has been registered!</asp:Label><br />
                                    <br />
                                    <asp:Label ID="Label6" runat="server" CssClass="confirmation-message">Their password has been sent to the email address on file.</asp:Label><br />
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
