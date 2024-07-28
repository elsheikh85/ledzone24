<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewProducts.aspx.cs" Inherits="ViewProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    عرض المنتجات
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Header" Runat="Server">
    <style>
        .container {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            width: 80%;
            direction: rtl;
            margin: 0 auto;
        }
        .container h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        table, th, td {
            border: 1px solid #ddd;
        }
        th, td {
            padding: 8px;
            text-align: center;
        }
        th {
            background-color: #f2f2f2;
        }
        .btn-edit {
            background-color: #28a745;
            color: #fff;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            text-decoration: none;
            border-radius: 4px;
        }
        .btn-edit:hover {
            background-color: #218838;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <h2>قائمة المنتجات</h2>
         <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="معرف المنتج" ReadOnly="True" SortExpression="ProductID" />
                <asp:BoundField DataField="ProductName" HeaderText="اسم المنتج" SortExpression="ProductName" />
                <asp:BoundField DataField="ProductDescription" HeaderText="وصف المنتج" SortExpression="ProductDescription" />
                <asp:BoundField DataField="ProductPrice" HeaderText="السعر" SortExpression="ProductPrice" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditProduct" CommandArgument='<%# Eval("ProductID") %>' CssClass="btn-edit">تعديل</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
             </div>
    </div>
</asp:Content>
