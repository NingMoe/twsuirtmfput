using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonDictionary_ResourceTreeDictionary : PageBase
{
    protected string Params = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("");
        if (Request["Params"] != null) Params = Request["Params"].ToString();
    } 
}