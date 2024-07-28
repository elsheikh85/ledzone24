using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;

public partial class EditProduct : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string productId = Request.QueryString["ProductId"];
            if (!string.IsNullOrEmpty(productId))
            {
                hfProductId.Value = productId;
                LoadProductDetails(productId);
            }
        }
    }

    private void LoadProductDetails(string productId)
    {
        string connStr = WebConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT ProductName, ProductDescription, ProductPrice, ProductImagePath FROM Products WHERE ProductID = @ProductID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ProductID", productId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtProductName.Text = reader["ProductName"].ToString();
                txtProductDescription.Text = reader["ProductDescription"].ToString();
                txtProductPrice.Text = reader["ProductPrice"].ToString();
                // يمكنك عرض مسار الصورة هنا إذا كنت تريد ذلك
                // imgProduct.ImageUrl = reader["ProductImagePath"].ToString();
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string connStr = WebConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "UPDATE Products SET ProductName = @ProductName, ProductDescription = @ProductDescription, ProductPrice = @ProductPrice, ProductImagePath = @ProductImagePath WHERE ProductID = @ProductID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ProductID", hfProductId.Value);
            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
            cmd.Parameters.AddWithValue("@ProductDescription", txtProductDescription.Text);
            cmd.Parameters.AddWithValue("@ProductPrice", txtProductPrice.Text);

            if (fuProductImage.HasFile)
            {
                string savePath = Server.MapPath("~/ProductImages/");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                string fileName = Path.GetFileName(fuProductImage.PostedFile.FileName);
                string filePath = Path.Combine(savePath, fileName);
                fuProductImage.SaveAs(filePath);

                cmd.Parameters.AddWithValue("@ProductImagePath", "ProductImages/" + fileName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProductImagePath", DBNull.Value);
            }

            conn.Open();
            cmd.ExecuteNonQuery();

            lblMessage.Text = "تم تحديث المنتج بنجاح!";
        }
    }
}
