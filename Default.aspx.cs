using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.ConstrainedExecution;
using System.Activities.Expressions;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadProducts();
    }
    private void LoadProducts()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT ProductID, ProductName, ProductDescription, ProductPrice, ProductImagePath FROM Products";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ProductsRepeater.DataSource = reader;
            ProductsRepeater.DataBind();
        }
    }

}
