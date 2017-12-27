using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonDictionary_DataDictionary :UserPagebase 
{
    public string ResourceID = "";
    public string ResourceColumn = "";
    public string Condition = "";
    public int PageSize = 10;
    public int ChildIndex = -1;
    protected bool IsMultiselect = false;
    protected bool IsAppend = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["ResourceID"] != null)
        {
            ResourceID = Request["ResourceID"];
        }
        if (Request["ResourceColumn"] != null)
        {
            ResourceColumn = Request["ResourceColumn"];
        }
        if (Request["ChildIndex"] != null)
        {
            ChildIndex = Convert.ToInt32(Request["ChildIndex"]);
        }
        if (Request["IsMultiselect"] != null)
        {
            IsMultiselect = Convert.ToBoolean(Convert.ToInt32(Request["IsMultiselect"]));
        }
        if (Request["IsAppend"] != null)
        {
            IsAppend = Convert.ToBoolean(Convert.ToInt32(Request["IsAppend"]));
        }
        if (Request["Condition"] != null) Condition =" and " + Server.UrlDecode(Request["Condition"]).Replace("**", "'"); ;
    }
}