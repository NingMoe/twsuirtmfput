using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Organization_ChangeDep : UserPagebase
{
    protected string ResID = "";
    protected string RecID = "";
    protected string KeyWord = "";

    protected string DeptID = "0";
    protected string DeptName = "";
    protected string UserID = "";
    protected string NeedUserIDStr = "";
    protected string SaveType = "0";
    protected string SaveID = "";
    protected string OpenDiv = "";
    protected string GridID = "";
    protected long NodeID = 0;
    protected string CurrentUserID = "";
    protected string ActionType = "";
    protected string ChildGridID = "";
    protected string NoSelectDeptID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserID = CurrentUser.ID;
        NeedUserIDStr = "readonly='readonly'";

        ActionType = "XZGL";

        ResID = Request["ResID"] == null ? "" : Request["ResID"].ToString();
        RecID = Request["RecID"] == null ? "" : Request["RecID"].ToString();
        KeyWord = Request["keyWordValue"] == null ? "" : Request["keyWordValue"].ToString();
        NodeID = Request["NodeID"] == null || string.IsNullOrWhiteSpace(Request["NodeID"].ToString()) ? 0 : Convert.ToInt64(Request["NodeID"].ToString());
        ActionType = Request["ActionType"] == null ? "" : Request["ActionType"].ToString();
        OpenDiv = Request["OpenDiv"] == null ? "" : Request["OpenDiv"].ToString();
        DeptID = Request["DeptID"] == null ? "" : Request["DeptID"].ToString();

        NoSelectDeptID = Request["NoSelectDeptID"] == null ? "" : Request["NoSelectDeptID"].ToString();
 
        if (!string.IsNullOrWhiteSpace(DeptID))
            DeptName = Common.GetOneRowValueBySQL("select ID,PID , NAME AS Name ,DEP_TYPE AS [Type]  FROM Cms_Department where ID=" + DeptID, "Name");
        else if (!string.IsNullOrWhiteSpace(RecID))
        {
            WebServices.Services oServices = new WebServices.Services();
            DeptID = Common.GetOneRowValueBySQL("select * FROM " + oServices.GetTableNameByResourceid(ResID) + " where ID=" + RecID, "DeparmentID");
            DeptName = Common.GetOneRowValueBySQL("select ID,PID , NAME AS Name ,DEP_TYPE AS [Type]  FROM Cms_Department where ID=" + DeptID, "Name");
        }

        if (ActionType == "edit")
        {
            SaveType = "1";
        }
    }
}