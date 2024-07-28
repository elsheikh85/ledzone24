<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditProduct.aspx.cs" Inherits="EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    تعديل منتج
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
        .form-group input, 
        .form-group textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .form-group textarea {
            resize: vertical;
            height: 100px;
        }
        .form-group input[type="file"] {
            border: none;
        }
        .form-group button {
            width: 100%;
            padding: 10px;
            background-color: #28a745;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }
        .form-group button:hover {
            background-color: #218838;
        }
        .message {
            text-align: center;
            margin-top: 15px;
            color: #d9534f;
        } .panel {
            width: 100%;
            height: 200px;
            border: 1px solid #ddd;
            border-radius: 4px;
            background-size: contain;
            background-position: center;
            background-repeat: no-repeat;
            margin: 15px;
            margin-bottom: 15px;
            padding: 30px;
            box-sizing: border-box;
        }
    </style>
    <script>
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var panel = document.getElementById('<%= Panel1.ClientID %>');
                    panel.style.backgroundImage = 'url(' + e.target.result + ')';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container" align="center">
        <h2>تعديل المنتج</h2>
       <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="" />
        <asp:HiddenField ID="hfProductId" runat="server" />
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtProductName">اسم المنتج:</label>
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtProductDescription">وصف المنتج:</label>
                    <asp:TextBox ID="txtProductDescription" runat="server" TextMode="MultiLine" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtProductPrice">السعر:</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">$</span>
                        </div>
                        <asp:TextBox ID="txtProductPrice" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="fuProductImage">صورة المنتج:</label>
                    <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control-file" OnChange="previewImage(this)" />
                    <asp:Panel ID="Panel1" runat="server" CssClass="panel"></asp:Panel> 
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Button ID="btnUpdate" runat="server" Text="تحديث" OnClick="btnUpdate_Click" CssClass="btn btn-success" />
        </div>
    </div>
</asp:Content>
