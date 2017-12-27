using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// LoadFormScript 的摘要说明
/// </summary>
public class LoadFormScript
{
	public LoadFormScript()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string GetScript1_4_3(string ApplicationPath)
    {
        string strScript ="\r\n\t" + "<link href='" + ApplicationPath + "Scripts/jquery-easyui-1.4.3/themes/default/easyui.css' rel='stylesheet' />" +
                        "\r\n\t" + "<link href='" + ApplicationPath + "Scripts/jquery-easyui-1.4.3/themes/icon.css' rel='stylesheet' />" +
                        "\r\n\t" + "<link href='" + ApplicationPath + "CSS/Style.css' rel='stylesheet' type='text/css' />" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/jquery1.11.3.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/easyui-lang-zh_CN.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/OtherJs.js'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/SetRead.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/js/FormValidate.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/SelectRecordForJS.js'  type='text/javascript'></script>";
        return strScript;
    }


    public static string GetScript_OperationForm(string ApplicationPath)
    {
        string strScript = "\r\n\t" + " <link href='" + ApplicationPath + "CSS/Style.css' rel='stylesheet' type='text/css' />" +
                        "\r\n\t" + "<link rel='stylesheet' type='text/css' href='" + ApplicationPath + "css/themes/default/easyui.css' id='swicth_style' />" +
                        "\r\n\t" + "<link rel='stylesheet' type='text/css' href='" + ApplicationPath + "css/themes/icon.css' />" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/jquery1.11.3.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/easyui-lang-zh_CN.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/OtherJs.js'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/SetRead.js' type='text/javascript'></script>" +
                        "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/SetChildField.js' type='text/javascript' ></script>" +
                    "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/js/FormValidate.js' type='text/javascript'></script>" +
                   "\r\n\t" + "<script src='" + ApplicationPath + "Scripts/SelectRecordForJS.js'  type='text/javascript'></script>";
       
        return strScript;
    }
}