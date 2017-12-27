using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Sample_AddorEditZMJS : UserPagebase
{
    public string ResID = "529328125323";//渠道兑换记录ID
    public string RecID = "";  //记录ID
    public string UserID = "";//当前用户账号
    public string UserName = "";//当前登录用户姓名
    public long NodeID = 0;
    public string SearchType = "";
    public string IsUpdate = "false";
    public string IsDelete = "false";
    public string keyWordValue = "";
    protected int PageSize = 10;
    public string YHM = "";
    public string SKR = "";
    public string SKZH = "";
    public string QD = "";
    public string BH = "";
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
        if (Request["用户名"] != null)
        {
            YHM = Request["用户名"].ToString();
        }
        if (Request["收款人"] != null)
        {
            SKR = Request["收款人"].ToString();
        }
        if (Request["收款账户"] != null)
        {
            SKZH = Request["收款账户"].ToString();
        }
        if (Request["渠道"] != null)
        {
            QD = Request["渠道"].ToString();
        }
        if (Request["编号"] != null)
        {
            BH = Request["编号"].ToString();
        }
    }
}