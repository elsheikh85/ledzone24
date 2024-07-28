<%@ Page Title="ملخص الطلب" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OrderSummary.aspx.cs" Inherits="OrderSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    ملخص الطلب
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container" dir="rtl" align="center">
        <h2>ملخص الطلب</h2>
        <asp:GridView ID="GridViewOrderSummary" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="اسم المنتج" />
                <asp:BoundField DataField="ProductDescription" HeaderText="وصف المنتج" />
                <asp:BoundField DataField="ProductPrice" HeaderText="السعر" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Quantity" HeaderText="الكمية" />
                <asp:BoundField DataField="TotalPrice" HeaderText="السعر الإجمالي" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <div align="right">
            <asp:Label ID="lblTotalPriceSummary" runat="server" CssClass="total-price-label"></asp:Label>
            <asp:Label ID="tp" runat="server" Text="" Visible="false"></asp:Label>
            <hr />
            <div class="row" align="center">
                <div class="col-md-4">
                    <asp:Label ID="lblPaymentMethod" runat="server" Text="طريقة الدفع: " CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control">
                        <asp:ListItem Text="الدفع عند الاستلام" Value="CashOnDelivery"></asp:ListItem>
                        <asp:ListItem Text="الدفع عبر الإنترنت" Value="OnlinePayment"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblDeliveryMethod" runat="server" Text="طريقة التوصيل: " CssClass="form-label" ></asp:Label>
                    <asp:DropDownList ID="ddlDeliveryMethod" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDeliveryMethod_SelectedIndexChanged">
                        <asp:ListItem Text="الاستلام من المتجر" Value="StorePickup"></asp:ListItem>
                        <asp:ListItem Text="توصيل إلى المنزل" Value="HomeDelivery"></asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label1" runat="server" Text="." CssClass="form-label"></asp:Label>
                    <asp:Button ID="btnConfirmOrder" runat="server" Text="تأكيد الطلب" CssClass="btn btn-success" OnClick="btnConfirmOrder_Click" />
                </div>
            </div>
            <div class="row" align="center" runat="server" id="addressdiv" visible="false">
                <div class="col-md-4">
                   
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label2" runat="server" Text="أدخل العنوان" CssClass="btn btn-success" ></asp:Label><br />
                    <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="btn btn-success" BackColor="White" ForeColor="Black"></asp:TextBox>
                                      
                </div>
                <div class="col-md-4">
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
