using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_AddChildResource : UserPagebase
{
    protected string ResID = "";
    protected string PResID = "";
    protected string RelatedResID = "";
    protected string RelatedValue = "";
    protected string RecID = "";
    protected string UserID = "";
    DataTable TableList = new DataTable();
    WebServices.Services Resource = new WebServices.Services();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("add");
        ResID = Request["ResID"];
        if (Request["PResID"] != null) PResID = Request["PResID"];
        if (Request["RelatedResID"] != null) RelatedResID = Request["RelatedResID"];
        if (Request["RelatedValue"] != null) RelatedValue = Request["RelatedValue"];  //关联字段值
        UserID = CurrentUser.ID;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }

        if (Request["RecID"] != null) RecID = Request["RecID"];
        
    }
}