<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserRegister.aspx.cs" Inherits="UserRegister" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    إنشاء مستخدمين
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
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
        }
        .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .form-group button {
            padding: 10px 15px;
            background-color: #28a745;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .form-group button:hover {
            background-color: #218838;
        }
        .message {
            text-align: center;
            color: #d9534f;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="container" align="center">
            <h2>تسجيل المستخدمين</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="" />
            <div class="form-group">
                <label for="txtUserName">اسم المستخدم:</label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtEmail">البريد الإلكتروني:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtPassword">كلمة المرور:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtPassword">العنوان:</label>
                <asp:TextBox ID="txtaddress" runat="server"  CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnRegister" runat="server" Text="تسجيل" OnClick="btnRegister_Click" CssClass="btn btn-success" />
            </div>
        </div>
   </asp:Content>

