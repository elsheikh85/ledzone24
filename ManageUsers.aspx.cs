using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ManageUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUsersGrid();
        }
    }

    protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewUsers.EditIndex = e.NewEditIndex;
        BindUsersGrid(); // Refresh GridView
    }

    protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewUsers.EditIndex = -1;
        BindUsersGrid(); // Refresh GridView
    }
    protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int userId = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
        string userName = e.NewValues["UserName"].ToString();
        string email = e.NewValues["Email"].ToString();
        string password = e.NewValues["Password"].ToString(); // Replace with actual password field name

        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        string query = "UPDATE Users SET UserName = @UserName, Email = @Email, Password = @Password WHERE UserID = @UserID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password); // Add parameter for password
                command.Parameters.AddWithValue("@UserID", userId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMessage.Text = "تم تحديث المستخدم بنجاح!";
                    GridViewUsers.EditIndex = -1; // Exit edit mode
                    BindUsersGrid(); // Refresh GridView after update
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "حدث خطأ أثناء تحديث المستخدم: " + ex.Message;
                }
            }
        }
    }

    protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);

        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        string query = "UPDATE Users SET IsDeleted = 1 WHERE UserID = @UserID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMessage.Text = "تم حذف المستخدم بنجاح!";
                    BindUsersGrid(); // Refresh GridView after deletion
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "حدث خطأ أثناء حذف المستخدم: " + ex.Message;
                }
            }
        }
    }


    private void BindUsersGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        string query = "SELECT UserID, UserName, Email,Password FROM Users WHERE IsDeleted = 0"; // Only fetch non-deleted users

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(dataTable);

                GridViewUsers.DataSource = dataTable;
                GridViewUsers.DataBind();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
