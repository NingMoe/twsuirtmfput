using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Organization_DeleteDept : System.Web.UI.Page
{
    protected string DeptID = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["DeptID"] != null) DeptID = Request["DeptID"];
    }
}