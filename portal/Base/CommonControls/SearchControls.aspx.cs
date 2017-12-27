using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonControls_SearchControls : UserPagebase
{
    public string ResId = "";
    public string KeyWord = "";
    public DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        ResId = Request.QueryString["resId"];
        KeyWord = Request.QueryString["KeyWord"] == null ? "" : Request.QueryString["KeyWord"];

        if (KeyWord != "")
        {
            //获取当前资源配置的查询字段
            WebServices.Services Resource = new WebServices.Services();
            string strSql = " select id,keyword,datetype,Searchcol,showCol from Sys_CXZJLB where keyword='"+ KeyWord + "' order by xh asc ";
            string[] changePassWord = Common.getChangePassWord();

            dt = Resource.Query(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        }
    }
}