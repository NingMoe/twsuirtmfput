using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysSettingsDictionary : UserPagebase
{
    protected string ResID = "";
    protected string PID = "";
    protected string NotShowDes = "";
    protected string KeyWord = "";
    protected string strUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Request["ResID"] != null) ResID = Request["ResID"];
        if (Request["PID"] != null) PID = Request["PID"];
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request["KeyWord"] != null)
        {
            KeyWord = Request["KeyWord"];
            WebServices.Services Resource = new WebServices.Services();
            ResID = Resource.GetResourceIDByDescription(KeyWord);
        }

        if (Request["NotShowDes"] != null) NotShowDes = Request["NotShowDes"];

        strUrl = WebUtilities.ApplicationPath + "Base/SystemConfig/ResourceTree.aspx?ResID=&PID=-1&IsShowDefaultMenu=0&FromUrl=DictionaryList.aspx";
    }
}