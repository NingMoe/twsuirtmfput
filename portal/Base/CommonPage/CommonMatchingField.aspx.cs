using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonPage_CommonMatchingField : UserPagebase
{
    
    protected string UserID = "";
    public string BaseKeyWordValue = "";
    public string BaseResid = "";
    public string ReportKey = "";
    public string SearchTitle = "";
    public string IsDynamicallyCreatTableHead = "";
    protected string OptionModelStr = "";
    protected string ReadExcelName = "";
    protected string UserTableName = "";
    protected string SaveTableName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        BaseKeyWordValue = Request["BaseKeyWordValue"] == null ? "" : Request["BaseKeyWordValue"];
        ReportKey = Request["argReportKey"] == null ? "" : Request["argReportKey"];
        BaseResid = Request["BaseResid"] == null ? "" : Request["BaseResid"];
        SearchTitle = Request["SearchTitle"] == null ? "" : Request["SearchTitle"];
        ReadExcelName = Request["ReadExcelName"] == null ? "" : Request["ReadExcelName"];
        string Sql = Request["Sql"] == null ? "" : Request["Sql"];
        string ORDER = Request["ORDER"] == null ? "" : Request["ORDER"];

        UserTableName = (Request["UserTableName"] == null ? "" : Request["UserTableName"]);
        SaveTableName = (Request["SaveTableName"] == null ? "" : Request["SaveTableName"]);

        GetExcelHelperOptionModel OptionModel = new GetExcelHelperOptionModel();
        OptionModelStr = Newtonsoft.Json.JsonConvert.SerializeObject(OptionModel);

    }
}