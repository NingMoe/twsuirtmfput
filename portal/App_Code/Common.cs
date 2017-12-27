using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text ;
using BPM;
using BPM.Client; 
using System.Linq;
using System.Web; 
using NetReusables;
using WebServices;
using System.Configuration;
public class Common
{

    public static Services Resource = new Services();

    public static string GetValueByKey(string Key)
    {
        Dictionary<string, string> Settings = GetSettings(CommonProperty.City);
        return Settings[Key];
    }


    private static Dictionary<string, string> GetSettings(string City)
    {
        Dictionary<string, string> Settings = new Dictionary<string, string>();
        if (City=="西安")
        {           
            Settings.Add("门户列表说明", "Portal");
        }
        else if (City == "上海")
        {
            Settings.Add("门户列表说明", "Portal");
        }
        return Settings;
    } 

    /// <summary>
    /// 根据关键词，获取资源配置信息
    /// </summary>
    /// <param name="KeyWord"></param>
    /// <returns></returns>
    public static SysSettings GetSysSettingsByENKey(string KeyWord)
    {
        SysSettings Sys = null;
       
        WebServices.Services Resource = new WebServices.Services();
        UserInfo  user=CommonGetInfo.GetUser();
        //string sql = "select * from SysSettings where ENKeyWord='" + KeyWord + "' ";
        //  DataSet ds = Resource.SelectData(sql);

        DataSet ds =  Resource.GetDataListByResID("437496952875","",CommonMethod.FilterSql(" and 参数关键字='"+KeyWord+"'"),user.ID);
      
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Sys = new SysSettings();
                Sys.ShowTitle = ds.Tables[0].Rows[0]["显示Title"].ToString();
                Sys.ENTableName = ds.Tables[0].Rows[0]["参数关键字"].ToString();
                Sys.KeyWordValue = ds.Tables[0].Rows[0]["参数关键字"].ToString();
                Sys.ResID = ds.Tables[0].Rows[0]["值"].ToString();

                Sys.AlignColStr = ds.Tables[0].Rows[0]["左对齐显示字段"].ToString();
                Sys.AddUrl = ds.Tables[0].Rows[0]["添加地址"].ToString();
                Sys.EidtUrl = ds.Tables[0].Rows[0]["编辑地址"].ToString();
                Sys.AccessoryCol = ds.Tables[0].Rows[0]["附件字段"].ToString();
                Sys.FJGLJD = ds.Tables[0].Rows[0]["附件主子表关联字段"].ToString();
                Sys.AccessoryResID = ds.Tables[0].Rows[0]["附件ResID"].ToString();
                Sys.SettingsNum = ds.Tables[0].Rows[0]["列表配置号"].ToString();
                Sys.LBLX = ds.Tables[0].Rows[0]["列表类型"].ToString();
                Sys.DialogWidth = ds.Tables[0].Rows[0]["弹出层宽度"].ToString() == "" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["弹出层宽度"]);
                Sys.DialogHeight = ds.Tables[0].Rows[0]["弹出层高度"].ToString() == "" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["弹出层高度"]);

                Sys.DefaultOrder = ds.Tables[0].Rows[0]["默认排序"].ToString();
                Sys.SortBy = ds.Tables[0].Rows[0]["排序类型"].ToString();
                Sys.TableName = ds.Tables[0].Rows[0]["数据表名称"].ToString();
                Sys.FilterCondtion = ds.Tables[0].Rows[0]["数据筛选条件"].ToString();

                DataRow dr = ds.Tables[0].Rows[0];
                Sys.IsCheckBox = NetReusables.DbField.GetBool(ref dr, "是否显示复选框");
                Sys.IsAdd = NetReusables.DbField.GetBool(ref dr, "启用添加");
                Sys.IsUpdate = NetReusables.DbField.GetBool(ref dr, "启用修改");
                Sys.IsDelete = NetReusables.DbField.GetBool(ref dr, "启用删除");
                Sys.IsExp = NetReusables.DbField.GetBool(ref dr, "启用导出");
                Sys.IsStartWidth = NetReusables.DbField.GetBool(ref dr, "启用字段宽度设置");
                Sys.IsOrder = NetReusables.DbField.GetBool(ref dr, "是否支持排序");
                Sys.IsRowRight = NetReusables.DbField.GetBool(ref dr, "是否启用行过滤");
                Sys.URLTarget = ds.Tables[0].Rows[0]["页面打开方式"].ToString();
                Sys.FootStr = ds.Tables[0].Rows[0]["统计字段"].ToString();

                return Sys;
            }
            else
            {
                return Sys;
            }
        }
        else
        {
            return Sys;
        }
    }

    /// <summary>
    /// 获取资源ID
    /// </summary>
    /// <param name="KeyWord"></param>
    /// <returns></returns>
    public static string GetResIDByENKekWord(string KeyWord)
    {
        WebServices.Services Resource = new WebServices.Services();
        //string sql = "select [Value] from SysSettings where ENKeyWord='" + KeyWord + "'";
        //DataSet ds = Resource.SelectData(sql);
        UserInfo user = CommonGetInfo.GetUser();
        DataSet ds = Resource.GetDataListByResID("437496952875", "", CommonMethod.FilterSql(" and 参数关键字='" + KeyWord + "'"), user.ID);
      
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["值"].ToString();
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 根据资源ID，获取资源配置信息
    /// </summary>
    /// <param name="ResID"></param>
    /// <returns></returns>
    public static SysSettings GetSysSettingsByResID(string ResID)
    {
        SysSettings Sys = null;
        WebServices.Services Resource = new WebServices.Services();
        // string sql = "select top(1) * from SysSettings where [Value]='" + ResID + "' ";
        // DataSet ds = Resource.SelectData(sql);
        UserInfo user = CommonGetInfo.GetUser();
        DataSet ds = Resource.GetDataListByResID("437496952875", "", CommonMethod.FilterSql(" and 值='" + ResID + "'"), user.ID);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Sys = new SysSettings();
                Sys.ShowTitle = ds.Tables[0].Rows[0]["显示Title"].ToString();
                Sys.ENTableName = ds.Tables[0].Rows[0]["参数关键字"].ToString();
                Sys.KeyWordValue = ds.Tables[0].Rows[0]["参数关键字"].ToString();
                Sys.ResID = ds.Tables[0].Rows[0]["值"].ToString();

                Sys.AlignColStr = ds.Tables[0].Rows[0]["左对齐显示字段"].ToString();
                Sys.AddUrl = ds.Tables[0].Rows[0]["添加地址"].ToString();
                Sys.EidtUrl = ds.Tables[0].Rows[0]["编辑地址"].ToString();
                Sys.AccessoryCol = ds.Tables[0].Rows[0]["附件字段"].ToString();
                Sys.FJGLJD = ds.Tables[0].Rows[0]["附件主子表关联字段"].ToString();
                Sys.AccessoryResID = ds.Tables[0].Rows[0]["附件ResID"].ToString();
                Sys.SettingsNum = ds.Tables[0].Rows[0]["列表配置号"].ToString();
                Sys.LBLX = ds.Tables[0].Rows[0]["列表类型"].ToString();
                Sys.DialogWidth = ds.Tables[0].Rows[0]["弹出层宽度"].ToString() == "" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["弹出层宽度"]);
                Sys.DialogHeight = ds.Tables[0].Rows[0]["弹出层高度"].ToString() == "" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["弹出层高度"]);

                Sys.DefaultOrder = ds.Tables[0].Rows[0]["默认排序"].ToString();
                Sys.SortBy = ds.Tables[0].Rows[0]["排序类型"].ToString();
                Sys.TableName = ds.Tables[0].Rows[0]["数据表名称"].ToString();
                Sys.FilterCondtion = ds.Tables[0].Rows[0]["数据筛选条件"].ToString();

                DataRow dr = ds.Tables[0].Rows[0];
                Sys.IsCheckBox = NetReusables.DbField.GetBool(ref dr, "是否显示复选框");
                Sys.IsAdd = NetReusables.DbField.GetBool(ref dr, "启用添加");
                Sys.IsUpdate = NetReusables.DbField.GetBool(ref dr, "启用修改");
                Sys.IsDelete = NetReusables.DbField.GetBool(ref dr, "启用删除");
                Sys.IsExp = NetReusables.DbField.GetBool(ref dr, "启用导出");
                Sys.IsStartWidth = NetReusables.DbField.GetBool(ref dr, "启用字段宽度设置");
                Sys.IsOrder = NetReusables.DbField.GetBool(ref dr, "是否支持排序");
                Sys.IsRowRight = NetReusables.DbField.GetBool(ref dr, "是否启用行过滤");
                Sys.URLTarget = ds.Tables[0].Rows[0]["页面打开方式"].ToString();
                Sys.FootStr = ds.Tables[0].Rows[0]["统计字段"].ToString();
                return Sys;
            }
            else
            {
                return Sys;
            }
        }
        else
        {
            return Sys;
        }
    }

    /// <summary>
    /// PageIndex 从1开始
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="PageIndex"></param>
    /// <param name="PageSize"></param>
    /// <param name="OrderBy"></param>
    /// <param name="QueryKeystr"></param>
    /// <returns></returns>
    private static DataSet GetDataListPage(string sql, int PageIndex, int PageSize, string OrderBy, string QueryKeystr, string ROW_NUMBER_ORDER, bool argNoPaging, string argCondition)
    {
        string sqldata = "";
        string OrderByStr = "";
        if (!string.IsNullOrEmpty(OrderBy.Trim()))
            OrderByStr = " order by " + OrderBy;

        if (!argNoPaging)
        {
            sqldata = "SELECT DISTINCT * from(SELECT ROW_NUMBER() OVER( " + ROW_NUMBER_ORDER + " ) AS rownum,* from ( " + sql + " ) t where 1=1 " + argCondition + ") c where rownum between " + (PageIndex - 1) * PageSize + " and " + PageIndex * PageSize + OrderByStr;
        }
        else
        {
            sqldata = "SELECT DISTINCT * from(SELECT ROW_NUMBER() OVER( " + ROW_NUMBER_ORDER + " ) AS rownum,* from ( " + sql + " ) t ) c where 1=1  " + argCondition + OrderByStr;
        }
        string[] changePassWord = Common.getChangePassWord();
        return Resource.Query(sqldata, changePassWord[0], changePassWord[1], changePassWord[2]);
    }


    public static bool GetOneRowValueBySQL(string SQL, ref Dictionary<string, object> QueryValueDic)
    {
        bool f = false;
        if (QueryValueDic.Count == 0) return f;
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.Query(SQL, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        if (dt == null || dt.Rows.Count == 0) return f;
        try
        {
            List<string> keys = QueryValueDic.Keys.ToList();
            foreach (string key in keys)
            {
                QueryValueDic[key] = dt.Rows[0][key].ToString();
            }
            f = true;
        }
        catch (Exception ex)
        {
            throw;
        }
        return f;
    }

    /// <summary>
    /// 根据Sql返回某列值
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="Field"></param>
    /// <returns></returns>
    public static string GetOneRowValueBySQL(string SQL, string Field)
    {
        string s = "";
        if (string.IsNullOrWhiteSpace(Field)) return s;
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.Query(SQL, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        if (dt == null || dt.Rows.Count == 0) return s;
        try
        {
            s = dt.Rows[0][Field].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
        return s;
    }

    //查询部门
    public static string QueryDepartment(string DeptID)
    {
        string json = "";
        // DeptID="524142543230";
        // string sql = " select Name 公司名称,IsNUll(Emp_Name,'') 管理员姓名 from CMS_DEPARTMENT D left join CMS_EMPLOYEE E on IsNull(D.DEP_ADMIN_ID,'')=E.Emp_ID where D.pid in(select PID from CMS_DEPARTMENT where id=" + DeptID + ")";

        string sql = " select D.ID,Name 公司名称,IsNUll(Emp_Name,'') 管理员姓名 from CMS_DEPARTMENT D left join CMS_EMPLOYEE E on IsNull(D.DEP_ADMIN_ID,'')=E.Emp_ID where D.pid in(" + (string.IsNullOrWhiteSpace(DeptID) ? "0" : DeptID) + ") order by D.show_order";
        string[] changePassWord = Common.getChangePassWord();

        DataTable dt = Resource.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        int RowCount = dt.Rows.Count;
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
        json = "{\"total\":" + RowCount + ",\"rows\":" + str + "}";
        return json;
    }


    /// <summary>
    /// 根据索引和pagesize返回记录
    /// </summary>
    /// <param name="dt">记录集 DataTable</param>
    /// <param name="PageIndex">当前页</param>
    /// <param name="pagesize">一页的记录数</param>
    /// <returns></returns>
    public static DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
    {
        if (PageIndex == 0)
            return dt;
        DataTable newdt = dt.Clone();
        //newdt.Clear();
        int rowbegin = (PageIndex - 1) * PageSize;
        int rowend = PageIndex * PageSize;

        if (rowbegin >= dt.Rows.Count)
            return newdt;

        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }

        return newdt;
    }

    /// <summary>
    /// 按后台表字段的排序来获取记录值
    /// </summary>
    /// <param name="argRecID"></param>
    /// <param name="argResid"></param>
    /// <param name="argCondition"></param>
    /// <returns></returns>
    public static string getSortDynamicFieldRecords(string argRecID, string argResid, string argCondition)
    {
        DataTable vDT = Resource.GetDataListByResourceID(argResid, false, CommonMethod.FilterSql(" and id=" + argRecID)).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(vDT, timeConverter);
        return "{\"total\":" + vDT.Rows.Count + ",\"rows\":" + str + "}";
    }

    public static int GetDepartOrder(string DepartID, string MyDepartID)
    {
        string[] changePassWord = Common.getChangePassWord();

        int rowCount = int.Parse(Resource.Query("SELECT count(*) FROM dbo.CMS_DEPARTMENT WHERE PID='" + CommonMethod.FilterSql(DepartID) + "' AND ID <> '" + CommonMethod.FilterSql(MyDepartID) + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0].Rows[0][0].ToString());
        if (rowCount > 0)
            return rowCount + 1;
        else
            return rowCount;
    }

    /// <summary>
    /// 获取自定义选择项
    /// </summary>
    /// <param name="argSql">sql语句</param>
    /// <param name="argValueField">取值</param>
    /// <param name="argShowField">显示值</param>
    /// <param name="argHasEmptySelected">是否需要空值</param>
    /// <param name="argSelectedValue">默认显示的值</param>
    /// <returns></returns>
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

    public static string[] getChangePassWord()
    {
        string[] arrStr=new string[3];
        string timeTicks = DateTime.Now.Ticks.ToString();
        Random ran = new Random();
        string randomNum = ran.Next(100000, 999999).ToString();
        string keyPass = ConfigurationManager.AppSettings["keyPass"].ToString();

        List<string> strlist = new List<string>();
        strlist.Add(timeTicks);
        strlist.Add(randomNum);
        strlist.Add(keyPass);
        strlist.Sort();
        string str = strlist[0] + strlist[1] + strlist[2];
        //进行 sha1 加密
        string pwdstr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
        //进行 MD5  加密
        arrStr[0] = timeTicks;
        arrStr[1] = randomNum;
        arrStr[2] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwdstr, "MD5");
        return arrStr;
    }


}

