using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Organization_Default : UserPagebase 
{
    protected string ObjectID = "";
    protected string GainerType = "";
    protected string key = "";
    protected string MenuResID = "";
    protected string DeptID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        if (Request["ObjectID"] != null) ObjectID = Request["ObjectID"];
        if (Request["GainerType"] != null) GainerType = Request["GainerType"];
        if (Request["key"] != null) key = Request["key"];
        if (Request["MenuResID"] != null) MenuResID = Request["MenuResID"];
        if (Request["DeptID"] != null) DeptID = Request["DeptID"];
    }
}