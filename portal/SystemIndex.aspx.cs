using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : UserPagebase
{
    public long ZSWorkingResID = 435252729656;
    public long ZWGLResID = 0;
    public string resourceInfoID = "0";
    public string UserID = "";
   
    public string StartDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-DateTime.Now.Day - 20));
    public string EndDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
    WebServices.Services document = new WebServices.Services();
    public bool  OpenOrCloseMessageDeliveryState =false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.ID!="sysuser")
        {
            Response.Redirect("login.aspx",true);
            return;
        }
        Literal oLiteral = new Literal();
        oLiteral.ID = "litHeader";
        oLiteral.Text = GetScript1_4;
     
        Bind();
        OpenOrCloseMessageDeliveryState = GetOpenOrCloseMessageDeliveryState();
        resourceInfoID = CommonProperty.PortalResourceID;

        if (!IsPostBack) return;
         
       
    }
    public void Bind()
    { 
       // string des = Common.GetValueByKey("门户列表说明");
        //string resID = CommonProperty.PortalResourceID;
        //WebServices.ResourceInfo[] ResourceInfo = document.GetNextPortalTreeRootByResourceIDAndUserID(UserID, resID);
        //if (ResourceInfo.Length == 0) 
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "alert('该用户还没有分配任务权限，请先找管理员分配权限！');window.location.href='../login.aspx'", true);
        //else resourceInfoID = ResourceInfo[0].ID;
 

    }

    protected bool GetOpenOrCloseMessageDeliveryState()
    {
        //DataTable dt = document.GetDataListByTableName("437496955203", "[OptionValue]", " where [OptionName] = 'OpenOrCloseMessageDelivery' ", true).Tables[0];
        //if (dt.Rows.Count>0)
        //{
        //    if (dt.Rows[0]["OptionValue"].ToString() == "1") return true;
        //}
        return false;
    }
}