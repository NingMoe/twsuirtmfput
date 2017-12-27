using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;
using System.Data;
using System.Collections;
using System.Text;
public partial class Base_Finance_Ajax_Request : UserPagebase
{
    Services ws = new Services();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string json = "";
        string ResID = "";
        string RecID = "";
        string UserID = "";
        if (Request["ResID"] != null) ResID = Request["ResID"].ToString();
        if (Request["RecID"] != null) RecID = Request["RecID"].Trim();
        UserID = CurrentUser.ID;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request.QueryString["typeValue"] != null)
        {
            string typeValue = Request.QueryString["typeValue"].ToString();
            int PageSize = Request["rows"] == null || Request["rows"].Equals("NaN") ? 15 : Convert.ToInt32(Request["rows"]);
            int PageNumber = Request["page"] == null || Request["page"].Equals("NaN") ? 0 : Convert.ToInt32(Request["page"]);
            switch (typeValue)
            {
                //待处理款项管理
                case "GetfieldByResEmpty":
                    json = GetfieldByResEmpty();
                    break;
                case "GetDataByResEmpty":
                    json = GetDataByResEmpty(ResID);
                    break;
                case "GetfieldInfo":
                    string ColTitle = Request["ColName"];
                    string ColField = Request["ColField"];
                    string ColWidth = Request["ColWidth"];
                    json = Getfield(ColTitle, ColField, ColWidth);
                    break;
                case "SaveInfo":
                    string dataJson = Request["Json"];
                    json = SaveInfo(ResID, dataJson, RecID, UserID);
                    break;
                case "GetDataByResourceInfo":
                    json = GetDataByResourceInfo();
                    break;
                case "GetfieldChild":
                    json = GetfieldChild(ResID);
                    break;
                case "GetDataOfPage":
                    json = GetDataOfPage(PageSize, PageNumber, ResID);
                    break;
                case "GetDataByUserDefinedSql":
                    json = GetDataByUserDefinedSql();
                    break;
                case "KeyDataSave"://保存字典项
                    dataJson = Request["Json"];
                    json = KeyDataSave(ResID, dataJson, RecID, UserID);
                    break;
                case "SaveInfo_Rights":
                    dataJson = Request["Json"];
                    json = SaveInfo_Rights(ResID, UserID, dataJson);
                    break;
                case "SaveSearchInfo"://查询字段配置保存
                    dataJson = Request["Json"];
                    string keyWordValue = "";
                    if (Request["keyWordValue"] != null) keyWordValue = Request["keyWordValue"];
                    json = SaveSearchInfo(ResID, UserID, dataJson, RecID, keyWordValue);
                    break;
                case "SaveListInfo"://列表显示字段配置保存
                    dataJson = Request["Json"];
                    string keyWordValues = "";
                    if (Request["keyWordValue"] != null) keyWordValues = Request["keyWordValue"];
                    json = SaveListInfo(ResID, UserID, dataJson, RecID, keyWordValues);
                    break;
                case "SaveSysSettingsInfo":
                    dataJson = Request["Json"];
                    string JsonChild_Search = Request["JsonChild_Search"];
                    string JsonChild_ResCol = Request["JsonChild_ResCol"];
                    json = SaveSysSettingsInfo(ResID, UserID, dataJson, JsonChild_Search, JsonChild_ResCol, RecID);
                    break;
                case "SaveResEmptyInfo":
                    dataJson = Request["Json"];
                    json = SaveResEmptyInfo(ResID, dataJson);
                    break;
                case "GetDataKeyList"://获取数据字典列表       
                    json = GetDataKeyList();
                    break;
                case "KeyDataDelete"://删除数据字典项
                     dataJson = Request["Json"];
                     json = KeyDataDelete(ResID, RecID, UserID);
                    break;
                case "ValidateCode":
                    string code = Request["KeyCode"];
                    string ID=Request["ID"];
                    json = ValidateCode(code,ID);
                    break;
                default:
                    break;
            }
        }
        Response.Write(json);
    }

    // '<a href=\"javascript:\" onclick=fnLink('FinanceProjectList.aspx?KHBH=11','500','600') >操作</a>'
    #region 待处理款项管理
    protected string GetfieldByResEmpty()
    {
        string ColumnsStr = "[["; ;
        string strEnable = "s='<input type=\"checkbox\" name=\"ch_IsEnable\" value=\"_blank\" id=ch_IsEnable  '+(row.是否启用==\"1\"?\"Checked\":\"\")+' />';";
        string strDefaultMenu = "s='<input type=\"checkbox\" name=\"ch_DefaultMenu\" value=\"_blank\" id=ch_DefaultMenu  '+(row.是否默认==\"1\"?\"Checked\":\"\")+' />';";
        string ResourceTarget = "s='<input type=\"checkbox\" name=\"ch_ResourceTarget\" value=\"_blank\" id=\"ch_blank\" onclick=\"ClickCheckBox(this)\" '+(row.打开方式==\"_blank\"?\"Checked\":\"\")+' />_blank" +
            "&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"ch_ResourceTarget\" value=\"_parent\" id=\"ch_parent\" onclick=\"ClickCheckBox(this)\"  '+ (row.打开方式==\"_parent\"?\"Checked\":\"\")+'/>_parent" +
            "&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"ch_ResourceTarget\" value=\"_search\" id=\"ch_search\" onclick=\"ClickCheckBox(this)\"  '+ (row.打开方式==\"_search\"?\"Checked\":\"\")+' />_search" +
            "&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"ch_ResourceTarget\" value=\"_self\" id=\"ch_self\" onclick=\"ClickCheckBox(this)\"  '+ (row.打开方式==\"_self\"?\"Checked\":\"\")+' />_self" +
            "&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"ch_ResourceTarget\" value=\"_top\" id=\"ch_top\" onclick=\"ClickCheckBox(this)\"  '+ (row.打开方式==\"_top\"?\"Checked\":\"\")+' />_top" +
            "&nbsp;&nbsp;&nbsp;<input type=\"text\" id=\"ch_txt\" style=\"width:50px;\" value=\"'+(row.打开方式!=null && row.打开方式!=\"_top\" && row.打开方式!=\"_self\" && row.打开方式!=\"_search\" && row.打开方式!=\"_parent\" && row.打开方式!=\"_blank\"? row.打开方式:\"\") +'\" />';";
        ColumnsStr += "{title:'资源名称',field:'资源名称',width:150,align:'center'},";
        ColumnsStr += "{title:'启用',field:'是否启用',width:60,align:'center',formatter: function (value, row, index) {" + strEnable + " return s;} },";
        ColumnsStr += "{title:'默认菜单',field:'是否默认',width:60,align:'center',formatter: function (value, row, index) {" + strDefaultMenu + " return s;} },";
        ColumnsStr += "{title:'关联参数关键字',field:'资源说明信息',width:100,align:'center'},";
        ColumnsStr += "{title:'资源链接路径',field:'资源链接',width:800,align:'center'},";
        ColumnsStr += "{title:'资源链接打开方式',field:'打开方式',width:400,align:'center',formatter: function (value, row, index) {" + ResourceTarget + " return s;} }";

        ColumnsStr += " ]]";
        return ColumnsStr;
    }

    /// <summary>
    /// 数据字典项，保存方法
    /// </summary>
    public string KeyDataSave(string ResID, string dataJson, string RecID, string userId)
    {

        if (CommonMethod.AddOrEidt(dataJson, ResID, RecID, userId))
        {
            return "{\"success\": true}";
        }
        else
        {
            return "{\"success\": false}";
        }

    }
    /// <summary>
    /// 数据字典项删除方法
    /// </summary>
    public string KeyDataDelete(string ResID, string RecID,string UserID)
    {
        Services Resource = new Services();
          if (Resource.Delete(ResID, RecID, UserID))
        {
            return "{\"success\": true}";
        }
        else
       {
           return "{\"success\": false}";
        }
    }

    protected string GetDataByResEmpty(string ResID)
    {
        string json = "";
        string strSql = " select ID,[NAME] 资源名称,SHOW_ENABLE 是否启用,RES_IsDefaultMenu 是否默认,RES_EMPTYRESOURCEURL 资源链接,RES_EMPTYRESOURCETARGET 打开方式,Res_Comments 资源说明信息,RES_ROWCOLOR3_WHERE 资源图标 from dbo.CMS_RESOURCE  where ID=" + ResID;
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        // timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        // //项目助理的合计工时

        string strS = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        json = "{\"total\":" + dt.Rows.Count + ",\"rows\":" + strS;
        json += ",\"footer\":[{}]";
        json += "}";
        return json;
    }

    protected string SaveResEmptyInfo(string ResID, string dataJson)
    {
        DataTable dt = CommonMethod.JsonToDataTable(dataJson);
        string strSql = "Update CMS_RESOURCE set EDIT_DATE='" + DateTime.Now + "'";
        List<FieldInfo> fieldList = CommonMethod.GetFieldList(dt.Rows[0]);
        for (int index = 0; index <= fieldList.Count - 1; index++)
        {
            FieldInfo fieldItem = fieldList[index];
            if (fieldItem.FieldDescription.Trim() == "资源名称")
                strSql += ",Name='" + fieldItem.FieldValue.ToString() + "'";
            else if (fieldItem.FieldDescription.Trim() == "是否启用")
                strSql += ",SHOW_ENABLE=" + fieldItem.FieldValue.ToString() + "";
            else if (fieldItem.FieldDescription.Trim() == "是否默认")
                strSql += ",RES_IsDefaultMenu=" + fieldItem.FieldValue.ToString() + "";
            else if (fieldItem.FieldDescription.Trim() == "资源链接")
                strSql += ",RES_EMPTYRESOURCEURL='" + fieldItem.FieldValue.ToString() + "'";
            else if (fieldItem.FieldDescription.Trim() == "打开方式")
                strSql += ",RES_EMPTYRESOURCETARGET='" + fieldItem.FieldValue.ToString() + "'";
            else if (fieldItem.FieldDescription.Trim() == "资源说明信息")
                strSql += ",Res_Comments='" + fieldItem.FieldValue.ToString() + "'";
            else if (fieldItem.FieldDescription.Trim() == "资源图标")
                strSql += ",RES_ROWCOLOR3_WHERE='" + fieldItem.FieldValue.ToString() + "'";
        }
        strSql += " where id=" + ResID;

        try
        {
            Services Resource = new Services();
            string[] changePassWord = Common.getChangePassWord();
            Resource.ExecuteSql(strSql, changePassWord[0], changePassWord[1], changePassWord[2]);
            return "{\"success\":true,\"recid\": 0}";
        }
        catch (Exception ex)
        {
            return "{\"success\":false}";
        }
    }
    #endregion

    protected string GetfieldChild(string ResID)
    {
        string strSql = "select D.CD_RESID,D.CD_COLNAME,D.CD_DISPNAME,S.CS_SHOW_WIDTH from CMS_TABLE_DEFINE D join CMS_TABLE_SHOW S on D.CD_RESID=S.CS_RESID and D.CD_COLNAME=S.CS_COLNAME where CD_RESID='" + ResID.ToString() + "' order by CS_SHOW_ORDER";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        string ColTitle = "";
        string ColField = "";
        string ColWidth = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ColTitle += "," + dt.Rows[i]["CD_DISPNAME"].ToString();
            ColField += "," + dt.Rows[i]["CD_DISPNAME"].ToString();
            ColWidth += "," + dt.Rows[i]["CS_SHOW_WIDTH"].ToString();
        }
        if (ColTitle.Trim() != "")
        {
            ColTitle = ColTitle.Substring(1);
            ColField = ColField.Substring(1);
            ColWidth = ColWidth.Substring(1);
        }
        return Getfield(ColTitle, ColField, ColWidth);
    }


    protected string Getfield(string ColTitle, string ColField, string ColWidth)
    {
        string ColumnsStr = "";
        string[] ch = { "," };
        string[] strColTitle = ColTitle.Split(ch, StringSplitOptions.RemoveEmptyEntries);
        string[] strColField = ColField.Split(ch, StringSplitOptions.RemoveEmptyEntries);
        string[] strColWidth = ColWidth.Split(ch, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strColTitle.Length; i++)
        {
            ColumnsStr += "{title:'" + strColTitle[i].ToString() + "',field:'" + strColField[i].ToString() + "',width:" + strColWidth[i].ToString() + ",align:'center'},";
        }

        if (ColumnsStr.Trim() != "") ColumnsStr = ColumnsStr.Trim().Substring(0, ColumnsStr.Length - 1);

        return ColumnsStr;
    }

    protected string GetDataByUserDefinedSql()
    {
        string UserDefinedSql = "";
        if (Request["UserDefinedSql"] != null)
            UserDefinedSql = Request["UserDefinedSql"].ToString().Trim();
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(UserDefinedSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        // //项目助理的合计工时

        string strS = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        string json = "{\"total\":" + dt.Rows.Count + ",\"rows\":" + strS;
        json += ",  \"footer\" :[{}]";
        json += "}";
        return json;
    }


    protected string GetDataByResourceInfo()
    {
        string ResID = Request["ResID"].ToString();
        string json = "";
        string strSql = "select ID,QTLabel 前台标签,SettingsNum 列表配置号 ,KeyWord 关键字 ,ShowTitle 显示Title ,ENKeyWord 参数关键字 ,Value 值  from SysSettings where Value='" + ResID + "'";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        // //项目助理的合计工时

        string strS = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        json = "{\"total\":" + dt.Rows.Count + ",\"rows\":" + strS;
        json += ",  \"footer\" :[{}]";
        json += "}";
        return json;
    }


    protected string GetDataOfPage(int PageSize, int PageNumber, string ResID)
    {
        string json = "";
        string Condition = Request["Condition"];

        WebServices.PageParameter p = new PageParameter();
        p.PageIndex = PageNumber - 1;
        p.PageSize = PageSize;

        string RowCount = ws.GetDataListRecordCount(Convert.ToInt64(ResID), "test", CommonMethod.FilterSql(Condition)).ToString();
        DataTable dt = ws.GetPageOfDataList(Convert.ToInt64(ResID), CommonMethod.FilterSql(Condition), p, "test").Tables[0];


        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        // //项目助理的合计工时

        string strS = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        json = "{\"total\":" + RowCount + ",\"rows\":" + strS;
        json += ",  \"footer\" :[{}]";
        json += "}";
        return json;
    }

    private string SaveSearchInfo(string ResID, string UserID, string dataJson, string RecID, string keyWordValue)
    {
        Services Resource = new Services();
        string[] ch = { "}]," };
        string[] Json1 = (dataJson + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);
        RecordInfo[] RecInfos = new RecordInfo[Json1.Length];

        for (int i = 0; i < Json1.Length; i++)
        {
            RecordInfo RecInfo = new RecordInfo();
            FieldInfo[] FieldListInfo = GetFieldList(ResID, Json1[i] + "}]");
            RecInfo.RecordID = RecID;
            RecInfo.ResourceID = ResID;
            RecInfo.FieldInfoList = FieldListInfo;
            RecInfos[i] = RecInfo;
        }
        try
        {
            string[] changePassWord = Common.getChangePassWord();
            Resource.ExecuteSql("delete from Sys_CXZJLB where KeyWord='" + keyWordValue.Trim() + "'", changePassWord[0], changePassWord[1], changePassWord[2]);
            Resource.Add(UserID, RecInfos);
            return "{\"success\":true,\"recid\": 0}";
        }
        catch (Exception ex)
        {
            return "{\"success\":false}";
        }
    }

    private string SaveListInfo(string ResID, string UserID, string dataJson, string RecID, string keyWordValue)
    {
        Services Resource = new Services();
        string[] ch = { "}]," };
        string[] Json1 = (dataJson + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);
        RecordInfo[] RecInfos = new RecordInfo[Json1.Length];

        for (int i = 0; i < Json1.Length; i++)
        {
            RecordInfo RecInfo = new RecordInfo();
            FieldInfo[] FieldListInfo = GetFieldList(ResID, Json1[i] + "}]");
            RecInfo.RecordID = RecID;
            RecInfo.ResourceID = ResID;
            RecInfo.FieldInfoList = FieldListInfo;
            RecInfos[i] = RecInfo;
        }
        try
        {
            string[] changePassWord = Common.getChangePassWord();
            Resource.ExecuteSql("delete from ResourceColumn where KeyWord='" + keyWordValue.Trim() + "'", changePassWord[0], changePassWord[1], changePassWord[2]);
            Resource.Add(UserID, RecInfos);
            return "{\"success\":true,\"recid\": 0}";
        }
        catch (Exception ex)
        {
            return "{\"success\":false}";
        }
    }

    #region 保存或更新记录
    private string SaveInfo(string ResID, string dataJson, string RecID, string UserID)
    {

        if (CommonMethod.AddOrEidt(dataJson, ResID, RecID, UserID))
        {
            Services Resource = new Services();
            //CommonProperty.InitProperty(Resource.GetUserInfo(UserID));
            return "{\"success\": true}";
        }
        else
        {
            return "{\"success\": false}";
        }
    }
    #endregion

    private string SaveInfo_Rights(string ResID, string UserID, string dataJson)
    {
        Services Resource = new Services();
        string[] ch = { "}]," };
        string[] Json1 = (dataJson + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);
        RecordInfo[] RecInfos = new RecordInfo[Json1.Length];

        for (int i = 0; i < Json1.Length; i++)
        {
            string RecID = "0";
            RecordInfo RecInfo = new RecordInfo();
            FieldInfo[] FieldListInfo = GetFieldList(ResID, Json1[i] + "}]", ref RecID);
            RecInfo.RecordID = RecID;
            RecInfo.ResourceID = ResID;
            RecInfo.FieldInfoList = FieldListInfo;
            RecInfos[i] = RecInfo;
        }
        try
        {
            Resource.Add(UserID, RecInfos);
            //CommonProperty.InitProperty(Resource.GetUserInfo(UserID));
            return "{\"success\":true,\"recid\": 0}";
        }
        catch (Exception ex)
        {
            return "{\"success\":false}";
        }
    }

    private string SaveSysSettingsInfo(string ResID, string UserID, string dataJson, string JsonChild_Search, string JsonChild_ResCol, string RecID)
    {
        Services Resource = new Services();
        RecordInfo ParentRecInfo = new RecordInfo();
        FieldInfo[] FieldListInfo = GetFieldList(ResID, dataJson);
        ParentRecInfo.RecordID = RecID;
        ParentRecInfo.ResourceID = ResID;
        ParentRecInfo.FieldInfoList = FieldListInfo;
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.SelectData("select * from ResourceColumn where KeyWord in (select ENKeyWord from SysSettings where ID=" + RecID.Trim() + ")", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        string strID = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strID += "," + dt.Rows[i]["ID"].ToString();
        }
        if (JsonChild_Search.Trim() != "" || JsonChild_ResCol.Trim() != "")
        {
            string[] ch = { "}]," };
            string[] Json1 = { };
            if (JsonChild_Search.Trim() != "") Json1 = (JsonChild_Search + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);

            string[] JsonResCol = { };
            if (JsonChild_ResCol.Trim() != "") JsonResCol = (JsonChild_ResCol + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);
            string SearchResID = Resource.GetResourceIDByTableName("Sys_CXZJLB");
            string ResColResID = Resource.GetResourceIDByTableName("ResourceColumn");
            RecordInfo[] RecInfos1 = new RecordInfo[Json1.Length];
            RecordInfo[] RecInfos2 = new RecordInfo[JsonResCol.Length];

            for (int i = 0; i < Json1.Length; i++)
            {
                RecordInfo RecInfo1 = new RecordInfo();
                FieldInfo[] FieldListInfo1 = GetFieldList(SearchResID, Json1[i] + "}]");
                RecInfo1.RecordID = "";
                RecInfo1.ResourceID = SearchResID;
                RecInfo1.FieldInfoList = FieldListInfo1;
                RecInfos1[i] = RecInfo1;
            }
            for (int j = 0; j < JsonResCol.Length; j++)
            {
                RecordInfo RecInfo = new RecordInfo();
                FieldInfo[] FieldListInfo1 = GetFieldList(ResColResID, JsonResCol[j] + "}]");
                RecInfo.RecordID = "";
                RecInfo.ResourceID = ResColResID;
                RecInfo.FieldInfoList = FieldListInfo1;
                RecInfos2[j] = RecInfo;
            }
            try
            {
                if (RecInfos1.Length > 0 && RecInfos2.Length > 0)
                {
                    RecordInfo[][] aList = new RecordInfo[2][];
                    aList[0] = RecInfos1;
                    aList[1] = RecInfos2;
                    Resource.Add(UserID, ParentRecInfo, aList);
                }
                else if (RecInfos1.Length > 0 || RecInfos2.Length > 0)
                {
                    RecordInfo[][] aList = new RecordInfo[1][];
                    if (RecInfos1.Length > 0) aList[0] = RecInfos1;
                    if (RecInfos2.Length > 0) aList[0] = RecInfos2;
                    Resource.Add(UserID, ParentRecInfo, aList);
                }
                else
                {
                    RecordInfo[] RecInfos = { ParentRecInfo };
                    Resource.Add(UserID, RecInfos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "{\"success\":false}";
            }
        }
        else
        {
            try
            {
                RecordInfo[] RecInfos = { ParentRecInfo };
                Resource.Add(UserID, RecInfos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "{\"success\":false}";
            }
        }
        changePassWord = Common.getChangePassWord();
        if (strID.Trim() != "") Resource.ExecuteSql("delete from ResourceColumn where id in (0" + strID.Trim() + ")", changePassWord[0], changePassWord[1], changePassWord[2]);
        return "{\"success\":true,\"recid\": 0}";

    }

    #region //获取WebServices.FieldInfo[]
    public static WebServices.FieldInfo[] GetFieldList(string resid, string dataJson)
    {
        WebServices.Services Resource = new WebServices.Services();
        WebServices.FieldInfo[] FieldListInfo = { };
        Field[] fl = Resource.GetFieldListAll(resid);
        DataTable dt = CommonMethod.JsonToDataTable(dataJson);
        List<FieldInfo> fieldList = CommonMethod.GetFieldList(dt.Rows[0]);
        int i = 0;
        foreach (WebServices.Field f in fl)
        {
            for (int index = 0; index <= fieldList.Count - 1; index++)
            {
                FieldInfo fieldItem = fieldList[index];
                if (fieldItem.FieldDescription == f.Description)
                {
                    FieldInfo fi = new FieldInfo();
                    fi.FieldDescription = f.Description;
                    fi.FieldName = f.Name;
                    fi.FieldValue = fieldItem.FieldValue;
                    Array.Resize(ref FieldListInfo, i + 1);
                    FieldListInfo[i] = fi;
                    i += 1; break; // TODO: might not be correct. Was :   Exit For;
                }
            }
        }
        return FieldListInfo;
    }
    #endregion

    #region //获取WebServices.FieldInfo[]
    public static WebServices.FieldInfo[] GetFieldList(string resid, string dataJson, ref string RecID)
    {
        WebServices.Services Resource = new WebServices.Services();
        WebServices.FieldInfo[] FieldListInfo = { };
        Field[] fl = Resource.GetFieldListAll(resid);
        DataTable dt = CommonMethod.JsonToDataTable(dataJson);
        if (dt.Columns.Contains("ID") && dt.Rows.Count > 0)
            RecID = dt.Rows[0]["ID"].ToString();
        List<FieldInfo> fieldList = CommonMethod.GetFieldList(dt.Rows[0]);
        int i = 0;
        foreach (WebServices.Field f in fl)
        {
            for (int index = 0; index <= fieldList.Count - 1; index++)
            {
                FieldInfo fieldItem = fieldList[index];
                if (fieldItem.FieldDescription == f.Description)
                {
                    FieldInfo fi = new FieldInfo();
                    fi.FieldDescription = f.Description;
                    fi.FieldName = f.Name;
                    fi.FieldValue = fieldItem.FieldValue;
                    Array.Resize(ref FieldListInfo, i + 1);
                    FieldListInfo[i] = fi;
                    i += 1; break; // TODO: might not be correct. Was :   Exit For;
                }
            }
        }
        return FieldListInfo;
    }
    #endregion

    public string getSum(string code, DataTable dt)
    {
        string sum = string.IsNullOrEmpty(dt.Compute("sum(" + code + ")", "true").ToString()) ? "0" : dt.Compute("sum(" + code + ")", "true").ToString();
        return sum;
    }
    #region // 获取数据字典配置列表
    /// <summary>
    /// 获取数据字典配置列表
    /// </summary>
    public string GetDataKeyList()
    {
        string json = "";
        string strSql = "select ID,KeyTitle,KeyValue,KeyDesc,KeySort,KeyCode,isnull(ParentId,0)_parentId,CreateDate ,CreateUser  from dbo.DataDictionary  order by KeySort ";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        string[] AppendKey = { "ID", "KeyTitle", "KeyValue", "KeyDesc", "KeySort", "KeyCode", "CreateDate", "CreateUser" };

        StringBuilder str = new StringBuilder();
        str.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":[");
        foreach (DataRow item in dt.Rows)
        {
            str.Append("{");
            for (int i = 0; i < AppendKey.Length; i++)
            {
                string AppendStr = getAppendStr(AppendKey[i], item, true);
                str.Append(AppendStr);
            }

            str.Append("\"CreateUser\":\"" + item["CreateUser"].ToString() + "\"");
            if (!"0".Equals(item["_parentId"].ToString()))
            {
                str.Append(",\"_parentId\":\"" + item["_parentId"].ToString() + "\"");
            }
            str.Append("},");
        }
        json = str.ToString();
        json = json.Substring(0, json.Length - 1) + "]}";

        return json;
    }

    private string getAppendStr(string Key, DataRow argitem, bool argFH)
    {
        return "\"" + Key + "\":\"" + argitem[Key].ToString() + "\"" + (argFH ? "," : "");
    }

    #endregion


    public string ValidateCode(string code, string ID)
    {
        //ID = ID.Equals("") ? "0" : ID;
        if (ID == "") ID = "0";
        string strSql = "select count(*)number from dbo.DataDictionary where Keycode='" + code + "' and ID!=" + ID;
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        int number = Convert.ToInt32(dt.Rows[0][0].ToString());
        if (number > 0)
        {
            return "{\"success\": false}";
        }
        else
        {
            return "{\"success\": true}";
        }
    }
}