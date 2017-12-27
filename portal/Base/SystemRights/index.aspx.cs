using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : UserPagebase 
{

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        if (CurrentUser.DepartmentName != "系统管理员")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
    }
}