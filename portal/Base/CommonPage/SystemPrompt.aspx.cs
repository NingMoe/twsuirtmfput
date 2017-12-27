using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Base_CommonPage_SystemPrompt : UserPagebase
{
    public string UserID = "";
    public string UserName = "";
    public string sql = "";
    public string keywordParameters = "";
    WebServices.Services Resource = new WebServices.Services();
    public string Time = DateTime.Now.ToString("yyyy-MM-dd");
    public string SortField = "id";
    public string SortBy = "desc";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        keywordParameters = Request.QueryString["key"];
        if (keywordParameters == "XTTS")
        {
            //待办事宜
            string[] changePassWord = Common.getChangePassWord();
            DataTable sql = Resource.SelectData(" select id,FLOWNAME 流程,MAINFIELDVALUE 主题,CREATORNAME 来源,CREATETIME 时间,flowinstid 流程RecID,id 流程ID from VIEW_WF_RECEIVEFILES  WHERE EMPCODE='" + UserID + "' ", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            DataView dv_DBSYtable = sql.DefaultView;
            dv_DBSYtable.Sort = " 时间 Desc";
            DataTable dt2_DBSYtable = dv_DBSYtable.ToTable();
            RepeaterDBLX.DataSource = dt2_DBSYtable;
            RepeaterDBLX.DataBind(); 
        }

    }
  
}