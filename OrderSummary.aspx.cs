using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class OrderSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Session["OrderSummaryDataTable"] as DataTable;
            if (dt != null)
            {
                try
                {
                    GridViewOrderSummary.DataSource = dt;
                    GridViewOrderSummary.DataBind();
                    CalculateTotalPrice(dt);
                    txtaddress.Text = Session["UserAddress"].ToString();
                }
                catch { }
            }
            else { txtaddress.Text = "إستلام من المتجر"; }
        }
    }

    private void CalculateTotalPrice(DataTable dt)
    {
        decimal totalPrice = 0;
        foreach (DataRow row in dt.Rows)
        {
            totalPrice += Convert.ToDecimal(row["TotalPrice"]);
        }

        lblTotalPriceSummary.Text = "مجموع الأسعار: " + totalPrice.ToString("C");
    }

    protected void btnConfirmOrder_Click(object sender, EventArgs e)
    {
        // هنا يمكنك إضافة الكود لحفظ الطلب في قاعدة البيانات أو تنفيذ الإجراءات المناسبة لتأكيد الطلب

        if (ddlDeliveryMethod.SelectedValue == "HomeDelivery" && string.IsNullOrWhiteSpace(txtaddress.Text))
        {
            // عرض رسالة خطأ إذا كان الحقل إلزاميًا ولم يتم تعبئته
            
            Response.Write("<script>alert('الرجاء إدخال العنوان')</script>");
            return;
        }

        // تنفيذ باقي منطق تأكيد الطلب هنا

        string username = Session["Username"] as string;
        if (string.IsNullOrEmpty(username))
        {
            // Redirect to login page if user is not logged in
            Response.Redirect("~/Login.aspx");
        }

        DataTable dt = Session["CartDataTable"] as DataTable;
        if (dt != null && dt.Rows.Count > 0)
        {
            int UserID=int.Parse( Session["UserID"].ToString());
            string paymentMethod = ddlPaymentMethod.SelectedValue;
            string deliveryMethod = ddlDeliveryMethod.SelectedValue;
            string address = txtaddress.Text.Trim();
            DateTime orderDate = DateTime.Now;
            string orderStatus = "في التجهيز";
            
            // تحديد النص الكامل
            string labelText = lblTotalPriceSummary.Text;  // قم بتغيير lblTotalPrice.Text إلى المتغير الذي تستخدمه بالفعل

            // استخراج الجزء الرقمي فقط
            string numericValue = labelText.Substring(labelText.IndexOf('$') + 1);  // يفصل النص بعد الرمز '$'

            // تحويل النص إلى عدد عشري
            decimal OrderPrice = decimal.Parse(numericValue);  // تحويل النص إلى عدد عشري

            // الآن totalPrice يحتوي على القيمة الرقمية فقط ($561.00 -> 561.00)

            string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                

                try
                {
                    // Insert order into Orders table
                    string insertOrderQuery = "INSERT INTO Orders (Username, OrderDate, PaymentMethod, DeliveryMethod, Address, OrderStatus,OrderPrice,UserID) OUTPUT INSERTED.OrderID VALUES (@Username, @OrderDate, @PaymentMethod, @DeliveryMethod, @Address, @OrderStatus,@OrderPrice,@UserID)";
                    SqlCommand cmdInsertOrder = new SqlCommand(insertOrderQuery, connection);
                    cmdInsertOrder.Parameters.AddWithValue("@Username", username);
                    cmdInsertOrder.Parameters.AddWithValue("@OrderDate", orderDate);
                    cmdInsertOrder.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmdInsertOrder.Parameters.AddWithValue("@DeliveryMethod", deliveryMethod);
                    cmdInsertOrder.Parameters.AddWithValue("@Address", address);
                    cmdInsertOrder.Parameters.AddWithValue("@OrderStatus", orderStatus);
                    cmdInsertOrder.Parameters.AddWithValue("@OrderPrice", OrderPrice);
                    cmdInsertOrder.Parameters.AddWithValue("@UserID", UserID);
                    int orderId = (int)cmdInsertOrder.ExecuteScalar();

                    // Insert order details into OrderDetails table
                    foreach (DataRow row in dt.Rows)
                    {
                        string insertOrderDetailsQuery = "INSERT INTO OrderDetails (OrderID, ProductName, ProductDescription, ProductPrice, Quantity, TotalPrice) VALUES (@OrderID, @ProductName, @ProductDescription, @ProductPrice, @Quantity, @TotalPrice)";
                        SqlCommand cmdInsertOrderDetails = new SqlCommand(insertOrderDetailsQuery, connection);
                        cmdInsertOrderDetails.Parameters.AddWithValue("@OrderID", orderId);
                        cmdInsertOrderDetails.Parameters.AddWithValue("@ProductName", row["ProductName"]);
                        cmdInsertOrderDetails.Parameters.AddWithValue("@ProductDescription", row["ProductDescription"]);
                        cmdInsertOrderDetails.Parameters.AddWithValue("@ProductPrice", row["ProductPrice"]);
                        cmdInsertOrderDetails.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                        cmdInsertOrderDetails.Parameters.AddWithValue("@TotalPrice", row["TotalPrice"]);

                        cmdInsertOrderDetails.ExecuteNonQuery();
                    }

                    

                    // Clear the cart session
                    Session.Remove("CartDataTable");
                    Session.Remove("SelectedProducts");

                    // Redirect to a confirmation page or display a success message
                    Response.Redirect("~/Cart.aspx");
                }
                catch (Exception ex)
                {
                    
                    // Log the exception and show an error message
                   
                    Response.Write("<script>alert('حدث خطأ أثناء إتمام الطلب. يرجى المحاولة مرة أخرى')</script>");
                    // Log the exception (not shown here for simplicity)
                }
            }
        }
    }


    protected void ddlDeliveryMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlDeliveryMethod.SelectedIndex==1)
        {
            addressdiv.Visible = true;
        }
        else
        {
            addressdiv.Visible = false;
            
        }
    }
}
