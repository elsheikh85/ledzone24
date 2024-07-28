using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Orders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadOrderDates();
            BindOrdersGrid();
        }
    }

    private void LoadOrderDates()
    {
        string constr = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT CONVERT(VARCHAR, OrderDate, 23) AS OrderDate FROM Orders"))
            {
                cmd.Connection = con;
                con.Open();
                OrderDateDropDownList.DataSource = cmd.ExecuteReader();
                OrderDateDropDownList.DataTextField = "OrderDate";
                OrderDateDropDownList.DataValueField = "OrderDate";
                OrderDateDropDownList.DataBind();
            }
        }
        OrderDateDropDownList.Items.Insert(0, new ListItem("اختر تاريخ", "0"));
    }

    protected void OrderDateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOrdersGrid();
    }

    private void BindOrdersGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT OrderID, Username, OrderDate, OrderPrice FROM Orders";
            if (OrderDateDropDownList.SelectedValue != "0")
            {
                query += " WHERE CONVERT(VARCHAR, OrderDate, 23) = @OrderDate";
            }
            using (SqlCommand cmd = new SqlCommand(query))
            {
                if (OrderDateDropDownList.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@OrderDate", OrderDateDropDownList.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        OrdersGridView.Columns[0].Visible = true;
                        OrdersGridView.DataSource = dt;
                        OrdersGridView.DataBind();
                        OrdersGridView.Columns[0].Visible = false;
                    }
                }
            }
        }
    }

    protected void OrdersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
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
}
