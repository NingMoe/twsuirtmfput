using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_EditResEmpty : UserPagebase
{ 
    protected string ResID = "";
    protected string RecID = "";
    protected string UserID = "";
    protected string NodeID = "";
    protected string gridID = "";
    DataTable TableList = new DataTable();
    WebServices.Services Resource = new WebServices.Services();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        UserID = CurrentUser.ID;
        if (Request["RecID"] != null) ResID = Request["RecID"];
    }
}