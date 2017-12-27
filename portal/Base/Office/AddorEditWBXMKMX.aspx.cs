using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Office_AddorEditWBXMKMX : UserPagebase
{
    public string ResID = "531326859155";//外包项目款明细
    public string RecID = "";  //记录ID
    public string UserID = "";//当前用户账号
    public string UserName = "";//当前登录用户姓名
    public long NodeID = 0;
    public string SearchType = "";
    public string IsUpdate = "false";
    public string IsDelete = "false";
    public string keyWordValue = "";
    public string GYSFKBH = "";
    public string FKSQSJ = "";
    public string FKJE = "";
    protected int PageSize = 10;
    DataTable TableList = new DataTable();
    public string Time = DateTime.Now.ToString("yyyy-MM-dd");
    WebServices.Services Resource = new WebServices.Services();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (Request["NodeID"] != null)
        {
            NodeID = Convert.ToInt64(Request["NodeID"]);
        }
        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"].ToString();
        }
        if (Request["keyWordValue"] != null)
        {
            keyWordValue = Request["keyWordValue"].ToString();
        }
        if (Request["SearchType"] != null)
        {
            SearchType = Request["SearchType"].ToString();
        }
        if (Request["IsUpdate"] != null)
        {
            IsUpdate = Request["IsUpdate"].ToString();
        }
          if (Request["供应商付款明细表编号"] != null)
        {
            GYSFKBH = Request["供应商付款明细表编号"].ToString();
        }
          if (Request["付款申请时间"] != null)
        {
            FKSQSJ = Request["付款申请时间"].ToString();
        }
          if (Request["付款金额"] != null)
        {
            FKJE = Request["付款金额"].ToString();
        }
      
    }
}