using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebServices;
using NetReusables;

/// <summary>
/// UserDefinedToolBar 的摘要说明
/// </summary>
public class UserDefinedToolBar
{
	public UserDefinedToolBar()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 操作编号
    /// </summary>
    public string OperationCode { get; set; }
    /// <summary>
    /// 关键字
    /// </summary>
    public string KeyWordValue { get; set; }
    /// <summary>
    /// 工具名称
    /// </summary>
    public string ToolName { get; set; }
    /// <summary>
    /// 工具图标
    /// </summary>
    public string ToolIcon { get; set; }
    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// 事件代码
    /// </summary>
    public string EventCode { get; set; }
    /// <summary>
    /// 是子函数
    /// </summary>
    public bool IsSubFunction { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNo { get; set; }
    /// <summary>
    /// 弹出页面
    /// </summary>
    public string PopUpPage { get; set; }
    /// <summary>
    /// 页面打开方式
    /// </summary>
    public string URLTarget { get; set; }
    /// <summary>
    /// 页面宽度
    /// </summary>
    public int DialogWidth { get; set; }
    /// <summary>
    /// 页面高度
    /// </summary>
    public int DialogHeight { get; set; }



    private static List<UserDefinedToolBar> GetDefaultUserDefinedToolBars(string KeyWordValue, string argType)
    {
        List<UserDefinedToolBar> vUserDefinedToolBars = new List<UserDefinedToolBar>();

        switch (argType)
        {
            case "Report":
                {
                    UserDefinedToolBar v = new UserDefinedToolBar()
                    {
                        OperationCode = "0",
                        ToolName = "导出",
                        ToolIcon = "icon-excel",
                        IsSubFunction = true,
                        OrderNo = 1,
                        IsEnabled = true,
                        EventCode = "CommonReport(checkedObj,PopUpPage,ThisGridID)",
                        PopUpPage = "",
                        DialogWidth = 0,
                        DialogHeight = 0,
                    };

                    v.EventCode = " function () {   var checkedObj = $('#' + _GridID + _keyWordValue).datagrid('getChecked') ; var ThisGridID = _GridID + _keyWordValue ;  " + (string.IsNullOrEmpty(v.PopUpPage.Trim()) ? "  var PopUpPage=''; " : " var PopUpPage = '" + v.PopUpPage + "'; ") + v.EventCode + " }";
                    vUserDefinedToolBars.Add(v);
                }
                break;
        }

        return vUserDefinedToolBars;
    }

    //private static void Setbar(ref UserDefinedToolBar argbar)
    //{ 
    //    if (argbar.IsSubFunction)
    //    {
    //        argbar.EventCode = "var checkedObj = $('#' + _GridID + _keyWordValue).datagrid('getChecked') ; var ThisGridID = _GridID + _keyWordValue ;  if (checkedObj.length == 0)  {  alert('请至少选择一项！') ;  return; }  ";
    //    }

    //}

    public static List<UserDefinedToolBar> GetUserDefinedToolBars(string KeyWordValue, string argType)
    {
        List<UserDefinedToolBar> vUserDefinedToolBars = new List<UserDefinedToolBar>();
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        DataSet vDS = Resource.SelectData(" select a.* from UserDefinedToolBar as a where  IsNull(a.IsEnabled,0)=1 and  a.KeyWordValue='" + KeyWordValue + "' order by a.OrderNo ", changePassWord[0], changePassWord[1], changePassWord[2]);

        vUserDefinedToolBars.AddRange(GetDefaultUserDefinedToolBars(KeyWordValue, argType));

        if (vDS.Tables.Count > 0 && vDS.Tables[0].Rows.Count > 0)
        {
            DataTable vDT = vDS.Tables[0];
            foreach (DataRow vDr in vDT.Rows)
            {
                UserDefinedToolBar v =
                    new UserDefinedToolBar()
                    {
                        KeyWordValue = vDr["KeyWordValue"].ToString(),
                        ToolName = string.IsNullOrEmpty(vDr["ToolName"].ToString()) ? "无名按钮" : vDr["ToolName"].ToString(),
                        ToolIcon = string.IsNullOrEmpty(vDr["ToolIcon"].ToString()) ? "icon-ok" : vDr["ToolIcon"].ToString(),
                        IsSubFunction = Convert.ToBoolean(vDr["IsSubFunction"].ToString() == "1"),
                        EventCode = vDr["EventCode"].ToString(),
                        OrderNo = string.IsNullOrEmpty(vDr["OrderNo"].ToString()) ? 10 : Convert.ToInt32(vDr["OrderNo"].ToString()),
                        OperationCode = vDr["OpNum"].ToString(),
                        IsEnabled = Convert.ToBoolean(vDr["IsEnabled"].ToString() == "1"),
                        PopUpPage = vDr["PopUpPage"].ToString(),
                        URLTarget = vDr["URLTarget"].ToString(),
                        DialogWidth = string.IsNullOrEmpty(vDr["Width"].ToString()) ? 0 : Convert.ToInt32(vDr["Width"].ToString()),

                        DialogHeight = string.IsNullOrEmpty(vDr["Height"].ToString()) ? 0 : Convert.ToInt32(vDr["Height"].ToString()),
                    };
                vUserDefinedToolBars.Add(v);
            }
        }

        return vUserDefinedToolBars;
    } 

  
}