using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using NetReusables;
using WebServices;

public partial class Base_ReportFormList : UserPagebase
{
    public string titleValue = "";
  
    public string ResID = "";
    public string keyWordValue = "";

    public string UserID = "";
    public string UserName = "";
    public int PageSize = 20;
    public string SearchType = "";
    public string NodeID = "";
    public string DefaultCondition = "";
    protected string strOperation = "";
    public string Time = DateTime.Now.ToString("yyyy-MM-dd");
    protected string strCondition = "";
    protected string ParentResID = "";
    protected string ParentRecID = "";
    protected Boolean IsAddRights = true;
    protected string strParams ="";
    protected bool IsSelected = false;
    protected string MenuResID = "";
    protected UserInfo oUserInfo = new UserInfo();
    public string GetDataType = "";   
    public string ROW_NUMBER_ORDER = "";

    //列表数据，初始化过滤条件
    public string UserDefinedSql = "";
    protected void Page_Load(object sender, System.EventArgs e)
    {
        LoadScript("");
        keyWordValue = Request.QueryString["key"];

        // 通过参数关键字获取对象
        SysSettings sys = Common.GetSysSettingsByENKey(keyWordValue); 
        
        oUserInfo = CurrentUser;
        ResID = sys.ResID.ToString();

        if (Request.QueryString["TypeResID"] != null) ResID = Request.QueryString["TypeResID"].ToString();
        if (sys.FilterCondtion != "" && sys.FilterCondtion!=null) UserDefinedSql = Server.UrlEncode(sys.FilterCondtion);
        if (Request.QueryString["SearchType"] != null) SearchType = Request.QueryString["SearchType"].ToString(); 
        if (Request.QueryString["NodeID"] != null)   NodeID = Request.QueryString["NodeID"].ToString();
        if (Request.QueryString["MenuResID"] != null) MenuResID = Request.QueryString["MenuResID"].ToString();
        
        if (Request.QueryString["ParentResID"] != null)
        {
            ParentResID = Request.QueryString["ParentResID"].ToString();
        }
        if (Request.QueryString["ParentRecID"] != null)
        {
            ParentRecID = Request.QueryString["ParentRecID"].ToString();
        }        

        if (!IsPostBack) return;
       
    } 
}
