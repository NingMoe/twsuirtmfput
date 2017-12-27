using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebServices;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/// <summary>
/// 带用户登录验证的页面基类,如果用户未登录此页面,将被自动弹出.
/// </summary>
public class UserPagebase : PageBase
{
    
    protected UserInfo CurrentUser
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
                //    Services Resource = new Services();
                //    HttpContext.Current.Session["Employee"] = Resource.GetUserInfo(Request.Cookies["EmployeeCookies"].Values["EmployeeCode"].Trim());
                //}
            }
            //oEmployee = HttpContext.Current.Session["Employee"] as UserInfo;
            return oEmployee;
        }
    }
 
    protected override void OnLoad(EventArgs e)
    { 
        base.OnLoad(e);

        if (HttpContext.Current.Session["Employee"] == null||Request.UrlReferrer == null)
        {
            if (Request.Url.AbsoluteUri.IndexOf("portal/Base/Finance/PrintBXGL.aspx") == -1 && Request.Url.AbsoluteUri.IndexOf("portal/Base/Finance/PrintQKGL.aspx") == -1 && Request.Url.AbsoluteUri.IndexOf("portal/Base/SolidWaste/PrintInfo.aspx") == -1)
            {
                Response.Write("登陆超时,请重新登录.");
                Response.Write("<script>top.document.location.href='" + Request.ApplicationPath + "/Login.aspx'</script>");
                Response.End();
            }
        }
        UserInfo oEmployee = HttpContext.Current.Session["Employee"] as UserInfo;
        if (!IsHaveQX(oEmployee, Request.Url.AbsoluteUri))
        {
            Response.Write("登陆超时,请重新登录.");
            Response.Write("<script>top.document.location.href='" + Request.ApplicationPath + "/Login.aspx'</script>");
            Response.End();
        }
    }


    protected static Boolean IsHaveQX(UserInfo oEmployee, string url)
    {
        Services Resource = new Services();
        Boolean isHaveQx = true;
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = Resource.GetDataListByResID("502194239949", "", " and ((权限对象类型='dept' and 权限对象编号='" + oEmployee.DepartmentID + "') or (权限对象类型='emp' and 权限对象编号='" + oEmployee.ID + "')) and 是否启用='1' and 权限值='1' ", oEmployee.ID).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string strSql = "select ID,RES_EMPTYRESOURCEURL 资源链接  from dbo.CMS_RESOURCE where isnull(RES_EMPTYRESOURCEURL,'')!='' and isnull(RES_IsDefaultMenu,'')!='1'";
            DataTable dt1 = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string zyljStr = dt1.Rows[i]["资源链接"].ToString();
                if (url.Length >zyljStr.Length)
                {
                    string urlStr = url.Substring(url.Length - zyljStr.Length, zyljStr.Length);
                    if (urlStr == zyljStr)
                    {
                        if (dt.Select(" 资源ID='" + dt1.Rows[i]["ID"].ToString() + "'").Length == 0)
                        {
                            isHaveQx = false;
                        }
                    }
                }
            }
        }
        return isHaveQx;
    }
    
    public static string GetToken()
    {
        tokenservice.Authentication Authentication = new tokenservice.Authentication();
        Authentication.UserID = ((UserInfo)HttpContext.Current.Session["Employee"]).ID;
        tokenservice.TokenService ts = new tokenservice.TokenService();
        ts.AuthenticationValue = Authentication;
        string token = ts.GetToken();
        return token;
    }

    protected void RedirectVerify()
    {
        if (HttpContext.Current.Request["user"] != null && HttpContext.Current.Request["pdk"] != null && HttpContext.Current.Request["key"] != null )
        {
            string user = HttpContext.Current.Request["user"].ToString();
            string pdk = HttpContext.Current.Request["pdk"].ToString();
            string key = HttpContext.Current.Request["key"].ToString();
            string MobelUrl = ConfigurationManager.AppSettings["MobelApiUrl"].ToString();
            //string MobelUrl = ConfigurationManager.AppSettings["TestMobelApiUrl"].ToString();
            string url = MobelUrl + "/VerifyPDK";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("UrlScheme", Request.Url.Scheme);
            param.Add("UrlAuthority", Request.Url.Authority);
            param.Add("MobileApiUserID", user);
            param.Add("pdk", System.Web.HttpUtility.UrlEncode(pdk));
            param.Add("key", key);
            string str = sendPost(url, param);
            JToken json = (JToken)JsonConvert.DeserializeObject(str);
            string Data = json.Root["Data"].ToString();
            if(Data=="1")
            {
                Services Resource = new Services();
                UserInfo u= Resource.GetUserInfo(user);
                if (u != null)
                {
                    HttpContext.Current.Session.Clear();
                    //HttpContext.Current.Request.Cookies.Clear();
                    HttpContext.Current.Session["Employee"] = u;
                    //HttpCookie myCookie = new HttpCookie("EmployeeCookies");
                    //myCookie.Values["EmployeeCode"] = u.ID.Trim();
                    //Response.Cookies.Add(myCookie);
                }  
            }
            else
            {
                string vError = "链接地址非法或失效，请重新登录！";
                //vError = pdk;
                string Js = "<script type='text/javascript'>alert('" + vError + "')" + "<" + "/script>";
                HttpContext.Current.Session.Clear();
                //HttpContext.Current.Request.Cookies.Clear(); 
                Response.Write(Js);
                Response.Write("<script>top.document.location.href='" + Request.ApplicationPath + "/Login.aspx'</script>");
                Response.End();
            }
        }
    }

    /// <summary>
    /// Post方式提交数据，返回网页的源代码
    /// </summary>
    /// <param name="url">发送请求的 URL</param>
    /// <param name="param">请求的参数集合</param>
    /// <returns>远程资源的响应结果</returns>
    private string sendPost(string url, Dictionary<string, string> param)
    {
        string result = "";
        StringBuilder postData = new StringBuilder();
        if (param != null && param.Count > 0)
        {
            foreach (var p in param)
            {
                if (postData.Length > 0)
                {
                    postData.Append("&");
                }
                postData.Append(p.Key);
                postData.Append("=");
                postData.Append(p.Value);
            }
        }
        byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = url;
            request.Accept = "*/*";
            request.Timeout = 30 * 1000;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            request.Method = "POST";
            request.ContentLength = byteData.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(byteData, 0, byteData.Length);
            stream.Flush();
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream backStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
            result = sr.ReadToEnd();
            sr.Close();
            backStream.Close();
            response.Close();
            request.Abort();
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }
}
