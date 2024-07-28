<%@ Page Title="تعديل معلومات المستخدم" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    تعديل معلومات المستخدم
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Header" Runat="Server">
     <style>
.profile-panel {
    background-color: #fff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0,0,0,0.1);
    width: 80%;
    direction: rtl;
    margin: 0 auto;
}

.profile-panel h2 {
    text-align: center;
    margin-bottom: 20px;
}

.profile-panel input[type="text"], .profile-panel input[type="password"] {
    width: 100%;
    padding: 10px;
    margin-bottom: 15px;
    border: 1px solid #ddd;
    border-radius: 4px;
}

.profile-panel .btn-edit {
    background-color: #28a745;
    color: #fff;
    border: none;
    padding: 10px 15px;
    cursor: pointer;
    border-radius: 4px;
}

.profile-panel .btn-edit:hover {
    background-color: #218838;
}

 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" dir="rtl" align="center">
        <h2>تعديل معلومات المستخدم</h2>

        <asp:Panel ID="pnlUserProfile" runat="server" CssClass="profile-panel">
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="" ForeColor="Red" />
            <div class="form-group">
                <asp:Label ID="lblUsername" runat="server" Text="اسم المستخدم:"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPassword" runat="server" Text="كلمة السر:"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblAddress" runat="server" Text="العنوان:"></asp:Label>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="تحديث" OnClick="btnUpdate_Click" CssClass="btn-edit" />
        </asp:Panel>
    </div>
</asp:Content>
