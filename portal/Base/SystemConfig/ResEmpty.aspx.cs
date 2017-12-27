using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Config_ResEmpty : UserPagebase
{ 
    public string titleValue = "";
    public string ResID = "";
    public string keyWordValue = "";
    public string UserID = "";
    public string UserName = "";
    public int PageSize = 15;
    public string SearchType = "";
    public string NodeID = "";
    protected string FieldValueStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (Request.QueryString["ResID"] != null)
        {
            ResID = Request.QueryString["ResID"].ToString();
        }
        if (Request.QueryString["NodeID"] != null)
        {
            NodeID = Request.QueryString["NodeID"].ToString();
        }
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        UserID = CurrentUser.ID;
        keyWordValue = "ResEmpty";
       // FieldValueStr = GetBaseFieldStr();

    } 
    protected string GetBaseFieldStr()
    {
        string ColumnsStr = "[["; 
        string ResourceTarget = "s='<input type=checkbox name=ch_ResourceTarget id=ch_blank />_blank <input type=checkbox name=ch_ResourceTarget id=ch_parent />_parent';"; 
        ColumnsStr += "{title:'空资源链接路径',field:'资源链接',width:100,align:'center'},";
        ColumnsStr += "{title:'空资源链接打开方式',field:'打开方式',width:300,align:'center',formatter: function (value, row, index) {" + ResourceTarget + " return s;} }"; 
        ColumnsStr += " ]]";
        return ColumnsStr;
    } 
}