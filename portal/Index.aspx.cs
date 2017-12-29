using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Index : UserPagebase
{
    public long ZSWorkingResID = 435252729656;
    public long ZWGLResID = 0;
    public string resourceInfoID = "";
    public string resourceInfoTitle = "";
    public string DBSXNum = "0";
    public UserInfo oUserInfo = null;

    public string StartDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-DateTime.Now.Day - 20));
    public string EndDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
    WebServices.Services document = new WebServices.Services();
    public bool OpenOrCloseMessageDeliveryState = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string reloadPage = Request["reloadPage"];
        //if (reloadPage == "yes") {
        //    Response.Write("<script type='text/javascript'>alert('12312312312312312313');</script>"); 
        //}
        oUserInfo = CurrentUser;
       
        Bind();
        if (!IsPostBack) return;

    }

    public void Bind()
    {
        string resID = CommonProperty.PortalResourceID;
        WebServices.ResourceInfo[] ResourceInfo = null;
        WebServices.Services Resource = new WebServices.Services();
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.SelectData("select Count(1) from VIEW_WF_RECEIVEFILES  WHERE EMPCODE='" + oUserInfo.ID + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DBSXNum = dt.Rows[0][0].ToString();
        }
        //获取一级导航菜单列表，如果是管理员则显示所有菜单
        if (CurrentUser.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
            ResourceInfo = document.GetNextDirectoryList(resID);
        else
            ResourceInfo = document.GetNextPortalTreeRootByResourceIDAndUserID(oUserInfo.ID, resID);

        if (ResourceInfo != null && ResourceInfo.Length > 0)
        {
            //获取第一个菜单ID
            resourceInfoID = ResourceInfo[0].ID;
            //获取第一个菜单Title
            resourceInfoTitle = ResourceInfo[0].Name;

            this.titleRepeater.DataSource = ResourceInfo;
            titleRepeater.DataBind();
        }
        else
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "alert('该用户还没有分配任务权限，请先找管理员分配权限！');window.location.href='../login.aspx'", true);


    }
}