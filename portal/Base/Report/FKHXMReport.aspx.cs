using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Base_Report_FKHXMReport : UserPagebase
{
    public string titleValue = "";
    public string ResID = "";
    public string keyWordValue = "";
    public string UserID = "";
    public int PageSize = 15;
    public string SearchType = "";
    public string NodeID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        keyWordValue = "FKHXM";
        if (Request.QueryString["SearchType"] != null)
        {
            SearchType = Request.QueryString["SearchType"].ToString();
        }
        if (Request.QueryString["NodeID"] != null)
        {
            NodeID = Request.QueryString["NodeID"].ToString();
        }
    }
}