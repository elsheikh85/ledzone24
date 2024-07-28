<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    الرئيسية
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="content">
        <div class="product-grid">
            <asp:Repeater ID="ProductsRepeater" runat="server">
                <ItemTemplate>
                    <div class="product">
                        <h2><%# Eval("ProductName") %></h2>
                        <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("ProductImagePath") %>' />
                        <p><%# Eval("ProductDescription") %></p>
                        <p>السعر: $<%# Eval("ProductPrice") %></p>
                        <asp:Button ID="btnAddToCart" runat="server" Text="شراء الآن" OnClick="AddToCart_Click" CommandArgument='<%# Eval("ProductID") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

