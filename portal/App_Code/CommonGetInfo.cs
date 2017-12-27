using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NetReusables;
using WebServices;
using System.Text;

/// <summary>
/// CommonGetInfo 的摘要说明
/// </summary>
public static class CommonGetInfo
{
    public static Services Resource = new Services();

    /// <summary>
    /// PageIndex 从1开始
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="PageIndex"></param>
    /// <param name="PageSize"></param>
    /// <param name="OrderBy"></param>
    /// <param name="QueryKeystr"></param>
    /// <returns></returns>
    private static DataSet GetDataListPage(string ResID, int PageIndex, int PageSize, string  SortField,string SortBy,string  UserID, string QueryKeystr, string ROW_NUMBER_ORDER, bool argNoPaging, string argCondition)
    {
        //if (!argNoPaging)
        //{
        //    sqldata = "SELECT  * from(SELECT ROW_NUMBER() OVER( " + ROW_NUMBER_ORDER + " ) AS rownum,* from ( " + sql + " ) t where 1=1 " + argCondition + ") c where rownum between " + ((PageIndex - 1) * PageSize+1) + " and " + PageIndex * PageSize + OrderByStr;
        //}
        //else
        //{
        //    sqldata = "SELECT  * from(SELECT ROW_NUMBER() OVER( " + ROW_NUMBER_ORDER + " ) AS rownum,* from ( " + sql + " ) t ) c where 1=1  " + argCondition + OrderByStr;
        //} 

        if (SortField=="")
        {
            SortField = "ID";
        }
        if (SortBy == "")
        {
            SortBy = "DESC";
        }
        PageParameter p = new PageParameter();
        p.PageIndex = PageIndex - 1;
        p.PageSize = PageSize;
        p.SortField = SortField;
        p.SortBy = SortBy;

        if (QueryKeystr.Trim()!="")
        {
            if (QueryKeystr.Substring(0,4).Trim().ToLower()=="and")
            {

            }
            else
            {
                QueryKeystr = " and " + QueryKeystr;
            }
        }
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.GetPageOfDataList(Convert.ToInt64(ResID), true, CommonMethod.FilterSql(QueryKeystr), p, UserID);
        return ds;
    }

    public static string GetfilterRulesStr(string argfilterRules)
    {
        List<filterRules> vfilterRules = null;
        vfilterRules = Newtonsoft.Json.JsonConvert.DeserializeObject<List<filterRules>>(argfilterRules);

        if (vfilterRules == null) return "";

        string vfilterRulesStr = " 1=1  ";

        foreach (filterRules Rule in vfilterRules)
        {
            if (string.IsNullOrEmpty(Rule.value.ToString())) continue;
            object RuleValue = "";
            switch (Rule.type)
            {
                case "numberbox":
                    RuleValue = Convert.ToInt32(Rule.value);
                    break;
                case "datebox":
                    RuleValue = Convert.ToDateTime(Rule.value);
                    break;
                case "dd":
                    // vfilterRulesStr += Rule.field + "%" + Rule.value + "%";
                    break;
                default:
                    RuleValue = "'" + Rule.value.ToString() + "'";
                    break;
            }

            switch (Rule.op)
            {
                case "contains":
                    vfilterRulesStr += "  and ( " + Rule.field + " like '%" + Rule.value + "%' )";
                    break;
                case "equal":
                    if (string.IsNullOrWhiteSpace(Rule.value.ToString()))
                        vfilterRulesStr += "  and ( " + Rule.field + " = " + RuleValue + " or " + Rule.field + " is null)";
                    else
                        vfilterRulesStr += "  and ( " + Rule.field + " = " + RuleValue + " )";
                    break;
                case "notequal":
                    vfilterRulesStr += "  and ( " + Rule.field + " <> " + RuleValue + " )";
                    break;
                case "less":
                    vfilterRulesStr += "  and ( " + Rule.field + " < " + RuleValue + " )";
                    break;
                case "greater":
                    vfilterRulesStr += "  and ( " + Rule.field + " > " + RuleValue + " )";
                    break;
                case "beginwith":
                    vfilterRulesStr += "  and ( " + Rule.field + " like " + RuleValue + "%' )";
                    break;
                case "endwith":
                    vfilterRulesStr += "  and ( " + Rule.field + " like '%" + RuleValue + " )";
                    break;
            }
        }

        return vfilterRulesStr.ToString();

    }

