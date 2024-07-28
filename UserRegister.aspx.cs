using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();
        string Address = txtaddress.Text.Trim();
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(Address))
        {
            lblMessage.Text = "جميع الحقول مطلوبة.";
            return;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Users (UserName, Email, Password,Address) VALUES (@UserName, @Email, @Password,@Address)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password); // Note: In a real application, make sure to hash the password before storing it
                command.Parameters.AddWithValue("@Address", Address);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMessage.Text = "تم التسجيل بنجاح!";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "حدث خطأ أثناء التسجيل: " + ex.Message;
                }
            }
        }
    }
}