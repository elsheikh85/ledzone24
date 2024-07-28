<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    إدارة المستخدمين
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" Runat="Server">
    <style>
        .container {
            width: 80%;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
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
            background-color: #dc3545;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .form-group button:hover {
            background-color: #c82333;
        }
        .message {
            text-align: center;
            color: #d9534f;
        }
        .gridview-container {
    width: 100%;
    margin: 20px auto;
    border: 1px solid #ddd;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0,0,0,0.1);
    background-color: #fff;
    overflow-x: auto; /* للتعامل مع الأفقي */
}

.gridview-container table {
    width: 100%;
    border-collapse: collapse;
}

.gridview-container th,
.gridview-container td {
    padding: 8px;
    text-align: center;
    border: 1px solid #ddd;
}

.gridview-container th {
    background-color: #f2f2f2;
    font-weight: bold;
    color: #333;
}

.gridview-container tr:nth-child(even) {
    background-color: #f9f9f9;
}

.gridview-container tr:hover {
    background-color: #f2f2f2;
}

.gridview-container .btn {
    padding: 8px 12px;
    background-color: #dc3545;
    color: #fff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    text-transform: uppercase;
}

.gridview-container .btn-edit {
    background-color: #007bff;
}

.gridview-container .btn-delete {
    background-color: #dc3545;
}

.gridview-container .btn:hover {
    opacity: 0.8;
}

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container" align="center">
        <h2>إدارة المستخدمين</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="" />
         <div class="table-responsive">
       <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
     OnRowEditing="GridViewUsers_RowEditing" OnRowCancelingEdit="GridViewUsers_RowCancelingEdit"
     OnRowUpdating="GridViewUsers_RowUpdating" OnRowDeleting="GridViewUsers_RowDeleting"
     AutoGenerateEditButton="True"  CssClass="table table-bordered">

    <Columns>
        <asp:BoundField DataField="UserID" HeaderText="معرف المستخدم" Visible="false" />
        <asp:BoundField DataField="UserName" HeaderText="اسم المستخدم" />
        <asp:BoundField DataField="Email" HeaderText="البريد الإلكتروني" />
        <asp:BoundField DataField="Password" HeaderText="كلمة السر" />
        <asp:TemplateField HeaderText="العمليات">
            <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="تعديل" CommandName="Edit" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-primary btn-edit" />
                <asp:Button ID="btnDelete" runat="server" Text="حذف" CommandName="Delete" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-danger" />
            </ItemTemplate>
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>
             </div>
    </div>
</asp:Content>
