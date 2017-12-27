using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_DataKeyFormList : UserPagebase
{
    public string titleValue = "";
    public string ResID = "536862532389";
    public string keyWordValue = "";
    public string UserID = "";
    public string UserName = "";
    public int PageSize = 15;
    public string SearchType = "";
    public string NodeID = "";
    protected string FieldValueStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request.QueryString["ResID"] != null)
        {
            ResID = Request.QueryString["ResID"].ToString();
        }
        if (Request.QueryString["NodeID"] != null)
        {
            NodeID = Request.QueryString["NodeID"].ToString();
        }
        UserID = CurrentUser.ID;
        keyWordValue = "";

    }
}