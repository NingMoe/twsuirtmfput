using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;


partial class Common_JQueryCallService : UserPagebase
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //这个参数是用来判断接下来是要进行做什么操作的。
        string typeValue = Request.QueryString["typeValue"];
        if (typeValue == "getTreeJson")
        {
            GetWestTreeJson();  //获取左侧菜单栏的tree
        }  
       
        //else if (typeValue == "getLiuChengData")
        //{
        //    GetLiuChengData();
        //} 
    }  
    
 
    protected void GetWestTreeJson()
    {
        String ResID=Request.QueryString["ResID"];
       // String UserID = Request.QueryString["UserID"];
        string str = "[]";
        if(ResID!=""&&ResID!=null){
            str = TreeJson.GetJson(ResID, CurrentUser);
        }
        Response.Write(str);
    }

   

    //titleName 显示字段，colName值字段 sunUrlDt 连接表 ,sys 对象, isSort 是否需要排序
    public String getFieldValue(String titleName, String colName, DataTable sunUrlDt,SysSettings sys,String isSort)
    {
        String[] titleList = titleName.Split(',');
        String[] colList = colName.Split(',');
        String gridField = "";
        for (int i = 0; i <= titleList.Length - 1; i++)
        {
            bool isHaveSun = false;
            if (sunUrlDt.Rows.Count > 0)
            {
                for (int j = 0; j < sunUrlDt.Rows.Count; j++)
                {
                    if (titleList[i].ToString().Equals(sunUrlDt.Rows[j]["链接字段"]))
                    {
                        String PageUrlName = "";
                        string FunctionName = "";
                        String urlType = "";
                        String newLinkStr = "";
                        String fieldUrl = "";

                        FunctionName = sunUrlDt.Rows[j]["Function名称"].ToString();
                        if (sunUrlDt.Rows[j]["链接地址"] != null)
                        {
                            PageUrlName = sunUrlDt.Rows[j]["链接地址"].ToString();
                        }
                        if (sunUrlDt.Rows[j]["操作类型"] != null)
                        {
                            urlType = sunUrlDt.Rows[j]["操作类型"].ToString();
                        }
                        if (sunUrlDt.Rows[j]["链接或方法参数"] != null)
                        {
                            String linkUrl = sunUrlDt.Rows[j]["链接或方法参数"].ToString();
                            String[] linkStr = linkUrl.Split('=');
                            if (urlType.Equals("跳转链接"))
                            {
                                if (linkStr.Length > 0)
                                {
                                    newLinkStr = "?" + linkStr[0] + "=";
                                    for (int n = 1; n < linkStr.Length; n++)
                                    {
                                        if (linkStr[n].IndexOf(",") > 0)
                                        {
                                            String fieldValueStr = linkStr[n].Split(',')[0];
                                            if (fieldValueStr.IndexOf("[") != -1)
                                            {
                                                newLinkStr += fieldValueStr.Replace("[", "").Replace("]", "");
                                            }
                                            else
                                            {
                                                newLinkStr += "\"+row." + linkStr[n].Split(',')[0] + "+\"";
                                            }
                                            newLinkStr += "&" + linkStr[n].Split(',')[1] + "=";
                                        }
                                        else
                                        {
                                            if (linkStr[n].IndexOf("[") != -1)
                                            {
                                                newLinkStr += linkStr[n].Replace("[", "").Replace("]", "");
                                            }
                                            else
                                            {
                                                newLinkStr += "\"+row." + linkStr[n] + "+\"";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (linkStr.Length > 0)
                                {
                                    for (int n = 1; n < linkStr.Length; n++)
                                    {
                                        if (linkStr[n].IndexOf(",") > 0)
                                        {
                                            String fieldValueStr = linkStr[n].Split(',')[0];
                                            if (fieldValueStr.IndexOf("[") != -1)
                                            {
                                                newLinkStr += fieldValueStr.Replace("[", "").Replace("]", "");
                                            }
                                            else
                                            {
                                                newLinkStr += "\"+row." + linkStr[n].Split(',')[0] + "+\"";
                                            }
                                            newLinkStr += ",";
                                        }
                                        else
                                        {
                                            if (linkStr[n].IndexOf("[") != -1)
                                            {
                                                newLinkStr += linkStr[n].Replace("[", "").Replace("]", "");
                                            }
                                            else
                                            {
                                                newLinkStr += "\"+row." + linkStr[n] + "+\"";
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        String fieldUrlType = "";
                        if (sunUrlDt.Rows[j]["链接跳转方式"] != null)
                        {
                            fieldUrlType = sunUrlDt.Rows[j]["链接跳转方式"].ToString();
                        }
                        if (PageUrlName.Trim()!="")
                        {
                            fieldUrl = " var s='<a href=\"#\" onclick=\"fnFormListDialog('+" + "\"'" + PageUrlName + newLinkStr + "'\"" + "+')\" style=\"text-decoration: none;color: #800080;\">'+value+'</a>';";
                        }
                        else
                        {
                            fieldUrl = " var s='<a href=\"#\"  onclick=\"" + FunctionName + "('+" + "\"'" + newLinkStr + "'\"" + "+')\" style=\"text-decoration: none;color: #800080;\">'+value+'</a>';";
                        }
                        String alignType = "center";
                        if (sys.AlignColStr.IndexOf(colList[i]) != -1)
                        {
                            alignType = "left";
                        }
                        gridField += "{field: '" + colList[i] + "',title:'" + titleList[i] + "',width:100, sortable: " + isSort + ",align:'" + alignType + "',formatter: function (value, row, index) {" + fieldUrl + " return s;} }";
                        isHaveSun = true;
                    }
                }
            }
            if (!isHaveSun)
            {
                String alignType = "center";
                if (sys.AlignColStr.IndexOf(colList[i]) != -1)
                {
                    alignType = "left";
                }
                gridField += "{field: '" + colList[i] + "',title:'" + titleList[i] + "',width:100, sortable: " + isSort + " ,align:'" + alignType + "'}";
            }
            if (i != titleList.Length - 1)
            {
                gridField += ",";
            }
        }
        return gridField;
    }
        
    //protected void GetLiuChengData()
    //{
    //    string condition = "";
    //    String type = Request["type"].ToString();
       
    //    string index = Request["index"].ToString();
    //    int rowCount;
    //    string Path = "";
    //    //分页
    //    int size = Convert.ToInt32(Request["pageSize"]);
    //    int PageIndex = Convert.ToInt32(Request["pageNumber"]) - 1;
    //    //查询
    //    string searchTextValue = "";
    //    if(Request["searchTextValue"]!=null){
    //        searchTextValue = Request["searchTextValue"];
    //    }
    //    if (searchTextValue != "")
    //    {
    //        condition = " (Description like '%" + searchTextValue + "%' or ProcessName like '%" + searchTextValue + "%'  or SerialNum like '%" + searchTextValue + "%' )";
    //    }
    //    //排序
    //    String SortStr = "";
    //    String SortField = "";
    //    String SortBy = "";
    //    if (Request["SortField"] != null)
    //    {
    //        SortField = Request["SortField"];
    //    }
    //    if (Request["SortBy"] != null)
    //    {
    //        SortBy = Request["SortBy"];
    //    }
    //    if(SortField!=null&&!SortField.Equals("")){
    //        SortStr = SortField + " " + SortField;
    //    }
       
    //    DataTable dt = new DataTable();
    //    if (type == "MyTask")
    //    {
    //        dt = Common.GetMyTaskTable(Path, PageIndex * size, size, out  rowCount, SortStr, condition);
    //    }
    //    else
    //    {
    //        dt = Common.GetHistoryTable(Path, type, PageIndex * size, size, out rowCount, SortStr, condition);
    //    }
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        string ss = dt.Rows[i]["Description"].ToString().Replace("</br>", "");
    //        int s = ss.IndexOf("<content>");
    //        if (s >= 0)
    //        {
    //            string kk = ss.Substring(s);
    //            int e = kk.IndexOf("</content>");
    //            ss = kk.Substring(0, e).Replace("<content>", "");
    //            dt.Rows[i]["Description"] = ss;
    //        }

    //    }
    //    string DataJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
    //    string json = "{\"total\":\"" + dt.Rows.Count.ToString() + "\",\"rows\":" + DataJson + "}";
    //    Response.Write(json);
    //}

    //protected void getFieldCombo()
    //{
    //    String keyValue = Request["keyWordValue"];
    //    String ResID = Request["ResID"];
    //    WebServices.Services Resource = new WebServices.Services();
    //    string gridField = "[[";
    //    //通过参数关键字查询系统设置表的数据，返回的是个对象
    //    SysSettings sys = Common.GetSysSettingsByENKey(keyValue);
    //    if (sys == null)
    //    {
    //        gridField += "]]";
    //    }
    //    else
    //    {
    //        DataTable sunUrlDt = new DataTable();
    //        string titleName = sys.TitleNameStr;
    //        string colName = sys.ColNameStr;
    //        gridField += getFieldValue(titleName, colName, sunUrlDt, sys, "true");
    //        gridField += "]]";
    //    }
    //    gridField += "[#]";//分隔符
    //    if (Request["dispname"] != null)
    //    {
    //        gridField += CommonMethod.GetDictionaryColName(ResID, Request["dispname"]);
    //    }
    //    //输出的就是成功后的msg
    //    Response.Write(gridField);
    //}

}
