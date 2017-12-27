using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemRights_UserTree : UserPagebase
{
    protected int IsLoadUser = 1;
    protected string FromUrl = "Center.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["IsLoadUser"] != null) IsLoadUser = Convert.ToInt32(Request["IsLoadUser"]);
        string MenuResID = "";
        if (Request["MenuResID"] != null) MenuResID = Request["MenuResID"];
        if (Request["FromUrl"] != null) FromUrl = Request["FromUrl"] + "&MenuResID=" + MenuResID;
        if (CurrentUser.DepartmentName != "系统管理员")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
    }
}