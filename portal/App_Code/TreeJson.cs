using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using WebServices; 

/// <summary>
///TreeJson 的摘要说明
/// </summary>
public class TreeJson
{
	public TreeJson()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetJson(string ResID, UserInfo  oUser)
    {
        if (ResID.Equals("undefined")) ResID = "0";

        Services Resource = new Services();
        
        DataTable dt = new DataTable();
        if (oUser.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
            dt = Resource.GetNextAllDirectoryList(ResID);
        else dt = Resource.GetNextPortalTreeRoot(oUser.ID, ResID);

        DataRow[] rows=dt.Select("ID="+ResID );
        string json = "";
        if (rows!=null&&rows.Length>0)
        {
             json = "[" + GetParentJson(rows[0], dt, ResID) + "]";
        }
        else
        {
            json = "[]";
        }
        return json;
    }
    private static string GetParentJson(ResourceInfo Info,UserInfo  oUser,bool IsOpenTree)
    {
        Services Resource = new Services();
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"id\":" + Info.ID + ",");
        sb.Append("\"attributes\":{\"urlName\":\"" + Info.ResourceLinkUrl + "\",\"urlType\":\""+Info.ResourceLinkTarget+"\"},");
        sb.Append("\"text\":\"" + Info.Name + "\"");

        ResourceInfo[] ResourceInfo1;
        if(oUser.DepartmentName.Trim()==CommonProperty .ManageDepartmentName.Trim())
            ResourceInfo1 = Resource.GetNextDirectoryList( Info.ID); 
        else  ResourceInfo1 = Resource.GetNextPortalTreeRootByResourceIDAndUserID(oUser.ID , Info.ID); // CommonResource.GetNextDirectoryList_ResourceInfo(Info.ID);
        if (ResourceInfo1.Length > 0)
        {
            sb.Append(",\"children\":" + GetChildJson(ResourceInfo1, oUser) + "");
            if (IsOpenTree) sb.Append(",\"state\":\"open\"");
            else sb.Append(",\"state\":\"closed\"");
        } 
        sb.Append("}");
        return sb.ToString();
    }

    private static string GetChildJson(ResourceInfo[] ResourceInfo1, UserInfo oUser)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < ResourceInfo1.Length; i++)
        {
            sb.Append(GetParentJson(ResourceInfo1[i], oUser,false));
            if (i < ResourceInfo1.Length - 1)
            {
                sb.Append(",");
            }
        }

        sb.Append("]");
        return sb.ToString();
    }

    private static string GetParentJson(DataRow row, DataTable dt,string ResID)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"id\":" + row["id"].ToString() + ",");
        sb.Append("\"attributes\":{\"urlName\":\"" + row["res_emptyresourceurl"].ToString() + "\",\"urlType\":\"" + row["res_emptyresourcetarget"].ToString() + "\"},");
        sb.Append("\"text\":\"" + row["name"].ToString() + "\"");
        DataRow[] ChildRows = dt.Select("pid=" + row["id"].ToString());
        if (ChildRows.Length > 0)
        {
            if (row["id"].ToString() == ResID.ToString())
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
    private  static string GetChildJson(DataRow[] rows, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < rows.Length; i++)
        {
            sb.Append(GetParentJson(rows[i], dt,"-1"));
            if (i < rows.Length - 1)
            {
                sb.Append(",");
            }
        }

        sb.Append("]");
        return sb.ToString();
    }

}