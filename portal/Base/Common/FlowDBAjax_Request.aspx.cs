using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;
using System.Data;

public partial class FlowDBAjax_Request : UserPagebase
{
    string UserID = "";
    string UserName = "";
    UserInfo oEmployee = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Services Resource = new Services();
        oEmployee = this.CurrentUser;
        UserID = oEmployee.ID;
        UserName = oEmployee.Name;
        string json = "";
        string typeValue = Request["typeValue"];

        if (typeValue == "GetDataDBList")
        {
            json = GetDataDBList();
        }
        Response.Write(json);
    }

    protected string GetDataDBList()
    {
        string Condition = "";//Request["Condition"].ToString();
        string KeyWord = Request["keyWordValue"].ToString();
        int PageSize = Convert.ToInt32(Request["rows"]);
        int PageNumber = Convert.ToInt32(Request["page"]);
        string SortField = "";
        string SortBy = "";
        if (Request["Condition"] != null)
        {
            Condition = Request["Condition"].ToString();
        }
        if (Request["SortField"] != null)
        {
            SortField = Request["SortField"].ToString();
        }
        if (Request["SortBy"] != null)
        {
            SortBy = Request["SortBy"].ToString();
        }
        string sqlData = "";
        if (KeyWord == "DBSY")//代办
        {
            sqlData = "select id,FLOWNAME 流程,MAINFIELDVALUE 主题,CREATORNAME 来源,CREATETIME 时间,flowinstid 流程RecID,id 流程ID from VIEW_WF_RECEIVEFILES  WHERE EMPCODE='" + UserID + "'";
        }
        if (KeyWord == "WFQDRW")//我发起的任务
        {
            sqlData = "SELECT a.flowinstid id,a.flowname 流程,a.mainfieldvalue 主题,b.empname 来源,a.CREATETIME 时间,a.flowinstid 流程RecID, ";
            sqlData += "b.id 流程ID FROM VIEW_WF_START a  left join  WF_USERTASK b  on a.usertaskid=b.id and a.EMPCODE=b.EMPCODE  WHERE a.EMPCODE='" + UserID + "'";
        }
       if (KeyWord == "WCLDRW")//我处理的任务
        {
            sqlData = "SELECT a.ID,a.flowname 流程,a.mainfieldvalue 主题,b.empname 来源,a.CREATETIME 时间,a.flowinstid 流程RecID,b.id 流程ID ";
            sqlData += "FROM VIEW_WF_ASSOCIATE   a  left join WF_USERTASK b  on a.taskid=b.taskid and a.EMPCODE=b.EMPCODE  WHERE a.EMPCODE='" + UserID + "' ";
        }

        string sql = "select * from (" + sqlData + ") as c  where 1=1 " + Condition;
        string OrderBy = "";//order  by
        if (SortField != "" && SortBy != "")
        {
            OrderBy = " " + SortField + " " + SortBy;
        }
        if (OrderBy == "") { OrderBy = " id desc "; }
        WebServices.Services services = new WebServices.Services();
        DataTable dt = GetDataListPage(sql, PageNumber, PageSize, OrderBy).Tables[0];//'services.SelectData(sql).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        // //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string[] changePassWord = Common.getChangePassWord();
        string dtCount = services.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0].Rows.Count.ToString();
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return "{\"total\":" + dtCount + ",\"rows\":" + str + "}";
    }

    protected DataSet GetDataListPage(string sql, int PageIndex, int PageSize, string OrderBy)
    {
        string sqldata = "";
        sqldata = " select top " + PageSize.ToString();
        sqldata += " * from (" + sql + ") T where id not in (select top " + ((PageIndex - 1) * PageSize).ToString();
        sqldata += " id from (" + sql + ") W order by " + OrderBy + ") order by " + OrderBy;
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        return Resource.SelectData(sqldata, changePassWord[0], changePassWord[1], changePassWord[2]);
    }

}