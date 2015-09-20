<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrieveCredentials.aspx.cs" Inherits="GoodsDeliverySystem.Account.RetrieveCredentials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-8">
            <section id="retrieveCredentialsForm">
                <div class="form-horizontal">
                    <h3>Retrieve Username and Password</h3>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                        <div class="col-md-10 text-danger">
                                            <asp:Literal runat="server" ID="FailureText" />
                                        </div>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder runat="server" ID="SuccessfulMessage" Visible="false">
                                        <div class="col-md-10 text-danger">
                                            <asp:Literal runat="server" ID="SuccessfulText" />
                                        </div>
                                    </asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Email"
                                        CssClass="text-danger" ErrorMessage="The email field is required." /><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"
                                        CssClass="text-danger" ErrorMessage="Must be a valid email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-offset-2 col-md-10">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="Submit" Text="Submit" CssClass="btn btn-default" />
                                    <asp:Button ID="btnMenu" runat="server" OnClick="btnMenu_Click" Text="Login" CssClass="btn btn-default" Visible="false" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
            </section>
        </div>
    </div>
</asp:Content>
