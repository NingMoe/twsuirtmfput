using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// 网格过滤类
/// </summary>
public class filterRules
{
    /// <summary>
    /// 过滤字段
    /// </summary>
    public string field { get; set; }
    /// <summary>
    /// 过滤方式
    /// </summary>
    public string op { get; set; }
    /// <summary>
    /// 过滤类型
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 选项
    /// </summary>
    //public string options { get; set; }
    /// <summary>
    /// 过滤值
    /// </summary>
    public object value { get; set; }
}


/// <summary>
/// 下拉选择框类
/// </summary>
public class filterRulesComboboxData
{
    public object value { get; set; }
    public string text { get; set; }
}


/// <summary>
/// 下拉选择框类
/// </summary>
public class ComboboxData
{
    public string text { get; set; }
    public List<filterRulesComboboxData> RulesComboboxData { get; set; }
}


/// <summary>
/// 下拉选择框类
/// </summary>
public class Combobox_Data
{
    public string text { get; set; }
    public object id { get; set; }
    public bool selected { get; set; }
}


public class options
{
    public int precision { get; set; }
    public string panelHeight { get; set; }
    public string data { get; set; }
    public string onChange { get; set; }
}


