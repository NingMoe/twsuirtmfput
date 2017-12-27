using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebServices;
using System.Text;
using NetReusables;


/// <summary>
///CommonMethod 的摘要说明
/// </summary>
public class CommonMethod
{
    //Services Resource = new Services();
	public CommonMethod()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 加查某条记录是否存在
    /// </summary>
    /// <param name="resid">资源ID</param>
    /// <param name="condtion">查询条件</param>
    /// <returns>结果</returns>
    public static int CheckExist(string resid,string condtion) 
    {
        Services Resource = new Services();        
        ResourceInfo info = Resource.GetResourceInfoByID(resid);//根据资源ID获取表名称
        string strSql = string.Format("select count(*)as unm  from {0} where {1}", info.TableName, CommonMethod.FilterSql(condtion));
        string[] changePassWord = Common.getChangePassWord();

        int res = int.Parse(Resource.Query(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0].Rows[0]["unm"].ToString());
        return res;
    }

    #region "普通的添加或修改"
    /// <summary>
    /// 普通的添加或修改，如果记录ID为空或 “0”则是添加，否则是修改
    /// </summary>
    /// <param name="json">请求数据</param>
    /// <param name="resid">资源ID</param>
    /// <param name="recid">记录ID</param>
    /// <param name="UserID">用户账号</param>
    /// <returns></returns>
    public static bool AddOrEidt(string json, string resid, string recid, string UserID)
    {
        Services Resource = new Services();
        ResourceInfo ResInfo = Resource.GetResourceInfoByID(resid);
        if (ResInfo.IsView) resid = ResInfo.ParentID;

        FieldInfo[] fiList = GetFieldList(resid, json);
        if (recid == "0" || recid == "")
        {
            return Resource.Add(resid, UserID, fiList);
        }
        else
        {
            return Resource.Edit(resid, recid, UserID, fiList);
        }
    }


   


    public static RecordInfo[] GetRecordInfo(string ResID, string dataJson)
    {
        Services Resource = new Services();
        DataTable dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dataJson);
        RecordInfo[] RecInfoList = new RecordInfo[dt.Rows.Count];

