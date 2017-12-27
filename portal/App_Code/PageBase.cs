using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.IO;
using WebServices;
/// <summary>
/// PageBase 的摘要说明
/// </summary>
public class PageBase : System.Web.UI.Page
{
    Services Resource = new Services();
    protected override void OnError(EventArgs e)
    {
        //SLog.Err(Server.GetLastError().Message, Server.GetLastError()); Response.Write("<br>");
        Response.Write("<br>");
        Response.Write("<div style=\"font-size:14px;color:red\" align=\"center\">");
        Response.Write("<b>系统发生异常:</b><br>" + Server.GetLastError().Message);
        Response.Write("</div>");
        Response.End();
        try
        {
            base.OnError(e);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }

    }

    protected UserInfo DataEmployee
    {
        get
        {
            UserInfo oEmployee = HttpContext.Current.Session["Employee"] as UserInfo;
            if (HttpContext.Current.Session["Employee"] == null)
            {
                //if (HttpContext.Current.Request.Cookies["EmployeeCookies"] == null)
                //{
                Response.Write("登陆超时,请重新登录.");
                Response.Write("<script>top.document.location.href='" + Request.ApplicationPath + "/Login.aspx'</script>");
                Response.End();
                //}
                //else
                //{
                //    HttpContext.Current.Session["Employee"] = Resource.GetUserInfo(Request.Cookies["EmployeeCookies"].Values["EmployeeCode"].Trim());
                //}
            }
            //oEmployee = HttpContext.Current.Session["Employee"] as UserInfo;
            return oEmployee;
        }
    }
 
    protected string GetScript1_4
    {
        get { return LoadFormScript.GetScript1_4_3(WebUtilities.ApplicationPath); }
    }

    protected string GetScript1_4_3
    {
        get { return LoadFormScript.GetScript1_4_3(WebUtilities.ApplicationPath); }
    }

    protected string GetScript_OperationForm
    {
        get { return LoadFormScript.GetScript_OperationForm(WebUtilities.ApplicationPath); }
    }

    protected void LoadScript(string ScriptVersion)
    {
        Literal oLiteral = new Literal();
        oLiteral.ID = "litHeader";

        if (ScriptVersion.Trim() == "1.4.3")
            oLiteral.Text = LoadFormScript.GetScript1_4_3(WebUtilities.ApplicationPath);         
        else if (ScriptVersion.ToLower().Trim() == "add")
            oLiteral.Text = LoadFormScript.GetScript_OperationForm(WebUtilities.ApplicationPath);
        else
            oLiteral.Text = LoadFormScript.GetScript1_4_3(WebUtilities.ApplicationPath);
        Page.Header.Controls.Add(oLiteral);
    }

    public static string StrCut(string strInput, int intLen)
    {
        
        strInput = strInput.Trim();
        byte[] myByte = System.Text.Encoding.Default.GetBytes(strInput);
        if (myByte.Length > intLen)
        {
            //截取操作
            string resultStr = "";
            for (int i = 0; i < strInput.Length; i++)
            {
                byte[] tempByte = System.Text.Encoding.Default.GetBytes(resultStr);
                if (tempByte.Length < intLen - 4)
                {
                    resultStr += strInput.Substring(i, 1);
                }
                else
                {
                    break;
                }
            }
            return resultStr + " ...";
        }
        else
        {
            return strInput;
        }
    }

    protected virtual void MessageBox(string text)
    { 
        Response.Write("<script>alert('" + text.Replace("'", "'") + "');</script>"); 
    }
}




