using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebServices;

public partial class Common_Ajax_Request : UserPagebase
{
    string UserID = "";
    string UserName = "";
    UserInfo oEmployee = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        oEmployee =  CurrentUser;       
        UserID = oEmployee.ID;
        UserName = oEmployee.Name;
        string json = "";
        string typeValue = Request["typeValue"]; 
        if (typeValue == "GetField")
        {
            string keywordParameters = Request.QueryString["keyWordValue"];
            string MenuResID = Request.QueryString["MenuResID"];
            bool IsChildResource = Convert.ToBoolean(Convert.ToInt32(Request.QueryString["IsChildResource"]));
            json = CommonMethod.GetFieldJson(keywordParameters, MenuResID, oEmployee);//获取grid的Field里面的值
        }
        if (typeValue == "GetfieldInfo")
        {
            string ColTitle = Request["ColName"];
            string ColField = Request["ColField"];
            string ColWidth = Request["ColWidth"];
            string OperatePageUrl = Request["OperatePageUrl"];
            string OperateText = Request["OperateText"];
            string OperateFunction = Request["OperateFunction"];
            Int32 OperatePageWidth = 1000;
            if (Request["OperatePageWidth"] != null) OperatePageWidth = Convert.ToInt32(Request["OperatePageWidth"]);
            Int32 OperatePageHeight = 800;
            if (Request["OperatePageHeight"] != null) OperatePageHeight = Convert.ToInt32(Request["OperatePageHeight"]);

            bool IsOperateButton = true;
            if (Request["IsOperateButton"] != null) IsOperateButton = Convert.ToBoolean(Convert.ToInt32(Request["IsOperateButton"]));
            json = CommonMethod.Getfield(ColTitle, ColField, ColWidth, OperatePageWidth, OperatePageHeight, IsOperateButton, OperatePageUrl,OperateFunction, OperateText);
        }

