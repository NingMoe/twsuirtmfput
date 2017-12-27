using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Base_Report_WGJKHTSReport : UserPagebase
{
    public string titleValue = "";
    public string ResID = "";
    public string keyWordValue = "";
    public string UserID = "";
    public int PageSize = 15;
    public string SearchType = "";
    public string NodeID = "";
    public string DefaultConStr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        keyWordValue = "WGJKHTS";
        if (Request.QueryString["SearchType"] != null)
        {
            SearchType = Request.QueryString["SearchType"].ToString();
        }
        if (Request.QueryString["NodeID"] != null)
        {
            NodeID = Request.QueryString["NodeID"].ToString();
        }
        if (CurrentUser.DepartmentName == "销售部")
        {
            DefaultConStr = " and C3_551182758185='" + CurrentUser.Name + "'";
        }
        else if (CurrentUser.DepartmentName == "市场部"||CurrentUser.DepartmentName == "销售部经理"||CurrentUser.DepartmentName == "总经理")
        {
            DefaultConStr = " and 1=1 ";   
        }
        else
        {
            DefaultConStr = " and 1=2";
        }

    }
}