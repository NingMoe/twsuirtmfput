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
        keyWordValue = keywordParameters;
        if (keywordParameters == "DBSY")//代办
        {
            titleValue = "我的任务";
            SortField = "时间";
            SortBy = "desc";
        }
        else if (keywordParameters == "WFQDRW")//我发起的任务
        {
            titleValue = "已发事宜";
            SortField = "时间";
            SortBy = "desc";
        }
        else if (keywordParameters == "WCLDRW")//我处理的任务
        {
            titleValue = "已办事宜";
            SortField = "时间";
            SortBy = "desc";
        }


      
    }
}
