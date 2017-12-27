using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Cms_DirectService : UserPagebase
{
    string UserID = "";
    string UserName = "";
    string UserPass = "";
    protected string WorkFlowWebPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        UserPass = CurrentUser.Password;
        string RecID="0"; //记录ID
        string mnuinmode = "2";
        if (Request["RecID"] != null) RecID = Request["RecID"];
        string ResID = "0"; //资源ID
        if (Request["ResID"] != null)
        {
            ResID = Request["ResID"];
            string ParentResID = ResID;//父资源ID
            string ParentRecID = RecID;//父记录ID
            if (Request["ParentResID"] != null) ParentResID = Request["ParentResID"]; 
            if (Request["ParentRecID"] != null)
            {
                ParentRecID = Request["ParentRecID"];
                if (ParentRecID.Trim ()!=ResID.Trim()) mnuinmode = "3";
            }
            if (Convert.ToInt32(RecID) > 0)
            {
                mnuinmode = "4";
                if (ParentRecID.Trim() != RecID.Trim()) mnuinmode = "5";
            }
            string cmswebPath = System.Configuration.ConfigurationManager.AppSettings["cmsweb"].ToString();
            WorkFlowWebPath = cmswebPath;
            if (cmswebPath.Trim().Substring(cmswebPath.Length - 1) != "/")
                WorkFlowWebPath += "/";
            WorkFlowWebPath += "cmsweb/SvcLogin.aspx?user=" + UserID + "&ucode=" + UserPass + "&mnuinmode=" + mnuinmode + "&mnuresid=" + ResID + "&mnurecid=" + RecID + "&mnuhostresid=" + ParentResID + "&mnuhostrecid=" + ParentRecID.Trim();

            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>window.parent.ParentCloseWindow();window.open('" + WorkFlowWebPath + "');</script>"); 
        }
        else
        {
            Response.Write("<div style='margin:15px;'><p style='color:red;'>传入参数不正确！</p><p style='color:red;'>所需参数【ResID（资源ID），RecID（记录ID），ParentResID（父资源ID），ParentRecID（父记录ID）】；</p><p style='color:red;'>其中：【ResID（资源ID】参数是必须的。</p></div>");
            
        }
    }
}