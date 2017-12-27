using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebServices;
using System.Text;

public partial class Base_Common_AuthorityAjax_Request : UserPagebase
{
    protected string UserID = "";
    UserInfo oEmployee = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Services Resource = new Services();
        oEmployee = CurrentUser;
        UserID = oEmployee.ID;
        string json = "";
        string typeValue = Request["typeValue"].ToString();
        if (typeValue == "DeleteGridRow")
        {
            json = DeleteGridRow();
        }
        if (typeValue == "DeleteResourceAuthority")
        {
            json = DeleteResourceAuthority();
        }

        if (typeValue == "AddRes")
        {
            string ResID = Request["ResID"].ToString();
            string ids = Request["ids"].ToString();
            string YHZH = Request["YHZH"].ToString();
            string YHMC = Request["YHMC"].ToString();
            string Type = Request["Type"].ToString();
            json = AddRes(ResID, ids, UserID, YHMC, YHZH, Type);
        }
        if (typeValue == "AddRowFilter")
        {
            string ZYID = Request["ZYID"].ToString();//资源ID
            string ZYMC = Request["ZYMC"].ToString();//资源名称
            string filedJson = Request["filedJson"].ToString();
            json = AddRowFilter(ZYID, ZYMC, filedJson);
        }

        if (typeValue == "GetQXDataJson")
        {
            int PageSize = Convert.ToInt32(Request["rows"]);
            int PageNumber = Convert.ToInt32(Request["page"]);
            string YHZH = Request["YHZH"].ToString();
            string ResID = Request["ResID"].ToString();
            json = GetQXDataJson(ResID, YHZH, PageNumber, PageSize);
        }

        if (typeValue == "UpdateQXGridRow")
        {
            string colName = "增加权限,修改权限,删除权限,复制权限,导出权限,查看权限,权限类型";
            string RecID = Request["RecID"].ToString();
            string ResID = Request["ResID"].ToString();
            string ColJson = Request["DataJson"].ToString();
            json = UpdateQXGridRow(ResID, ColJson, colName, RecID);
        }

        if (typeValue == "GetFilterDataJson")
        {
            string YHZH = Request["YHZH"].ToString();
            string ZYID = Request["ZYID"].ToString();
            string type = Request["type"].ToString();
            json = GetFilterDataJson(ZYID, YHZH, type);
        }

        if (typeValue == "GetSelectFieldJson")
        {
            string ZYID = Request["ZYID"].ToString();
            json = GetSelectFieldJson(ZYID);
        }

        if (typeValue == "UpdateFilterGridRow")
        {
            string colName = "关联逻辑";
            string RecID = Request["RecID"].ToString();
            string ResID = Request["ResID"].ToString();
            string ColJson = Request["DataJson"].ToString();
            json = UpdateFilterGridRow(ResID, ColJson, colName, RecID);
        }


