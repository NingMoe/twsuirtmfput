using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Finance_AddorEditSKJL : UserPagebase
{
    public string ResID = "401290354058";//收款记录表资源ID
    public string RecID = "";  //记录ID
    public string UserID = "";//当前用户账号
    public string UserName = "";//当前登录用户姓名
    public long NodeID = 0;
    public string SearchType = "";
    public string IsUpdate = "false";
    public string IsDelete = "false";
    public string XMBH = "";
    public string XMMC = "";
    public string SKBH = "";
    public string SK = "";
    public string FPSQDH = ""; //发票申请单号
    public string SKRQ_KSZGYH = "";
    public string SKRQ_XNGQ = "";
    public string SKRQ_KWZGYH = "";
    public string SKRQ_KRMSYH = "";
    public string SKRQ_ZFBKR126 = "";
    public string SKRQ_ZFBQQ1126 = "";
    public string SKRQ_ZFBQQ126 = "";
    public string SKRQ_KSNSYH = "";
    public string keyWordValue = "";
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
        if (Request["项目编号"] != null)
        {
            XMBH = Request["项目编号"].ToString();
        }
        if (Request["项目名称"] != null)
        {
            XMMC = Request["项目名称"].ToString();
        }
         if (Request["收款编号"] != null)
        {
            SKBH = Request["收款编号"].ToString();
        }
        if (Request["收款"] != null)
        {
            SK = Request["收款"].ToString();
        }
        if (Request["收款日期_库思中国银行"] != null)
        {
            SKRQ_KSZGYH = Request["收款日期_库思中国银行"].ToString();
        }
        if (Request["收款日期_虚拟股权"] != null)
        {
            SKRQ_XNGQ = Request["收款日期_虚拟股权"].ToString();
        }
        if (Request["收款日期_库威中国银行"] != null)
        {
            SKRQ_KWZGYH = Request["收款日期_库威中国银行"].ToString();
        }
        if (Request["收款日期_库润民生银行"] != null)
        {
            SKRQ_KRMSYH = Request["收款日期_库润民生银行"].ToString();
        }
        if (Request["收款日期_库润126"] != null)
        {
            SKRQ_ZFBKR126 = Request["收款日期_库润126"].ToString();
        }
        if (Request["收款日期_QQ126"] != null)
        {
            SKRQ_ZFBQQ126 = Request["收款日期_QQ126"].ToString();
        }
        if (Request["收款日期_QQ1126"] != null)
        {
            SKRQ_ZFBQQ1126 = Request["收款日期_QQ1126"].ToString();
        }
        if (Request["收款日期_库思农商银行"] != null)
        {
            SKRQ_KSNSYH = Request["收款日期_库思农商银行"].ToString();
        }
        if (Request["发票申请单号"] != null)
        {
            FPSQDH = Request["发票申请单号"].ToString();
        }

   
    }
}