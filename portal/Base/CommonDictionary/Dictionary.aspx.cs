using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dictionary : System.Web.UI.Page
{
    public string ResID = "";
    public string keyWordValue = "";
    public string ParentResID = "";
    public string FromResID = "";
    public string dispname = "";    //高级字典的字段
    public int PageSize = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["DictionaryResID"] != null)
        {
            ResID = Request["DictionaryResID"];
        }
        if (Request["DictionaryKey"] != null)
        {
            keyWordValue = Request["DictionaryKey"];
        }
        if (Request["ParentResID"] != null)
        {
            ParentResID = Request["ParentResID"];
        }
        if (Request["FromResID"] != null)
        {
            FromResID = Request["FromResID"];
        }
        if (Request["dispname"] != null)
        {
            dispname = Request["dispname"];
        }
       
    }
}