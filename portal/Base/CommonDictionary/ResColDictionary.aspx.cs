using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonDictionary_ResColDictionary : UserPagebase
{ 
    public string ResID = "437496952875";
    public string Key = "";
    public string keyWordValue = "";
    public string UserID = "";
    public string UserName = ""; 
    public string Params = "";
    public string UserDefinedSql = "";
    public bool IsmultiSelect = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (Request.QueryString["ResID"] != null)
        {
            ResID = Request.QueryString["ResID"].ToString();
        }

        if (Request["key"] != null)
        {
            Key = Request.QueryString["key"].ToString();
            if (ResID.Trim() == "")
            {
                ResID = Common.GetResIDByENKekWord(Key);
            }
        }
        if (Request.QueryString["IsmultiSelect"] != null) IsmultiSelect = Convert.ToBoolean(Convert.ToInt32(Request.QueryString["IsmultiSelect"]));
        if (Request["Params"] != null) Params = Request["Params"].ToString();
        UserID = CurrentUser.ID;
        keyWordValue = "ResColDictionary";
        UserDefinedSql = ResID; //"SELECT [CD_ID],[CD_RESID] ,[CD_COLNAME] 内部字段名,[CD_DISPNAME] 显示字段名,CS_SHOW_ORDER 排序 FROM [CMS_TABLE_DEFINE] D join  [CMS_TABLE_SHOW] S on D.CD_COLNAME=S.CS_COLNAME and D.CD_RESID=S.CS_RESID where CD_RESID in (select (case when IsNull(RES_USE_PARENTSHOW,0)=1 then PID else ID end) from CMS_RESOURCE where id='" + ResID + "' ) order by CS_SHOW_ORDER";

       
    } 
}