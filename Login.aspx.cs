using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.Security;

public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Optional: You can perform any initialization here
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        string userAddress;
        string isAdmin;
        string userid;

        if (ValidateUser(username, password, out userAddress, out isAdmin,out userid))
        {
            // Set session variables
            Session["Username"] = username;
            Session["UserAddress"] = userAddress;
            Session["IsAdmin"] = isAdmin;
            Session["UserID"] = userid;
            FormsAuthentication.SetAuthCookie(username, false);
            // Redirect to another page after successful login
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            lblMessage.Text = "اسم المستخدم أو كلمة المرور غير صحيحة!";
        }
    }

    private bool ValidateUser(string username, string password, out string userAddress, out string isAdmin,out string userid)
    {
        userAddress = null;
        isAdmin = null;
        userid = null;
        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        string query = "SELECT UserID, Address, IsAdmin FROM Users WHERE UserName = @Username AND Password = @Password AND IsDeleted = 0"; // Include IsAdmin in query

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        userAddress = reader["Address"].ToString(); // Assuming "Address" is the column name
                        isAdmin = reader["IsAdmin"].ToString(); // Assuming "IsAdmin" is the column name
                        userid = reader["UserID"].ToString(); // Assuming "IsAdmin" is the column name
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    // Handle exception, log it, etc.
                    lblMessage.Text = "حدث خطأ أثناء التحقق من المستخدم.";
                    throw new Exception("An error occurred while validating user.", ex);
                }
            }
        }
    }
}
