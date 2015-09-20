<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyUserPermissions.aspx.cs" Inherits="GoodsDeliverySystem.Account.ModifyUserPermissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-11">
            <section id="modifyRoleForm">
                <div class="form-horizontal">
                    <div id="Error" runat="server" class="alert alert-danger" style="display:none;">
                        <strong>Warning: </strong>There was a problem updating the user.
                    </div>
                    <div id="Warning" runat="server" class="alert alert-warning" style="display:none;">
                        <strong>Warning: </strong>No user selected.
                    </div>
                    <div id="Success" runat="server" class="alert alert-success" style="display:none;">
                        <strong>Successfully changed role.</strong>
                    </div>
                    <h3>Modify User Role</h3>
                    <div class="form-group" style="visibility:hidden">
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="Search" CssClass="col-md-2 control-label">Search</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Search" CssClass="form-control" Text="" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:GridView ID="gvUsers" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="GdsDataSource1" CssClass="table table-bordered" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" AllowPaging="True" PageSize="8" >
                                <AlternatingRowStyle BackColor="#EFEFEF" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="GdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="UserSearch" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="Search" Name="searchString" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="form-group" aria-multiline="False">
                        <asp:Label runat="server" CssClass="col-md-2 control-label" Font-Bold="true">Selected User</asp:Label>
                        <asp:Label ID="SelectedName" runat="server" CssClass="col-md-2"></asp:Label><br />
                        <asp:Label ID="SelectedEmail" runat="server" CssClass="col-md-2"></asp:Label><br />
                        <div class="col-sm-offset-2">
                            <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal" CellSpacing="5" CellPadding="5" Visible="false">
                                <asp:ListItem>Staff</asp:ListItem>
                                <asp:ListItem>Manager</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="Update" runat="server" Text="Update" CssClass="btn btn-default" OnClick="Update_Click" CausesValidation="True" />
                        </div>
                    </div>
                    <br />
                </div>
            </section>
        </div>
    </div>
</asp:Content>
