using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

public partial class UploadProduct : System.Web.UI.Page
{
    string productImagePath;
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string productName = txtProductName.Text;
        string productDescription = txtProductDescription.Text;
        decimal productPrice;
        
        if (!decimal.TryParse(txtProductPrice.Text, out productPrice))
        {
            lblMessage.Text = "السعر غير صالح.";
            return;
        }
        //
        if (fuProductImage.HasFile && fuProductImage.PostedFile.ContentLength > 0)
        {
            try
            {
                string fileName = Path.GetFileName(fuProductImage.PostedFile.FileName);
                string savePath = Server.MapPath("~/ProductImages/" + fileName);

                // Ensure the directory exists
                string directoryPath = Server.MapPath("~/ProductImages/");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Save the file
                fuProductImage.SaveAs(savePath);

                // Set the product image path
                 productImagePath = "ProductImages/" + fileName;

                // Display success message or perform further actions
                lblMessage.Text = "تم تحميل الصورة بنجاح.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "خطأ أثناء تحميل الصورة: " + ex.Message;
            }
        }
        else
        {
            lblMessage.Text = "يرجى اختيار ملف صورة للتحميل.";
        }
        //
       
        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        string query = "INSERT INTO Products (ProductName, ProductDescription, ProductImagePath, ProductPrice) VALUES (@ProductName, @ProductDescription, @ProductImagePath, @ProductPrice)";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ProductDescription", productDescription);
                command.Parameters.AddWithValue("@ProductImagePath", productImagePath);
                command.Parameters.AddWithValue("@ProductPrice", productPrice);

                connection.Open();
                command.ExecuteNonQuery();
            }

            lblMessage.Text = "تم تحميل المنتج بنجاح!";
            lblMessage.ForeColor = System.Drawing.Color.Green;  // Set message color to green for success
            txtProductDescription.Text = txtProductName.Text = txtProductPrice.Text = "";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "حدث خطأ أثناء تحميل المنتج: " + ex.Message;
        }
    }
}
