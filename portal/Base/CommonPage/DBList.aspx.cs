using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_DBList : UserPagebase
{
    public string titleValue = "";
    public string ResID = "";
    public string keyWordValue = "";
    public string UserID = "";
    public int PageSize = 20;
    public string SearchType = "";
    public string NodeID = "";
    public string sql = "";
    public string SortField = "id";
    public string SortBy = "desc";
    public string keywordParameters = "";
    protected void Page_Load(object sender, System.EventArgs e)
    {
        LoadScript("");
        UserID = CurrentUser.ID;
        if (Request.QueryString["SearchType"] != null)
        {
            SearchType = Request.QueryString["SearchType"].ToString();
        }
        if (Request.QueryString["NodeID"] != null)
        {
            NodeID = Request.QueryString["NodeID"].ToString();
        }
        keywordParameters = Request.QueryString["title"];
        if (keywordParameters == "DBSY")//代办
        {
            titleValue = "我的任务";
            sql = "select id,FLOWNAME 流程,MAINFIELDVALUE 主题,CREATORNAME 来源,CREATETIME 时间,flowinstid 流程RecID,id 流程ID from VIEW_WF_RECEIVEFILES  WHERE EMPCODE='" + UserID + "'  ";
            SortField = "时间";
            SortBy = "desc";
        }
        else if (keywordParameters == "WFQDRW")//我发起的任务
        {
            titleValue = "已发事宜";
            sql = "SELECT a.flowinstid id,a.flowname 流程,a.mainfieldvalue 主题,b.empname 来源,a.CREATETIME 时间,a.flowinstid 流程RecID, ";
            sql += "b.id 流程ID FROM VIEW_WF_START a  left join  WF_USERTASK b  on a.usertaskid=b.id and a.EMPCODE=b.EMPCODE  WHERE a.EMPCODE='" + UserID + "'";
            SortField = "时间";
            SortBy = "desc";
        }
        else if (keywordParameters == "WCLDRW")//我处理的任务
        {
            titleValue = "已办事宜";
            sql = "SELECT a.ID,a.flowname 流程,a.mainfieldvalue 主题,b.empname 来源,a.CREATETIME 时间,a.flowinstid 流程RecID,b.id 流程ID ";
            sql += "FROM VIEW_WF_ASSOCIATE   a  left join WF_USERTASK b  on a.taskid=b.taskid and a.EMPCODE=b.EMPCODE  WHERE a.EMPCODE='" + UserID + "' ";
            SortField = "时间";
            SortBy = "desc";
        } 
    } 
}
