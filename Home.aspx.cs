using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.ConstrainedExecution;
using System.Activities.Expressions;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProducts();

        }
        UpdateCartCounter();
    }
    private void UpdateCartCounter()
    {
        SiteMaster masterPage = (SiteMaster)this.Master;
        masterPage.UpdateCartCounter();
    }
    private void LoadProducts()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT ProductID, ProductName, ProductDescription, ProductPrice, ProductImagePath FROM Products";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ProductsRepeater.DataSource = reader;
            ProductsRepeater.DataBind();
        }
    }
    protected void AddToCart_Click(object sender, EventArgs e)
    {
        // استرجاع معرف المنتج الذي تم النقر عليه
        string productId = ((Button)sender).CommandArgument;

        // التحقق من وجود المنتج في العربة
        if (IsProductInCart(productId))
        {
            // عرض رسالة تنبيهية

            Response.Write("<script>alert('المنتج موجود بالفعل في العربة')</script>");
        }
        else
        {
            // إضافة معرف المنتج إلى قائمة العناصر المحددة في الجلسة
            AddProductToCart(productId);
            // تحديث العداد
            UpdateCartCounter();//لا يقوم بعمل تحديث للعداد الا بعد فتحه الكارت 
                                // استدعاء الدالة BindCartItems مباشرة
                                // UpdateCartCounter();

            // إعادة تحميل المنتجات
            LoadProducts();
            // توجيه المستخدم إلى صفحة العربة
            Response.Redirect("Cart.aspx");

        }
    }

    private void AddProductToCart(string productId)
    {
        // استخدام الجلسة لتخزين معرفات المنتجات المحددة
        List<string> selectedProducts = Session["SelectedProducts"] as List<string>;
        if (selectedProducts == null)
        {
            selectedProducts = new List<string>();
        }
        selectedProducts.Add(productId);
        Session["SelectedProducts"] = selectedProducts;
        UpdateCartCounter();
    }
    private bool IsProductInCart(string productId)
    {
        List<string> selectedProducts = Session["SelectedProducts"] as List<string>;
        return selectedProducts != null && selectedProducts.Contains(productId);
    }
}