using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Center : UserPagebase 
{
    protected string ObjectID = "";
    protected string GainerType = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        if (Request["ObjectID"] != null) ObjectID = Request["ObjectID"];
        if (Request["GainerType"] != null) GainerType = Request["GainerType"];
        if (CurrentUser.DepartmentName != "系统管理员")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
    }
}