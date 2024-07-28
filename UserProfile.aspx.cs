using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUserProfile();
        }
    }

    private void LoadUserProfile()
    {
        string userId = GetLoggedInUserId(); // Implement this method to get the logged-in user's ID
        string constr = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT Username, Password, Address FROM Users WHERE UserID = @UserID";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtPassword.Text = reader["Password"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                    }
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string userId = GetLoggedInUserId(); // Implement this method to get the logged-in user's ID
        string constr = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "UPDATE Users SET Password = @Password, Address = @Address WHERE UserID = @UserID";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
                lblMessage.Text = "تم تحديث المنتج بنجاح!";
                // Clear the session and sign out
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
           
        }
    }

    private string GetLoggedInUserId()
    {
        return Session["UserID"].ToString();
    }
}
