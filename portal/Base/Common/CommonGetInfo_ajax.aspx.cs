using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NetReusables;
using WebServices;


public partial class Base_Common_CommonGetInfo_ajax : UserPagebase
{
    string UserID = "";
    string UserName = "";
    UserInfo oEmployee = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = this.CurrentUser.ID;
        UserName = this.CurrentUser.Name;
        string Condition = "";
        string json = "";
        string typeValue = Request["typeValue"];

        int PageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
        int PageNumber = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);


        if (typeValue == "GetDataByType")
        {
            string ByType = Request["ByType"] == null ? "" : Request["ByType"].ToString();

            if (ByType == "0")
            {
                typeValue = "CommonGetDataByProc";
            }
            else
            {
                typeValue = "GetDataByUserDefinedSql";
            }
        }

        switch (typeValue)
        {
            case "GetDataByUserDefinedSql":
                {
                    string ResID = "";
                    string SortField = "";
                    string SortBy = "";
                    string QueryKeystr = "";
                    string ROW_NUMBER_ORDER = "";
                    bool NoPaging = false;
                    List<ReportFoot> vReportFoots = null;

                    if (!string.IsNullOrEmpty(Request["ReportFoot"]))
                    {
                        string s = Request["ReportFoot"].ToString();
                        vReportFoots = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportFoot>>(s);
                    }

                    if (!string.IsNullOrEmpty(Request["Condition"]))
                    {
                        Condition = Request["Condition"].ToString();
                    }

                    QueryKeystr = Request["QueryKeystr"] == null ? "" : Request["QueryKeystr"].ToString();

                    if (!string.IsNullOrWhiteSpace(QueryKeystr))
                    {
                        if (QueryKeystr.Substring(0,4).ToLower().Trim()=="and")
                        {
                            Condition = Condition + QueryKeystr;
                        }
                        else
                        {
                            Condition = Condition + " and " + QueryKeystr;
                        }
                    }

                    if (!string.IsNullOrEmpty(Request["page"]))
                    {
                        PageNumber = Convert.ToInt32(Request["page"]);
                    }
                    if (Request["sort"] != null)
                    {
                        SortField = Request["sort"].ToString();
                    }
                    if (Request["order"] != null)
                    {
                        SortBy = Request["order"].ToString();
                    }
                    if (Request["ROW_NUMBER_ORDER"] != null)
                    {
                        ROW_NUMBER_ORDER = Request["ROW_NUMBER_ORDER"].ToString();
                    }
                    if (Request["NoPaging"] != null)
                    {
                        NoPaging = true;
                    }
                    if (Request["UserDefinedSql"] != null)
                        ResID = Request["UserDefinedSql"].ToString().Trim();
                    json = CommonGetInfo.GetDataByUserDefinedSql(ResID, PageNumber, PageSize, SortField, SortBy, UserID, Condition, ROW_NUMBER_ORDER, NoPaging, vReportFoots, false);
                    break;
                }
            case "GetALLCommonSearchField":
                {

                    string argResourcesId = Request["argResourcesId"] == null ? "" : Request["argResourcesId"].ToString();
                    string argSearchType = Request["argSearchType"] == null ? "" : Request["argSearchType"].ToString();
                    string argReportKey = Request["argReportKey"] == null ? "" : Request["argReportKey"].ToString();

                    string argIsDynamicallyCreatTableHead = Request["argIsDynamicallyCreatTableHead"] == null ? "" : Request["argIsDynamicallyCreatTableHead"].ToString();
                    json = CommonGetInfo.GetALLCommonSearchField(argResourcesId, argReportKey, argSearchType, argIsDynamicallyCreatTableHead);
                }
                break;
            case "GetDictionaryReturnRelatedColumn":
                Services oServices = new Services();
                string ResourceID = Request["ResourceID"];
                string ResourceColumn = Request["ResourceColumn"];
                DataTable dt = oServices.GetDictionaryReturnRelatedColumn(ResourceID, ResourceColumn).Tables[0];
                json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                break;
            case "GetDictionaryReturnColumn":
                oServices = new Services();
                ResourceID = Request["ResourceID"];
                ResourceColumn = Request["ResourceColumn"];
                bool IsMultiselect = Convert.ToBoolean(Convert.ToInt32(Request["IsMultiselect"]));
                dt = oServices.GetDictionaryReturnColumn(ResourceID, ResourceColumn);
                string gridField = "";
                string searchField = "";
                // string DicResourceID = "";
                string str = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (DbField.GetInt(ref dr, "DataType") == 9)
                    {
                        string strEnable = "s='<input type=\"checkbox\" name=\"ch_IsEnable\" value=\"_blank\" id=\"ch_IsEnable\" disabled=\"true\"  '+(row." + DbField.GetStr(ref dr, "CD_DISPNAME") + "==\"1\"?\"Checked\":\"\")+' />';";
                        gridField += ",{field: '" + DbField.GetStr(ref dr, "CD_DISPNAME") + "',title:'" + DbField.GetStr(ref dr, "CD_DISPNAME") + "',width:100, sortable:true ,align:'center',formatter: function (value, row, index) {" + strEnable + " return s;}}";
                    }
                    else gridField += ",{field: '" + DbField.GetStr(ref dr, "CD_DISPNAME") + "',title:'" + DbField.GetStr(ref dr, "CD_DISPNAME") + "',width:100, sortable:true ,align:'center'}";

                    searchField += ",{field:'" + DbField.GetStr(ref dr, "CD_DISPNAME") + "'}";
                }

                dt = oServices.GetDictionaryReturnRelatedColumn(ResourceID, ResourceColumn).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (!IsMultiselect) str += "," + DbField.GetStr(ref dr, "ResColCNName") + ":\"'+rowData." + DbField.GetStr(ref dr, "DicColCNName") + "+'\"";
                    else str += "," + DbField.GetStr(ref dr, "ResColCNName") + ":" + DbField.GetStr(ref dr, "DicColCNName") + "";
                }
                if (!IsMultiselect) gridField = "{title:'选择',field:'选择',width:50,align:'center' ,formatter:function(value,rowData,rowIndex){return '<a href=\"javascript:\" onclick=SelectOneRow({" + str.Substring(1) + "})>选择</a>';}}" + gridField;
                else gridField = "{field: 'ID',title:'',checkbox:true,width:30, sortable:false ,align:'center'}" + gridField;
                if (gridField.Trim() != "") gridField = "[[" + gridField + "]]";
                if (searchField.Trim() != "") searchField = "[#][" + searchField.Substring(1) + "]";
                json = gridField + searchField;
                break;
            case "GetDictionaryList":
                oServices = new Services();
                ResourceID = Request["ResourceID"];
                ResourceColumn = Server.UrlDecode(Request["ResourceColumn"]);
                if (Request["Condition"] != null) Condition = Server.UrlDecode(Request["Condition"]);


                string DicResourceID = oServices.GetDictionaryReturnResourceID(ResourceID, ResourceColumn);
                PageParameter p = new PageParameter();
                p.PageIndex = PageNumber - 1;
                p.PageSize = PageSize;
                p.SortField = "ID";
                p.SortBy = "";
                DataSet ds = oServices.GetPageOfDataList(Convert.ToInt64(DicResourceID), CommonMethod.FilterSql(Condition), p, "test");
                string RowCount = oServices.GetDataListRecordCount(Convert.ToInt64(DicResourceID), UserID, CommonMethod.FilterSql(Condition)).ToString();
                json = "{\"total\":" + RowCount + ",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]) + "}";
                break;
            case "GetDataOfReportOfDynamicHeadReportColumnStrBySqlStr":
                {
                    string ByType = Request["ByType"] == null ? "" : Request["ByType"].ToString();
                    string filterRulesByHeadStr = "";
                    string frozenColumnsJson = "";
                    string DynamicHeadReportStr = Request["DynamicHeadReportStr"];

                    if (ByType == "0")
                    {
                        string ProcName = Request["ProcName"] == null ? "" : Request["ProcName"].ToString();
                        string ProcOutPutCount = Request["ProcOutPutCount"] == null ? "" : Request["ProcOutPutCount"].ToString();
                        string ProcInPutStr = Request["ProcInPutStr"] == null ? "" : Request["ProcInPutStr"].ToString();
                        DataTable vDT = new DataTable();
                        json = CommonGetDataByProc(CommonGetInfo.GetUser(), ProcOutPutCount, ProcInPutStr, ProcName, "", PageSize.ToString(), PageNumber.ToString(), "", "", "", "", out vDT, false, null, true);
                    }
                    else
                    {
                        string UserDefinedSql = Request["UserDefinedSql"] == null ? "" : Request["UserDefinedSql"].ToString();
                        string SortField = Request["sort"] == null ? "" : Request["sort"].ToString();
                        string SortBy = Request["order"] == null ? "" : Request["order"].ToString();
                        Condition = Request["Condition"] == null ? "" : Request["Condition"].ToString();
                        string ROW_NUMBER_ORDER = Request["ROW_NUMBER_ORDER"].ToString();
                        bool NoPaging = Request["NoPaging"] == null ? false : true;
                        bool IsExport = Request["IsExport"] == null ? false : true; ;
                        List<ReportFoot> vReportFoots = null;
                        if (!string.IsNullOrEmpty(Request["ReportFoot"]))
                        {
                            string s = Request["ReportFoot"].ToString();
                            vReportFoots = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportFoot>>(s);
                        }
                        json = CommonGetInfo.GetDataByUserDefinedSql(UserDefinedSql, PageNumber, PageSize, SortField, SortBy, UserID, "", ROW_NUMBER_ORDER, NoPaging, vReportFoots, IsExport, Condition.Trim());
                    }
                    json = CommonGetInfo.GetColumnsJson(null, out filterRulesByHeadStr, out frozenColumnsJson, json, DynamicHeadReportStr) + "#" + filterRulesByHeadStr + "#" + frozenColumnsJson;
                }
                break;
            case "CommonGetUserDefinedToolBars":
                {
                    string keyWordValue = Request["keyWordValue"] == null ? "" : Request["keyWordValue"].ToString();
                    string argToolBarType = Request["argToolBarType"] == null ? "" : Request["argToolBarType"].ToString();
                    bool argIsUseNewEasyui = Request["argIsUseNewEasyui"] == null ? true : (Request["argIsUseNewEasyui"].ToString() == "1" ? true : false);
                    string DelNameStr = "";
                    json = "^" + CommonGetInfo.CommonGetUserDefinedToolBars(keyWordValue, out DelNameStr, argToolBarType, argIsUseNewEasyui) + "^";
                    json += "***" + DelNameStr;
                }
                break;
            case "CommonGetDataByProc":
                {
                    string ProcName = Request["ProcName"] == null ? "" : Request["ProcName"].ToString();
                    string ProcOutPutCount = Request["ProcOutPutCount"] == null ? "" : Request["ProcOutPutCount"].ToString();
                    string ProcInPutStr = Request["ProcInPutStr"] == null ? "" : Request["ProcInPutStr"].ToString();

                    string SortField = Request["sort"] == null ? "" : Request["sort"].ToString();
                    string SortBy = Request["order"] == null ? "" : Request["order"].ToString();

                    bool IsExport = Request["IsExport"] == null ? false : true;

                    DataTable vDT = new DataTable();
                    json =CommonGetDataByProc(CommonGetInfo.GetUser(), ProcOutPutCount, ProcInPutStr, ProcName, "", PageSize.ToString(), PageNumber.ToString(), "1", "", SortField, SortBy, out vDT, false, null, IsExport);
                    break;
                }
            default:
                break;
        }
        Response.Write(json);
    }

    /// <summary>
    /// 调用存储过程
    /// </summary>
    /// <param name="argEmployee"></param>
    /// <param name="argProcOutPutCount"></param>
    /// <param name="argProcInPutStr"></param>
    /// <param name="argProcName"></param>
    /// <param name="argReportFoot"></param>
    /// <param name="argRows"></param>
    /// <param name="argPage"></param>
    /// <param name="argHasSortByProc"></param>
    /// <param name="argHasPagingByProc"></param>
    /// <param name="argSort"></param>
    /// <param name="argOrder"></param>
    /// <param name="argIsExport"></param>
    /// <param name="argReportFoots"></param>
    /// <returns></returns>
    public static string CommonGetDataByProc(UserInfo argEmployee, string argProcOutPutCount, string argProcInPutStr, string argProcName, string argReportFoot, string argRows, string argPage, string argHasSortByProc, string argHasPagingByProc, string argSort, string argOrder, out DataTable dt, bool argIsExport = false, List<ReportFoot> argReportFoots = null, bool argReturnJson = false)
    {

        string json = "";
        dt = new DataTable();
        if (argEmployee == null) return json;
        int ProcOutPutCount = string.IsNullOrEmpty(argProcOutPutCount) ? 0 : Convert.ToInt32(argProcOutPutCount);
        string ProcInPutStr = argProcInPutStr;
        string ProcName = argProcName;
        string ReportFoot = argReportFoot;
        int PageSize = string.IsNullOrEmpty(argRows) ? 0 : Convert.ToInt32(argRows);
        int PageNumber = string.IsNullOrEmpty(argPage) ? 0 : Convert.ToInt32(argPage);
        string[] ProcInPuts = ProcInPutStr.Split(',');
        string SortField = "";
        string SortBy = "";
        List<object> paraValues = new List<object>();
        List<object> output = new List<object>();
        int allCount = 0;
        bool HasSortByProc = false;
        bool HasPagingByProc = false;

        if (!string.IsNullOrEmpty(argHasSortByProc))
        {
            if (argHasSortByProc == "1") HasSortByProc = true;
        }

        if (string.IsNullOrEmpty(argHasPagingByProc))
        {
            if (argHasPagingByProc == "1") HasPagingByProc = true;
        }

        if (!string.IsNullOrEmpty(argSort))
        {
            SortField = argSort;
        }
        if (!string.IsNullOrEmpty(argOrder))
        {
            SortBy = argOrder;
        }

        for (int i = 0; i < ProcOutPutCount; i++)
        {
            output.Add("");
            paraValues.Add("");
        }

        for (int i = 0; i < ProcInPuts.Length; i++)
        {
            paraValues.Add(ProcInPuts[i]);
        }

        if (HasPagingByProc)
        {
            paraValues.Add(PageSize);
            paraValues.Add(PageNumber);
        }


        //if (HasSortByProc)
        //{
        //    paraValues.Add(SortField);
        //    paraValues.Add(SortBy);
        //}

        DataTable dtNew = new DataTable();
        object[] outputs = output.ToArray();

        StoreProcedure sp = new StoreProcedure(ProcName);//类的对象

        if (ProcOutPutCount == 0)
        {
            dtNew = sp.ExecuteDataTable(paraValues.ToArray());
            allCount = dtNew.Rows.Count;
        }
        else
        {
            dtNew = sp.ExecProcOutput(out outputs, ProcOutPutCount, paraValues.ToArray());
            allCount = Convert.ToInt32(outputs[0].ToString());
        }


        if (HasSortByProc)
        {
            if (!string.IsNullOrWhiteSpace(SortField) && !string.IsNullOrWhiteSpace(SortBy))
            {
                dtNew.DefaultView.Sort = SortField + " " + SortBy;
                dtNew = dtNew.DefaultView.ToTable();
            }
        }


        if (dtNew.Rows.Count > 0 && dtNew.Columns.Contains("TotleCount"))
            allCount = Convert.ToInt32(dtNew.Rows[0]["TotleCount"]);

        WebServices.Services services = new WebServices.Services();
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        // //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = "";
        dt = dtNew;
        if (argIsExport)
            return Newtonsoft.Json.JsonConvert.SerializeObject(dtNew, timeConverter);
        else if (argReturnJson)
            return Newtonsoft.Json.JsonConvert.SerializeObject(SplitDataTable(dtNew, PageNumber, PageSize), timeConverter);
        else
        {
            if (PageNumber == 0 && PageSize == 0)
            {
                allCount = dtNew.Rows.Count;
                str = Newtonsoft.Json.JsonConvert.SerializeObject(dtNew, timeConverter);
            }
            else
            {
                allCount = dtNew.Rows.Count;
                str = Newtonsoft.Json.JsonConvert.SerializeObject(SplitDataTable(dtNew, PageNumber, PageSize), timeConverter);
            }

            return "{\"total\":" + allCount + ",\"rows\":" + str + "}";

            // return "{\"total\":" + allCount + ",\"rows\":" + str + ",\"footer\":[" + vReportFootStr + "]}";
        }
    }

    /// <summary>
    /// 根据索引和pagesize返回记录
    /// </summary>
    /// <param name="dt">记录集 DataTable</param>
    /// <param name="PageIndex">当前页</param>
    /// <param name="pagesize">一页的记录数</param>
    /// <returns></returns>
    private static DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
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
    /// 筛选DataTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="FilterStr"></param>
    /// <returns></returns>
    private static DataTable FilterDataTable(DataTable dt, string FilterStr)
    {
        if (string.IsNullOrWhiteSpace(FilterStr))
            return dt;
        DataTable newdt = dt.Clone();
        DataRow[] drArr = dt.Select(FilterStr);
        newdt.Clear();
        foreach (DataRow dr in drArr)
        {
            DataRow newdr = newdt.NewRow();
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }
        return newdt;
    }

}