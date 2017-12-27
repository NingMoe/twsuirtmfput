using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetReusables;
using System.Web.Security;

public partial class login :PageBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    } 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        WebServices.UserInfo oEmployee = null;
        try
        {
            WebServices.Services services = new WebServices.Services();
            if (services.Login(txtUserCode.Text.Trim(), txtPwd.Text.Trim()))
            {
                oEmployee = services.GetUserInfo(txtUserCode.Text.Trim());
                string pwd = NetReusables.Encrypt.Encrypt(txtPwd.Text.Trim());
                if (oEmployee.Password == pwd || (oEmployee.Password == null && pwd == ""))
                {
                    if (oEmployee.Status)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "alert('该账号已被禁用，请联系管理员！');", true);
                    }
                    else if (oEmployee.ID.ToLower().Equals("admin"))//超级管理员
                    {
                        string url = "/cmsweb/SvcLogin.aspx?targetpage=" + Server.UrlEncode("/cmsweb/cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminres/ResourceFrameBody.aspx") + "&user=" + oEmployee.ID + "&ucode=" + oEmployee.Password;
                        Response.Redirect(url, false);
                    }
                    else
                    {
                        Session["Employee"] = oEmployee;

                        //HttpCookie myCookie = new HttpCookie("EmployeeCookies");
                        //myCookie.Values["EmployeeCode"] = oEmployee.ID.Trim();
                        //Response.Cookies.Add(myCookie);

                        //不同用户登录，进入不同界面
                        if (oEmployee.ID.Trim().ToLower() == "sysuser")
                            Response.Redirect("SystemIndex.aspx", false);
                        else Response.Redirect("Index.aspx", false);
                         
                    }
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "alert('用户名或密码不正确！');", true);
            }
        }
        catch (Exception)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "alert('用户名或密码不正确！');", true);
            txtPwd.Text = "";
        }       
    }
}