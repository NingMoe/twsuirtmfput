using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonControls_ExportExcel : UserPagebase
{
    public string ResId = "";
    public string KeyWord = "";
    public DataTable dt = new DataTable();
    public DataTable dtCol = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResId = Request.QueryString["resId"];
            KeyWord = Request.QueryString["KeyWord"] == null ? "" : Request.QueryString["KeyWord"];

            if (KeyWord != "")
            {
                //获取当前资源配置的查询字段
                WebServices.Services Resource = new WebServices.Services();
                string strSql = " select id,keyword,datetype,Searchcol,showCol from Sys_CXZJLB where keyword='" + KeyWord + "' order by xh asc ";
                string[] changePassWord = Common.getChangePassWord();
                dt = Resource.Query(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

                //获取当前资源配置的列表字段
                string strColSql = "   select rc.id,rc.columnName,rc.orderNum,rc.DateType,rc.keyword from ResourceColumn rc where rc.keyword='" + KeyWord + "' order by rc.orderNum asc ";
                dtCol = Resource.Query(strColSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

            }
        }       
    }
}