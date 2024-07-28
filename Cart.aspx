<%@ Page Title="عربة التسوق" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    عربة التسوق
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container" dir="rtl" align="center">
        <h2>عربة التسوق</h2>
        <div class="table-responsive">
        <asp:GridView ID="GridViewCart" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="table table-striped " OnRowDataBound="GridViewCart_RowDataBound" OnRowDeleting="GridViewCart_RowDeleting" DataKeyNames="ProductID">
            <Columns>
                <asp:TemplateField HeaderText="صورة المنتج">
                    <ItemTemplate>
                        <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("ProductImagePath", "~/{0}") %>' AlternateText="Product Image" Style="width:100px;height:100px;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProductName" HeaderText="اسم المنتج" />
                <asp:BoundField DataField="ProductDescription" HeaderText="وصف المنتج" />
                <asp:TemplateField HeaderText="السعر">
                    <ItemTemplate>
                        <%# Eval("ProductPrice", "{0:C}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الكمية المطلوبة">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlQuantity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="QuantityChanged" CssClass="form-control">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="السعر الإجمالي">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalPricePerItem" runat="server" Text='<%# Eval("TotalPrice", "{0:C}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
            </div>
        <div align="right">
            <asp:Label ID="lblTotalPrice" runat="server" CssClass="total-price-label"></asp:Label>
            <hr />
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="btnClearCart" runat="server" Text="حذف الكل" OnClick="btnClearCart_Click" CssClass="btn btn-success" />
                </div>
                <div class="col-md-2"></div><div class="col-md-2"></div><div class="col-md-2"></div><div class="col-md-2"></div>
                <div class="col-md-2" align="left">
                    <asp:Button ID="btncopmleteorder" runat="server" Text="إتمام الطلب" OnClick="btncopmleteorder_Click" CssClass="btn btn-success" />
                </div>

            </div>
            
        </div>

    </div>
</asp:Content>
