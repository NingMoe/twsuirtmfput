using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetReusables;

public partial class Base_Config_ResRelatedColDictionary : UserPagebase
{
    public string PResID = "0";//主资源ID
    public string ResID = "0";//子资源ID  
    public string PResName = "0";//主资源名称
    public string ResName = "0";//子资源名称 
    public string Params = "";
    public bool IsmultiSelect = false;
    public DataTable dtParent = new DataTable();
    public DataTable dtChild = new DataTable();

    WebServices.Services Service = new WebServices.Services();

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request.QueryString["ResID"] != null)
        {
            ResID = Request.QueryString["ResID"];
        }
        if (Request.QueryString["PResID"] != null)
        {
            PResID = Request.QueryString["PResID"];
        }
        if (Request["Params"] != null) Params = Request["Params"].ToString();
    
        if (Request.QueryString["IsmultiSelect"] != null) IsmultiSelect = Convert.ToBoolean(Convert.ToInt32(Request.QueryString["IsmultiSelect"]));

        string strSql = "select D.CD_RESID,D.CD_COLNAME,D.CD_DISPNAME,S.CS_SHOW_WIDTH from CMS_TABLE_DEFINE D join CMS_TABLE_SHOW S on D.CD_RESID = S.CS_RESID and D.CD_COLNAME = S.CS_COLNAME where CD_RESID = {0} and CS_SHOW_ENABLE = 1 order by CS_SHOW_ORDER ";

        SysSettings Psys = Common.GetSysSettingsByResID(PResID);
        PResName = Psys.ShowTitle;
        SysSettings Csys = Common.GetSysSettingsByResID(ResID);
        ResName = Csys.ShowTitle;
        string[] changePassWord = Common.getChangePassWord();
        //获取主资源字段列表
        dtParent = Service.Query(string.Format(strSql, PResID), changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        //获取子资源字段列表
        dtChild = Service.Query(string.Format(strSql, ResID), changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];



    } 
 
}