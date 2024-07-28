<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Login Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Header" Runat="Server">
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
        .form-group input[type="text"], 
        .form-group input[type="password"] {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-group button {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .form-group button:hover {
            background-color: #0056b3;
        }
        .message {
            text-align: center;
            margin-top: 15px;
            color: #dc3545;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container" align="center">
        <h2>تسجيل الدخول</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="" />
        <div class="form-group">
            <label for="txtUsername">اسم المستخدم:</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" style="text-align: center" />
        </div>
        <div class="form-group">
            <label for="txtPassword">كلمة المرور:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" style="text-align: center" />
        </div>
        <div class="form-group">
            <asp:Button ID="btnLogin" runat="server" Text="تسجيل الدخول" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
