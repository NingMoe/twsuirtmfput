<%@ WebHandler Language="C#" Class="UserTreeData" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using WebServices;

public class UserTreeData : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        bool IsLoadUser = true; //是否加载用户
        if (context.Request["IsLoadUser"] != null) IsLoadUser = Convert.ToBoolean(Convert.ToInt32(context.Request["IsLoadUser"]));
        
        context.Response.ContentType = "text/plain";
        string json = GetJson(IsLoadUser);
        context.Response.Write(json);
        context.Response.Flush();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string GetJson(bool IsLoadUser)
    {
        Services Resource = new Services();
        string sql = "select type='部门',id,pid,name,EMP_ID='',show_order from CMS_DEPARTMENT union all select type='员工',id,host_id as pid,emp_name as name,EMP_ID,show_order from dbo.CMS_EMPLOYEE where host_id<>0 order by show_order ";
        if (!IsLoadUser) sql = "select type='部门',id,pid,name,EMP_ID='',show_order from CMS_DEPARTMENT  order by show_order";
        //string sql = "select id,pid,name,show_order from Cms_resource ";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        DataRow[] ParentRows = dt.Select(" pid=-1");
        string json = "[" + GetParentJson(ParentRows[0], dt) + "]";
        return json;
    }

    private  string GetParentJson(DataRow row, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"id\":" + row["id"].ToString() + ",");
        string UserID = row["EMP_ID"].ToString();
        if (UserID=="")
        {
            UserID = row["ID"].ToString();
        }
        sb.Append("\"attributes\":{\"url\":\"QXGrid.aspx\",\"UserID\":\"" + UserID + "\",\"Type\":\"" + row["Type"].ToString() + "\"},");
        sb.Append("\"text\":\"" + row["name"].ToString() + "\"");
        if (row["Type"].ToString() == "部门") sb.Append(",\"iconCls\":\"tree-folder-Dept\"");
        else sb.Append(",\"iconCls\":\"tree-folder-Emp\"");
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
            sb.Append(GetParentJson(rows[i], dt));
            if (i < rows.Length - 1)
            {
                sb.Append(",");
            }
        }

        sb.Append("]");
        return sb.ToString();
    }
    
    
    
    
    
}