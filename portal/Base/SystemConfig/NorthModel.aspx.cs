using System;
using System.Data ;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonPage_NorthModel : UserPagebase
{
   
    protected string UserID = "";
    protected string resourceInfoID = "";
    protected string NowDate = "";
    protected string UserName = "";
    protected long FlowResID = 409496323718;
    protected string MyTaskCountStr="";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        NowDate = DateTime.Now.ToString("yyyy-MM-dd") + " ";
        NowDate += ((DateTime.Now.DayOfWeek.ToString().Length > 3) ? DateTime.Now.DayOfWeek.ToString().Substring(0, 3) : DateTime.Now.DayOfWeek.ToString());
        //Bind();             
    }


    //public void Bind()
    //{
    //    WebServices.Services document = new WebServices.Services();

          
    //   // string des = Common.GetValueByKey("门户列表说明");
    //    string resID = CommonProperty.PortalResourceID;
    //    if (CurrentUser.DepartmentName.Trim() == CommonProperty.ManageDepartmentName.Trim())
    //    {
    //        this.titleRepeater.DataSource = document.GetNextDirectoryList(resID);
    //    }
    //    else this.titleRepeater.DataSource = document.GetNextPortalTreeRootByResourceIDAndUserID(UserID, resID);
    //    titleRepeater.DataBind();
    //}

}