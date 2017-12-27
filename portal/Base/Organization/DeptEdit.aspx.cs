using NetReusables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Organization_DeptEdit : UserPagebase
{
    protected string DeptID = "0";
    protected string DeptName = "0";
    protected long NodeID = 0;
    protected string TableName = "CMS_DEPARTMENT";
    protected int ShowOrder = 0;
    protected string ParentRecID = "";
    protected string UserID = "";
    protected string RecID = "";
    protected string OpenDiv = "";
    protected string ActionType = "";
    protected string MenuID = "";
    protected string FactoryID = "";
    protected string SaveID = "";
    protected string SaveType = "0";
    protected string GridID = "";
    protected string KeyWord = "";
    protected string ChildGridID = "";
    protected string PID = "";
    protected string PIDName = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        KeyWord = Request["keyWordValue"] == null ? "" : Request["keyWordValue"].ToString();
        RecID = Request["RecID"] == null ? "" : Request["RecID"].ToString();
        NodeID = Request["NodeID"] == null || string.IsNullOrWhiteSpace(Request["NodeID"].ToString()) ? 0 : Convert.ToInt64(Request["NodeID"].ToString());
        OpenDiv = Request["OpenDiv"] == null ? "" : Request["OpenDiv"].ToString();
        GridID = Request["GridID"] == null ? "CenterGrid" + KeyWord : Request["GridID"].ToString();
        ChildGridID = Request["ChildGridID"] == null ? "" : Request["ChildGridID"].ToString();

        ActionType = Request["ActionType"] == null ? "" : Request["ActionType"].ToString();
        DeptID = Request["DeptID"] == null ? "" : Request["DeptID"].ToString();
        DeptID = RecID;
        if (string.IsNullOrWhiteSpace(ActionType))
            SaveID = TimeId.CurrentMilliseconds(30).ToString();
        else
            SaveID = DeptID;

        ShowOrder = Common.GetDepartOrder(DeptID, "");

        string SelectSql = "";
        if (ActionType == "Edit")
        {
            SelectSql = "select ID,PID , NAME AS Name ,DEP_TYPE AS [Type]  FROM Cms_Department where ID=(SELECT  TOP(1) PID  FROM Cms_Department  WHERE ID='" + DeptID + "')";
        }
        else
        {
            SelectSql = "select ID,PID , NAME AS Name ,DEP_TYPE AS [Type]  FROM Cms_Department where ID='" + DeptID + "'";
        }

        Dictionary<string, object> QueryValueDic = new Dictionary<string, object>();
        QueryValueDic.Add("ID", "");
        QueryValueDic.Add("Name", "");
        if (Common.GetOneRowValueBySQL(SelectSql, ref QueryValueDic))
        {
            PID = QueryValueDic["ID"].ToString();
            PIDName = QueryValueDic["Name"].ToString();
        }
    }
}