/// <summary>
/// 报表选择字段
/// </summary>
public class SelectReportField
{
    /// <summary>
    /// 字段名称
    /// </summary>
    public string ReportFieldName { get; set; }
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool IsShow { get; set; }
    /// <summary>
    /// 字段显示名称
    /// </summary>
    public string ReportFieldShowName { get; set; }
    /// <summary>
    /// 字段类型
    /// </summary>
    public string ReportFieldType { get; set; }

}

/// <summary>
/// 组织架构实体类
/// </summary>
public class OrganizationTree
{
    public string DeparmentID { get; set; }
    public string DeparmentName { get; set; }

    public List<OrganizationTree> DeparmentChild { get; set; }

    public static List<string> GetChildDeparmentID(string DeparmentID, bool IsGetParentDeparment = false)
    {
        List<string> ChildDeparmentID = new List<string>();
        List<OrganizationTree> AllDeparment = new List<OrganizationTree>();
        List<OrganizationTree> AllDeparmentID = new List<OrganizationTree>();
        OrganizationTree Deparment = AllDeparment.FirstOrDefault(p => p.DeparmentID.Equals(DeparmentID));
        GetJson(false, "", out Deparment, out AllDeparment, false);
        if (AllDeparment.Count > 0)
        {
            OrganizationTree MyDeparment = AllDeparment.FirstOrDefault(p => p.DeparmentID.Equals(DeparmentID));
            if (MyDeparment != null && MyDeparment.DeparmentChild != null && MyDeparment.DeparmentChild.Count > 0)
            {
                GetAllDeparmentID(ref AllDeparmentID, MyDeparment);
                ChildDeparmentID = AllDeparmentID.Select(p => p.DeparmentID).ToList();
            }
        }

        if (IsGetParentDeparment)
            ChildDeparmentID.Add(DeparmentID);
        return ChildDeparmentID.Distinct().ToList();
    }


