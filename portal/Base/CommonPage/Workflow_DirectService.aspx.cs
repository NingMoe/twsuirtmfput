using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Workflow_DirectService : UserPagebase
{
    string UserID = "";
    string UserName = "";
    string UserPass = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        UserPass = CurrentUser.Password;
        //string cmswebPath = System.Configuration.ConfigurationManager.AppSettings["cmsweb"].ToString();  
        string cmswebPath = Request.Url.GetLeftPart(UriPartial.Authority);
        string action = Request.QueryString["action"];
        string url = Request.QueryString["strURL"];
        string WorkFlowWebPath = cmswebPath + "/webflow" + "/UserValidate.aspx?user=" + UserID + "&ucode=" + UserPass + "&url=" + Server.UrlEncode(url);
        Response.Redirect(WorkFlowWebPath);
    }
}
