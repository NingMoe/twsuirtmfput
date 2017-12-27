using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebServices;

/// <summary>
/// 报表表头信息
/// </summary>
public class ReportHeaderInfo
{
    /// <summary>
    /// 报表Key
    /// </summary>
    public string ReportKey { get; set; }
    /// <summary>
    /// 报表行集合
    /// </summary>
    public List<ReportRowInfo> ReportRowInfo { get; set; }


    public static ReportHeaderInfo GetReportHeaderInfo(string argReportKey, bool IsChild)
    {
        ReportHeaderInfo vReportHeaderInfo = new ReportHeaderInfo();

        if (string.IsNullOrEmpty(argReportKey)) return vReportHeaderInfo;

        string QueryStr = " and ( IsChild ='' or  IsChild is null )";
        if (IsChild)
            QueryStr = " and IsChild=1 ";

        vReportHeaderInfo.ReportRowInfo = new List<ReportRowInfo>();
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();

        DataTable dt = Resource.Query(" select * from " + Resource.GetTableNameByResourceid("500915419601") + " where ReportKey='" + argReportKey + "' " + QueryStr, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        if (dt.Rows.Count > 0)
        {
            int lastr = 0;
            ReportRowInfo vReportRowInfo = new ReportRowInfo();

            foreach (DataRow vDr in dt.Rows)
            {
                if (lastr != (string.IsNullOrEmpty(vDr["RowNo"].ToString()) ? 0 : Convert.ToInt32(vDr["RowNo"].ToString())) || lastr == 0)
                {
                    if (lastr > 0)
                    {
                        vReportHeaderInfo.ReportRowInfo.Add(vReportRowInfo);
                    }
                    vReportRowInfo = new ReportRowInfo();
                    vReportRowInfo.ReportColumnInfo = new List<ReportColumnInfo>();
                    vReportRowInfo.RowNo = string.IsNullOrEmpty(vDr["RowNo"].ToString()) ? 0 : Convert.ToInt32(vDr["RowNo"].ToString());
                    lastr = vReportRowInfo.RowNo;
                }

                ReportColumnInfo vReportColumnInfo = new ReportColumnInfo();
                vReportColumnInfo.ReportKey = string.IsNullOrEmpty(vDr["ReportKey"].ToString()) ? "" : vDr["ReportKey"].ToString();
                vReportColumnInfo.RowNo = Convert.ToInt32(vDr["RowNo"].ToString());
                vReportColumnInfo.ColumnNo = Convert.ToInt32(vDr["ColumnNo"].ToString());
                vReportColumnInfo.RowHeight = string.IsNullOrEmpty(vDr["RowHeight"].ToString()) ? "" : vDr["RowHeight"].ToString();
                vReportColumnInfo.ColumnWidth =
                  string.IsNullOrEmpty(vDr["ColumnWidth"].ToString()) ? "80" : vDr["ColumnWidth"].ToString();
                vReportColumnInfo.ShowName = string.IsNullOrEmpty(vDr["ShowName"].ToString()) ? "" : vDr["ShowName"].ToString();
                vReportColumnInfo.FieldName = string.IsNullOrEmpty(vDr["FieldName"].ToString()) ? "" : vDr["FieldName"].ToString();
                vReportColumnInfo.DataType = string.IsNullOrEmpty(vDr["DataType"].ToString()) ? 1 : Convert.ToInt32(vDr["DataType"].ToString());
                vReportColumnInfo.IsHide = Convert.ToBoolean(vDr["IsHide"].ToString() == "1");
                vReportColumnInfo.AcrossColumns = string.IsNullOrEmpty(vDr["AcrossColumns"].ToString()) ? 0 : Convert.ToInt32(vDr["AcrossColumns"].ToString());
                vReportColumnInfo.InterbankNumber = string.IsNullOrEmpty(vDr["InterbankNumber"].ToString()) ? 0 : Convert.ToInt32(vDr["InterbankNumber"].ToString());
                vReportColumnInfo.IsAddCommonQuery = Convert.ToBoolean(vDr["IsAddCommonQuery"].ToString() == "1");

                vReportRowInfo.ReportColumnInfo.Add(vReportColumnInfo);
                vReportRowInfo.ReportKey = vReportColumnInfo.ReportKey;
            }
            vReportHeaderInfo.ReportRowInfo.Add(vReportRowInfo);
            vReportHeaderInfo.ReportKey = vReportRowInfo.ReportKey;
        }
        return vReportHeaderInfo;
    }

}


/// <summary>
/// 报表行集合
/// </summary>
public class ReportRowInfo
{
    /// <summary>
    /// 报表Key
    /// </summary>
    public string ReportKey { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int RowNo { get; set; }
    /// <summary>
    /// 报表列集合
    /// </summary>
    public List<ReportColumnInfo> ReportColumnInfo { get; set; }
}

/// <summary>
/// 报表列集合
/// </summary>
public class ReportColumnInfo
{
    /// <summary>
    /// 报表Key
    /// </summary>
    public string ReportKey { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int RowNo { get; set; }
    /// <summary>
    /// 列号
    /// </summary>
    public int ColumnNo { get; set; }
    /// <summary>
    /// 行高
    /// </summary>
    public string RowHeight { get; set; }
    /// <summary>
    /// 列宽
    /// </summary>
    public string ColumnWidth { get; set; }
    /// <summary>
    /// 显示名称
    /// </summary>
    public string ShowName { get; set; }
    /// <summary>
    /// 字段名称
    /// </summary>
    public string FieldName { get; set; }
    /// <summary>
    /// 跨列数
    /// </summary>
    public int AcrossColumns { get; set; }
    /// <summary>
    /// 跨行数
    /// </summary>
    public int InterbankNumber { get; set; }
    /// <summary>
    /// 其他事件Js
    /// </summary>
    public string OtherEventJs { get; set; }
    /// <summary>
    /// 数据类型
    /// </summary>
    public int DataType { get; set; }
    /// <summary>
    /// 是否隐藏
    /// </summary>
    public bool IsHide { get; set; }
    /// <summary>
    /// 是否添加公共查询
    /// </summary>
    public bool IsAddCommonQuery { get; set; }
}


public class ReportFoot
{
    public string FootName { get; set; }
    public string FootInColumn { get; set; }
    public string SummaryType { get; set; }
    public bool IsShow { get; set; }

}


