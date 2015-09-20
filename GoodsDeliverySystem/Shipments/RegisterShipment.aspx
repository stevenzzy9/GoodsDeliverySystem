<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterShipment.aspx.cs" Inherits="GoodsDeliverySystem.Shipments.RegisterShipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.updateCostOptions = function updateCostOptions() {
            var weight = document.getElementById('MainContent_Weight').value;
            var cost = 0.0;

            if (parseFloat(weight) != NaN) {
                if (weight <= 0)
                    cost = 0;
                else if (weight < 5)
                    cost = 5.75;
                else if (weight < 10)
                    cost = 10.75;
                else if (weight < 20)
                    cost = 17.95;
                else if (weight < 30)
                    cost = 24.95;
                else if (weight < 45)
                    cost = 28.95;
                else if (weight < 70)
                    cost = 35.95;
                else if (weight < 90)
                    cost = 67.95;
                else if (weight < 110)
                    cost = 93.95;
                else if (weight >= 110)
                    cost = 110.95;
            }

            //add arbitary insurance cost
            var insuranceDdl = document.getElementById('<%=Insurance.ClientID%>');
            if (insuranceDdl.options[insuranceDdl.selectedIndex].value == 1) {
                cost += 2.0;
            }
            else if (insuranceDdl.options[insuranceDdl.selectedIndex].value == 2) {
                cost += 5.0;
            }

            //add arbitrary cost for increased shipping speed
            var shippingSpeedDdl = document.getElementById('<%=ShippingSpeed.ClientID%>');
            if (shippingSpeedDdl.options[shippingSpeedDdl.selectedIndex].value == 1) {
                cost += 2.0;
            }
            else if (shippingSpeedDdl.options[shippingSpeedDdl.selectedIndex].value == 2) {
                cost += 5.0;
            }

            if (cost == 0.0)
                document.getElementById('<%=Cost.ClientID %>').innerText = "$0.00";
            else
                document.getElementById('<%=Cost.ClientID %>').innerText = "$" + cost;
        }
    </script>

    <br />
    <br />
    <div class="row">
        <div class="col-md-12">
            <section id="addTruckForm">
                <div class="form-horizontal">
                    <h3>Register Shipment</h3>
                    <br />
                    <asp:UpdatePanel ID="FullPage" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="MainPage" runat="server">
                                <ContentTemplate>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div id="ShipmentInfo" runat="server">
                                                <h4 style="white-space: pre"><b><u>1. Shipment Information</u></b>                                     2. Delivery Information                                     3. Payment Information</h4>
                                                <br />
                                                <div class="control-column">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="Weight" CssClass="col-md-3 control-label">Weight</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="Weight" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                            <div class="float-left text-box-post-label">pounds</div>
                                                            <br />
                                                            <asp:CustomValidator ID="WeightValidator" runat="server" ControlToValidate="Weight" ValidationGroup="Shipment"
                                                                OnServerValidate="WeightValidator_ServerValidate" ErrorMessage="<br/>Weight must be a number between 0 and 150." CssClass="text-danger" ValidateEmptyText="true" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="Length" CssClass="col-md-3 control-label">Length</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="Length" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                            <div class="float-left text-box-post-label">inches</div>
                                                            <br />
                                                            <asp:CustomValidator ID="LengthValidator" runat="server" ControlToValidate="Length" ValidationGroup="Shipment"
                                                                OnServerValidate="LengthValidator_ServerValidate" ErrorMessage="<br/>Length must be a number between 0 and 120." CssClass="text-danger" ValidateEmptyText="true" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="Width" CssClass="col-md-3 control-label">Width</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="Width" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                            <div class="float-left text-box-post-label">inches</div>
                                                            <br />
                                                            <asp:CustomValidator ID="WidthValidator" runat="server" ControlToValidate="Width" ValidationGroup="Shipment"
                                                                OnServerValidate="WidthValidator_ServerValidate" ErrorMessage="<br/>Width must be a number between 0 and 120." CssClass="text-danger" ValidateEmptyText="true" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="Height" CssClass="col-md-3 control-label">Height</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="Height" CssClass="form-control float-left" TextMode="Number" Width="50%" />
                                                            <div class="float-left text-box-post-label">inches</div>
                                                            <br />
                                                            <asp:CustomValidator ID="HeightValidator" runat="server" ControlToValidate="Height" ValidationGroup="Shipment"
                                                                OnServerValidate="HeightValidator_ServerValidate" ErrorMessage="<br/>Height must be a number between 0 and 120." CssClass="text-danger" ValidateEmptyText="true" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="control-column">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="ShipmentType" CssClass="col-md-3 control-label">Type</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList runat="server" ID="ShipmentType" CssClass="form-control" DataSourceID="GdsDataSource2" DataTextField="TypeName" DataValueField="Id" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="" Value="" Selected="True" />
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="GdsDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [ShipmentTypes]"></asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <asp:Label ID="Label8" runat="server" AssociatedControlID="ShippingSpeed" CssClass="col-md-3 control-label">Shipping Speed</asp:Label>
                                                                <div class="col-md-9">
                                                                <asp:DropDownList runat="server" ID="ShippingSpeed" CssClass="form-control" DataSourceID="GdsDataSource5" DataTextField="Speed" DataValueField="Id">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="GdsDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [ShippingSpeeds] ORDER BY [Id]"></asp:SqlDataSource>
                                                            </div>
                                                            </div>
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <asp:Label runat="server" AssociatedControlID="Insurance" CssClass="col-md-3 control-label">Insurance</asp:Label>
                                                                <div class="col-md-9">
                                                                    <asp:DropDownList runat="server" ID="Insurance" CssClass="form-control" DataSourceID="GdsDataSource3" DataTextField="InsuranceType" DataValueField="Id" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="" Value="" Selected="True" />
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="GdsDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [InsuranceType]"></asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div id="ItemValueDiv" runat="server" class="form-group">
                                                                <asp:Label runat="server" AssociatedControlID="ItemValue" CssClass="col-md-3 control-label">Item Value</asp:Label>
                                                                <div class="float-left text-box-post-label">$ </div>
                                                                <asp:TextBox runat="server" ID="ItemValue" CssClass="form-control float-left" />
                                                                <br />
                                                                <asp:CustomValidator ID="ValueValidator" runat="server" ControlToValidate="ItemValue" ValidationGroup="Shipment"
                                                                    OnServerValidate="ValueValidator_ServerValidate" ErrorMessage="Item value must be a number between 0 and 25,000." CssClass="text-danger col-md-9 float-right" />
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:Label runat="server" AssociatedControlID="Cost" CssClass="col-md-3 control-label">Cost</asp:Label>
                                                                <div class="col-md-9" style="margin-top: 7px">
                                                                    <asp:Label ID="Cost" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br />
                                                    <div class="form-group">
                                                        <div class="col-md-offset-2 col-md-9 pull-right">
                                                            <asp:Button ID="ToDeliveryInfo" runat="server" Text="Continue" CssClass="btn btn-default" CausesValidation="true" ValidationGroup="Shipment" OnClick="ToDeliveryInfo_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div id="DeliveryInfo" runat="server" visible="false">
                                                <h4 style="white-space: pre">1. Shipment Information                                     <b><u>2. Delivery Information</u></b>                                     3. Payment Information</h4>
                                                <br />
                                                <div class="control-column">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server" AssociatedControlID="FirstName" CssClass="col-md-3 control-label">First Name</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName" ValidationGroup="Delivery"
                                                                CssClass="text-danger" ErrorMessage="The first name field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server" AssociatedControlID="LastName" CssClass="col-md-3 control-label">Last Name</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastName" ValidationGroup="Delivery"
                                                                CssClass="text-danger" ErrorMessage="The last name field is required." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="control-column">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server" AssociatedControlID="Street" CssClass="col-md-3 control-label">Street</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="Street" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Street" ValidationGroup="Delivery"
                                                                CssClass="text-danger" ErrorMessage="The street field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label4" runat="server" AssociatedControlID="City" CssClass="col-md-3 control-label">City</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="City" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="City" ValidationGroup="Delivery"
                                                                CssClass="text-danger" ErrorMessage="The city field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server" AssociatedControlID="State" CssClass="col-md-3 control-label">State</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList runat="server" ID="State" CssClass="form-control" AppendDataBoundItems="true" DataSourceID="GdsDataSource1" DataTextField="Name" DataValueField="Abbreviation">
                                                                <asp:ListItem Value="" Text="" Selected="True" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="State" ValidationGroup="Delivery"
                                                                CssClass="text-danger" ErrorMessage="The state field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label6" runat="server" AssociatedControlID="Zip" CssClass="col-md-3 control-label">Zip</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="Zip" CssClass="form-control" MaxLength="5" TextMode="Number" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Zip" ValidationGroup="Delivery"
                                                                CssClass="text-danger" ErrorMessage="The zip code field is required." />
                                                            <asp:CustomValidator ID="ZipValidator" runat="server" ControlToValidate="Zip" OnServerValidate="ZipValidator_ServerValidate" ErrorMessage="<br/>The zip code must be exactly 5 digits" CssClass="text-danger" ValidateEmptyText="true" ValidationGroup="Delivery" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-offset-2 col-md-9 pull-right">
                                                            <asp:Button ID="ToPaymentInfo" runat="server" Text="Continue" CssClass="btn btn-default" OnClick="ToPaymentInfo_Click" />
                                                            <asp:Button ID="BackToShipmentInfo" runat="server" Text="Back" CssClass="btn btn-default" CausesValidation="false" OnClick="BackToShipmentInfo_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div id="PaymentInfo" runat="server" visible="false">
                                                <h4 style="white-space: pre">1. Shipment Information                                     2. Delivery Information                                     <b><u>3. Payment Information</u></b></h4>
                                                <br />
                                                <div class="control-column">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" AssociatedControlID="NameOnCard" CssClass="col-md-3 control-label">Name on Card</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="NameOnCard" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NameOnCard" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The name field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label13" runat="server" AssociatedControlID="CardType" CssClass="col-md-3 control-label">Card Type</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList runat="server" ID="CardType" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="" Text="" Selected="True" />
                                                                <asp:ListItem Value="Visa" Text="Visa" />
                                                                <asp:ListItem Value="MasterCard" Text="MasterCard" />
                                                                <asp:ListItem Value="Discover" Text="Discover" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="CardType" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The card type field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label14" runat="server" AssociatedControlID="CardNumber" CssClass="col-md-3 control-label">Card Number</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="CardNumber" CssClass="form-control" />
                                                            <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="Month"
                                                                OnServerValidate="CardNumValidator_ServerValidate" ErrorMessage="The card number is invalid" CssClass="text-danger"
                                                                ValidateEmptyText="true" ValidationGroup="Payment" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label16" runat="server" AssociatedControlID="SecurityCode" CssClass="col-md-3 control-label">Security Code</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="SecurityCode" CssClass="form-control" />
                                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="SecurityCode" OnServerValidate="SecurityCodeValidator_ServerValidate" ErrorMessage="The security code must be 3 or 4 digits" CssClass="text-danger" ValidateEmptyText="true" ValidationGroup="Payment" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label15" runat="server" AssociatedControlID="Month" CssClass="col-md-3 control-label">Expiration</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList runat="server" ID="Month" CssClass="form-control float-left" Width="32%">
                                                                <asp:ListItem Value="" Text="" Selected="True" />
                                                                <asp:ListItem Value="1" Text="January" />
                                                                <asp:ListItem Value="2" Text="February" />
                                                                <asp:ListItem Value="3" Text="March" />
                                                                <asp:ListItem Value="4" Text="April" />
                                                                <asp:ListItem Value="5" Text="May" />
                                                                <asp:ListItem Value="6" Text="June" />
                                                                <asp:ListItem Value="7" Text="July" />
                                                                <asp:ListItem Value="8" Text="August" />
                                                                <asp:ListItem Value="9" Text="September" />
                                                                <asp:ListItem Value="10" Text="October" />
                                                                <asp:ListItem Value="11" Text="November" />
                                                                <asp:ListItem Value="12" Text="December" />
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" ID="Year" CssClass="form-control float-left left-margin" Width="32%">
                                                                <asp:ListItem Value="" Text="" />
                                                            </asp:DropDownList><br />
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="Month" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The month field is required." />
                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Year" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The year field is required." />
                                                            <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="Month"
                                                                OnServerValidate="ExpirationValidator_ServerValidate" ErrorMessage="<br/>The card is expired" CssClass="text-danger"
                                                                ValidateEmptyText="true" ValidationGroup="Payment" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="control-column">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label9" runat="server" AssociatedControlID="StreetPay" CssClass="col-md-3 control-label">Street</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="StreetPay" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="StreetPay" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The street field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label10" runat="server" AssociatedControlID="CityPay" CssClass="col-md-3 control-label">City</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="CityPay" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="CityPay" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The city field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label11" runat="server" AssociatedControlID="StatePay" CssClass="col-md-3 control-label">State</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList runat="server" ID="StatePay" CssClass="form-control" DataSourceID="GdsDataSource1" DataTextField="Name" DataValueField="Abbreviation" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="" Text="" Selected="True" />
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="GdsDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GoodsDeliveryConnectionString %>" SelectCommand="SELECT * FROM [States] ORDER BY [Name]"></asp:SqlDataSource>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="StatePay" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The state field is required." />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label12" runat="server" AssociatedControlID="ZipPay" CssClass="col-md-3 control-label">Zip</asp:Label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="ZipPay" CssClass="form-control" MaxLength="5" TextMode="Number" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ZipPay" ValidationGroup="Payment"
                                                                CssClass="text-danger" ErrorMessage="The zip code field is required." />
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="Zip" OnServerValidate="ZipPayValidator_ServerValidate" ErrorMessage="<br/>The zip code must be exactly 5 digits" CssClass="text-danger" ValidateEmptyText="true" ValidationGroup="Payment" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="form-group">
                                                        <div class="col-md-offset-2 col-md-9 pull-right">
                                                            <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="Submit_Click" />
                                                            <asp:Button ID="BackToDeliveryInfo" runat="server" Text="Back" CssClass="btn btn-default" CausesValidation="false" OnClick="BackToDeliveryInfo_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="Confirmation" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:Label ID="ConfirmLabel" runat="server" CssClass="confirmation-message">Your shipment has been successfully registered!</asp:Label><br />
                                    <br />
                                    <asp:Label runat="server" CssClass="confirmation-message">A confirmation will be sent to your email address on file.</asp:Label><br />
                                    <br />
                                    <a href="../Account/CustomerMenu.aspx" class="confirmation-message">Return to Menu</a>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
