using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_SystemConfig_DictionaryList : UserPagebase
{ 
    public string ResID = "437496952875";
    protected string keyWordValue = "";
    protected string NodeID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
       
        if (Request.QueryString["ResID"] != null) 
            ResID = Request.QueryString["ResID"].ToString(); 

        if (Request["NodeID"] != null) 
            NodeID = Request.QueryString["NodeID"].ToString();

        keyWordValue = NodeID;
    } 
}