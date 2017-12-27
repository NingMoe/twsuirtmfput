using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_SystemConfig_AddListColumn : UserPagebase
{
    protected string ResID = "";
    protected string PResID = "";
    protected string RelatedResID = "";
    protected string RelatedValue = "";
    protected string RecID = "";
    protected string UserID = "";
    DataTable TableList = new DataTable();
    WebServices.Services Resource = new WebServices.Services();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("add");
        ResID = Request["ResID"];
        if (Request["PResID"] != null) PResID = Request["PResID"];
        if (Request["RelatedResID"] != null) RelatedResID = Request["RelatedResID"];
        if (Request["RelatedValue"] != null) RelatedValue = Request["RelatedValue"];  //关联字段值
        UserID = CurrentUser.ID;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request["RecID"] != null) RecID = Request["RecID"];
        BindData();
    }

    void BindData()
    {
        string strSql = "select T.*,(Case when IsNull(S.ID,0)=0 then CD_DISPNAME else S.ShowColumnName end) CD_ShowName,(Case when IsNull(S.ID,0)=0 then 0 else 1 end) IsBe from (SELECT [CD_ID],[CD_RESID],[CD_COLNAME],[CD_DISPNAME],[CS_SHOW_ORDER],(case CD_Type when 1 then 'nvarchar' when 2 then 'float' when 3 then 'int' when 4 then 'datetime'  when 5 then 'ntext'  when 6 then 'image'  when 7 then 'money' when 8 then 'datetime' when 9 then 'bit' when 10 then 'ntext'  end) DataType FROM [CMS_TABLE_DEFINE] D join  [CMS_TABLE_SHOW] S on D.CD_COLNAME=S.CS_COLNAME and D.CD_RESID=S.CS_RESID where CD_RESID  in (select (case when IsNull(RES_USE_PARENTSHOW,0)=1 then PID else ID end) from CMS_RESOURCE where id=" + RelatedResID.Trim() + ") and CD_Type<>6) T  left join (select ColumnName,ShowColumnName,ID from ResourceColumn where KeyWord='" + RelatedValue.Trim() + "') S on S.ColumnName=T.CD_DISPNAME  order by CS_SHOW_ORDER ";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        dlCol.DataSource = dt;
        dlCol.DataBind();
    }
}