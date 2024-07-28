using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<string> selectedProducts = Session["SelectedProducts"] as List<string>;
            if (selectedProducts != null && selectedProducts.Count > 0)
            {
                BindCartItems(selectedProducts);
            }
        }
        UpdateCartCounter();
    }

    private void BindCartItems(List<string> selectedProducts)
    {
        DataTable dt = CreateCartDataTable();

        string connectionString = ConfigurationManager.ConnectionStrings["MarketingDBConnectionString"].ConnectionString;
        string productIds = string.Join(",", selectedProducts.Select(id => "'" + id + "'"));
        string query = "SELECT ProductID, ProductName, ProductDescription, ProductImagePath, ProductPrice FROM Products WHERE ProductID IN (" + productIds + ")";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable newProductsTable = new DataTable();
            da.Fill(newProductsTable);

            foreach (DataRow newRow in newProductsTable.Rows)
            {
                string newProductId = newRow["ProductID"].ToString();
                DataRow existingRow = dt.AsEnumerable().FirstOrDefault(row => row.Field<string>("ProductID") == newProductId);
                if (existingRow == null)
                {
                    DataRow row = dt.NewRow();
                    row["ProductID"] = newRow["ProductID"].ToString();
                    row["ProductName"] = newRow["ProductName"];
                    row["ProductDescription"] = newRow["ProductDescription"];
                    row["ProductImagePath"] = newRow["ProductImagePath"];
                    row["ProductPrice"] = newRow["ProductPrice"];
                    row["Quantity"] = 1;
                    row["TotalPrice"] = Convert.ToDecimal(newRow["ProductPrice"]);
                    dt.Rows.Add(row);
                }
            }
        }

        Session["CartDataTable"] = dt;

        GridViewCart.DataSource = dt;
        GridViewCart.DataBind();

        CalculateTotalPrice(dt);
        UpdateCartCounter();
    }

    public DataTable CreateCartDataTable()
    {
        DataTable dt = Session["CartDataTable"] as DataTable;

        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("ProductDescription", typeof(string));
            dt.Columns.Add("ProductImagePath", typeof(string));
            dt.Columns.Add("ProductPrice", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("TotalPrice", typeof(decimal));
        }

        return dt;
    }

    protected void QuantityChanged(object sender, EventArgs e)
    {
        DropDownList ddlQuantity = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlQuantity.NamingContainer;
        int rowIndex = row.RowIndex;

        DataTable dt = Session["CartDataTable"] as DataTable;
        if (dt != null)
        {
            int quantity = Convert.ToInt32(ddlQuantity.SelectedValue);
            dt.Rows[rowIndex]["Quantity"] = quantity;
            dt.Rows[rowIndex]["TotalPrice"] = Convert.ToDecimal(dt.Rows[rowIndex]["ProductPrice"]) * quantity;

            Session["CartDataTable"] = dt;

            GridViewCart.DataSource = dt;
            GridViewCart.DataBind();

            CalculateTotalPrice(dt);
            UpdateCartCounter();
        }
    }

    protected void GridViewCart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgProduct = (Image)e.Row.FindControl("imgProduct");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            imgProduct.ImageUrl = "~/" + rowView["ProductImagePath"].ToString();

            DropDownList ddlQuantity = (DropDownList)e.Row.FindControl("ddlQuantity");
            ddlQuantity.SelectedValue = rowView["Quantity"].ToString();
        }
    }

    protected void GridViewCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = Session["CartDataTable"] as DataTable;
        if (dt != null)
        {
            string productId = GridViewCart.DataKeys[e.RowIndex].Value.ToString();
            DataRow[] rowsToDelete = dt.Select("ProductID = '" + productId + "'");
            foreach (DataRow row in rowsToDelete)
            {
                dt.Rows.Remove(row);
            }

            // تحديث قائمة المنتجات المحددة في الجلسة
            List<string> selectedProducts = Session["SelectedProducts"] as List<string>;
            if (selectedProducts != null)
            {
                selectedProducts.Remove(productId);
                Session["SelectedProducts"] = selectedProducts;
            }

            Session["CartDataTable"] = dt;

            GridViewCart.DataSource = dt;
            GridViewCart.DataBind();

            CalculateTotalPrice(dt);
            UpdateCartCounter();
        }
    }

    protected void btnClearCart_Click(object sender, EventArgs e)
    {
        Session.Remove("SelectedProducts");
        Session.Remove("CartDataTable");

        Response.Redirect(Request.RawUrl);
    }

    private void CalculateTotalPrice(DataTable dt)
    {
        decimal totalPrice = 0;
        foreach (DataRow row in dt.Rows)
        {
            totalPrice += Convert.ToDecimal(row["TotalPrice"]);
        }

        lblTotalPrice.Text = "مجموع الأسعار: " + totalPrice.ToString("C");
    }
    private void UpdateCartCounter()
    {
        SiteMaster masterPage = (SiteMaster)this.Master;
        masterPage.UpdateCartCounter();
        
    }


    protected void btncopmleteorder_Click(object sender, EventArgs e)
    {
        // حفظ بيانات العربة في الجلسة لعرضها في ملخص الطلب
        DataTable dt = Session["CartDataTable"] as DataTable;
        if (dt != null)
        {
            Session["OrderSummaryDataTable"] = dt;
            Response.Redirect("OrderSummary.aspx");
        }
    }

}
