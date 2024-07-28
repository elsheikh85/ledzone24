<%@ Page Title="طلبات المستخدم" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserOrders.aspx.cs" Inherits="UserOrders" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    طلبات المستخدم
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

        /* Styles for Modal Popup */
        /* Styles for Modal Popup */
.modalPopup {
    background-color: #fff;
    border: 1px solid #ddd;
    border-radius: 8px;
    box-shadow: 0 0 15px rgba(0,0,0,0.3);
    padding: 20px;
    width: 35%;
    position: fixed; /* Use fixed positioning */
    top: 50%;
    left: 30%; /* Start from 30% from the left */
    transform: translateY(-50%); /* Adjust vertical alignment only */
    z-index: 1002; /* Ensure it's above other content */
}

        .modalPopup h3 {
            text-align: center;
            margin-bottom: 20px;
        }
        .modalPopup table {
            width: 100%;
            border-collapse: collapse;
        }
        .modalPopup table, th, td {
            border: 1px solid #ddd;
        }
        .modalPopup th, .modalPopup td {
            padding: 8px;
            text-align: center;
        }
        .modalPopup th {
            background-color: #f2f2f2;
        }
        .modalBackground {
            background-color: rgba(0, 0, 0, 0.7);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1000;
        }
        /* Responsive Design for Mobile Devices */
    @media (max-width: 768px) {
        .modalPopup {
            width: 90%;
            left: 5%;
            transform: translateY(-50%);
        }
        .modalPopup h3 {
            font-size: 18px;
        }
    }

    @media (max-width: 480px) {
        .modalPopup {
            width: 95%;
            left: 2.5%;
            padding: 10px;
            font-size: 14px;
        }
        .modalPopup h3 {
            font-size: 16px;
        }
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" dir="rtl" align="center">
        <h2>طلبات المستخدم</h2>
         <div class="table-responsive">
       <asp:GridView ID="UserOrdersGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" EmptyDataText="لا توجد طلبات" OnRowCommand="UserOrdersGridView_RowCommand">
    <Columns>
        <asp:BoundField DataField="OrderID" HeaderText="رقم الطلب" />
        <asp:BoundField DataField="Username" HeaderText="اسم العميل" />
        <asp:BoundField DataField="OrderDate" HeaderText="تاريخ الطلب" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="OrderPrice" HeaderText="إجمالي المبلغ" DataFormatString="{0:C}" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnViewDetails" runat="server" Text="عرض التفاصيل" CommandName="ViewDetails" CommandArgument='<%# Eval("OrderID") %>' CssClass="btn-edit" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
             </div>
    </div>
    
    <!-- Modal Popup for Order Details -->
    <asp:Panel ID="pnlOrderDetails" runat="server" CssClass="modalPopup" style="display:none;">
        <h3>تفاصيل الطلب</h3>
         <div class="table-responsive">
        <asp:GridView ID="OrderDetailsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="اسم المنتج" />
                <asp:BoundField DataField="ProductDescription" HeaderText="وصف المنتج" />
                <asp:BoundField DataField="ProductPrice" HeaderText="سعر المنتج" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Quantity" HeaderText="الكمية" />
                <asp:BoundField DataField="TotalPrice" HeaderText="الإجمالي" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
             </div>
        <asp:Button ID="btnClose" runat="server" Text="إغلاق" OnClick="btnClose_Click" CssClass="btn-edit" />
    </asp:Panel>
    <asp:Button ID="btnHidden" runat="server" style="display:none;" />
    <asp:ModalPopupExtender ID="mpeOrderDetails" runat="server" TargetControlID="btnHidden" PopupControlID="pnlOrderDetails" BackgroundCssClass="modalBackground" />
</asp:Content>
