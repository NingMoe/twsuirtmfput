using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

public class SysSettings
{

    private bool _IsOrder;

    public bool IsOrder
    {
        get { return _IsOrder; }
        set { _IsOrder = value; }
    }

    private string _WDTJ;

    //public string WDTJ
    //{
    //    get { return _WDTJ; }
    //    set { _WDTJ = value; }
    //}

    private int _DialogWidth;
    private int _DialogHeight;

    public int DialogHeight
    {
        get { return _DialogHeight; }
        set { _DialogHeight = value; }
    }

    public int DialogWidth
    {
        get { return _DialogWidth; }
        set { _DialogWidth = value; }
    }

    private string _LBLX;

    public string LBLX
    {
        get { return _LBLX; }
        set { _LBLX = value; }
    }

    private bool _IsStartWidth;

    public bool IsStartWidth
    {
        get { return _IsStartWidth; }
        set { _IsStartWidth = value; }
    }

  

    private string _SettingsNum;

    public string SettingsNum
    {
        get { return _SettingsNum; }
        set { _SettingsNum = value; }
    }

    private string _keyWordValue;

    public string KeyWordValue
    {
        get { return _keyWordValue; }
        set { _keyWordValue = value; }
    } 

    /// <summary>
    /// 附件字段
    /// </summary>
    /// <remarks></remarks>
    private string _AccessoryCol;
    public string AccessoryCol
    {
        get { return _AccessoryCol; }
        set { _AccessoryCol = value; }
    }

    /// <summary>
    /// 附件表的RESID
    /// </summary>
    /// <remarks></remarks>
    private string _AccessoryResID;
    public string AccessoryResID
    {
        get { return _AccessoryResID; }
        set { _AccessoryResID = value; }
    }
    /// <summary>
    /// 到附件表中查询附件条件
    /// </summary>
    /// <remarks></remarks>
    private string _FJGLJD;
    public string FJGLJD
    {
        get { return _FJGLJD; }
        set { _FJGLJD = value; }
    }

    /// <summary>
    /// 添加地址
    /// </summary>
    /// <remarks></remarks>
    private string _AddUrl;
    public string AddUrl
    {
        get { return _AddUrl; }
        set { _AddUrl = value; }
    }
    /// <summary>
    /// 修改地址
    /// </summary>
    /// <remarks></remarks>
    private string _EidtUrl;
    public string EidtUrl
    {
        get { return _EidtUrl; }
        set { _EidtUrl = value; }
    }



    private string _URLTarget;
   public string URLTarget
    {
        get { return _URLTarget; }
        set { _URLTarget = value; }
    }

    /// <summary>
    /// 左对齐显示字段
    /// </summary>
    /// <remarks></remarks>
    private string _AlignColStr;
    public string AlignColStr
    {
        get { return _AlignColStr; }
        set { _AlignColStr = value; }
    }


    /// <summary>
    /// 参数关键字（大部分是表名）
    /// </summary>
    /// <remarks></remarks>
    private string _ENTableName;
    public string ENTableName
    {
        get { return _ENTableName; }
        set { _ENTableName = value; }
    }



    /// <summary>
    /// 关键字（大部分是表名）
    /// </summary>
    /// <remarks></remarks>
    private string _ShowTitle;
    public string ShowTitle
    {
        get { return _ShowTitle; }
        set { _ShowTitle = value; }
    }

    /// <summary>
    /// 值（大部分是Resid）
    /// </summary>
    /// <remarks></remarks>
    private string _ResID;
    public string ResID
    {
        get { return _ResID; }
        set { _ResID = value; }
    }
     

    private bool _IsDelete = false;

    public bool IsDelete
    {
        get { return _IsDelete; }
        set { _IsDelete = value; }
    }


    private bool _IsAdd = false;

    public bool IsAdd
    {
        get { return _IsAdd; }
        set { _IsAdd = value; }
    }

    private bool _IsUpdate = false;

    public bool IsUpdate
    {
        get { return _IsUpdate; }
        set { _IsUpdate = value; }
    }

    private bool _IsExp = false;

    public bool IsExp 
    {
        get { return _IsExp; }
        set { _IsExp = value; }
    }

    private bool _IsDeleteRights = false;

    public bool IsDeleteRights
    {
        get { return _IsDeleteRights; }
        set { _IsDeleteRights = value; }
    }


    private bool _IsAddRights = false;

    public bool IsAddRights
    {
        get { return _IsAddRights; }
        set { _IsAddRights = value; }
    }

    private bool _IsUpdateRights = false;

    public bool IsUpdateRights
    {
        get { return _IsUpdateRights; }
        set { _IsUpdateRights = value; }
    }

    private bool _IsExpRights = false;

    public bool IsExpRights
    {
        get { return _IsExpRights; }
        set { _IsExpRights = value; }
    }

    private bool _IsCopy = false;

    public bool IsCopy
    {
        get { return _IsCopy; }
        set { _IsCopy = value; }
    }

    private bool _IsRowRight = false;//是否启用行过滤 

    public bool IsRowRight
    {
        get { return _IsRowRight; }
        set { _IsRowRight = value; }
    }

    private string _DefaultOrder;

    public string DefaultOrder
    {
        get { return _DefaultOrder; }
        set { _DefaultOrder = value; }
    }

    private string _SortBy="";

    public string SortBy
    {
        get { return _SortBy; }
        set { _SortBy = value; }
    }


    private string _TableName = "";

    public string TableName
    {
        get { return _TableName; }
        set { _TableName = value; }
    }


    private bool  _IsCheckBox;

    public bool IsCheckBox
    {
        get { return _IsCheckBox; }
        set { _IsCheckBox = value; }
    }

    /// <summary>
    /// 列表数据初始化筛选条件
    /// </summary>
    private string _filterCondtion;
    public string FilterCondtion
    {
        get { return _filterCondtion; }
        set { _filterCondtion = value; }
    }


    /// <summary>
    /// 统计字段
    /// </summary>
    private string _FootStr;
    public string FootStr
    {
        get { return _FootStr; }
        set { _FootStr = value; }
    }
}
