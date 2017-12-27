<%@ WebHandler Language="C#" Class="ResourceTreeData" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using WebServices;

public class ResourceTreeData : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string PID = "";
        string ResID = "";
        bool IsEnable = false;//是否过滤不启用资源
        bool IsShowDefaultMenu = true; //不显示资源的“资源说明”
        
        if (context.Request["PID"] != null) PID = context.Request["PID"];
        if (context.Request["ResID"] != null) ResID = context.Request["ResID"];
        if (context.Request["IsShowDefaultMenu"] != null) IsShowDefaultMenu = Convert.ToBoolean(Convert.ToInt32(context.Request["IsShowDefaultMenu"]));
        if (context.Request["IsEnable"] != null) IsEnable = Convert.ToBoolean ( Convert.ToInt32( context.Request["IsEnable"]));
        
        context.Response.ContentType = "text/plain";
        string json = GetJson(ResID, PID, IsEnable, IsShowDefaultMenu);
        context.Response.Write(json);
        context.Response.Flush();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string GetJson(string ResID, string PID, bool IsEnable, bool IsShowDefaultMenu)
    { 
        Services Resource = new Services();
       
        //string strWhere = "";
        //if (!IsShowDefaultMenu)
        //{
        //    strWhere += "  and (IsNull(RES_IsDefaultMenu,0)=0 or ID='"+ResID+"')";
        //}
        //string sql = "select * from (select ID,PID,Name,RES_ICONNAME [Type],SHOW_ORDER from CMS_Resource where PID<>-1 " + (IsEnable ? " and IsNull(SHOW_ENABLE,0)=1" : "") + strWhere+ "  union select ID,PID,Name,'dept' [Type],0 SHOW_ORDER from CMS_DEPARTMENT where ID=0) T order by SHOW_ORDER";
       
        
        
       // DataTable dt = Resource.SelectData(sql).Tables[0];
        DataTable dt = Resource.GetNextDirectoryList(ResID,IsEnable, IsShowDefaultMenu);

        DataView dv = dt.DefaultView;
        dv.Sort = "SHOW_ORDER";


        dt = new DataTable();
        dt = dv.ToTable();
        if (PID.Trim() == "-1")
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = 0;
            dr["Name"] = "资源配置管理";
            dr["PID"] = "-1";
            dt.Rows.Add(dr);
        }
        DataRow[] ParentRows;
        string OpenNodeID = "0";
        if (PID.Trim() != "") ParentRows = dt.Select(" PID=" + PID);
        else
        {
            OpenNodeID = ResID;
            ParentRows = dt.Select("ID=" + ResID);
        }

        string json = "[" + GetParentJson(ParentRows[0], dt, OpenNodeID) + "]";
        //for (int i = 0; i < ParentRows.Length; i++)
        //{
        //    json += "[" + GetParentJson(ParentRows[i], dt, OpenNodeID) + "]";
        //}
        
        return json;
    }

    private  string GetParentJson(DataRow row, DataTable dt,string OpenNodeID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"id\":" + row["id"].ToString() + ",");
        string strID = row["ID"].ToString();
        if (strID == "")
        {
            strID = row["ID"].ToString();
        }
        sb.Append("\"attributes\":{\"url\":\"QXGrid.aspx\",\"strID\":\"" + strID + "\",\"Type\":\"" + row["RES_ICONNAME"].ToString() + "\"},");
        sb.Append("\"text\":\"" + row["Name"].ToString() + "\"");
        DataRow[] ChildRows = dt.Select("pid=" + row["id"].ToString());
        if (ChildRows.Length > 0)
        {
            if (row["id"].ToString() == OpenNodeID)
            {
                sb.Append(",\"state\":\"open\"");
            }
            else
            {
                sb.Append(",\"state\":\"closed\"");
            }
            sb.Append(",\"children\":" + GetChildJson(ChildRows, dt) + "");
        }
        sb.Append("}");
        return sb.ToString();
    }
    private  string GetChildJson(DataRow[] rows, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < rows.Length; i++)
        {
            sb.Append(GetParentJson(rows[i], dt,"0"));
            if (i < rows.Length - 1)
            {
                sb.Append(",");
            }
        }

        sb.Append("]");
        return sb.ToString();
    }
    
    
    
    
    
}