    public static void GetAllDeparmentID(ref List<OrganizationTree> AllDeparmentID, OrganizationTree Deparment)
    {
        if (Deparment.DeparmentChild != null)
        {
            foreach (OrganizationTree d in Deparment.DeparmentChild)
            {
                if (d.DeparmentChild == null || d.DeparmentChild.Count == 0)
                    AllDeparmentID.Add(d);
                GetAllDeparmentID(ref AllDeparmentID, d);
            }
        }
    }

    public static string GetJson(bool IsLoadUser, string argType, out OrganizationTree Deparment, out List<OrganizationTree> AllDeparment, bool IsMultiSelect = false, bool IsSelectCompany = false)
    {
        Deparment = new OrganizationTree();
        AllDeparment = new List<OrganizationTree>();
        string sql = " ";
        if (argType == "common")
        {
            sql = "select type='部门',id,pid,name,EMP_ID='',show_order from CMS_DEPARTMENT union all select type='员工',id,host_id as pid,emp_name as name,EMP_ID,show_order from dbo.CMS_EMPLOYEE where host_id<>0 order by show_order ";
        }
        else if (argType == "HasEmployeeAccount")
        {
            sql = " select type='部门',id,pid,name,EMP_ID='',show_order from CMS_DEPARTMENT union all select type='员工',id,DeparmentID as pid,EmployeeName as name,EmployeeAccount,0 from dbo.EmployeeInfo where EmployeeCode IS NOT NULL AND EmployeeCode <> '' and ( EmployeeAccount IS NOT NULL and EmployeeAccount <> '' )  order by show_order ";
        }
        else
        {
            sql = " select type='部门',id,pid,name,EMP_ID='',show_order from CMS_DEPARTMENT union all select type='员工',id,DeparmentID as pid,EmployeeName as name,EmployeeCode,0 from dbo.EmployeeInfo where EmployeeCode IS NOT NULL  order by show_order ";
        }

        if (!IsLoadUser) sql = "select type=(CASE WHEN IsCompany=1 THEN  '公司' ELSE '部门' END ),id,pid,name,EMP_ID='',show_order from CMS_DEPARTMENT ";
        string[] changePassWord = Common.getChangePassWord();


        DataTable dt = Common.Resource.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        DataRow[] ParentRows = dt.Select(" pid=-1");
        string json = "[" + GetParentJson(ParentRows[0], dt, out Deparment, ref AllDeparment, IsMultiSelect, IsSelectCompany) + "]";
        return json;
    }

