using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;
using NetReusables;

public partial class Base_FormChildrenList : UserPagebase
{
    public string titleValue = "";
    public string recid = ""; 
    public string ResID = "";
    public string keyWordValue = "";
    public bool IsChild = true;
    public string UserID = "";
    public string UserName = "";
    public int PageSize = 20;
    public string SearchType = "";
    public string NodeID = "";  
    public string Time = DateTime.Now.ToString("yyyy-MM-dd"); 
    protected string ParentResID = "";
    protected string ParentRecID = "";
    protected Boolean IsAddRights = true;
    protected string strParams = "";
    protected bool IsSelected = false;
    protected string MenuResID = "";
    protected UserInfo oUserInfo = new UserInfo();
    public string GetDataType = ""; 
    public string ROW_NUMBER_ORDER = "";

    //主表数据初始化筛选条件
    public string UserDefinedSql = "";
    protected void Page_Load(object sender, System.EventArgs e)
    {
        recid = NetReusables.TimeId.GetCurrentMilliseconds().ToString();

        LoadScript("");
        keyWordValue = Request.QueryString["key"];

        // 通过参数关键字获取对象
        SysSettings sys = Common.GetSysSettingsByENKey(keyWordValue);
        oUserInfo = CurrentUser;
        if (sys!=null)
        {
            ResID = sys.ResID.ToString();
            if (sys.FilterCondtion != "" && sys.FilterCondtion != null) UserDefinedSql = Server.UrlEncode(sys.FilterCondtion);
            if (Request.QueryString["SearchType"] != null) SearchType = Request.QueryString["SearchType"].ToString();
            if (Request.QueryString["NodeID"] != null) NodeID = Request.QueryString["NodeID"].ToString();
            if (Request.QueryString["MenuResID"] != null) MenuResID = Request.QueryString["MenuResID"].ToString();

            if (Request.QueryString["ParentResID"] != null)
            {
                ParentResID = Request.QueryString["ParentResID"].ToString();
            }
            if (Request.QueryString["ParentRecID"] != null)
            {
                ParentRecID = Request.QueryString["ParentRecID"].ToString();
            }
        }
        WebServices.Services Resource = new WebServices.Services();


        DataTable dt = new DataTable();
        if (CurrentUser.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
        {
            dt = Resource.GetAllPortalChildResourceList(keyWordValue);
        }
        else
        {
            dt = Resource.GetPortalChildResourceList(CurrentUser.ID, keyWordValue);
        }
        if (dt.Rows.Count == 0)
        {
            IsChild = false;
        }
        RepTabList.DataSource = dt;
        RepTabList.DataBind();

    }
}