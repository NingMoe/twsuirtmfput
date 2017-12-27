using NetReusables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_Organization_SystemEmployeeEdit : UserPagebase
{
    protected string ResID = "";
    protected string RecID = "";
    protected string KeyWord = "";

    protected string SystemResID = "1300";
    protected string DeptID = "0";
    protected string DeptName = "";
    protected string UserID = "";
    protected string SaveType = "0";
    protected string SaveID = "";
    protected string IsSysRecID = "";
    protected string gridID = ""; 
    protected long NodeID = 0;
    protected string CurrentUserID = "";
    protected string ActionType = "";
    protected string ChildGridID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserID = CurrentUser.ID;

        ActionType = "XZGL";

        ResID = Request["ResID"] == null ? "" : Request["ResID"].ToString();
        RecID = Request["RecID"] == null ? "" : Request["RecID"].ToString();
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
            Dictionary<string, object> QueryValueDic = new Dictionary<string, object>();
            QueryValueDic.Add("DeptID", "");
            QueryValueDic.Add("DeptName", "");
            QueryValueDic.Add("IsSysRecID", "");

            if (Common.GetOneRowValueBySQL(" SELECT * FROM (SELECT a.id, b.NAME DeptName, a.HOST_ID DeptID, 1 IsSysRecID  FROM dbo.CMS_EMPLOYEE AS a INNER JOIN dbo.CMS_DEPARTMENT AS b  ON a.HOST_ID = b.id UNION SELECT id, Deparment DeptName, DeparmentID DeptID, 0  FROM dbo.EmployeeInfo) u WHERE id =" + RecID, ref QueryValueDic))
            {
                DeptID = QueryValueDic["DeptID"].ToString();
                DeptName = QueryValueDic["DeptName"].ToString();
                IsSysRecID = QueryValueDic["IsSysRecID"].ToString();
            }

        }

        SaveID = TimeId.CurrentMilliseconds(30).ToString();

    }
}