    private static string GetParentJson(DataRow row, DataTable dt, out OrganizationTree Deparment, ref List<OrganizationTree> AllDeparment, bool IsMultiSelect = false, bool IsSelectCompany = false)
    {

        Deparment = new OrganizationTree();
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"id\":" + row["id"].ToString() + ",");
        string UserID = row["EMP_ID"].ToString();
        if (UserID == "")
        {
            UserID = row["ID"].ToString();
        }
        sb.Append("\"attributes\":{\"url\":\"QXGrid.aspx\",\"UserID\":\"" + UserID + "\",\"Type\":\"" + row["Type"].ToString() + "\"},");
        sb.Append("\"text\":\"" + row["name"].ToString() + "\"");

        Deparment.DeparmentID = row["id"].ToString();
        Deparment.DeparmentName = row["name"].ToString();
        AllDeparment.Add(Deparment);

        if (row["id"].ToString() == "0") sb.Append(",\"iconCls\":\"tree-folder-brand\"");
        else if (row["Type"].ToString() == "公司") sb.Append(",\"iconCls\":\"tree-folder-Company\""); else if (row["Type"].ToString() == "部门") sb.Append(",\"iconCls\":\"tree-folder-Dept\""); else sb.Append(",\"iconCls\":\"tree-folder-Emp\"");
        DataRow[] ChildRows = dt.Select("pid=" + row["id"].ToString());
        if (ChildRows.Length > 0)
        {
            if (row["id"].ToString() == "0")
            {
                sb.Append(",\"state\":\"open\"");
            }
            else
            {
                sb.Append(",\"state\":\"closed\"");
            }
            List<OrganizationTree> DeparmentChild = new List<OrganizationTree>();
            sb.Append(",\"children\":" + GetChildJson(ChildRows, dt, out DeparmentChild, ref AllDeparment, IsMultiSelect, IsSelectCompany) + "");
            Deparment.DeparmentChild = DeparmentChild;
        }
        sb.Append("}");
        return sb.ToString();
    }
    private static string GetChildJson(DataRow[] rows, DataTable dt, out List<OrganizationTree> DeparmentChild, ref List<OrganizationTree> AllDeparment, bool IsMultiSelect = false, bool IsSelectCompany = false)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        DeparmentChild = new List<OrganizationTree>();
        for (int i = 0; i < rows.Length; i++)
        {
            OrganizationTree Deparment = new OrganizationTree();
            sb.Append(GetParentJson(rows[i], dt, out Deparment, ref AllDeparment, IsMultiSelect, IsSelectCompany));
            DeparmentChild.Add(Deparment);
            if (i < rows.Length - 1)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");
        return sb.ToString();
    }

}
