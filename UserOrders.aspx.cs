using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class UserOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserOrdersGrid();
        }
    }

    private void BindUserOrdersGrid()
    {
        string userId = GetLoggedInUserId(); // Implement this method based on your authentication system
        string constr = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT OrderID, Username, OrderDate, OrderPrice FROM Orders WHERE Username = @Username";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@Username", userId);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        UserOrdersGridView.Columns[0].Visible = true;
                        UserOrdersGridView.DataSource = dt;
                        UserOrdersGridView.DataBind();
                        UserOrdersGridView.Columns[0].Visible = false;
                    }
                }
            }
        }
    }

    protected void UserOrdersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            int orderId = Convert.ToInt32(e.CommandArgument);
            BindOrderDetailsGrid(orderId);
            mpeOrderDetails.Show();
        }
    }


    private void BindOrderDetailsGrid(int orderId)
    {
        string constr = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT ProductName, ProductDescription, ProductPrice, Quantity, TotalPrice FROM OrderDetails WHERE OrderID = @OrderID"))
            {
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        OrderDetailsGridView.DataSource = dt;
                        OrderDetailsGridView.DataBind();
                    }
                }
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        mpeOrderDetails.Hide();
    }

    private string GetLoggedInUserId()
    {
        // Replace this with actual logic to get the logged-in user ID
        return Session["Username"].ToString();
    }
}
