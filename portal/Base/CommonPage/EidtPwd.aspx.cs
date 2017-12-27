using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_CommonPage_EidtPwd : UserPagebase
{
    public string UserID = "";
    string PassWord = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        PassWord = CurrentUser.Password;
        string typeValue = Request.QueryString["typeValue"] == null ? "" : Request.QueryString["typeValue"];
        string json = "";
        #region 修改密码
        if (typeValue.Equals("Updpass"))
        {
            string userId = Request.Form["userId"];
            string oldPwd = Request.Form["oldPwd"];
            string newPwd = Request.Form["newPwd"];

            WebServices.Services services = new WebServices.Services();
            if (services.Login(userId, oldPwd))
            {
                if (services.ChangePassword(userId, newPwd))
                    json = "{\"success\": true}";
                else
                    json = "{\"success\": false}";
            }
            else
                json = "{\"success\": false}";


            Response.Write(json);
            Response.End();
        }
        #endregion
    }    
}