        if (typeValue == "GetChildField")
        {
            string keywordParameters = Request.QueryString["keyWordValue"];
            string ParentKeyWord = Request.QueryString["ParentKeyWord"];
            string MenuResID = Request.QueryString["MenuResID"];
            json = CommonMethod.GetFieldJson_ChildResource(keywordParameters,ParentKeyWord,MenuResID, oEmployee);//获取grid的Field里面的值
        }
        if (typeValue == "GetSysSettings")
        {
            string keyWordValue = Request.QueryString["keyWordValue"];
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Common.GetSysSettingsByENKey(keyWordValue));
            json = "[" + json + "]";
        }
        if (typeValue == "GetData")
        {
            string ResID = Request["ResID"].ToString();
            string Condition = Request["Condition"].ToString();  
            json = CommonMethod.GetDataJson( ResID, "ID", Condition, UserID);
        }

        if (typeValue == "GetChildDataByParent")
        {
            string ParentResID = Request["ParentResID"].ToString();
            string ParentRecID = Request["ParentRecID"].ToString();
            string ResID = Request["ResID"].ToString();
            json = CommonMethod.GetDataJson(ParentResID, ResID, ParentRecID, "ID", UserID);
        }

        if (typeValue == "GetDataOfPageByTableName")
        {
            string KeyWord = Request["keyWordValue"];
            SysSettings sys = Common.GetSysSettingsByENKey(KeyWord);
            string ResID = Request["ResID"].ToString();
            
                int PageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
                int PageNumber = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            
                string SortField = "";
                string SortBy = "";
                if (Request["sort"] != null)
                {
                    SortField = Request["sort"].ToString();
                }
                if (Request["order"] != null)
                {
                    SortBy = Request["order"].ToString();
                }

                string Condition = Request["Condition"];
                string TableName = Request["TableName"];


                json = CommonMethod.GetDataJsonOfPageByTableName(KeyWord, ResID, PageNumber, PageSize,  Condition, UserID);
          
        }
        if (typeValue == "GetDataByTableName")
        {
            string KeyWord = Request["keyWordValue"];
            SysSettings sys = Common.GetSysSettingsByENKey(KeyWord);
            string ResID = Request["ResID"].ToString();
 
            string SortField = "";
            string SortBy = "";
            if (Request["sort"] != null)
            {
                SortField = Request["sort"].ToString();
            }
            if (Request["order"] != null)
            {
                SortBy = Request["order"].ToString();
            }
            string Condition = Request["Condition"];
            string TableName = Request["TableName"];
            json = CommonMethod.GetDataJsonByTableName(KeyWord, ResID, Condition, UserID);

        }
        if (typeValue == "GetDataOfPage")
        {
            string ResID = Request["ResID"].ToString();
            if (ResID == "")
            {
                json = "{\"total\":0,\"rows\":[]}";
            }
            else
            {
                string KeyWord = Request["keyWordValue"];
                int PageSize =Request["rows"]==null?10: Convert.ToInt32(Request["rows"]);
                int PageNumber = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]); 
                string SortField = "";
                string SortBy = "";
                string Condition = "";//条件
                if (Request["sort"] != null)
                {
                    SortField = Request["sort"];
                }
                if (Request["order"] != null)
                {
                    SortBy = Request["order"];
                }

                //关键字查询条件
                string where = Request["Condition"] == null ? "" : Request["Condition"];
                if(where.Trim()!="") {
                    Condition += CommonMethod.GetResouceCondtion(KeyWord, where.Trim());
                }
                //列表初始化筛选条件
                if (Request["Authority"] != null && Request["Authority"] !="")
                {
                    Condition += CommonMethod.GetFilterCondition(Server.UrlDecode(Request["Authority"]), oEmployee);
                }
                if (ResID=="1300")
                {
                    Condition += " and 登录帐号 not in ('admin','sysuser','security')";
                }
                //高级组合筛选条件
                if (Request["FilterRules"] != null && Request["FilterRules"] != "")
                {
                    Condition +=" and "+ Request["FilterRules"];                    
                }

                //主子表关联字段筛选，主子表关联字段只能有一个
                if (Request["RelationCondtion"] != null && Request["RelationCondtion"] != "")
                {
                    string str = Request["RelationCondtion"];
                    string str1 = Request["RelationCondtion"].Substring(0, Request["RelationCondtion"].IndexOf("=")+1);
                    string str2 = Request["RelationCondtion"].Substring(Request["RelationCondtion"].IndexOf("=") + 1);
                    //截取出关联字段值，判断是否需要添加引号
                    if (str2.Trim().Length > 0)
                    {
                        //if (!CommonMethod.IsNum(str2.Trim()))//判断类型
                        //    Condition += " and " + Request["RelationCondtion"];
                        //else
                        Condition += " and " + str1 + "'" + str2 + "'";                       
                    }
                }
                string footName = Request["FootStr"];
                if (footName!=null&&footName!="")
                {
                    json = CommonMethod.GetDataOfPageForFooter(KeyWord, ResID, PageNumber, PageSize, SortField, SortBy, Condition, UserID,footName);
                }
                else
                {
                    json = CommonMethod.GetDataJsonOfPage(KeyWord, ResID, PageNumber, PageSize, SortField, SortBy, Condition, UserID);
                }
            }
        } 
        if (typeValue == "GetDataBySql")
        {
            string Condition = Request["Condition"].ToString();
            int PageSize = 0;
            if (Request["rows"] != null) PageSize=Convert.ToInt32(Request["rows"]);
            int PageNumber = 0;
            if (Request["rows"] != null) PageNumber = Convert.ToInt32(Request["page"]);
            string sqlData = Request["sqlData"].ToString();
            string SortField = "";
            string SortBy = "";
            if (Request["SortField"] != null) SortField = Request["SortField"].ToString();
            if (Request["SortBy"] != null) SortBy = Request["SortBy"].ToString();  
            string sql = "select * from (" + sqlData + ") as c  where 1=1 " + Condition;
            string OrderBy = "";//order  by
            if (SortField != "" && SortBy != "")
            {
                OrderBy = " " + SortField + " " + SortBy;
            }
            WebServices.Services services = new WebServices.Services();
            DataTable dt = null;
            string dtCount = "0";
            string[] changePassWord = Common.getChangePassWord();
            if (PageSize == 0 && PageNumber == 0) dt = services.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0]; 
            else
            {
                dt = GetDataListPage(sql, PageNumber, PageSize, OrderBy).Tables[0];
                dtCount = services.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0].Rows.Count.ToString();
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            // //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd"; 
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
            json = "{\"total\":" + dtCount + ",\"rows\":" + str + "}";

        }

        //单表单记录查询
        if (typeValue == "GetOneRowByRecID")
        {
            string RecID = Request["RecID"];
            string ResID = Request["ResID"];
            json = CommonMethod.GetOneRowByRecID(ResID, RecID);
        }
        //单表单记录查询  
        if (typeValue == "GetOneRowBySql")
        {
            Services oServices = new Services();
            string strSql = Request["sql"];
            string RecID = Request["RecID"];
            if (strSql.ToLower().Contains("where")) strSql += " and ID=" + RecID;
            else strSql += " where ID=" + RecID;
            string[] changePassWord = Common.getChangePassWord();
            json = Newtonsoft.Json.JsonConvert.SerializeObject(oServices.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0]);
             
        }
        //删除普通Grid数据
        if (typeValue == "DeleteRow")
        {
            json = DeleteRow();
        }

        //由于筛选条件和要导出的Excel列字段数据较大，无法通过url传递，因此先通过此方法将其存储在cookes中
        if (typeValue.Equals("saveExcelData"))
        {
            string s1 = Request["conditionStr"] == null ? "" : Request["conditionStr"];
            string where = Request["Condition"] == null ? "" : Request["Condition"];
            string KeyWord = Request["KeyWord"] == null ? "" : Request["KeyWord"];
            if (where.Trim() != "")
            {
                where= CommonMethod.GetResouceCondtion(KeyWord, where.Trim());
                s1 += where.Substring(4, where.Length-4);
            }
            //string s2 = Request["columnStr"];
            //if (Request["columnStr"] == null || Request["columnStr"] == "")
            //    json = "{\"success\": false,\"key\": \"0\"}";
            //else
            //{
            string CookieKey = Guid.NewGuid().ToString();
            HttpCookie CookieCdn = new HttpCookie("cdn" + CookieKey);
            CookieCdn.Value = s1;
            CookieCdn.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(CookieCdn);

            HttpCookie CookiecdnCol = new HttpCookie("col" + CookieKey);
            CookiecdnCol.Value = "";
            CookiecdnCol.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(CookiecdnCol);

            json = "{\"success\": true,\"key\": \"" + CookieKey + "\"}";
            //}            
        }
        if (typeValue.Equals("Excel"))
        {
            if (Request.QueryString["cookeId"] == null)
            {
                Response.Write("导出失败，请重新操作！");
                Response.End();
            }

            string cookeId = Request.QueryString["cookeId"];
            //从Cookies中取出存储的参数 筛选条件、导出字段，然后在销毁Cookies
            string Condition = Request.Cookies["cdn" + cookeId].Value;
            string Column = Request.Cookies["col" + cookeId].Value;

            Request.Cookies["col" + cookeId].Expires = DateTime.Now.AddDays(-1);
            Request.Cookies["cdn" + cookeId].Expires = DateTime.Now.AddDays(-1);

            Condition = Condition.Equals("") ? "" : " and " + Condition;//导出筛选条件
            Column = Column.Equals("") ? "*" : Column;//导出的列        


            string fileName = Request.QueryString["ExcelfileName"] == null ? "数据" : Request.QueryString["ExcelfileName"];

            //获取资源ID及资源关键字参数
            string resid = Request.QueryString["resId"];
            string keyWord = Request.QueryString["KeyWord"];

            //根据筛选条件及字段列，获取导出数据
            WebServices.Services services = new WebServices.Services();

            //列表初始化筛选条件
            if (Request["Authority"] != null && Request["Authority"] != "")
            {
                Condition += CommonMethod.GetFilterCondition(Server.UrlDecode(Request["Authority"]), oEmployee);
            }
            DataTable dt = services.GetDataListByResID(resid, "", Condition, UserID).Tables[0];
            DataTable newDt = new DataTable();
            DataTable colDt = services.GetDataListByResID("502816446274", " 排序号 asc", " and 参数关键字='" + keyWord + "'", UserID).Tables[0];
            for (int i = 0; i < colDt.Rows.Count; i++)
            {
                newDt.Columns.Add(colDt.Rows[i]["显示字段"].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newDt.ImportRow(dt.Rows[i]);
            }

            // DataTable dt = services.GetDataColumnListByResID(resid, Column, "ID", Condition, UserID).Tables[0];

            Byte[] fileByte = new DoExcel().ExportExcel(newDt, fileName);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", fileByte.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName + DateTime.Now.ToString("yyyyMMdd") + ".xlsx", System.Text.Encoding.UTF8));
            Response.BinaryWrite(fileByte);
            Response.Flush();
            Response.End();
        }
        Response.Write(json);
    } 
    protected DataSet GetDataListPage(string sql, int PageIndex, int PageSize, string OrderBy)
    {
        if (!string.IsNullOrEmpty(OrderBy))
        {
            OrderBy = "order by " + OrderBy;
        }
        string sqldata = "";
        sqldata = " select top " + PageSize.ToString();
        sqldata += " * from (" + sql + ") T where id not in (select top " + ((PageIndex - 1) * PageSize).ToString();
        sqldata += " id from (" + sql + ") W  " + OrderBy + ")  " + OrderBy;
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        return Resource.SelectData(sqldata, changePassWord[0], changePassWord[1], changePassWord[2]);
    }
 
    //删除grid的行数据
    protected string DeleteRow()
    {
        String ResID = Request["ResID"];
        String RecID = Request["RecID"];
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
     
}