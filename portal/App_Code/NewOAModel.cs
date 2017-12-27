using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ToColumn
{
    public string ShowName { get; set; }
    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 列类型
    /// </summary>
    public int ColumnType { get; set; }
    /// <summary>
    /// 列宽
    /// </summary>
    public int ColumnWidth { get; set; }

    /// <summary>
    /// 是否启用编辑
    /// </summary>
    public bool IsEditColumn { get; set; }

    /// <summary>
    /// 编辑类型
    /// </summary>
    public int editorType { get; set; }

    public ToColumn()
    {
    }

    public ToColumn(string ColumnName)
    {
        this.ColumnName = ColumnName;
        this.ColumnType = 1;
        //this.ColumnType = "System.String";
    }

    public ToColumn(string ColumnName, int ColumnType)
    {
        this.ColumnName = ColumnName;
        this.ColumnType = ColumnType;
    }

}

/// <summary>
/// 动态表头类
/// </summary>
public class DynamicHeadReport
{
    /// <summary>
    /// ReportKey
    /// </summary>
    public string ReportKey { get; set; }
    /// <summary>
    /// 是否子表
    /// </summary>
    public bool IsChild { get; set; }
    /// <summary>
    /// 需要的列
    /// </summary>
    public List<ToColumn> NeedColumn { get; set; }

    /// <summary>
    /// 冻结列
    /// </summary>
    public List<ToColumn> frozenColumns { get; set; }

    /// <summary>
    /// 不需要的列
    /// </summary>
    public List<ToColumn> NoNeedColumn { get; set; }
    /// <summary>
    /// 默认列宽
    /// </summary>
    public int CommonDynamicHeadWidth { get; set; }
    /// <summary>
    /// 所有列都是编辑列
    /// </summary>
    public bool IsAllEditColumn { get; set; }

}


public class ChildDataGirdTabList
{
    public string ReportKey { get; set; }
    public string ChildKeyWord { get; set; }
    public string ShowTitle { get; set; }
    public string ChildUserDefinedSql { get; set; }
    public string ChildORDERBY { get; set; }
    public DynamicHeadReport DynamicHeadqueryParamsChild { get; set; }
    public string ProcName { get; set; }
    public string ProcOutPutCount { get; set; }
    public string ProcInPutStr { get; set; }
    public string GetDataByType { get; set; }
    public string ProcInPutStrByHead { get; set; }
    public string hasPermission { get; set; }
    public UserDefinedToolBars DefinedToolBars { get; set; }
}


public class UserDefinedToolBars
{
    public bool isAddDisabled { get; set; }
    public bool IsUpdateDisabled { get; set; }
    public bool isDeleteDisabled { get; set; }
    public string addUrl { get; set; }
    public string editUrl { get; set; }
    public int DialogWidth { get; set; }
    public int DialogHeight { get; set; }
    public bool IsAddDefaultTools { get; set; }
    public string hasPermission { get; set; }
}
 