        Response.Write(json);
    }

    private string DeleteResourceAuthority()
    {
        Services Resource = new Services();
        string RecID = Request["RecID"].ToString();
        string ZYID = Request["ZYID"].ToString();
        string YHZH = Request["YHZH"].ToString();
        string sql = "delete Sys_RYQXB where id=" + RecID + ";delete Sys_ZYHGLQX where ZYID='" + ZYID + "' and YHZH='" + YHZH + "'";
        try
        {
            string[] changePassWord = Common.getChangePassWord();
            Resource.ExecuteSql(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
            return  "{\"success\": true}";
        }
        catch (Exception)
        {

            return "{\"success\": false}";
        }
    }



    #region "给一个用户添加要授权的资源列表"
    private string AddRes(string ResID, string Ids, string UserID, string YHMC, string YHZH, string Type)
    {
        string QXResID = "437496957656";//人员权限表ID
        try
        {
            Services Resource = new Services();
            DataTable dt = Resource.GetDataListByResourceID(ResID, false, " and id in (" + Ids + ")").Tables[0];
            List<RecordInfo> infoList = new List<RecordInfo>();
            for (int i = 0; i < dt.Rows.Count ; i++)
            {
                RecordInfo info = new RecordInfo();
                info.RecordID = "0";
                info.ResourceID = QXResID;
                info.FieldInfoList = GetFieldList(dt.Rows[i], YHZH, YHMC, Type).ToArray();
                infoList.Add(info);
            }
            Resource.Add(UserID, infoList.ToArray());
            return "{\"success\": true}";
        }
        catch (Exception)
        {
            return "{\"success\": false}";
        }
    }
    #endregion
    private List<FieldInfo> GetFieldList(DataRow row, string YHZH, string YHMC, string Type)
    {
        List<FieldInfo> fiList = new List<FieldInfo>();

        FieldInfo fi = new FieldInfo();
        fi.FieldDescription = "用户账号";
        fi.FieldValue = YHZH;
        fiList.Add(fi);

        fi = new FieldInfo();
        fi.FieldDescription = "用户名称";
        fi.FieldValue = YHMC;
        fiList.Add(fi);

        fi = new FieldInfo();
        fi.FieldDescription = "资源ID";
        fi.FieldValue = row["资源ID"].ToString();
        fiList.Add(fi);


        fi = new FieldInfo();
        fi.FieldDescription = "资源名称";
        fi.FieldValue = row["资源名称"].ToString();
        fiList.Add(fi);

        fi = new FieldInfo();
        fi.FieldDescription = "资源描述";
        fi.FieldValue = row["资源说明"].ToString();
        fiList.Add(fi);

         fi = new FieldInfo();
         fi.FieldDescription = "资源类型";
         fi.FieldValue = row["资源类型"].ToString();
        fiList.Add(fi);
        fi = new FieldInfo();
        fi.FieldDescription = "权限类型";
        fi.FieldValue = Type;
        fiList.Add(fi);
        
        return fiList;
    }
    private string GetSelectFieldJson()
    {
        Services Resource = new Services();
        string ZYID = "";
        if (Request["ZYID"] != null)
        {
            ZYID = Request["ZYID"].ToString();
        }
        Field[] ColumnList = Resource.GetFieldListAll(ZYID);
        int i = 0;
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (Field fd in ColumnList)
        {
            sb.Append("{");
            sb.Append("\"id\":\"" + fd.Name + "\",");
            sb.Append("\"text\":\"" + fd.Description + "\"");
            sb.Append("}");
            if (i < ColumnList.Length - 1)
            {
                sb.Append(",");
            }
            i++;
        }
        sb.Append("]");
        return sb.ToString();
    }
    #region "返回字段Gird的JSON"
    /// <summary>
    /// 返回字段Gird的JSON
    /// </summary>
    /// <param name="IsCK">是否添加checkbox</param>
    /// <returns></returns>
    private string GetGridField()
    {
        string fieldStr = "外部字段,关联条件,字段条件值,关联逻辑";
        string[] fieldList = fieldStr.Split(',');
        string fieldJson = "[[";
        fieldJson += "{field: \"CK\",checkbox: true },";
        fieldJson += "{field: \"ID\",title: \"ID\",hidden: true },";
        for (int i = 0; i < fieldList.Length; i++)
        {
            if (fieldList[i] == "关联逻辑")
            {
                fieldJson += "{field: \"" + fieldList[i] + "\",title:\"" + fieldList[i] + "\",width:120, sortable: true,editor:{type:'combobox',options:{valueField:'productid',textField:'name',data:products,required:true}}}";
            }
            else
            {
                fieldJson += "{field: \"" + fieldList[i] + "\",title:\"" + fieldList[i] + "\",width:120, sortable: true }";
            }
            if (i < fieldList.Length - 1)
            {
                fieldJson += ",";
            }
        }
        fieldJson += "]]";
        return fieldJson;
    }
    #endregion
    #region “返回数据JSON”
    private string AddRowFilter(string ZYID, string ZYMC,string filedJson)
    {
        string RowFilterResID = "437496958734";
        Services Resource = new Services();
        string json = "";
        try
        {

            string[] jsonList = filedJson.Split(';');
            List<FieldInfo> list = new List<FieldInfo>();
            FieldInfo fi = null;
            for (int i = 0; i < jsonList.Length; i++)
            {
                string[] item = jsonList[i].Split(':');
                fi = new FieldInfo();
                fi.FieldDescription = item[0];
                fi.FieldValue = item[1];
                list.Add(fi);
            }
            fi = new FieldInfo();
            fi.FieldDescription = "资源ID";
            fi.FieldValue = ZYID;
            list.Add(fi);

            fi = new FieldInfo();
            fi.FieldDescription = "资源名称";
            fi.FieldValue = ZYMC;
            list.Add(fi);

            if (Resource.Add(RowFilterResID, UserID, list.ToArray()))
            {
                json = "{\"success\": true}";
            }
            else
            {
                json = "{\"success\": false}";
            }
            return json;
        }
        catch (Exception)
        {
            json = "{\"success\": false}";
            return json;
        }
    }
    #endregion
    #region "修改记录"
    private string UpdateQXGridRow(string ResID, string json, string colName, string RowID)
    {
        try
        {
            Services Resource = new Services();
            string[] jsonList = json.Split(';');
            string[] arr = colName.Split(',');
            ArrayList alist = new ArrayList(arr);
            List<FieldInfo> list = new List<FieldInfo>();
            for (int i = 0; i < jsonList.Length; i++)
            {
                string[] item = jsonList[i].Split(':');
                if (alist.Contains(item[0]))
                {
                    FieldInfo fi = new FieldInfo();
                    fi.FieldDescription = item[0];
                    fi.FieldValue = item[1];
                    list.Add(fi);
                }
            }
            if (Resource.Edit(ResID, RowID, UserID, list.ToArray()))
            {
                return "{\"success\": true}";
            }
            else
            {
                return "{\"success\": false}";
            }
        }
        catch (Exception)
        {
            return "{\"success\": false}";
        }
    }
    #endregion
    #region "资源权限表"
    private string GetQXDataJson(string ResID, string YHZH, int pageNumber, int pageSize)
    {
        Services Resource = new Services();
        string condition = " and 用户账号='" + YHZH + "'";
        PageParameter p = new PageParameter();
        p.PageIndex = pageNumber - 1;
        p.PageSize = pageSize;
        p.SortField = "ID";
        p.SortBy = "desc";
        string RowCount = Resource.GetDataListRecordCount(Convert.ToInt64(ResID), UserID, CommonMethod.FilterSql(condition)).ToString();
        DataTable dt = Resource.GetPageOfDataList(Convert.ToInt64(ResID),CommonMethod.FilterSql(condition), p, UserID).Tables[0];
        dt.Columns.Add("查询行过滤");
        dt.Columns.Add("修改行过滤");
        dt.Columns.Add("保存");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["查询行过滤"] = "<a href='javascript:' onclick=fnSetFilter(" + dt.Rows[i]["资源ID"].ToString() + ",'" + dt.Rows[i]["资源名称"].ToString() + "','查询')>条件设置</a>";
            dt.Rows[i]["修改行过滤"] = "<a href='javascript:' onclick=fnSetFilter(" + dt.Rows[i]["资源ID"].ToString() + ",'" + dt.Rows[i]["资源名称"].ToString() + "','修改')>条件设置</a>";
            dt.Rows[i]["保存"] = "<a href='javascript:'><input onclick=fnSave(" + dt.Rows[i]["id"].ToString() + ") type='image'  src='../images/bar_save.gif' /></a>";
        }
        string DataJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
        string json = "{\"total\":\"" + RowCount + "\",\"rows\":" + DataJson + "}";
        return json;
    }
  #endregion
    #region "删除"
    private string DeleteGridRow()
    {
        try
        {
            Services Resource = new Services();
          
            string ResID = Request["ResID"].ToString();
            string RecID = Request["RecID"].ToString();
            if (Resource.Delete(ResID, RecID, UserID))
            {
                return "{\"success\": true}";
            }
            else
            {
                return "{\"success\": false}";
            }
        }
        catch (Exception)
        {
            return "{\"success\": false}";
        }
    }
    #endregion



    #region “查询行过滤权限表记录”
    private string GetFilterDataJson(string ZYID, string YHZH, string type)
    {
        string FilterResID = "437496958734";
        Services Resource = new Services();
        string condition = " and 资源ID='" + ZYID + "' and 用户账号='" + YHZH + "' and 过滤类型='" + type + "'";
        DataTable dt = Resource.GetDataListByResourceID(FilterResID, false, condition).Tables[0];
        string DataJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
        string json = "{\"total\":\"" + dt.Rows.Count.ToString() + "\",\"rows\":" + DataJson + "}";
        return json;
    }
    #endregion


    //行过滤所有字段的下拉框
    private string GetSelectFieldJson(string ResID )
    {
        Services Resource = new Services();
        Field[] ColumnList = Resource.GetFieldListAll(ResID);
        int i = 0;
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (Field fd in ColumnList)
        {
            sb.Append("{");
            sb.Append("\"id\":\"" + fd.Name + "\",");
            sb.Append("\"text\":\"" + fd.Description + "\"");
            sb.Append("}");
            if (i < ColumnList.Length - 1)
            {
                sb.Append(",");
            }
            i++;
        }
        sb.Append("]");
        return sb.ToString();
    }

    #region "修改行过滤表记录"
    private string UpdateFilterGridRow(string ResID, string json, string colName, string RowID)
    {
        try
        {
            Services Resource = new Services();
            string[] jsonList = json.Split(';');
            string[] arr = colName.Split(',');
            ArrayList alist = new ArrayList(arr);
            List<FieldInfo> list = new List<FieldInfo>();
            for (int i = 0; i < jsonList.Length; i++)
            {
                string[] item = jsonList[i].Split(':');
                if (alist.Contains(item[0]))
                {
                    FieldInfo fi = new FieldInfo();
                    fi.FieldDescription = item[0];
                    fi.FieldValue = item[1];
                    list.Add(fi);
                }
            }
            if (Resource.Edit(ResID, RowID, UserID, list.ToArray()))
            {
                return "{\"success\": true}";
            }
            else
            {
                return "{\"success\": false}";
            }
        }
        catch (Exception)
        {
            return "{\"success\": false}";
        }
    }
    #endregion

}