        Field[] oFieldList = Resource.GetFieldList(ResID);
        if (dt.Rows.Count > 0)
        {
          
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<FieldInfo> fieldList = GetFieldList(dt.Rows[i]);
                FieldInfo[] FieldListInfo = { };
                int j = 0;
                foreach (Field oField in oFieldList)
                {
                    for (int index = 0; index <= fieldList.Count - 1; index++)
                    {
                        FieldInfo fieldItem = fieldList[index];
                        if (fieldItem.FieldDescription == oField.Description || fieldItem.FieldName == oField.Name)
                        {
                            FieldInfo fi = new FieldInfo();
                            fi.FieldName = oField.Name.Trim();
                            fi.FieldDescription = oField.Description.Trim();
                            fi.FieldValue = fieldItem.FieldValue;
                            Array.Resize(ref FieldListInfo, j + 1);
                            FieldListInfo[j] = fi;
                            j += 1; break;
                        }
                    }
                }



                if (dt.Columns.Contains("*IsUpDirectory*"))
                {
                    FieldInfo fi = new FieldInfo();
                    fi.FieldName = "IsUpDirectory";
                    fi.FieldDescription = "IsUpDirectory";
                    fi.FieldValue = dt.Rows[i]["*IsUpDirectory*"];
                    Array.Resize(ref FieldListInfo, j + 1);
                    FieldListInfo[j] = fi;
                    j += 1; 
                }
                if ( dt.Columns.Contains("*DocHostName*"))
                {
                    FieldInfo fi = new FieldInfo();
                    fi.FieldName = "DocHostName";
                    fi.FieldDescription = "DocHostName";
                    fi.FieldValue = dt.Rows[i]["*DocHostName*"];
                    Array.Resize(ref FieldListInfo, j + 1);
                    FieldListInfo[j] = fi;
                    j += 1;
                }

                RecordInfo RecInfo = new RecordInfo();
                RecInfo.FieldInfoList = FieldListInfo;
                RecInfo.ResourceID = ResID;
                RecInfo.RecordID = dt.Rows[i]["RecID"].ToString();
                RecInfoList[i] = RecInfo;
            }
            return RecInfoList;
        }
        else return null; 
    }



    public static string SaveInfo_MultiLine(string ResID, string Json, string UserID)
    {
        Services Resource = new Services();
        ResourceInfo ResInfo = Resource.GetResourceInfoByID(ResID);
        if (ResInfo.IsView) ResID = ResInfo.ParentID;
        RecordInfo[] list = GetRecordInfo(ResID, Json);

        try
        {
            if (list != null)
            {
                Resource.Add(UserID, list);
                return "{\"success\":true,\"recid\": 0}";
            }
            else return "{\"success\":false}";
        }
        catch (Exception ex)
        { 
            return "{\"success\":false}";
        }
    }
    public static string SaveInfoByMultiLineAndDocument(string ResID, string ChildJson, string UserID)
    {
        Services Resource = new Services();
        string[] ch = { "]," };

        RecordInfo[] list = null;
        if (ChildJson.Trim() != "")
        {
            string[] ChildJsonList = (ChildJson + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < ChildJsonList.Length; i++)
            {
                if (ChildJsonList[i] + "]".Trim() != "[]")
                {
                    list = GetRecordInfo(ResID, ChildJsonList[i] + "]");
                }
            }
        }
        try
        {
            if (Resource.AddOfDocument(UserID, list).Length > 0)
                return "{\"success\":true,\"recid\": 0}";
            else
                return "{\"success\":false}";
        }
        catch (Exception ex)
        {
            return "{\"success\":false}";
        }
    } 



    public static string SaveInfoByParentAndChildDocument(string ParentResID, string ParentRecID, string ParentJson, string ChildResID, string ChildJson, string UserID)
    {
        Services Resource = new Services();
        RecordInfo ParentRecInfo = new RecordInfo();

        ParentRecInfo.FieldInfoList = GetFieldList(ParentResID, ParentJson);
        ParentRecInfo.RecordID = ParentRecID;
        ParentRecInfo.ResourceID = ParentResID;
          
        string[] ch = { "," };
        string[] ChildResIDList = ChildResID.Split(ch, StringSplitOptions.RemoveEmptyEntries);
        ch[0] = "],";

        List<RecordInfo[]> list = new List<RecordInfo[]>();
        if (ChildJson.Trim() != "")
        {
            string[] ChildJsonList = (ChildJson + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);
         
            for (int i = 0; i < ChildJsonList.Length; i++)
            {
                if (ChildJsonList[i] + "]".Trim() != "[]")
                {

                    list.Add(GetRecordInfo(ChildResIDList[i], ChildJsonList[i] + "]")); 
                }
            }
        } 
        try
        {
            if (Resource.AddofDoc(UserID, ParentRecInfo, list.ToArray()) > 0)
                return "{\"success\":true,\"recid\": 0}";
            else
                return "{\"success\":false}";
        }
        catch (Exception ex)
        {
          //  Log.Error(ex.Message);
            return "{\"success\":false}";
        }
    }


    public static string SaveInfoByParentAndChild(string ParentResID, string ParentRecID, string ParentJson, string ChildResID, string ChildJson, string UserID)
    { 
        try
        {
            long RecID = SaveInfo_ParentAndChild(ParentResID, ParentRecID, ParentJson, ChildResID, ChildJson, UserID);
            if (RecID > 0)
                return "{\"success\":true,\"recid\": " + RecID + "}";
            else
                return "{\"success\":false}";
        }
        catch (Exception ex)
        {
            //  Log.Error(ex.Message);
            return "{\"success\":false}";
        }
    }


    public static string SaveChildInfo(string ParentResID, string ParentRecID,  string ResID, string  Json, string UserID)
    {
        try
        {
            if (SaveInfo_Child(ParentResID, ParentRecID, ResID, Json, UserID))
                return "{\"success\":true,\"recid\":0}";
            else
                return "{\"success\":false}";
        }
        catch (Exception ex)
        {
            //  Log.Error(ex.Message);
            return "{\"success\":false}";
        }
    }

    public static Int64  SaveInfo_ParentAndChild(string ParentResID, string ParentRecID, string ParentJson, string ChildResID, string ChildJson, string UserID)
    {
        Services Resource = new Services();
        RecordInfo ParentRecInfo = new RecordInfo();

        ParentRecInfo.FieldInfoList = GetFieldList(ParentResID, ParentJson);
        ParentRecInfo.RecordID = ParentRecID;
        ParentRecInfo.ResourceID = ParentResID;

        string[] ch = { "," };
        string[] ChildResIDList = ChildResID.Split(ch, StringSplitOptions.RemoveEmptyEntries);
        ch[0] = "],";

        List<RecordInfo[]> list = new List<RecordInfo[]>();
        if (ChildJson.Trim() != "")
        {
            string[] ChildJsonList = (ChildJson + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < ChildJsonList.Length; i++)
            {
                if (ChildJsonList[i] + "]".Trim() != "[]")
                {

                    list.Add(GetRecordInfo(ChildResIDList[i], ChildJsonList[i] + "]"));
                }
            }
        }

        try
        {
            long RecID = Resource.Add(UserID, ParentRecInfo, list.ToArray());
            return RecID;
        }
        catch (Exception ex)
        { 
            Log.Error(ex.Message);
            return 0;
        }
    }


    public static bool SaveInfo_Child(string ParentResID, string ParentRecID, string ResID, string Json, string UserID)
    {
        Services Resource = new Services();
      //  RecordInfo ParentRecInfo = new RecordInfo();


        FieldInfo[] oFieldInfoList = GetFieldList(ResID, Json);

        //ParentRecInfo.FieldInfoList = GetFieldList(ParentResID, ParentJson);
        //ParentRecInfo.RecordID = ParentRecID;
        //ParentRecInfo.ResourceID = ParentResID;

      //  string[] ch = { "]," };
      ////  string[] ChildResIDList = ChildResID.Split(ch, StringSplitOptions.RemoveEmptyEntries);
       

      //  List<RecordInfo[]> list = new List<RecordInfo[]>();
      //  if ( Json.Trim() != "")
      //  {
      //      string[] ChildJsonList = ( Json + ",").Split(ch, StringSplitOptions.RemoveEmptyEntries);

      //      for (int i = 0; i < ChildJsonList.Length; i++)
      //      {
      //          if (ChildJsonList[i] + "]".Trim() != "[]")
      //          {

      //              list.Add(GetRecordInfo(ResID, ChildJsonList[i] + "]"));
      //          }
      //      }
      //  }

        try
        {
            return  Resource.Add(ParentResID, ParentRecID, ResID, UserID, oFieldInfoList); 
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return false ;
        }
    }
    
    #endregion


    #region "普通的添加或修改"
    /// <summary>
    /// 普通的添加或修改，如果记录ID为空或 “0”则是添加，否则是修改
    /// </summary>
    /// <param name="json">请求数据</param>
    /// <param name="resid">资源ID</param>
    /// <param name="recid">记录ID</param>
    /// <param name="UserID">用户账号</param>
    /// <returns></returns>
    public static bool CopyInfo(string resid, string recid, string UserID)
    {
        Services Resource = new Services();
        DataTable dt = Resource.GetDataListByResID(resid, "",CommonMethod.FilterSql( " and id='" + recid + "'"), UserID).Tables[0];
        List<FieldInfo> fiList = GetFieldList(dt.Rows[0]);
        if (dt.Rows.Count > 0)
        {
            dt.Rows[0]["ID"] = 0;
            return Resource.Add(resid, UserID, fiList.ToArray());
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region "得到要添加的字段Key-Value的集合"
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
                    fi.FieldDescription =f.Description.ToString();
                    fi.FieldName = f.Name.ToString(); 
                    fi.FieldValue = FilterSql(fieldItem.FieldValue.ToString());

                    Array.Resize(ref FieldListInfo, i + 1);
                    FieldListInfo[i] = fi;
                    i += 1; break; // TODO: might not be correct. Was :   Exit For;
                }
            }
        }
        return FieldListInfo;
    }
    #endregion


    #region 过滤处理 Sql 语句字符串中的注入脚本
    /**/
    /// <summary>
    /// 过滤处理 Sql 语句字符串中的注入脚本
    /// </summary>
    /// <param name="source">传入的字符串</param>
    /// <returns></returns>
    #endregion
    public static string FilterSql(string source)
    {

        if (source != "")
        {
            //source = source.Replace("'", "'");//单引号替换成两个单引号
            source = source.Replace("\"", "“");
            source = source.Replace("|", "｜");
            //半角封号替换为全角封号，防止多语句执行
            source = source.Replace(";", "；");

            //半角括号替换为全角括号
            source = source.Replace("(", " ( ");
            source = source.Replace("--", "- -");
            source = source.Replace(")", " ) ");
            source = source.Replace("/*", "/ *");
            /**/
            ///////////////要用正则表达式替换，防止字母大小写得情况////////////////////

            //去除执行存储过程的命令关键字
            if (source.ToLower().IndexOf("exec") != -1)
            {
                source = source.ToLower().Replace("exec", "e x e c");
            }
            if (source.ToLower().IndexOf("execute") != -1)
            {
                source = source.ToLower().Replace("execute", "e x e c u t e");
            }
            if (source.ToLower().IndexOf("delete") != -1)
            {
                source = source.ToLower().Replace("delete", "d e l e t e");
            }
            if (source.ToLower().IndexOf("temp") != -1)
            {
                source = source.ToLower().Replace("temp", "t e m p");
            }
            if (source.ToLower().IndexOf("select") != -1)
            {
                source = source.ToLower().Replace("select", "s e l e c t");
            }
            if (source.ToLower().IndexOf("update") != -1)
            {
                source = source.ToLower().Replace("update", "u p d a t e");
            }
            if (source.ToLower().IndexOf("sysobjects") != -1)
            {
                source = source.ToLower().Replace("sysobjects", "s y s o b j e c t s");
            }
            if (source.ToLower().IndexOf("create") != -1)
            {
                source = source.ToLower().Replace("create", "c r e a t e");
            }
            if (source.ToLower().IndexOf("union") != -1)
            {
                source = source.ToLower().Replace("union", "u n i o n");
            }
            if (source.ToLower().IndexOf("drop") != -1)
            {
                source = source.ToLower().Replace("drop", "d r o p");
            }
            if (source.ToLower().IndexOf("master") != -1)
            {
                source = source.ToLower().Replace("master", "m a s t e r");
            }
            if (source.ToLower().IndexOf("truncate") != -1)
            {
                source = source.ToLower().Replace("truncate", "t r u n c a t e");
            }
            if (source.ToLower().IndexOf("declare") != -1)
            {
                source = source.ToLower().Replace("declare", "d e c l a r e");
            }
            if (source.ToLower().IndexOf("database") != -1)
            {
                source = source.ToLower().Replace("database", "d a t a b a s e");
            }
            if (source.ToLower().IndexOf("echo") != -1)
            {
                source = source.ToLower().Replace("echo", "e c h o");
            }
            if (source.ToLower().IndexOf("insert") != -1)
            {
                source = source.ToLower().Replace("insert", "i n s e r t");
            }
            if (source.ToLower().IndexOf("execute") != -1)
            {
                source = source.ToLower().Replace("execute", "e x e c u t e");
            }

            if (source.ToLower().IndexOf("char(") != -1)
            {
                source = source.ToLower().Replace("char(", "c h a r(");
            }

            //去除系统存储过程或扩展存储过程关键字
            if (source.ToLower().IndexOf("xp_") != -1)
            {
                source = source.ToLower().Replace("xp_", "x p_");
            }
            if (source.ToLower().IndexOf("sp_") != -1)
            {
                source = source.ToLower().Replace("sp_", "s p_");
            }
            //防止16进制注入
            if (source.ToLower().IndexOf("0x") != -1)
            {
                source = source.ToLower().Replace("0x", "0 x");
            }
        }
        return source;
    }


    public static List<FieldInfo> GetFieldList(DataRow row)
    {
        List<FieldInfo> fiList = new List<FieldInfo>();
        for (int i = 0; i < row.Table.Columns.Count; i++)
        {
            FieldInfo fi = new FieldInfo();
            fi.FieldDescription = row.Table.Columns[i].ColumnName;
            fi.FieldName = row.Table.Columns[i].ColumnName;
            string value = row[fi.FieldDescription].ToString();
            if (value != "")
            {
                value = value.Trim();
            }
            fi.FieldValue = value;
            fiList.Add(fi);
        }
        return fiList;
    }

    public static FieldInfo FillFieldInfo(string FieldDescription, string FieldName, object FieldValue)
    { 
        FieldInfo fi = new FieldInfo();
        fi.FieldDescription = FieldDescription;
        fi.FieldName =FieldName;
        fi.FieldValue = FilterSql(FieldValue.ToString());
        return fi;
    }
    #endregion

    #region "子表要添加的字段，支持多条记录"
    public static List<RecordInfo> GetChildfieldinfo(DataTable dt, string resid)
    {
        Services Resource = new Services();
        List<RecordInfo> childList = new List<RecordInfo>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            RecordInfo info = new RecordInfo();
            info.RecordID = "0";
            info.ResourceID = resid;
            List<FieldInfo> fiList = new List<FieldInfo>();
            #region "得到要添加的字段Key-Value的集合"
            for (int j = 0; j < dt.Rows[i].Table.Columns.Count; j++)
            {
                FieldInfo fi = new FieldInfo();
                fi.FieldDescription = dt.Rows[i].Table.Columns[j].ColumnName;
                string value = dt.Rows[i][fi.FieldDescription].ToString();
                if (value!="")
                {
                    value = value.Trim();
                }
                fi.FieldValue = value;
                if (fi.FieldDescription.ToLower() == "id")
                {
                    info.RecordID = fi.FieldValue.ToString();
                }
                fiList.Add(fi);
            }
            #endregion
            info.FieldInfoList = fiList.ToArray();
            childList.Add(info);
        }
        return childList;
    }
    #endregion

    #region "将JSON转成DataTable"
    public static DataTable JsonToDataTable(string strJson)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = (DataTable)JsonConvert.DeserializeObject<DataTable>(strJson);
        }
        catch (Exception)
        {
        }
        return dt;
    }
    #endregion

    #region "高级字段返回需要带出的字段"
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ResID">表资源ID</param>
    /// <param name="dispname">高级字典字段</param>
    /// <returns></returns>
    public static string GetDictionaryColName(string ResID, string dispname)
    {
        Services Resource = new Services();
        string str = "";
        string sql = "select cd_colname from CMS_TABLE_DEFINE where cd_resid=" + ResID + " and CD_DISPNAME='" + CommonMethod.FilterSql(dispname) + "'";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        string colname = dt.Rows[0][0].ToString();
        //sql = "SELECT  CMS_TABLE_DEFINE.CD_DISPNAME,CD_dicNAME=(SELECT CD_DISPNAME FROM CMS_TABLE_DEFINE WHERE CD_RESID=CMS_COL_DICTIONARY.CDZ2_RESID2 and CD_COLNAME=CMS_COL_DICTIONARY.cdz2_col2)FROM CMS_TABLE_DEFINE, CMS_COL_DICTIONARY WHERE CDZ2_RESID1='" + ResID + "' AND CDZ2_MAINCOL='" + colname + "' AND CD_RESID='" + ResID + "' AND CD_COLNAME=CDZ2_COL1 AND (CDZ2_TYPE IS NULL OR CDZ2_TYPE=0 OR CDZ2_TYPE=1) ORDER BY CDZ2_SHOWORDER ASC";
        sql = " select R1.*,D1.CD_dicNAME,D1.DictResID from (select d.*,Def.CD_DISPNAME from  CMS_TABLE_DEFINE Def join CMS_COL_DICTIONARY  D on def.CD_RESID=d.CDZ2_RESID1 and def.CD_COLNAME=d.CDZ2_COL1) R1" +
            " join (select Dict.*,Def.CD_DISPNAME CD_dicNAME from  CMS_TABLE_DEFINE Def join " +
            " (select D.*, (CASe when isNull(Res_Type,0)=1 then Res_PID2 else id end) DictResID from CMS_COL_DICTIONARY D join CMS_RESOURCE R on D.CDZ2_RESID2=R.ID ) Dict on Def.CD_RESID=Dict.DictResID and   Def.CD_COLNAME=Dict.cdz2_col2) D1 " +
            " on R1.CDZ2_RESID1=d1.CDZ2_RESID1 and R1.CDZ2_COL1=d1.CDZ2_COL1   where R1.CDZ2_RESID1='" + ResID + "' AND R1.CDZ2_MAINCOL='" + CommonMethod.FilterSql(colname) + "' ";
        changePassWord = Common.getChangePassWord();
        dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str += dt.Rows[i]["CD_DISPNAME"].ToString() + ":" + dt.Rows[i]["CD_dicNAME"].ToString();
            if (i < dt.Rows.Count - 1)
            {
                str += ",";
            }
        }
        return str;
    }
    #endregion 

    /// <summary>
    /// 获取列表显示字段、按钮权限等
    /// </summary>
    /// <param name="keywordParameters"></param>
    /// <param name="MenuResID"></param>
    /// <param name="oEmployee"></param>
    /// <returns></returns>
    public static string GetFieldJson(string keywordParameters, string MenuResID,UserInfo oEmployee)
    {
        string UserID = oEmployee.ID;
        string json = "[[";
        SysSettings sys = Common.GetSysSettingsByENKey(keywordParameters);
        if (sys == null) json += "]]"; 
        else
        { 
            DataRow[] drRights = null;
            SysSettings sysDefault = sys;
            if (keywordParameters.Contains("--"))
            {
                keywordParameters = keywordParameters.Substring(0, keywordParameters.IndexOf("--"));
                sysDefault = Common.GetSysSettingsByENKey(keywordParameters);
            }
            if (oEmployee.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
            {
                if (sys.EidtUrl.Trim() != "" && sys.IsUpdate) sys.IsUpdateRights = true;
                if (sys.IsDelete) sys.IsDeleteRights = true;
                if (sys.IsExp) sys.IsExpRights = true;
                if (sys.AddUrl.Trim() != "" && sys.IsAdd) sys.IsAddRights = true;
            }
            else
            {
                Services Resource = new Services();
                if (MenuResID==null || MenuResID.Trim() == "") MenuResID = "0";
                string[] changePassWord = Common.getChangePassWord();
                DataTable dt = Resource.SelectData("select * from cms_resource where RES_COMMENTS='" + keywordParameters + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                if (MenuResID=="0"&&dt.Rows.Count>0)
                {
                    MenuResID = dt.Rows[0]["ID"].ToString();
                }
                DataTable dtRights = Resource.GetAllPortalOperationByResourceIDAndUserID(oEmployee.ID, MenuResID, false); //CommonProperty.PortalRights(oEmployee);
                drRights = dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode=''");
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='' and RightsValue='2'").Length > 0 && sys.AddUrl.Trim() != "" && sys.IsAdd)
                    sys.IsAddRights = true;
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='' and RightsValue=3").Length > 0 && sys.EidtUrl.Trim() != "" && sys.IsUpdate)
                    sys.IsUpdateRights = true;
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='' and RightsValue=4").Length > 0 && sys.IsDelete)
                    sys.IsDeleteRights = true;
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='' and RightsValue=6").Length > 0 && sys.IsExp)
                    sys.IsExpRights = true;
            }
            json += GetFieldValue(sys, oEmployee, drRights) + "]]";
            json += "[#]";
            json += "[" + Newtonsoft.Json.JsonConvert.SerializeObject(sys) + "]";
            json += "[#]";
            json += "[" + GetUserDefinedToolBars(sysDefault, oEmployee, drRights) + "]";
        }
        return json;
    }


    public static string Getfield(string ColTitle, string ColField, string ColWidth, int OperatePageWidth, int OperatePageHeight, bool IsOperateButton, string OperatePageUrl, string OperateFunction, string OperateText)
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

        if (IsOperateButton)
        {
            if (OperatePageUrl!=null && OperatePageUrl.Trim() != "") ColumnsStr = "[[" + ColumnsStr + "{title:'操作',field:'操作',width:50,align:'center' ,formatter:function(value,rowData,rowIndex){return '<a href=\"javascript:\" onclick=fnLink(encodeURI(\"" + OperatePageUrl.ToString() + "\"),\"" + OperatePageWidth + "\",\"" + OperatePageHeight + "\")  >" + OperateText + "</a>';}}]]";
            else
            {
                if (OperateFunction!=null && OperateFunction.Trim() != "") ColumnsStr = "[[" + ColumnsStr + "{title:'操作',field:'操作',width:50,align:'center' ,formatter:function(value,rowData,rowIndex){return '<a href=\"javascript:\" onclick=" + OperateFunction + "('+rowData.ID+',this)>" + OperateText + "</a>';}}]]";
            }
        }
        else
        {
            ColumnsStr = ColumnsStr.Trim().Substring(0, ColumnsStr.Length - 1);
            ColumnsStr = "[[" + ColumnsStr.Trim() + "]]";
        }
      
        return ColumnsStr;
    }


    public static string GetFieldJson_ChildResource(string keywordParameters,string ParentKeyWord,string MenuResID, UserInfo oEmployee)
    { 
        string UserID = oEmployee.ID;
        string json = "[[";
        SysSettings sys = Common.GetSysSettingsByENKey(keywordParameters);
        if (sys == null)
        {
            json += "]]";
        }
        else
        {
            SysSettings sysDefault = sys;
            if (keywordParameters.Contains("--"))
            {
                keywordParameters = keywordParameters.Substring(0, keywordParameters.IndexOf("--"));
                sysDefault = Common.GetSysSettingsByENKey(keywordParameters);
            }

            Services Resource = new Services();
            string[] changePassWord = Common.getChangePassWord();
            DataTable dt = Resource.SelectData("select ChildNum Code,* from MasterTableAssociation where MasterKeyWord='" + CommonMethod.FilterSql(ParentKeyWord.Trim()) + "' and ChildKeyWord='" +CommonMethod.FilterSql(sysDefault.KeyWordValue)+ "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            string ChildModuleCode = "";
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ChildModuleCode = dr["Code"].ToString();
                if (DbField.GetBool(ref dr, "AllowAdd")) sys.IsAdd = true;
                else sys.IsAdd = false;
                if (DbField.GetBool(ref dr, "AllowEdit")) sys.IsUpdate = true;
                else sys.IsUpdate = false;
                if (DbField.GetBool(ref dr, "AllowDel")) sys.IsDelete = true;
                else sys.IsDelete = false;
                if (DbField.GetBool(ref dr, "AllowExport")) sys.IsExp = true;
                else sys.IsExp = false;

            }

            DataRow[] drRights = null;
            
            if (oEmployee.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
            { 
               if(sys.IsUpdate) sys.IsUpdateRights = true;
               if (sys.IsDelete) sys.IsDeleteRights = true;
               if (sys.IsExp) sys.IsExpRights = true;
               if (sys.IsAdd) sys.IsAddRights = true;
            }
            else
            {
                if (MenuResID==null || MenuResID.Trim() == "") MenuResID = "0";
                changePassWord = Common.getChangePassWord();
                DataTable dt1 = Resource.SelectData("select * from cms_resource where RES_COMMENTS='" + CommonMethod.FilterSql(keywordParameters) + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                if (MenuResID == "0" && dt1.Rows.Count > 0)
                {
                    MenuResID = dt1.Rows[0]["ID"].ToString();
                }
                DataTable dtRights = Resource.GetAllPortalOperationByResourceIDAndUserID(oEmployee.ID, MenuResID, false); 
                drRights = dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='" + ChildModuleCode + "'");
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='" + ChildModuleCode + "' and RightsValue='2'").Length > 0 && sys.IsAdd)
                    sys.IsAddRights = true;
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='" + ChildModuleCode + "' and RightsValue=3").Length > 0 && sys.IsUpdate)
                    sys.IsUpdateRights = true;
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='" + ChildModuleCode + "' and RightsValue=4").Length > 0 && sys.IsDelete)
                    sys.IsDeleteRights = true;
                if (dtRights.Select("ID='" + MenuResID + "' and ChildModuleCode='" + ChildModuleCode + "' and RightsValue=6").Length > 0 && sys.IsExp)
                    sys.IsExpRights = true;
            }
            json += GetFieldValue(sys, oEmployee, drRights) + "]]";
            json += "[#]";
            json += "[" + Newtonsoft.Json.JsonConvert.SerializeObject(sys) + "]";
            json += "[#]";
            json += "[" + GetUserDefinedToolBars(sysDefault, oEmployee, drRights) + "]";
        }
        return json;
    }

    public static string GetUserDefinedToolBars(SysSettings sys, WebServices.UserInfo oEmployee, DataRow[] drRights)
    { 
        string json = "";
        List<UserDefinedToolBar> oUserDefinedToolBar = UserDefinedToolBar.GetUserDefinedToolBars(sys.KeyWordValue, "");
     
        if (oEmployee.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
        {
            for (int i = 0; i < oUserDefinedToolBar.Count; i++)
            {
                json += "," + Newtonsoft.Json.JsonConvert.SerializeObject(oUserDefinedToolBar[i]); 
            }
        }
        else if (drRights != null && drRights.Length > 0)
        {
            Log.Error("drRights:" + drRights.Length.ToString());
            for (int i = 0; i < oUserDefinedToolBar.Count; i++)
            {
                string OperationCode = oUserDefinedToolBar[i].OperationCode.ToString();
                for (int j = 0; j < drRights.Length; j++)
                {
                    if (drRights[j]["RightsValue"].ToString() == OperationCode.Trim())
                    {
                        json += ","+ Newtonsoft.Json.JsonConvert.SerializeObject(oUserDefinedToolBar[i]) ; 
                        break;
                    }
                }
            }
        }
        if (json.Trim() != "") json = json.Substring(1);
        return json;
    }

    private static string GetFieldValue(SysSettings sys, WebServices.UserInfo oEmployee, DataRow[] drRights)
    {
        WebServices.Services Resource = new WebServices.Services();
         string gridField = "";
        string ResID = Resource.GetTopParentID(sys.ResID).ToString(); 
        string strSql = "select * from ResourceColumn R join (select D.CD_COLNAME,D.CD_DISPNAME,S.CS_SHOW_WIDTH from CMS_TABLE_SHOW S join CMS_TABLE_DEFINE D on S.CS_RESID=D.CD_RESID and S.CS_COLNAME=D.CD_COLNAME where CS_RESID  in (select (case when IsNull([RES_TYPE],0)=1 then RES_PID2 else ID end) from cms_resource where id=" + ResID + ")) S on  R.ColumnName=S.CD_DISPNAME where KeyWord='" + sys.KeyWordValue + "' order by OrderNum;";
        strSql += "select OpNum 操作编号 ,ENKeyWord 参数关键字 ,LinkCol 链接字段 ,OperateType 操作类型 ,LinkTarget 链接跳转方式 ,LinkUrl 链接地址,FunctionName Function名称  ,LinkOrFnParameters 链接或方法参数 , Width 打开的页面宽度 ,Height 打开的页面高度 ,IsEnabled 是否启用,Title 跳转页面标题  from Sys_ColOperateSettings where ENKeyWord='" + sys.KeyWordValue.Trim() + "' and IsNull(IsEnabled,0)=1 order by OrderNum";
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]);

        //左对齐的字段
        List<string> alignColList = new List<string>();
        for (int i = 0; i < sys.AlignColStr.Split(',').Length; i++)
        {
            alignColList.Add(sys.AlignColStr.Split(',')[i]);
        }
         if(sys.IsCheckBox )
             gridField += "{field: 'ID',title:'',checkbox:true,width:30, sortable:false ,align:'center'},";
        DataTable fieldUrlDt = ds.Tables[1];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dr = ds.Tables[0].Rows[i];
            int temp = 0;
            string width = "80";
            string alignType = "center";
            string ColumnName = DbField.GetStr(ref dr, "ColumnName");
            string ShowColumnName = DbField.GetStr(ref dr, "ShowColumnName");
            string strDataType = DbField.GetStr(ref dr, "DateType");
            if (alignColList.Contains(ColumnName))
            {
                alignType = "left";
            }
            #region "设置字段width"

            if (sys.IsStartWidth)//如果启动字段width设置
            {
                width = DbField.GetStr(ref dr, "CS_SHOW_WIDTH");
            }
            else
            {
                width = "100";
            }
            #endregion 
            string fieldUrl = GetFieldUrl(fieldUrlDt, ColumnName,sys.KeyWordValue ,sys.ShowTitle.Trim(), sys.IsUpdate, sys.IsDelete);
            string sortable = "true";
            if (sys.IsOrder)
            {
                sortable = "false";
            }
            if (fieldUrl != "")
            {
                gridField += "{field: '" + ColumnName + "',title:'" + ShowColumnName + "',width:" + width + ", sortable: " + sortable + ",align:'" + alignType + "',formatter: function (value, row, index) {" + fieldUrl + " return s;} },";
            }
            else
            {
                if (strDataType.Trim() == "bit")
                {
                    string strEnable = "s='<input type=\"checkbox\" name=\"ch_IsEnable\" value=\"_blank\" id=\"ch_IsEnable\" disabled=\"true\"  '+(row." + ColumnName + "==\"1\"?\"Checked\":\"\")+' />';"; 
                    gridField += "{field: '" + ColumnName + "',title:'" + ShowColumnName + "',width:" + width + ", sortable: " + sortable + " ,align:'" + alignType + "',formatter: function (value, row, index) {" + strEnable + " return s;}},";
                }
                else gridField += "{field: '" + ColumnName + "',title:'" + ShowColumnName + "',width:" + width + ", sortable: " + sortable + " ,align:'" + alignType + "'},";
            }
            temp = 1;
        }

        string strOperationField = "";
        DataRow[] CZRows = fieldUrlDt.Select("操作类型='操作'");
        if (CZRows.Length > 0)
        {
            
            string fieldTypeStr ="";
            int intwidth = 0;
            if (oEmployee.DepartmentName.Trim() ==CommonProperty.ManageDepartmentName.Trim())
            {
                for (int i = 0; i < CZRows.Length; i++)
                {
                    intwidth += CZRows[i]["链接字段"].ToString().Trim().Length * 18;
                    if (i > 0)
                    {
                        fieldTypeStr += "s+='&nbsp;||&nbsp;';";
                    }
                    fieldTypeStr += ModleOperation(CZRows[i], sys.ShowTitle.Trim());
                } 
            }
            else if (drRights != null && drRights.Length > 0)
            {
                for (int i = 0; i < CZRows.Length; i++)
                {
                    string OperationCode = CZRows[i]["操作编号"].ToString(); 
                    if (i > 0)
                    {
                        fieldTypeStr += "s+='&nbsp;||&nbsp;';";
                       // intwidth += 10;
                    }
                    for (int j = 0; j < drRights.Length; j++)
                    {
                        
                        if (drRights[j]["RightsValue"].ToString() == OperationCode.Trim())
                        {
                            fieldTypeStr += ModleOperation(CZRows[i], sys.ShowTitle.Trim());
                            intwidth += (CZRows[i]["链接字段"].ToString().Length) * 18;
                            break;
                        }
                    }
                } 
            } 
            if (fieldTypeStr.Trim()!="")
            {
                strOperationField += "{field: '操作',title:'操作',width:" + intwidth + ", sortable:false,align:'center',formatter: function (value, row, index) { var s='';" + fieldTypeStr + " return s;} },";
            }
        }
         
        DataRow[] LCRows = fieldUrlDt.Select("操作类型='流程操作'");
        if (LCRows.Length > 0)
        {
            int intwidth = 0;
            string fieldTypeStr = ""; 
            string str = "";
            if (oEmployee.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
            {
                for (int i = 0; i < LCRows.Length; i++)
                {
                    if (i > 0)
                    {
                        fieldTypeStr += "s+='&nbsp;||&nbsp;';";
                    }
                    str += FlowOperation(LCRows[i]);
                    intwidth += (LCRows[i]["链接字段"].ToString().Length) * 18;
                }
            }
            else if (drRights != null && drRights.Length > 0)
            {
                for (int i = 0; i < LCRows.Length; i++)
                {
                    string OperationCode = LCRows[i]["操作编号"].ToString();

                    if (i > 0)
                    {
                        fieldTypeStr += "s+='&nbsp;||&nbsp;';";
                    }
                    for (int j = 0; j < drRights.Length; j++)
                    {
                        if (drRights[j]["RightsValue"].ToString() == OperationCode.Trim())
                        {
                            str += FlowOperation(LCRows[i]);
                            intwidth += (LCRows[i]["链接字段"].ToString().Length )*18;
                            break;
                        }
                    }
                }
            } 
            fieldTypeStr = fieldTypeStr + str;

            if (fieldTypeStr.Trim() != "")
            {
                strOperationField += "{field: '流程操作',title:'流程操作',width:" + intwidth + ", sortable:false,align:'center',formatter: function (value, row, index) {var s='';" + fieldTypeStr + " return s;} },";
            }
        }

        DataRow[] ZDYRows = fieldUrlDt.Select("操作类型='自定义类型'");
        if (ZDYRows.Length > 0)
        {
            for (int i = 0; i < ZDYRows.Length; i++)
            {

                int FieldWidth = ZDYRows[i]["链接或方法参数"] == null ? 80 : Convert.ToInt32(ZDYRows[i]["链接或方法参数"]);
                string DialogHeight = ZDYRows[i]["打开的页面高度"].ToString();
                string DialogWidth = ZDYRows[i]["打开的页面宽度"].ToString();

                string HeaderStr = ",formatter: function (value, row, index) { var s= SetFieldformatterByCom('" + sys.ENTableName + "',value, row, index,'" + (false ? "1" : "") + "','" + ZDYRows[i]["链接字段"] + "','" + DialogWidth + "','" + DialogHeight + "'); return s;}}";

                gridField += ("{field: '" + ZDYRows[i]["链接字段"] + "',title:'" + ZDYRows[i]["链接字段"] + "',width:" + FieldWidth + ", sortable:false,align:'center'" + HeaderStr + ",");
            }
        }
         
        //如果自定义，操作列，不为空，则截取最后一个空格
        if (strOperationField != "")
        {
            strOperationField = strOperationField.Substring(0, strOperationField.Length - 1);
        }
        else
        {
            gridField = gridField.Substring(0, gridField.Length - 1);
        }
        return gridField + strOperationField;
    }

    private static string FlowOperation(DataRow LCRows)
    {
        string str = "";
        string fieldUrlType = LCRows["链接跳转方式"].ToString();
        string PageUrlName = LCRows["链接地址"].ToString();
        string FunctionName = LCRows["Function名称"].ToString();
        string linkUrl = LCRows["链接或方法参数"].ToString();
        string urlType = LCRows["操作类型"].ToString();
        string DialogHeight = LCRows["打开的页面高度"].ToString();
        string DialogWidth = LCRows["打开的页面宽度"].ToString();
        string[] linkStr = linkUrl.Split(',');
        string newLinkStr = "";
        if (!PageUrlName.Contains("?")) newLinkStr = "?";
        else newLinkStr = "&";
        if (linkStr.Length > 0)
        {
            if (linkStr[0] != "")
            {
                for (int j = 0; j < linkStr.Length; j++)
                {
                    string[] item = linkStr[j].Split('=');
                    newLinkStr += item[0] + "=";
                    if (item[1].Contains("[") && item[1].Contains("]"))
                    {
                        newLinkStr += item[1].Replace("[", "").Replace("]", "");
                    }
                    else
                    {
                        newLinkStr += "'+row." + item[1] + "+'";
                    } 
                }
            }
        }
        if (str != "")
        {
            str += "  s+=' || <a target =\"_blank\"  href=\"" + PageUrlName + newLinkStr + "\"   style=\"text-decoration: none;color: #800080;\">';s+='" + LCRows["链接字段"] + "</a>';";

        }
        else
        {
            str += "  s+='<a target =\"_blank\"  href=\"" + PageUrlName + newLinkStr + "\"   style=\"text-decoration: none;color: #800080;\">';s+='" + LCRows["链接字段"] + "</a>';";

        }
        return str;
    }

    private static string ModleOperation(DataRow CZRows,string ShowTitle)
    {
        string fieldTypeStr = "";
        string fieldUrlType = CZRows["链接跳转方式"].ToString();
        string PageUrlName = CZRows["链接地址"].ToString();
        string FunctionName = CZRows["Function名称"].ToString();
        string linkUrl = CZRows["链接或方法参数"].ToString();
        string urlType = CZRows["操作类型"].ToString();
        string DialogHeight = CZRows["打开的页面高度"].ToString();
        string DialogWidth = CZRows["打开的页面宽度"].ToString();
        string FormTitle = CZRows["跳转页面标题"].ToString();
        if (FormTitle.Trim() == "") FormTitle = ShowTitle;
        string[] linkStr = linkUrl.Split(',');
        string newLinkStr = "";

        string strParams = "";
        if (!PageUrlName.Contains("?")) newLinkStr = "?";
        else newLinkStr = "&";

        if (linkStr.Length > 0)
        {
            if (linkStr[0] != "")
            {
                for (int j = 0; j < linkStr.Length; j++)
                {
                    string[] item = linkStr[j].Split('=');
                    newLinkStr += item[0] + "=";
                    if (item[1].Contains("[") && item[1].Contains("]"))
                    {
                        newLinkStr += item[1].Replace("[", "").Replace("]", "");
                        if (item[1].Replace("[", "").Replace("]", "").ToLower().Trim() == "currentdate") strParams += ",\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\"";
                        else if (item[1].Replace("[", "").Replace("]", "").ToLower().Trim() == "currentdatetime") strParams += ",\"" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss") + "\"";
                        else strParams += ",\"" + item[1].Replace("[", "").Replace("]", "") + "\"";
                    }
                    else
                    {
                        newLinkStr += "\"+row." + item[1] + "+\""; ;
                        strParams += "," + "'+row." + item[1] + "+'";
                    }
                    if (j < linkStr.Length - 1)
                    {
                        newLinkStr += "&";
                    }
                }
            }
        }
        if (PageUrlName.Trim() != "")
        {
            string strFunction = "showWindow('+" + "\"'" + FormTitle.Trim() + "'\"" + "+','+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+'," + DialogWidth + "," + DialogHeight + ")";
            if (fieldUrlType == "_blank") strFunction = "window.open('+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+','+\"'" + fieldUrlType.Trim() + "'\"+', '+\"''\"+', false)";
            else if (fieldUrlType == "_parent") strFunction = "window.location.href = '+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+'";

            fieldTypeStr = "s+='<a href=\"#\" onclick=\"" + strFunction + "\" style=\"text-decoration: none;color: #800080;\">" + CZRows["链接字段"] + "</a>';";
        }
        else
        {
            if (strParams.Trim() != "") strParams = strParams.Substring(1, strParams.Length - 1);
            fieldTypeStr += "  s+='<a href=\"#\" onclick=" + FunctionName.Trim() + "(" + strParams.Trim() + ",this)" + " style=\"text-decoration: none;color: #800080;\">';  s+='" + CZRows["链接字段"] + "</a>&nbsp;&nbsp;';";
        }
        return fieldTypeStr;
    }


    #region "通过取值字段名得到该字段上的链接地址"
    private static string GetFieldUrl(DataTable fieldUrlDt, string colName, string keyWordValue, string ShowTitle, Boolean IsUpdate, Boolean IsDelete)
    {
        DataRow[] rows = fieldUrlDt.Select("链接字段='" + colName + "'");
        string fieldUrl = "";//
        if (rows.Length > 0)
        {
            string fieldUrlType = rows[0]["链接跳转方式"].ToString(); 

            string PageUrlName = rows[0]["链接地址"].ToString();
            string FunctionName = rows[0]["Function名称"].ToString();

            string linkUrl = rows[0]["链接或方法参数"].ToString();
            string urlType = rows[0]["操作类型"].ToString();
            string DialogHeight = rows[0]["打开的页面高度"].ToString();
            string DialogWidth = rows[0]["打开的页面宽度"].ToString();
            string FormTitle = rows[0]["跳转页面标题"].ToString();
            if (FormTitle.Trim() == "") FormTitle = ShowTitle;
            if (DialogWidth == "")
            {
                DialogWidth = "900";
            }
            if (DialogHeight=="")
            {
                DialogHeight = "400";
            }
            string[] linkStr = linkUrl.Split(',');
            string newLinkStr = "";
            string strParams = "";
            if (!PageUrlName.Contains("?")) newLinkStr = "?";
            else newLinkStr = "&";
            newLinkStr += "keyWordValue=" + keyWordValue + "&";
            for (int i = 0; i < linkStr.Length; i++)
            {
                if (linkStr[i].IndexOf("=") >= 0)
                {
                    string[] item = linkStr[i].Split('=');
                    newLinkStr += item[0] + "=";
                    if (item[1].Contains("[") && item[1].Contains("]"))
                    {
                        newLinkStr += item[1].Replace("[", "").Replace("]", "");
                        strParams += ",'" + item[1].Replace("[", "").Replace("]", "") + "'";
                    }
                    else
                    {
                        newLinkStr += "\"+row." + item[1] + "+\"&";
                        strParams += "," + "\"+row." + item[1] + "+\"";
                    } 
                }
            }

            newLinkStr += "IsUpdate=" + (IsUpdate?"true":"false") + "&IsDelete=" + (IsDelete?"true":"false");
            if (PageUrlName.Trim() != "")
            {
                string strFunction = "showWindow('+" + "\"'" + FormTitle + "'\"" + "+','+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+'," + DialogWidth + "," + DialogHeight + ")";
                if (fieldUrlType == "_blank") strFunction = "window.open('+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+','+\"'" + fieldUrlType.Trim() + "'\"+', '+\"''\"+', false)";
                else if (fieldUrlType == "_parent") strFunction = "window.location.href = '+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+'";

                fieldUrl = " var s='<a href=\"#\" onclick=\"" + strFunction + "\" style=\"text-decoration: none;color: #800080;\">'+value+'</a>';";
            }
            else
            {
                if (strParams.Trim() != "") strParams = strParams.Substring(1, strParams.Length - 1);
                fieldUrl += " var s='<a href=\"#\" onclick=" + FunctionName.Trim() + "(" + strParams.Trim() + ")" + " style=\"text-decoration: none;color: #800080;\">'+value+'</a>';";
            }
        }
        return fieldUrl;
    }
    #endregion



    #region "设置附件链接"
    private static string GetFileUrl()
    {
        string newLinkStr = "\"+row.路径+\"";
        string fieldUrl = " var s='<a href=\"" + newLinkStr + "\"  target=\"_blank\" >'+row.文档名+'</a>';";
        return fieldUrl;
    }
    #endregion


    #region " 通过条件查询数据，并返回Json,需要分页"
    public static string GetDataJsonOfPage(string key, string ResID, int pageNumber, int pageSize, string SortField, string SortBy, string condition, string UserID)
    {
        SysSettings sys = new SysSettings();
        if (key != "") sys = Common.GetSysSettingsByENKey(key);
        //查询组件列表这个是业态的表的ID
        PageParameter p = new PageParameter();
        p.PageIndex = pageNumber - 1;
        p.PageSize = pageSize;
        if (SortField != null && SortField != "")
        {
            p.SortField = SortField;
            p.SortBy = SortBy;
        }
        else
        {
            if (sys.DefaultOrder != "")
                p.SortField = sys.DefaultOrder;
            else
                p.SortField = "ID";
            if (sys.SortBy.Trim() != "")
                p.SortBy = sys.SortBy;
            else p.SortBy = "desc";

        }

        Services Resource = new Services();
        string RowCount = "0";
        DataTable dt = new DataTable();
        if (sys.IsRowRight)
        {
            RowCount = Resource.GetDataListRecordCount(Convert.ToInt64(ResID), UserID, FilterSql(condition), true).ToString();
            dt = Resource.GetPageOfDataList(Convert.ToInt64(ResID), true, FilterSql(condition), p, UserID).Tables[0];
        }
        else
        {
            RowCount = Resource.GetDataListRecordCount(Convert.ToInt64(ResID), UserID, FilterSql(condition)).ToString();
            dt = Resource.GetPageOfDataList(Convert.ToInt64(ResID), FilterSql(condition), p, UserID).Tables[0];
        }
        if (key != "")
        {

            #region "列表中展示子表附件"
            string GLTJStr = "";
            string ParentFJCol = "";
            string ChildFJCol = "";
            if (sys.FJGLJD.Contains("="))
            {
                ChildFJCol = sys.FJGLJD.Split('=')[0];
                ParentFJCol = sys.FJGLJD.Split('=')[1];
            }
            else
            {
                ParentFJCol = sys.FJGLJD;
                ChildFJCol = sys.FJGLJD;
            }
            string[] changePassWord = Common.getChangePassWord();
            DataTable dtColumnName = Resource.SelectData("select * from ResourceColumn where KeyWord='" + CommonMethod.FilterSql(key.Trim()) + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            if (sys.AccessoryCol != "" && sys.ResID != "" && dtColumnName.Select("ColumnName='" + sys.AccessoryCol + "'").Length > 0)
            {
                dt.Columns.Add(sys.AccessoryCol);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GLTJStr += "'" + dt.Rows[i][ParentFJCol].ToString() + "'";
                    if (i < dt.Rows.Count - 1)
                    {
                        GLTJStr += ",";
                    }
                }
                if (GLTJStr != "")
                {
                    string FJCondition = " and " + ChildFJCol + " in (" + GLTJStr + ")";
                    DataTable AccessoryDt = Resource.GetDataListByResourceID(sys.AccessoryResID, false, FJCondition).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] rows = AccessoryDt.Select(ChildFJCol + "='" + dt.Rows[i][ParentFJCol].ToString() + "'");
                        if (rows.Length > 0)
                        {
                            string fileName = rows[0]["类型"].ToString() == "" ? rows[0]["文档名"].ToString() : rows[0]["文档名"].ToString() + "." + rows[0]["类型"].ToString();
                            string filePath = "<a href='" + rows[0]["路径"].ToString() + "' target='_blank'>" + fileName + "<a/>";
                            dt.Rows[i][sys.AccessoryCol] = filePath;
                        }
                    }
                }
            }

            #endregion
            if (sys.LBLX == "文档表")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string fileName = dt.Rows[i]["类型"].ToString() == "" ? dt.Rows[i]["文档名"].ToString() : dt.Rows[i]["文档名"].ToString() + "." + dt.Rows[i]["类型"].ToString();
                    string filePath = "<a href='" + dt.Rows[i]["路径"].ToString() + "' target='_blank'>" + fileName + "<a/>";
                    dt.Rows[i]["文档名"] = filePath;
                }
            }
        }
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";

        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return "{\"total\":" + RowCount + ",\"rows\":" + str + "}";

    }

    public static string GetDataOfPageForFooter(string key, string ResID, int pageNumber, int pageSize, string SortField, string SortBy, string condition, string UserID,string footName)
    {
        SysSettings sys = new SysSettings();
        if (key != "") sys = Common.GetSysSettingsByENKey(key);
        //查询组件列表这个是业态的表的ID
        PageParameter p = new PageParameter();
        p.PageIndex = pageNumber - 1;
        p.PageSize = pageSize;
        if (SortField != null && SortField != "")
        {
            p.SortField = SortField;
            p.SortBy = SortBy;
        }
        else
        {
            if (sys.DefaultOrder != "")
                p.SortField = sys.DefaultOrder;
            else
                p.SortField = "ID";
            if (sys.SortBy.Trim() != "")
                p.SortBy = sys.SortBy;
            else p.SortBy = "desc";

        }

        Services Resource = new Services();
        string RowCount = "0";
        DataTable dt = new DataTable();
        if (sys.IsRowRight)
        {
            RowCount = Resource.GetDataListRecordCount(Convert.ToInt64(ResID), UserID, FilterSql(condition), true).ToString();
            dt = Resource.GetPageOfDataList(Convert.ToInt64(ResID), true, FilterSql(condition), p, UserID).Tables[0];
        }
        else
        {
            RowCount = Resource.GetDataListRecordCount(Convert.ToInt64(ResID), UserID, FilterSql(condition)).ToString();
            dt = Resource.GetPageOfDataList(Convert.ToInt64(ResID), FilterSql(condition), p, UserID).Tables[0];
        }
        if (key != "")
        {

            #region "列表中展示子表附件"
            string GLTJStr = "";
            string ParentFJCol = "";
            string ChildFJCol = "";
            if (sys.FJGLJD.Contains("="))
            {
                ChildFJCol = sys.FJGLJD.Split('=')[0];
                ParentFJCol = sys.FJGLJD.Split('=')[1];
            }
            else
            {
                ParentFJCol = sys.FJGLJD;
                ChildFJCol = sys.FJGLJD;
            }
            string[] changePassWord = Common.getChangePassWord();
            DataTable dtColumnName = Resource.SelectData("select * from ResourceColumn where KeyWord='" + key.Trim() + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            if (sys.AccessoryCol != "" && sys.ResID != "" && dtColumnName.Select("ColumnName='" + sys.AccessoryCol + "'").Length > 0)
            {
                dt.Columns.Add(sys.AccessoryCol);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GLTJStr += "'" + dt.Rows[i][ParentFJCol].ToString() + "'";
                    if (i < dt.Rows.Count - 1)
                    {
                        GLTJStr += ",";
                    }
                }
                if (GLTJStr != "")
                {
                    string FJCondition = " and " + ChildFJCol + " in (" + GLTJStr + ")";
                    DataTable AccessoryDt = Resource.GetDataListByResourceID(sys.AccessoryResID, false, FJCondition).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] rows = AccessoryDt.Select(ChildFJCol + "='" + dt.Rows[i][ParentFJCol].ToString() + "'");
                        if (rows.Length > 0)
                        {
                            string fileName = rows[0]["类型"].ToString() == "" ? rows[0]["文档名"].ToString() : rows[0]["文档名"].ToString() + "." + rows[0]["类型"].ToString();
                            string filePath = "<a href='" + rows[0]["路径"].ToString() + "' target='_blank'>" + fileName + "<a/>";
                            dt.Rows[i][sys.AccessoryCol] = filePath;
                        }
                    }
                }
            }

            #endregion
            if (sys.LBLX == "文档表")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string fileName = dt.Rows[i]["类型"].ToString() == "" ? dt.Rows[i]["文档名"].ToString() : dt.Rows[i]["文档名"].ToString() + "." + dt.Rows[i]["类型"].ToString();
                    string filePath = "<a href='" + dt.Rows[i]["路径"].ToString() + "' target='_blank'>" + fileName + "<a/>";
                    dt.Rows[i]["文档名"] = filePath;
                }
            }
        }
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        if (footName != null && footName != "")
        {
            DataTable sumDt = Resource.GetFieldNameSumList(Convert.ToInt64(ResID), UserID, condition, false, footName);
            string[] nameList = footName.Split(',');
            string footerStr = " ,\"footer\":[{\"" + nameList[0] + "\":\"查询合计\"";
            for (int i = 1; i < nameList.Length; i++)
            {
                int isHaveColums = 0;
                for (int j = 0; j < sumDt.Columns.Count; j++)
                {
                    if (sumDt.Columns[j].ColumnName == nameList[i])
                    {
                        isHaveColums = 1;
                    }
                }
                if (isHaveColums == 1)
                {
                    if (sumDt.Rows.Count>0)
                    {
                        footerStr += ",\"" + nameList[i] + "\":\"" + Convert.ToDouble(sumDt.Rows[0][nameList[i]].ToString()).ToString("###,###.00") + "\"";
                    }
                    else
                    {
                        footerStr += ",\"" + nameList[i] + "\":\" \"";
                    }
                }
            }
            footerStr += "}]";
            str += footerStr;
        }
        return "{\"total\":" + RowCount + ",\"rows\":" + str + "}";
    }

    public static string GetDataJsonOfPageByTableName(string key, string ResID, int pageNumber, int pageSize, string condition, string UserID)
    {

        SysSettings sys = new SysSettings();
        if (key != "")
        {
            sys = Common.GetSysSettingsByENKey(key);
           
        } 

        if(sys.DefaultOrder.Trim()=="") sys.DefaultOrder="ID";
        if (sys.SortBy.Trim() == "") sys.DefaultOrder = "asc";



        Services Resource = new Services();
        string strSql="select * from "+sys.TableName .Trim()+ " where 1=1 "+ condition .Trim()+";";
        strSql += "select Top " + pageSize + " * from " + sys.TableName.Trim() + " where ID not in(select top " + (pageNumber-1) * pageSize + " ID from " + sys.TableName.Trim() + " where 1=1 " + condition.Trim() + " order by " + sys.DefaultOrder + " " + sys.SortBy + ") " + condition.Trim() + " order by " + sys.DefaultOrder + " " + sys.SortBy + ";";
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]);
        string RowCount = ds.Tables[0].Rows.Count.ToString();// Resource.GetDataListRecordCount(Convert.ToInt64(ResID), UserID, condition).ToString();
        DataTable dt = ds.Tables[1];// Resource.GetPageOfDataList(Convert.ToInt64(ResID), condition, p, UserID).Tables[0];
         
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return "{\"total\":" + RowCount + ",\"rows\":" + str + "}";
    }


    public static string GetDataJsonByTableName(string key, string ResID,  string condition, string UserID)
    {

        SysSettings sys = new SysSettings();
        if (key != "")
        {
            sys = Common.GetSysSettingsByENKey(key);

        }

        if (sys.DefaultOrder.Trim() == "") sys.DefaultOrder = "ID";
        if (sys.SortBy.Trim() == "") sys.DefaultOrder += " asc";



        Services Resource = new Services();
        //string strSql = "select * from " + sys.TableName.Trim() + " where 1=1 " + condition.Trim() + ";";
       // strSql += "select Top " + pageSize + " * from " + sys.TableName.Trim() + " where ID not in(select top " + (pageNumber - 1) * pageSize + " ID from " + sys.TableName.Trim() + " where 1=1 " + condition.Trim() + " order by " + sys.DefaultOrder + " " + sys.SortBy + ") " + condition.Trim() + " order by " + sys.DefaultOrder + " " + sys.SortBy + ";";
        string strSql = "select * from " + sys.TableName.Trim() + " where 1=1 " + condition.Trim() + ( sys.DefaultOrder .Trim()==""?"": " order by " + sys.DefaultOrder + " " + sys.SortBy) + ";";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];// Resource.GetPageOfDataList(Convert.ToInt64(ResID), condition, p, UserID).Tables[0];

        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return "{\"total\":" + dt.Rows.Count + ",\"rows\":" + str + "}";
    }

    #endregion     



    #region "通过列表配置 通过条件查询数据，并返回Json,不可以分页"
    public static string GetDataJson(string ResID, string orderby, string condition, string UserID)
    { 
        Services Resource = new Services();
        DataTable dt= Resource.GetDataListByResID(ResID, orderby, condition, "").Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return "{\"total\":" + dt.Rows.Count + ",\"rows\":" + str + "}";
    }

    public static string GetDataJson(string ParentResID, string ResID, string ParentRecID, string orderby, string UserID)
    {
        Services oServices = new Services();
        DataTable dt = oServices.GetDataListByParentRecID(ParentResID, ResID, ParentRecID);
         Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        //JsonSerializerSettings jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = JsonConvert.SerializeObject(dt, timeConverter);
        str = str.Replace(":null", ":''");
        return "{\"total\":" + dt.Rows.Count + ",\"rows\":" + str + "}"; 
    }

    #endregion

    #region "获取资源查询字段，并组装SQL"
    public static string GetResouceCondtion(string key,string where)
    {
        //将多个查询字段，拼接成 (户编号 like '%{0}%' OR 客户名称 like '%{0}%' )
        string strSql = " select stuff((select [SearchCol]+ ' like #%{0}%# OR ' from Sys_CXZJLB where keyword = A.keyword for xml path('')),1,0,'')Search from Sys_CXZJLB A  ";
        strSql += string.Format(" where A.keyword = '{0}' group by keyword ", key);

        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();

        DataTable dtStr = Resource.Query(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        if (dtStr.Rows.Count == 0)
            return "";

        string Condition = dtStr.Rows[0]["Search"].ToString();
        Condition = string.Format(" and (" + Condition.Substring(0, Condition.Length - 4).Replace("#", "'") + ")", where);
        return Condition;         
    }
    #endregion

    #region "列表初始化筛选Sql转换"
    public static string GetFilterCondition(string Condition, UserInfo User)
    {
        string conditionStr = "";
        try
        {
            string[] condtionAry = Condition.Split(',');
            for (int i = 0; i < condtionAry.Length; i++)
            {
                if (condtionAry[i].IndexOf("=") > 0)
                {//截取等号后面的部分，如：姓名=$uName
                    string str1 = condtionAry[i].Substring(0, condtionAry[i].IndexOf("=")+1);//姓名=
                    string str3 = condtionAry[i].Substring(0, condtionAry[i].IndexOf("="));//姓名
                    string str2 = condtionAry[i].Substring(condtionAry[i].IndexOf("=") + 1);//$uName
                    if (str2 != "")
                    {
                        if (conditionStr.Length > 0)
                            conditionStr += " and "; 
                        if (str2.IndexOf("$") > -1)//系统值
                        {
                            string str = "";
                            if (str2.ToLower().Equals("$uid"))
                                str = "isnull(" + str3 + ",'')='" + User.ID + "'";
                            else if (str2.ToLower().Equals("$uname"))
                                str = "isnull(" + str3 + ",'')='" + User.Name + "'";
                            else if (str2.ToLower().Equals("$udepname"))
                                str = "isnull(" + str3 + ",'')='" + User.DepartmentName + "'";
                            else if (str2.ToLower().Equals("$date"))
                                str = "isnull(" + str3 + ",'')='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                            else if (str2.ToLower().Equals("$year"))
                                str = "isnull(" + str3 + ",'')='" + DateTime.Now.Year + "'";
                            else if (str2.ToLower().Equals("$month"))
                                str = "isnull(" + str3 + ",'')='" + DateTime.Now.Month + "'";
                            else if (str2.ToLower().Equals("$null"))
                                str = "isnull(" + str3 + ",'')=''";
                            if (str != "")
                                conditionStr += str;
                        }
                        else
                        {//固定值
                            if (!IsNum(str2.Trim()))//判断固定值类型
                                conditionStr += condtionAry[i];
                            else
                                conditionStr += str1 + "'" + str2.Trim() + "'";

                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            return "";
        }
        if (conditionStr.Length > 0)
            conditionStr = " and " + conditionStr;
        return conditionStr;
    }
    #endregion

    #region 类型判断
    public static bool IsNum(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (!Char.IsNumber(text, i))
            {
                return true;  //不是数字  
            }
        }
        return false;  //是数字
    }
    #endregion

    #region "日常事务"
    //记录日常
    public static string AddJLRC(string RecID, string wsJson, string swdpJson, string UserID)
    {
        Services Resource = new Services();
        string ResID = "379270212296";//日常事务表ResID
        RecordInfo Parentfieldinfo = new RecordInfo();
        Parentfieldinfo.RecordID = RecID;
        Parentfieldinfo.ResourceID = ResID;
        Parentfieldinfo.FieldInfoList = GetParentInfoOfRCSW(wsJson).ToArray();
        RecordInfo[][] s = new RecordInfo[1][];
        s[0] = GetChildInfoOfRCSW(swdpJson).ToArray();
        return Resource.Add(UserID, Parentfieldinfo, s).ToString();
    }
    //任务派发
    public static string AddRWPF(string RecID, string wsJson, string swdpJson, string ygyStr, string type, string UserID)
    {
        Services Resource = new Services();
        string ResID = "379270212296";//日常事务表ResID
        RecordInfo Parentfieldinfo = new RecordInfo();
        Parentfieldinfo.RecordID = RecID;
        Parentfieldinfo.ResourceID = ResID;
        Parentfieldinfo.FieldInfoList = GetParentInfoOfRCSW(wsJson).ToArray();
        RecordInfo[][] s = new RecordInfo[1][];
        if (type == "店铺")
        {
            s[0] = GetChildInfoOfRCSW(swdpJson).ToArray();
        }
        else
        {
            s[0] = GetChildInfoOfSWYGY(ygyStr).ToArray();
        }
        return Resource.Add(UserID, Parentfieldinfo, s).ToString();
    }

    //任务派发-营管员的处理方法    
    public static string RWPFCLOfYGY(string RecID, string swygyJson, string swdpJson, string UserID)
    {
        Services Resource = new Services();
        string ResID = "420224113562";//事务营管员ResID;
        RecordInfo Parentfieldinfo = new RecordInfo();
        Parentfieldinfo.RecordID = RecID;
        Parentfieldinfo.ResourceID = ResID;
        Parentfieldinfo.FieldInfoList = GetParentInfoOfRCSW(swygyJson).ToArray();
        RecordInfo[][] s = new RecordInfo[1][];
        s[0] = GetChildInfoOfRCSW(swdpJson).ToArray();
        return Resource.Add(UserID, Parentfieldinfo, s).ToString();
    }


    //记录日常和任务派发的主表添加字段
    private static List<FieldInfo> GetParentInfoOfRCSW(string wsJson)
    {
        DataTable dt = JsonToDataTable(wsJson);
        List<FieldInfo> fieldList = new List<FieldInfo>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            FieldInfo fi = new FieldInfo();
            fi.FieldDescription = dt.Columns[i].ColumnName;
            fi.FieldValue = dt.Rows[0][dt.Columns[i].ColumnName].ToString();
            fieldList.Add(fi);
        }
        return fieldList;
    }
    //记录日常和任务派发子表添加字段（任务派发时只有当选择店铺时有效）
    private static List<RecordInfo> GetChildInfoOfRCSW(string json)
    {
        DataTable dt = JsonToDataTable(json);
        string ResID = "412336189357";//日常事务下的事务店铺ResID
        List<RecordInfo> InfoList = new List<RecordInfo>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            RecordInfo info = new RecordInfo();
            info.RecordID = "0";
            info.ResourceID = ResID;
            List<FieldInfo> FieldList = new List<FieldInfo>();
            FieldInfo fi = new FieldInfo();
            fi.FieldDescription = "方案系统编号";
            fi.FieldValue = dt.Rows[i]["方案系统编号"].ToString();
            FieldList.Add(fi);

            fi = new WebServices.FieldInfo();
            fi.FieldDescription = "店铺名称";
            fi.FieldValue = dt.Rows[i]["店铺名称"].ToString();
            FieldList.Add(fi);

            fi = new WebServices.FieldInfo();
            fi.FieldDescription = "营管员";
            fi.FieldValue = dt.Rows[i]["营管员"].ToString();
            FieldList.Add(fi);

            fi = new WebServices.FieldInfo();
            fi.FieldDescription = "营运经理";
            fi.FieldValue = dt.Rows[i]["营运经理"].ToString();
            FieldList.Add(fi);

            fi = new WebServices.FieldInfo();
            fi.FieldDescription = "楼层";
            fi.FieldValue = dt.Rows[i]["楼层"].ToString();
            FieldList.Add(fi);

            fi = new WebServices.FieldInfo();
            fi.FieldDescription = "业态";
            fi.FieldValue = dt.Rows[i]["业态"].ToString();
            FieldList.Add(fi);

            fi = new WebServices.FieldInfo();
            fi.FieldDescription = "铺位编号";
            fi.FieldValue = dt.Rows[i]["铺位编号"].ToString();
            FieldList.Add(fi);

            info.FieldInfoList = FieldList.ToArray();
            InfoList.Add(info);
        }
        return InfoList;
    }
    //任务派发子表添加字段,如果选择营管员
    private static List<RecordInfo> GetChildInfoOfSWYGY(string str)
    {
        string[] array = str.Split(',');
        string ResID = "420224113562";//日常事务下的事务店铺ResID
        List<RecordInfo> InfoList = new List<RecordInfo>();
        for (int i = 0; i < array.Length; i++)
        {
            RecordInfo info = new RecordInfo();
            info.RecordID = "0";
            info.ResourceID = ResID;
            List<FieldInfo> FieldList = new List<FieldInfo>();
            FieldInfo fi = new FieldInfo();
            fi.FieldDescription = "营管员";
            fi.FieldValue = array[i];
            FieldList.Add(fi);
            fi = new FieldInfo();
            fi.FieldDescription = "处理完成状态";
            fi.FieldValue = "未处理";
            FieldList.Add(fi);
            fi = new FieldInfo();
            fi.FieldDescription = "事务类型";
            fi.FieldValue = "任务派发-营管员";
            FieldList.Add(fi);
            info.FieldInfoList = FieldList.ToArray();
            InfoList.Add(info);
        }
        return InfoList;
    }
    #endregion   

     


    #region "修改项目状态"
    public static bool UpdateRow(string ResID, string RecID, string UserID, string Operation)
    {
        string strSql = "update XM_Project set XMJDZT = '" + Operation + "' where ID = " + RecID;
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        return Resource.ExecuteSql(strSql, changePassWord[0], changePassWord[1], changePassWord[2]) > 0 ? true : false;
    }
    #endregion
 

    #region "通过记录ID返回一条数据的Data,单表单记录查询"
    public static string GetOneRowByRecID(string ResID, string RecID)
    {
        WebServices.Services Resource = new WebServices.Services();
        DataTable dt = Resource.GetDataListByResourceID(ResID, false, CommonMethod.FilterSql(" and id=" + RecID)).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";

        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return str;
    }

    public static string GetDataByKeyWord(string KeyWord, string Condition)
    {
        WebServices.Services Resource = new WebServices.Services();
        SysSettings sys = Common.GetSysSettingsByENKey(KeyWord);

        DataTable dt = Resource.GetDataListByResourceID(sys.ResID, false, Condition).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return "{\"total\":" + dt.Rows.Count + ",\"rows\":" + str + "}"; 
    }
    #endregion
   

}