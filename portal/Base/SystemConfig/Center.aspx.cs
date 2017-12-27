using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Center : UserPagebase
{
    protected string ResID = "";
    protected string PID = "";
    protected string NotShowDes = "";
    protected string KeyWord = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        LoadScript("");
        if (Request["ResID"] != null) ResID = Request["ResID"];
        if (Request["PID"] != null) PID = Request["PID"];
        if (Request["KeyWord"] != null)
        {
            KeyWord = Request["KeyWord"];
            WebServices.Services Resource = new WebServices.Services();
            ResID = Resource.GetResourceIDByDescription(KeyWord);
        }

        if (Request["NotShowDes"] != null) NotShowDes = Request["NotShowDes"];
    }
}