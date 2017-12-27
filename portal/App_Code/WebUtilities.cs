using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WebUtilities 的摘要说明
/// </summary>
public class WebUtilities
{
    /// <summary>
    /// 获取网站根路径
    /// </summary>
    public static string WebSiteRootHttpPath
    {
        get
        {
            string sOut = WebSitePath + System.Web.HttpContext.Current.Request.ApplicationPath;
            if (sOut.EndsWith("/")) return sOut.TrimEnd('/');
            else return sOut;
        }
    }

    public static string ApplicationPath
    {
        get
        {
            return System.Web.HttpContext.Current.Request.ApplicationPath + "/";
        }
    }

    /// <summary>
    /// 获取网站根路径
    /// </summary>
    public static string WebSitePath
    {
        get
        {
            string Port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (Port == null || Port == "80" || Port == "443")
                Port = "";
            else
                Port = ":" + Port;

            string Protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";

            string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port;
            if (sOut.EndsWith("/")) return sOut.TrimEnd('/');
            else return sOut;
        }
    }

    /// <summary>
    /// 网站文件的物理存放路径
    /// </summary>
    public static string PhysicalApplicationPath
    {
        get
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }

    } 
}