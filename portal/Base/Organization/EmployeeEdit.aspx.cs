using NetReusables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_Organization_EmployeeEdit : UserPagebase
{
    protected string ResID = "";
    protected string XZResID = "524056303313";
    protected string XZRecID = "";
    protected string RecID = "";
    protected string KeyWord = "";
    protected string DeptID = "0";
    protected string DeptName = "";
    protected string UserID = "";
    protected string SaveType = "0";
    protected string SaveID = "";
    protected string gridID = ""; 
    protected long NodeID = 0;
    protected string CurrentUserID = "";
    protected string ActionType = "";
    protected string ChildGridID = "";
    protected string SearchType = "";
    public string UserDefinedSql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserID = CurrentUser.ID;
        Services Resource = new Services();
        ActionType = "XZGL";

        ResID = Request["ResID"] == null ? "" : Request["ResID"].ToString();
        RecID = Request["RecID"] == null ? "" : Request["RecID"].ToString();
        if (Request["SearchType"] != null) SearchType = Request["SearchType"];
        KeyWord = Request["keyWordValue"] == null ? "" : Request["keyWordValue"].ToString();
        NodeID = Request["NodeID"] == null || string.IsNullOrWhiteSpace(Request["NodeID"].ToString()) ? 0 : Convert.ToInt64(Request["NodeID"].ToString());
        ActionType = Request["ActionType"] == null ? "" : Request["ActionType"].ToString();
        // OpenDiv = Request["OpenDiv"] == null ? "" : Request["OpenDiv"].ToString();
        gridID = Request["gridID"] == null ? "" : Request["gridID"].ToString();
        DeptID = Request["DeptID"] == null ? "" : Request["DeptID"].ToString();

        if (!string.IsNullOrWhiteSpace(DeptID))
            DeptName = Common.GetOneRowValueBySQL("select ID,PID , NAME AS Name ,DEP_TYPE AS [Type]  FROM Cms_Department where ID=" + DeptID, "Name");
        else if (!string.IsNullOrWhiteSpace(RecID))
        {
            DeptID = Common.GetOneRowValueBySQL("select * FROM " + Resource.GetTableNameByResourceid(ResID) + " where ID=" + RecID, "DeparmentID");
            DeptName = Common.GetOneRowValueBySQL("select ID,PID , NAME AS Name ,DEP_TYPE AS [Type]  FROM Cms_Department where ID=" + DeptID, "Name");
        }

        if (string.IsNullOrWhiteSpace(ActionType))
            SaveID = TimeId.CurrentMilliseconds(30).ToString();

        XZRecID = TimeId.CurrentMilliseconds(30).ToString();
        if (ActionType == "edit" || !string.IsNullOrWhiteSpace(RecID))
        {
            SaveType = "1";
        }
        UserDefinedSql = "1300";//"select a.id, EMP_ID 账号,EMP_NAME 姓名,EMP_HANDPHONE 联系方式 ,[Status], Sex 性别, EMP_EMAIL 邮箱 , HeadShip 职务, a.[HOST_ID],b.NAME  FROM dbo.CMS_EMPLOYEE AS a INNER JOIN dbo.CMS_DEPARTMENT AS b ON a.HOST_ID=b.ID WHERE EMP_ID=";

    }
}