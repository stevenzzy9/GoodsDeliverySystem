<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FireDriver.aspx.cs" Inherits="GoodsDeliverySystem.Transportation.FireDriver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <div class="col-md-11">
        <section id="fireDriverForm" >
            <div class="form-horizontal">
                    <br />
                    <br />
                <div id="Success" runat="server" class="alert alert-success" style="display:none;">
                        <strong>Successfully deleted Driver.</strong>
                    </div>
                <div id="Error" runat="server" class="alert alert-danger" style="display:none;">
                        <strong>Warning: </strong>There was a problem deleting the Driver. <asp:Label ID="lblReason" runat="server" />
                    </div>
                <h3>Fire/Edit Driver</h3>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="Search" CssClass="col-md-2 control-label">Search</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="Search" CssClass="form-control" Text="" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:GridView ID="gvDrivers" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="gdsDataSource1" CssClass="table table-bordered" OnSelectedIndexChanged="gvDrivers_SelectedIndexChanged" AllowPaging="True" OnRowDeleting="gvDrivers_RowDeleting" >
                            <AlternatingRowStyle BackColor="#EFEFEF" />
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                              
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteButton" runat="server" Text="Fire"
                                            CommandName="Delete" OnClientClick="return confirm('Are you sure you want to fire this driver?');"
                                            AlternateText="Delete" />               
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="false" ReadOnly="true" SortExpression="licenseNum" Visible="true" />
                               
                                <asp:TemplateField HeaderText="First Name" SortExpression="firstName" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbFirstName" runat="server" Text='<%# Bind("firstName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="firstName" runat="server" Text='<%# Bind("firstName") %>' Columns="15" MaxLength="15" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="firstName" CssClass="text-danger" Text="First Name must be provided." />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name" SortExpression="lastName" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbLastName" runat="server" Text='<%# Bind("lastName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lastName" runat="server" Text='<%# Bind("lastName") %>' Columns="15" MaxLength="15" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="lastName" CssClass="text-danger" Text="Last Name must be provided." />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="License Number" SortExpression="licenseNum" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseNum" runat="server" Text='<%# Bind("licenseNum") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="licenseNum" runat="server" Text='<%# Bind("licenseNum") %>' Columns="15" MaxLength="15" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="licenseNum" CssClass="text-danger" Text="License Number must be provided." />
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="licenseNum" OnServerValidate="LicenseNum_ServerValidate" ErrorMessage="The license number is in an invalid format" CssClass="text-danger" ValidateEmptyText="true" Display="Dynamic" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="License Expiration" SortExpression="licenseExperation" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbLicenseExpiration" runat="server" Text='<%# Bind("licenseExpiration") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="licenseExpiration" runat="server" Text='<%# Bind("licenseExpiration") %>' Columns="15" MaxLength="15" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="licenseExpiration" CssClass="text-danger" Text="License Expiration must be provided." />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="License State" SortExpression="state">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseState" runat='server' Text='<%# Bind("LicenseState") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="state" Width="150px" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Abbreviation" AppendDataBoundItems="true" SelectedValue='<%#Bind("LicenseState")%>' >
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [States] ORDER BY [Name]"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="state"
                                                    CssClass="text-danger" ErrorMessage="The license state field is required." />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="remark" HeaderText="Remarks" SortExpression="remark" />
                                <asp:TemplateField HeaderText="Local Driver?" SortExpression="LocalDriver">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Local" runat="server" Checked='<%# Eval("LocalDriver") %>' Enabled="false" />
                                        </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="Local" runat="server" Checked='<%# Bind("LocalDriver") %>' />
                                    </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="gdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" DeleteCommand="DriverDelete" DeleteCommandType="StoredProcedure" SelectCommand="DriverSearch" SelectCommandType="StoredProcedure" UpdateCommand="DriverUpdate" UpdateCommandType="StoredProcedure" >
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Search" Name="searchString" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="Id" Type="Int32" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Id" Type="Int32" />
                                <asp:Parameter Name="firstName" Type="String" />
                                <asp:Parameter Name="lastName" Type="String" />
                                <asp:Parameter Name="licenseNum" Type="String" />
                                <asp:Parameter Name="licenseExpiration" Type="String" />
                                <asp:Parameter Name="LicenseState" Type="String" />
                                <asp:Parameter Name="remark" Type="String" />
                             
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button ID="Fire" runat="server" Text="Fire" CssClass="btn btn-danger" OnClick="Fire_Click" Visible="False" />
                    </div>
                </div>
            </div>
        </section>
    </div>
</div>
</asp:Content>
