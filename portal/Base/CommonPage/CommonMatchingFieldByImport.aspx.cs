
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_CommonPage_CommonMatchingFieldByImport : UserPagebase
{
    public string keyWordTitle = "";
    protected string UserID = "";
    public string keyWordValue = "";
    public string BaseResid = "";
    public string ReportKey = "";
    public string SearchTitle = "";
    public string IsDynamicallyCreatTableHead = "";
    protected string OptionModelStr = "";
    protected string ReadExcelName = "";
    protected string UserTableName = "";
    protected string SaveTableName = "";

    protected string tip = "";
    protected string optionKeyStr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        keyWordValue = Request["keyWordValue"] == null ? "" : Request["keyWordValue"];
        ReportKey = Request["argReportKey"] == null ? "" : Request["argReportKey"];
        BaseResid = Request["ResID"] == null ? "" : Request["ResID"];
        SearchTitle = Request["SearchTitle"] == null ? "" : Request["SearchTitle"];
        ReadExcelName = Request["ReadExcelName"] == null ? "" : Request["ReadExcelName"];
        string Sql = Request["Sql"] == null ? "" : Request["Sql"];
        string ORDER = Request["ORDER"] == null ? "" : Request["ORDER"];

        UserTableName = (Request["UserTableName"] == null ? "" : Request["UserTableName"]);
        SaveTableName = (Request["SaveTableName"] == null ? "" : Request["SaveTableName"]);

        GetExcelHelperOptionModel OptionModel = new GetExcelHelperOptionModel();
        OptionModelStr = Newtonsoft.Json.JsonConvert.SerializeObject(OptionModel);

        this.UploadFile1.ResID = BaseResid;
        this.UploadFile1.RecID = "";
        this.UploadFile1.UserID = UserID;
        this.UploadFile1.IsNotViewFiles = "1";

        optionKeyStr = GetOptionStrBySql(" SELECT DISTINCT a.ShowTitle OptionName, ( ENKeyWord + ',' + [Value] + ',' + b.RES_TABLE+',' + a.ShowTitle) OptionValue FROM dbo.SysSettings AS a inner JOIN dbo.CMS_RESOURCE AS b ON a.[Value]=b.id ", "OptionValue", "OptionName", "1", "");

        // 通过参数关键字获取对象
        SysSettings sys = Common.GetSysSettingsByENKey(keyWordValue);
         
        if (sys != null)
        {
            if (!string.IsNullOrEmpty(sys.ShowTitle))
            {
                keyWordTitle = sys.ShowTitle;
            }
        }
    }


    public static string GetOptionStrBySql(string argSql, string argValueField, string argShowField, string argHasEmptySelected, string argSelectedValue)
    {
        string OptionStr = "";

        if (argHasEmptySelected == "1")
            OptionStr = "<option value='' optionText='' ></option>";

        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        DataTable vdt = Resource.Query(argSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        foreach (DataRow vDr in vdt.Rows)
        {
            if (!string.IsNullOrWhiteSpace(argSelectedValue)
                && argSelectedValue == vDr["argValueField"].ToString())
            {
                OptionStr += "<option  selected=\"selected\" value='" + vDr[argValueField].ToString() + "' optionText='" + vDr[argShowField].ToString() + "' >" + vDr[argShowField].ToString() + "</option>";
            }
            else
            {
                OptionStr += "<option value='" + vDr[argValueField].ToString() + "' optionText='" + vDr[argShowField].ToString() + "' >" + vDr[argShowField].ToString() + "</option>";
            }
        }
        return OptionStr;
    }
}