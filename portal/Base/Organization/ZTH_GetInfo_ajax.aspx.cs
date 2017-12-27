using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_Common_ZTH_GetInfo_ajax : UserPagebase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string json = "";
        string typeValue = Request["typeValue"];
        int PageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
        int PageNumber = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);

        string _UserID = CurrentUser.ID;

        switch (typeValue)
        { 
            case "SaveHstListByJArray":
                {
                    string JArray = Request["JArray"] == null ? "" : Request["JArray"].ToString();
                    string SaveTableName = Request["SaveTableName"] == null ? "" : Request["SaveTableName"].ToString();
                    string UserID = CurrentUser.ID;
                    int type = Request["type"] == null ? 0 : Convert.ToInt32(Request["type"].ToString());
                    string UpdateCon = Request["UpdateCon"] == null ? "" : Request["UpdateCon"].ToString();
                    bool CreateNewHst = Request["CreateNewHst"] == null ? false : true;
                    bool SaveSameTable = Request["SaveSameTable"] == null ? true : false;

                    List<Hashtable> hsts = UnionsoftExcelHelper.CommonGetHstListByJArray(JArray, "", SaveTableName, UserID, false, true, true, true);
                    int o= UnionsoftExcelHelper.CommonSaveHstList(hsts,"", SaveTableName, UserID, UpdateCon);
                    json = o.ToString();
                    break;
                }
            case "CommonSelect":
                {
                    string Sql = Request["Sql"] == null ? "" : Request["Sql"].ToString();
                    Services Resource = new Services();
                    string[] changePassWord = Common.getChangePassWord();
                    DataTable dt = Resource.Query(Sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(Common.SplitDataTable(dt, PageNumber, PageSize), timeConverter);
                }
                break;
            case "GetDepartOrder":
                {
                    string DepartID = Request["DepartID"] == null ? "" : Request["DepartID"].ToString();
                    string MyDepartID = Request["MyDepartID"] == null ? "" : Request["MyDepartID"].ToString();
                    json = Common.GetDepartOrder(DepartID, MyDepartID).ToString();
                }
                break;
            case "QueryDepartment":
                {
                    string DeptID = Request["DeptID"] == null ? "" : Request["DeptID"].ToString();
                    json = Common.QueryDepartment(DeptID).ToString();
                }
                break;
            case "GetDataBySystemEmployeeInfo":
                {
                    string argResID = Request["ResID"] == null ? "" : Request["ResID"].ToString();
                    //string UserDefinedSql = "select u.* ,b.EMP_ID,( CASE WHEN b.EMP_ID IS NULL OR b.EMP_ID = '' THEN 0 ELSE 1 end) 是否分配账号 from ( " + UnionsoftExcelHelper.SetBaseSql("", argResID, true) + " ) u left JOIN dbo.CMS_EMPLOYEE AS b ON u.登录账户 = b.EMP_ID where 1=1 ";
                    Services Resource = new Services();
                    string UserDefinedSql = " SELECT * FROM (  select ISNULL(u.ID,b.ID) ID ,u.RESID,b.EMP_ID,( CASE WHEN b.EMP_ID IS NULL OR b.EMP_ID = '' THEN 0 ELSE 1 end) 是否分配账号,( CASE WHEN u.员工编号 IS NULL OR u.员工编号 = '' THEN b.EMP_ID ELSE u.登录账户 end) 登录账户 ,( CASE WHEN u.员工编号 IS NULL OR u.员工编号 = '' THEN b.EMP_NAME ELSE u.员工名称 end) 员工名称,( CASE WHEN u.员工编号 IS NULL OR u.员工编号 = '' THEN b.EMP_EMAIL ELSE u.邮箱 end) 邮箱,( CASE WHEN u.员工编号 IS NULL OR u.员工编号 = '' THEN b.Domain_mobile ELSE u.手机 end) 手机,( CASE WHEN u.员工编号 IS NULL OR u.员工编号 = '' THEN '' ELSE u.员工编号 end) 员工编号,( CASE WHEN u.员工编号 IS NULL OR u.员工编号 = '' THEN b.HOST_ID ELSE u.部门ID end) 部门ID from ( " + Resource.GetSqlByResourceID(argResID,"","") + " ) u full JOIN dbo.CMS_EMPLOYEE AS b ON u.登录账户 = b.EMP_ID ) h where 1=1 ";

                    string SortField = "";
                    string SortBy = "";
                    string Condition = Request["Condition"] == null ? "" : Request["Condition"].ToString();

                    if (!string.IsNullOrEmpty(Request["page"]))
                    {
                        PageNumber = Convert.ToInt32(Request["page"]);
                    }
                    if (Request["sort"] != null && !string.IsNullOrWhiteSpace(Request["sort"]))
                    {
                        SortField = " order by " + Request["sort"].ToString();
                    }

                    if (Request["order"] != null)
                    {
                        SortBy = Request["order"].ToString();
                    }
                    string[] changePassWord = Common.getChangePassWord();
                    DataTable dt = Resource.Query(UserDefinedSql + Condition + SortField + " " + SortBy, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                    if (dt == null) json = "";
                    else
                    {
                        int RowCount = dt.Rows.Count;
                        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
                        string str = Newtonsoft.Json.JsonConvert.SerializeObject(Common.SplitDataTable(dt, PageNumber, PageSize), timeConverter);
                        json = "{\"total\":" + RowCount + ",\"rows\":" + str + "}";

                    }
                }
                break; 
            case "GetCompanyAllDepartment":
                {
                    string CompanyID = Request["CompanyID"] == null ? "" : Request["CompanyID"].ToString();
                    string GetSqlStr = Request["GetSqlStr"] == null ? "" : Request["GetSqlStr"].ToString();
                    List<string> ChildDeparmentID = new List<string>();
                    ChildDeparmentID = OrganizationTree.GetChildDeparmentID(CompanyID);
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(ChildDeparmentID);
                }
                break;
            case "GetOrganizationTree":
                {
                    string IsMultiSelect = Request["IsMultiSelect"] == null ? "" : Request["IsMultiSelect"].ToString();
                    string IsLoadUser = Request["IsLoadUser"] == null ? "" : Request["IsLoadUser"].ToString();
                    string IsSelectCompany = Request["IsSelectCompany"] == null ? "" : Request["IsSelectCompany"].ToString();
                     
                    string argType = Request["argType"] == null ? "" : Request["argType"].ToString();
                    OrganizationTree Deparment = new OrganizationTree();
                    List<OrganizationTree> AllDeparment = new List<OrganizationTree>();
                    //GetDeparmentID = "523551382099";
                    //List<string> ChildDeparmentID = new List<string>();
                    //ChildDeparmentID = OrganizationTree.GetChildDeparmentID(GetDeparmentID);
                    json = OrganizationTree.GetJson(IsLoadUser == "1", argType,out Deparment,out AllDeparment, IsMultiSelect == "1", IsSelectCompany=="1");
                }
                break;
            case "CommonGetData":
                {
                    string argSql = Request["argSql"] == null ? "" : Request["argSql"].ToString();
                    string[] changePassWord = Common.getChangePassWord();
                    DataTable dt = CommonGetInfo.Resource.Query(argSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                    if (dt == null) json = "";
                    else
                    {
                        int RowCount = dt.Rows.Count;
                        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
                        string str = Newtonsoft.Json.JsonConvert.SerializeObject(Common.SplitDataTable(dt, PageNumber, PageSize), timeConverter);
                        json = "{\"total\":" + RowCount + ",\"rows\":" + str + "}";

                    }
                }
                break; 
            case "ExecSql":
                {
                    string Sql = Request["Sql"] == null ? "" : Request["Sql"].ToString();
                    string[] changePassWord = Common.getChangePassWord();
                    json = CommonGetInfo.Resource.ExecuteSql(Sql, changePassWord[0], changePassWord[1], changePassWord[2]).ToString();
                }
                break;
            case "getSortDynamicFieldRecords":
                {
                    string argCondition = Request["argCondition"] == null ? "" : Request["argCondition"].ToString();
                    string argResid = Request["argResid"] == null ? "" : Request["argResid"].ToString();
                    string argRecID = Request["argRecID"] == null ? "" : Request["argRecID"].ToString();

                    json = Common.getSortDynamicFieldRecords(argRecID, argResid, argCondition);
                    break;
                }
        }
        Response.Write(json);
    }
}