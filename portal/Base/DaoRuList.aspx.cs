using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Base_DaoRuList : System.Web.UI.Page
{
    public string ResID = "";
    public string NodeID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ResID = Request["typeResID"];
    }

}