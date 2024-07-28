using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class SiteMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateCartCounter();
            if (Session["IsAdmin"] != null && Session["IsAdmin"].ToString() == "True")
            {
                // Show admin links
                li1.Visible = li2.Visible = li3.Visible = li4.Visible = li5.Visible = true;
            }
            else
            {
                // Hide admin links
               li1.Visible = li2.Visible = li3.Visible = li4.Visible = li5.Visible= false;
            }
        }
        else
        {
            UpdateCartCounter();
        }
    }
    public void UpdateCartCounter()
    {

        int totalItems = 0;
        DataTable dt = Session["CartDataTable"] as DataTable;
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {
                totalItems += Convert.ToInt32(row["Quantity"]);
            }
        }
        CartCounter.Text = totalItems.ToString();
    }

}
