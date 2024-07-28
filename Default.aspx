<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    الرئيسية
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container mt-5">
        <!-- Carousel -->
        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
            </ol>
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img class="d-block w-100" src="png/slider1.png" alt="First slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>عنوان الصورة الأولى</h5>
                        <p>وصف الصورة الأولى.</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img class="d-block w-100" src="png/slider2.png" alt="Second slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>عنوان الصورة الثانية</h5>
                        <p>وصف الصورة الثانية.</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img class="d-block w-100" src="png/slider3.png" alt="Third slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>عنوان الصورة الثالثة</h5>
                        <p>وصف الصورة الثالثة.</p>
                    </div>
                </div>
            </div>
            <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">السابق</span>
            </a>
            <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">التالي</span>
            </a>
        </div>
        </div>
        <!-- معلومات عن الشركة -->
    <div class="container " align="right">
        <div class="row mt-12" >
            <div class="col-lg-6">
                <h2>عن الشركة</h2>
                <p>هنا يمكنك كتابة معلومات عن الشركة وما تقدمه من خدمات أو منتجات.</p>
            </div>
            <div class="col-lg-6">
                <h2>أخبار الشركة</h2>
                <ul>
                    <li>خبر 1: تفاصيل الخبر الأول.</li>
                    <li>خبر 2: تفاصيل الخبر الثاني.</li>
                    <li>خبر 3: تفاصيل الخبر الثالث.</li>
                </ul>
            </div>
        </div>

        <!-- منتجات -->
        <div class="row mt-12" >
            <div class="col-12">
                <h2>منتجاتنا</h2>
                <div class="product-grid">
                    <asp:Repeater ID="ProductsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="product">
                                <h3><%# Eval("ProductName") %></h3>
                                <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("ProductImagePath") %>' />
                                <p><%# Eval("ProductDescription") %></p>
                                <p>السعر: $<%# Eval("ProductPrice") %></p>
                                
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <!-- معلومات إضافية -->
        <div class="row mt-12" >
            <div class="col-12">
                <h2>معلومات إضافية</h2>
                <p>هنا يمكنك إضافة أي معلومات إضافية ترغب في عرضها على الصفحة الرئيسية.</p>
            </div>
        </div>

        <!-- حقوق الملكية -->
        <footer class="mt-12" >
            <div class="text-center">
                <p>&copy; 2024 شركتي. جميع الحقوق محفوظة.</p>
                <p>العنوان: 123 شارع الأعمال، المدينة، الدولة</p>
            </div>
        </footer>
    
        </div>
    
</asp:Content>