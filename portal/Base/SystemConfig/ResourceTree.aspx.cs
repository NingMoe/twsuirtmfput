using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResourceTree : UserPagebase 
{
    protected string ResID = "";
    protected string PID = "";
    protected int IsShowDefaultMenu = 1;
    protected string strUrl = "";
    protected string FromUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["ResID"] != null) ResID = Request["ResID"];
        if (Request["PID"] != null) PID = Request["PID"];
        if (Request["FromUrl"] != null) FromUrl = Request["FromUrl"];
        if (Request["IsShowDefaultMenu"] != null) IsShowDefaultMenu = Convert.ToInt32(Request["IsShowDefaultMenu"]);
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }

        strUrl =WebUtilities.ApplicationPath + "Base/CommonHandler/ResourceTreeData.ashx?ResID=" + ResID + "&PID=" + PID + "&IsShowDefaultMenu=" + IsShowDefaultMenu;
    }
}