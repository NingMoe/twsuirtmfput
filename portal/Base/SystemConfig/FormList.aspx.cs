using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using WebServices;

public partial class Base_Config_FormList : UserPagebase
{
    public string titleValue = "";
    public string ResID = "437496952875";
    //public string keyWordValue = "";
    public string UserID = "";
    public int PageSize = 15;
    public int PageSize2 = 10;
    public string SearchType = "";
    public string NodeID = "";
    public string QuertTZKey1 = "";
    public string QuertTZKey2 = "";
    public string QuertTZKey3 = "";


    public string MasterTableAssociationstr = "";

   // string QuertTZ = "";
    DataTable TableList = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        UserID = CurrentUser.ID;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request.QueryString["SearchType"] != null)
        {
            SearchType = Request.QueryString["SearchType"].ToString();
        }
        if (Request.QueryString["NodeID"] != null)
        {
            NodeID = Request.QueryString["NodeID"].ToString();
        } 
        
        WebServices.Services Resource = new WebServices.Services();
        ResID = Resource.GetResourceIDByTableName("SysSettings");


        string strSql = "  select RT_TAB1_RESID ResID,'ListConfig' keyWordValue ,RT_TAB2_RESID ChildResID,Res.NAME ChildResName,(D1.CD_DISPNAME+'='+D2.CD_DISPNAME) 主子表关联字段 FROM [CMS_RELATED_TABLE] R " +
                     " Join CMS_RESOURCE  Res on R.RT_TAB2_RESID=Res.ID" +
                     "  join CMS_TABLE_DEFINE D1 on D1.CD_RESID=R.RT_TAB1_RESID and D1.CD_COLNAME=R.RT_TAB1_COLNAME " +
                     "  join CMS_TABLE_DEFINE D2 on D2.CD_RESID=R.RT_TAB2_RESID and D2.CD_COLNAME=R.RT_TAB2_COLNAME  " +
                     "   where RT_TAB1_RESID=" + ResID + " order by RT_SHOWORDER";
        string[] changePassWord = Common.getChangePassWord();
        DataTable TableList = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        RepTabList.DataSource = TableList;
        RepTabList.DataBind();
         
    } 
}