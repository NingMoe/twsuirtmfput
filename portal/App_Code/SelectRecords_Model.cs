using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

[Serializable]
/// <summary>
/// SelectRecords_Model 的摘要说明
/// </summary>
public class SelectRecords_Model
{
    /// <summary>
    /// 标题
    /// </summary>
    public string titleValue { get; set; }  
    /// <summary
    /// ResID（选择框ID前缀名）
    /// </summary>
    public string ResID { get; set; } 
    /// <summary>
    /// 
    /// </summary>
    public string SelectkeyWordValue { get; set; }
    /// <summary>
    /// 选择项的ResID
    /// </summary>
    public string SelectResID { get; set; } 
    /// <summary>
    /// 设置值的ResID
    /// </summary>
    public string SetValueResID { get; set; } 
    /// <summary>
    /// 是否设置为只读值
    /// </summary>
    public string SetResIDReadOnly { get; set; } 
    /// <summary>
    /// 
    /// </summary>
    public string SelectfieldValue { get; set; } 
    /// <summary>
    /// 选择框ID(由ResID+Key)组成
    /// 如果设置了UserDefinedKey，则使用UserDefinedKey作为选择框ID
    /// </summary>
    public string ResID_Key { get; set; } 
    /// <summary>
    /// 是否有选择后操作
    /// </summary>
    public bool HasLastOperation { get; set; }
    /// <summary>
    /// 是否有选择前操作
    /// </summary>
    public bool HasFirstOperation { get; set; } 
    /// <summary>
    /// 检索字段
    /// </summary>
    public string QueryKeyField { get; set; }
    /// <summary>
    /// 自定义SQL
    /// </summary>
    public string UserDefinedSql { get; set; } 
    /// <summary>
    /// 自定义选择框ID
    /// </summary>
    public string UserDefinedKey { get; set; } 
    /// <summary>
    /// 参数关键字
    /// </summary>
    public string keyWordValue { get; set; } 
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserID { get; set; }
    /// <summary>
    /// 显示数据条数
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 显示数据页数
    /// </summary>
    public int PageNumber { get; set; } 
    /// <summary>
    /// 查询类型
    /// </summary>
    public string SearchType { get; set; }
    /// <summary>
    /// 设置其他SelectRecord的值
    /// </summary>
    public string SetSelectRecordValue { get; set; } 
    /// <summary>
    /// 选择框宽度
    /// </summary>
    public int ControlWidth { get; set; }
    /// <summary>
    /// 选择面板宽度
    /// </summary>
    public int panelWidth { get; set; }
    /// <summary>
    /// 排序字段
    /// </summary>
    public string OrderByStr { get; set; }
    /// <summary>
    /// 显示字段
    /// </summary>
    public string idField { get; set; } 
    /// <summary>
    /// 取值字段
    /// </summary>
    public string textField { get; set; }
    /// <summary>
    /// 选择框ID后缀名
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// NodeID
    /// </summary>
    public long NodeID { get; set; }
    /// <summary>
    /// IsUpdate
    /// </summary>
    public string IsUpdate { get; set; }
    //{
    //    get { return "false"; }
    //    set {; }
    //}
    /// <summary>
    /// IsDelete
    /// </summary>
    public string IsDelete { get; set; }
    /// <summary>
    /// 是否只读
    /// </summary>
    public string IsRead { get; set; }
    /// <summary>
    /// Time
    /// </summary>
    public string Time { get; set; }
    //{
    //    get { return DateTime.Today.ToString("yyyy-MM-dd"); }
    //    set {; }
    //}
    /// <summary>
    /// 筛选条件
    /// </summary>
    protected string Condition { get; set; } 
    /// <summary>
    /// 设置其他Input框
    /// </summary>
    public string SetValueStr { get; set; }
    /// <summary>
    /// 排序分页字段
    /// </summary>
    public string ROW_NUMBER_ORDER { get; set; }
    /// <summary>
    /// 是否添加行筛选
    /// </summary>
    public int IsAddRowFit { get; set; }
    /// <summary>
    /// 是否为必填
    /// </summary>
    public string MustWrite { get; set; }
    /// <summary>
    /// 是否多选
    /// </summary>
    public bool IsmultiSelect { get; set; } 
}




[Serializable]
/// <summary>
/// 克隆类
/// </summary>
public static class ObjectClone
{
    /// <summary>
    /// 实现深度复制
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="RealObject"></param>
    /// <returns></returns>
    public static T Clone<T>(T RealObject)
    {
        using (Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, RealObject);
            objectStream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(objectStream);
        }
    }
}
