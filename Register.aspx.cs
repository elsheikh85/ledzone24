using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Register : Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();
        string Address = txtaddress.Text.Trim();
        int IsAdmin;
        if (CheckBox1.Checked == true) { IsAdmin = 1; } else { IsAdmin = 0; }
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(Address))
        {
            lblMessage.Text = "جميع الحقول مطلوبة.";
            return;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Users (UserName, Email, Password,Address,IsAdmin) VALUES (@UserName, @Email, @Password,@Address,@IsAdmin)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password); // Note: In a real application, make sure to hash the password before storing it
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@IsAdmin", IsAdmin);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMessage.Text = "تم التسجيل بنجاح!";
                    txtaddress.Text = txtEmail.Text = txtPassword.Text = txtUserName.Text = "";CheckBox1.Checked = false;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "حدث خطأ أثناء التسجيل: " + ex.Message;
                }
            }
        }
    }
}
