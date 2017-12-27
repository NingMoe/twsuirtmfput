using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_SystemRights_List : UserPagebase 
{
    WebServices.Services Resource = new WebServices.Services();
    protected string ResID = "502194239949";
    protected string RightsResID = "";
    protected string RightsResName = "";
    protected string ObjectID = "";
    protected string GainerType = "";
    protected string UserID = "";
    protected bool IsRowRihgt = false;
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Request["ObjectID"] != null) ObjectID = Request["ObjectID"];
        if (Request["GainerType"] != null) GainerType = Server.UrlDecode(Request["GainerType"]);
        ResID = Resource.GetResourceIDByTableName("SystemRights");
        if (Request["ResID"] != null) RightsResID = Request["ResID"];
        if (Request["ResName"] != null) RightsResName = Server.UrlDecode(Request["ResName"]);
        UserID = this.CurrentUser.ID;
        if (CurrentUser.DepartmentName != "系统管理员")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        BindData();
    }

    protected void BindData()
    {
        string Sql = "[dbo].[RightsSetList] " + RightsResID + ",'" + ObjectID.Trim() + "'";
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.SelectData(Sql, changePassWord[0], changePassWord[1], changePassWord[2]);
        dlRights.DataSource = ds.Tables[0];
        dlRights.DataBind();
        if (ds.Tables.Count > 1)
        {
            dlRights1.DataSource = ds.Tables[1];
            dlRights1.DataBind();

            dlRights2.DataSource = ds.Tables[2];
            dlRights2.DataBind();
            if (ds.Tables[3].Rows.Count > 0) IsRowRihgt = true;
        }
        ds = Resource.SelectData("select ChildNum 子表配置号 ,ShowTitle 显示标题 ,MasterKeyWord 主表关键字 ,ChildKeyWord 子表关键字 ,RSResID 主表资源ID ,ChildResId 子表资源ID , ChildOrderNo 子表排序号 from MasterTableAssociation where MasterKeyWord in (select Res_Comments from CMS_RESOURCE where IsNull(SHOW_ENABLE,0)=1 and ID=" +CommonMethod.FilterSql( RightsResID )+ ")", changePassWord[0], changePassWord[1], changePassWord[2]);
        repChildRes.DataSource = ds.Tables[0];
        repChildRes.DataBind(); 
    }
    protected void repChildRes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataList dl = (DataList)e.Item.FindControl("dlRights_ChildRes");
        DataList dl1 = (DataList)e.Item.FindControl("dlRights_ChildRes1");
        DataList dl2 = (DataList)e.Item.FindControl("dlRights_ChildRes2");
        DataRowView dr = (DataRowView)e.Item.DataItem;
        string ChildKeyWord = dr["子表关键字"].ToString();
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.SelectData("[dbo].[RightsSet_ChildRes] " + RightsResID + ",'" + ChildKeyWord.Trim() + "','" + ObjectID.Trim() + "'", changePassWord[0], changePassWord[1], changePassWord[2]);

        dl.DataSource = ds.Tables[0];
        dl.DataBind(); 
        dl1.DataSource = ds.Tables[1];
        dl1.DataBind(); 
        dl2.DataSource = ds.Tables[2];
        dl2.DataBind();
    }
}