    public static string GetDataByUserDefinedSql(string ResID, int PageIndex, int PageSize, string SortField, string SortBy, string UserID, string QueryKeystr, string ROW_NUMBER_ORDER, bool argNoPaging, List<ReportFoot> argReportFoots = null, bool argIsReport = false, string argCondition = "")
    {
        if (argIsReport)
        {
            argNoPaging = true;
            argReportFoots = null;
        }
        
        DataSet ds = GetDataListPage(ResID, PageIndex, PageSize, SortField, SortBy, UserID, QueryKeystr, ROW_NUMBER_ORDER, argNoPaging, argCondition);
        DataTable dt = ds.Tables[0];
        string[] temp = ROW_NUMBER_ORDER.Split(',');
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].Split('=').Length>1)
            {
                dt.Columns.Add(temp[i].Split('=')[0]);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dt.Rows[j][temp[i].Split('=')[0]] = dt.Rows[j][temp[i].Split('=')[1]].ToString();
                }
            }
        }
        if (ds == null || ds.Tables.Count == 0) return "";
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0], timeConverter);
        if (QueryKeystr.Trim()!="")
        {
            if (QueryKeystr.Substring(0,4).ToLower().Trim()=="and")
            {
            }
            else
            {
                QueryKeystr = " and " + QueryKeystr;
            }
           
        }

        int RowCount = Resource.GetDataListByResID(ResID, "",QueryKeystr, UserID).Tables[0].Rows.Count;

        if (argIsReport)
        {
            return str;
        }
        else
        {
            return "{\"total\":" + RowCount + ",\"rows\":" + str + "}";
        }
    }
     

    public static string GetCombobox_Data(List<Field> FieldList, string argComboboxType, string argSearchType, bool argIsUsedFieldNameQuery)
    {
        List<Combobox_Data> vCombobox_Datas = new List<Combobox_Data>();
        switch (argComboboxType)
        {
            case "field":
                {
                    if (FieldList != null)
                    {
                        foreach (Field r in FieldList)
                        {
                            vCombobox_Datas.Add(new Combobox_Data()
                            {
                                id = (argIsUsedFieldNameQuery ? r.Description : r.Description),
                                text = r.Description,
                            });
                        }
                    }
                }
                break;
            case "OP":
                {
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = "like",
                        text = "包含"
                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = "=",
                        text = "等于"
                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = "<>",
                        text = "不等于"
                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = ">",
                        text = "大于"
                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = "<",
                        text = "小于"
                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = ">=",
                        text = "大于等于"

                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = "<=",
                        text = "小于等于"
                    });
                    vCombobox_Datas.Add(new Combobox_Data()
                    {
                        id = "Between...And...",
                        text = "日期区间"
                    });
                }
                break;
        }

        string str = Newtonsoft.Json.JsonConvert.SerializeObject(vCombobox_Datas);
        return str;
    }

    public static string GetALLCommonSearchField(string argResourcesId, string argReportKey, string argSearchType, string argIsDynamicallyCreatTableHead, bool argAutomaticFilteringColumn = true, bool argIsUsedFieldNameQuery = false, bool argIsSearchQuery = false)
    {
        List<Field> FieldList = new List<Field>();
        string str = "";
        if (string.IsNullOrWhiteSpace(argIsDynamicallyCreatTableHead))
        {
            FieldList = GetSearchFieldList(argResourcesId, argReportKey);
        }

        if(argIsSearchQuery)
        {
            dynamic ReturnFieldList = FieldList.Select(p => new { FieldId = p.Name, FieldName = p.Description, FieldType = ReadDataColumnSet.getType(p.DataType) });
            str = Newtonsoft.Json.JsonConvert.SerializeObject(ReturnFieldList);
        }
        else
        {
            dynamic ReturnFieldList = FieldList.Select(p => new { FieldId = p.Name, FieldName = p.Description, FieldType = p.DataType });
            str = Newtonsoft.Json.JsonConvert.SerializeObject(ReturnFieldList);
        }
        return str + "[#]" + GetCombobox_Data(null, "OP", "", argIsUsedFieldNameQuery) + "[#]" + GetCombobox_Data(FieldList, "field", "", argIsUsedFieldNameQuery);
    }

    public static UserInfo GetUser()
    {
        UserInfo oEmployee = HttpContext.Current.Session["Employee"] as UserInfo;
        //if (HttpContext.Current.Session["PortalEmployee"] == null)
        //{
        //    if (HttpContext.Current.Request.Cookies["EmployeeCookies"] == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        Services Resource = new Services();
        //        HttpContext.Current.Session["PortalEmployee"] = Resource.GetUserInfo(HttpContext.Current.Request.Cookies["EmployeeCookies"].Values["EmployeeCode"].Trim());
        //    }
        //}
        //oEmployee = HttpContext.Current.Session["PortalEmployee"] as UserInfo;

        return oEmployee;

    }

    public static List<Field> GetSearchFieldList(string argResourcesId, string argKey)
    {
        List<Field> FieldList = new List<Field>();
        string MyKey = argKey;
        string BaseResid = "";
        if (!string.IsNullOrWhiteSpace(argKey))
        {
            SysSettings sys = Common.GetSysSettingsByENKey(argKey);
            if (sys != null && !string.IsNullOrWhiteSpace(sys.ResID))
            {
                string TopParentID = Resource.GetTopParentID(sys.ResID).ToString();
                if (!string.IsNullOrWhiteSpace(TopParentID))
                {
                    //sys = Common.GetSysSettingsByResID(TopParentID);
                    BaseResid = TopParentID;
                    //if (sys != null && !string.IsNullOrWhiteSpace(sys.KeyWordValue))
                    //{
                    //    MyKey = sys.KeyWordValue;
                    //}
                }
            }

            if(string.IsNullOrWhiteSpace(BaseResid))
            {
                BaseResid = @" ( CASE
                           WHEN c.RES_IsView = 1 THEN c.PID
                              ELSE c.id
                            END ) ";
            }

            string sql = @"SELECT DISTINCT *
FROM   (SELECT DISTINCT a.*,
                        " + BaseResid + @" BaseResid
        FROM   Sys_CXZJLB AS a
               INNER JOIN dbo.SysSettings AS b
                       ON a.KeyWord = b.ENKeyWord
               INNER JOIN dbo.CMS_RESOURCE AS c
                       ON c.id = b.[Value]) t
       INNER JOIN dbo.CMS_TABLE_DEFINE AS b
              ON b.CD_DISPNAME = t.SearchCol
			  AND b.CD_RESID = t.BaseResid
WHERE  KeyWord = '" + MyKey + "' ";
            string[] changePassWord = Common.getChangePassWord();
            DataSet ds = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow vDr in ds.Tables[0].Rows)
                    {
                        FieldList.Add(new Field()
                        {
                            Name = vDr["SearchCol"].ToString(),
                            Description = vDr["ShowCol"].ToString(),
                            DataType = vDr["DateType"].ToString()
                        });
                    }
                }
            }
        }
        else if (!string.IsNullOrWhiteSpace(argResourcesId))
        {
            FieldList = Resource.GetFieldList(argResourcesId).ToList();
        }
        return FieldList;
    }
    
    public static string GetColumnsJson(DataTable argNewDt, out string filterRulesByHeadStr, out string frozenColumnsJson, string dtJson = "", string DynamicHeadReportStr = "", bool HasRulesByHead = true)
    {
        filterRulesByHeadStr = "";
        List<filterRules> vfilterRules = new List<filterRules>();
        frozenColumnsJson = "";
        DataTable dt = new DataTable();
        if (!string.IsNullOrWhiteSpace(dtJson))
        {
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dtJson);
            if (dt == null || dt.Rows.Count == 0) return "";
        }
        else
        {
            dt = argNewDt;
        }
        DynamicHeadReport dh = null;
        if (!string.IsNullOrWhiteSpace(DynamicHeadReportStr))
        {
            dh = Newtonsoft.Json.JsonConvert.DeserializeObject<DynamicHeadReport>(DynamicHeadReportStr);
            if (dh == null) return "";
        }

        string ColumnsJson = "[[";
        foreach (DataColumn dc in dt.Columns)
        {

            bool IsShow = true;
            bool IsFrozen = false;
            bool addEditColumn = false;
            int CommonDynamicHeadWidth = 100;
            if (dh != null)
            {
                if (dh.NoNeedColumn.Count > 0 && dh.NoNeedColumn.Find(p => p.ColumnName.Equals(dc.ColumnName)) != null)
                    IsShow = false;

                if (dh.frozenColumns.Count > 0 && dh.frozenColumns.Find(p => p.ColumnName.Equals(dc.ColumnName)) != null)
                    IsFrozen = true;

            }

            if (IsShow)
            {

                if (HasRulesByHead)
                {
                    vfilterRules.Add(new filterRules()
                    {
                        field = dc.ColumnName,
                        type = "text",
                        //"numberbox" ,"datebox"
                        op = "contains"
                    });
                }


                if (IsFrozen)
                {
                    ToColumn vToColumn = dh.frozenColumns.Find(p => p.ColumnName.Equals(dc.ColumnName));

                    if (string.IsNullOrWhiteSpace(frozenColumnsJson))
                    {

                        if (dc.ColumnName == "ck")
                        {
                            frozenColumnsJson = "[[{field:'ck',checkbox:true}";
                            continue;
                        }
                        else
                        {
                            frozenColumnsJson = "[[{title:'" + dc.ColumnName + "',field:'" + dc.ColumnName + "', align:'center',sortable:true";
                        }
                    }
                    else
                    {
                        if (dc.ColumnName == "ck")
                        {
                            frozenColumnsJson += ",{field:'ck',checkbox:true}";
                            continue;
                        }
                        else
                        {
                            frozenColumnsJson += ",{title:'" + dc.ColumnName + "',field:'" + dc.ColumnName + "', align:'center',sortable:true";
                        }
                    }

                    if (dh != null)
                    {
                        frozenColumnsJson += ",styler: function (value, row, index) { var s= SetFieldStyle('" + dh.ReportKey + "',value, row, index,'" + (dh.IsChild ? "1" : "") + "');  return s;}";

                        frozenColumnsJson += ",formatter: function (value, row, index) { var s= SetFieldformatter('" + dh.ReportKey + "',value, row, index,'" + (dh.IsChild ? "1" : "") + "','" + dc.ColumnName + "'); return s;}";


                        if (vToColumn != null)
                        {
                            if (vToColumn.ColumnWidth == 0)
                            {
                                if (dh.CommonDynamicHeadWidth > 0)
                                    CommonDynamicHeadWidth = dh.CommonDynamicHeadWidth;
                            }
                            else
                            {
                                CommonDynamicHeadWidth = vToColumn.ColumnWidth;
                            }

                            if (vToColumn.IsEditColumn)
                            {
                                switch (vToColumn.editorType)
                                {
                                    case 0:
                                        frozenColumnsJson += ",editor:'textbox'";
                                        addEditColumn = true;
                                        break;
                                    case 1:
                                        frozenColumnsJson += ",editor:'numberbox'";
                                        addEditColumn = true;
                                        break;
                                    case 2:
                                        frozenColumnsJson += ",editor:{type:'checkbox',options:{on:'是',off:'否'}}";
                                        addEditColumn = true;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (dh.CommonDynamicHeadWidth > 0)
                                CommonDynamicHeadWidth = dh.CommonDynamicHeadWidth;
                        }

                        if (dh.IsAllEditColumn && !addEditColumn)
                        {
                            frozenColumnsJson += ",editor:'textbox'";
                            addEditColumn = true;
                        }
                        frozenColumnsJson += ",width:" + CommonDynamicHeadWidth + " }";

                    }
                }
                else
                {
                    ToColumn vToColumn = dh.NeedColumn.Find(p => p.ColumnName.Equals(dc.ColumnName));
                    string ShowName = dc.ColumnName;
                    if (vToColumn != null)
                    {
                        if (!string.IsNullOrWhiteSpace(vToColumn.ShowName))
                            ShowName = vToColumn.ShowName;
                    }

                    if (ColumnsJson != "[[")
                        ColumnsJson += ",";
                    ColumnsJson += "{title:'" + ShowName + "',field:'" + dc.ColumnName + "', align:'center',sortable:true";

                    if (dh != null)
                    {
                        ColumnsJson += ",styler: function (value, row, index) { var s= SetFieldStyle('" + dh.ReportKey + "',value, row, index,'" + (dh.IsChild ? "1" : "") + "');  return s;}";

                        ColumnsJson += ",formatter: function (value, row, index) { var s= SetFieldformatter('" + dh.ReportKey + "',value, row, index,'" + (dh.IsChild ? "1" : "") + "','" + dc.ColumnName + "'); return s;}";

                        if (dh.IsAllEditColumn && !addEditColumn)
                        {
                            ColumnsJson += ",editor:'textbox'";
                            addEditColumn = true;
                        }

                        if (dh.CommonDynamicHeadWidth > 0)
                            CommonDynamicHeadWidth = dh.CommonDynamicHeadWidth;
                    }

                    if (vToColumn != null)
                    {
                        if (vToColumn.ColumnWidth > 0)
                            CommonDynamicHeadWidth = vToColumn.ColumnWidth;
                    }

                    ColumnsJson += ",width:" + CommonDynamicHeadWidth + " }";
                }
            }
            //ColumnsJson += ",sortable:true";
        }

        filterRulesByHeadStr = Newtonsoft.Json.JsonConvert.SerializeObject(vfilterRules); ;

        if (!string.IsNullOrWhiteSpace(frozenColumnsJson))
            frozenColumnsJson += "]]";
        return ColumnsJson + "]]";
    }

    public static string CommonGetUserDefinedToolBars(string keyWordValue, out string DelNameStr, string argToolBarType = "",
   bool argIsUseNewEasyui = true)
    {
        DelNameStr = "";
        UserInfo oEmployee = GetUser();
        if (oEmployee == null) return "";

        List<UserDefinedToolBar> vUserDefinedToolBars = UserDefinedToolBar.GetUserDefinedToolBars(keyWordValue, argToolBarType);
        DelNameStr = "";
        if (vUserDefinedToolBars.Count == 0) return "''";
        string Str = "[";
        for (int i = 0; i < vUserDefinedToolBars.Count; i++)
        {
            if (!vUserDefinedToolBars[i].IsEnabled)
            {
                if (Str != "[") Str += ",'-',";
                Str += "{'text':'" + vUserDefinedToolBars[i].ToolName + "','iconCls':'" + vUserDefinedToolBars[i].ToolIcon + "','disabled':" + vUserDefinedToolBars[i].IsEnabled.ToString().ToLower() + ",'handler':" + vUserDefinedToolBars[i].EventCode + "}";
            }
            else
            {
                if (DelNameStr != "") DelNameStr += ",";
                DelNameStr += vUserDefinedToolBars[i].ToolName;
            }
        }
        Str += "]";
        return Str;
    }

}