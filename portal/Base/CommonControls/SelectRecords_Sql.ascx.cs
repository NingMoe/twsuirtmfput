using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonControls_SelectRecords_Sql : System.Web.UI.UserControl
{
    public SelectRecords_Model SelectRecords_Model = null;
 
    /// <summary>
    /// 设置值的ResID
    /// </summary>
    public string SetValueResID = ""; 
    /// <summary>
    /// 是否有选择后操作
    /// 调用方法 LastOperation();
    /// </summary>
    public bool HasLastOperation = false;
    /// <summary>
    /// 是否有选择前操作
    /// 调用方法 FirstOperation();
    /// </summary>
    public bool HasFirstOperation = false;

    /// <summary>
    /// 自定义SQL
    /// </summary>
    public string UserDefinedSql = "";

    /// <summary>
    /// 字段名称
    /// </summary>
    public string ColumnName = "";
    /// <summary>
    /// 参数关键字
    /// </summary>
    public string keyWordValue = "";

    /// <summary>
    /// 显示数据条数
    /// </summary>
    public int PageSize = 20;
    /// <summary>
    /// 显示数据页数
    /// </summary>
    public int PageNumber = 1;
    /// <summary>
    /// 查询类型
    /// </summary>
    public string SearchType = "";
 
    /// <summary>
    /// 选择框宽度
    /// </summary>
    public int ControlWidth = 0;
   


    public string idField = "";

    public string textField = "";
     
    /// <summary>
    /// 是否只读
    /// </summary>
    public string IsReadOnly = "false";
   
    /// <summary>
    /// 设置其他Input框
    /// </summary>
    public string SetValueStr = "";
    /// <summary>
    /// 排序分页字段
    /// </summary>
    public string ROW_NUMBER_ORDER = "";


    /// <summary>
    /// 是否为必填
    /// </summary>
    public string MustWrite = "";

    /// <summary>
    /// 是否多选
    /// </summary>
    public bool IsmultiSelect = false;

    /// <summary>
    /// 检索字段
    /// </summary>
    protected string QueryKeyField = "";
    /// <summary>
    /// 
    /// </summary>
    protected string SelectfieldValue = "";

    protected string ResID_Key = "";
    protected int panelWidth = 0;
    /// <summary>
    /// 列表配置
    /// </summary>
    protected SysSettings sys = new SysSettings();


    WebServices.Services Resource = new WebServices.Services();

    DataTable TableList = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (SelectRecords_Model != null)
        { 
            SetValueResID = SelectRecords_Model.SetValueResID; 
            SelectfieldValue = SelectRecords_Model.SelectfieldValue;
            ResID_Key = SelectRecords_Model.ResID_Key;
            HasLastOperation = SelectRecords_Model.HasLastOperation;
            HasFirstOperation = SelectRecords_Model.HasFirstOperation;
            QueryKeyField = SelectRecords_Model.QueryKeyField;
            UserDefinedSql = SelectRecords_Model.UserDefinedSql; 
            keyWordValue = SelectRecords_Model.keyWordValue;
            ColumnName = SelectRecords_Model.ColumnName;
            PageSize = SelectRecords_Model.PageSize <= 0 ? 1 : SelectRecords_Model.PageSize;
            PageNumber = SelectRecords_Model.PageNumber <= 0 ? 20 : SelectRecords_Model.PageNumber; ;
            SearchType = SelectRecords_Model.SearchType;
            ControlWidth = SelectRecords_Model.ControlWidth; 
            idField = SelectRecords_Model.idField;
            textField = SelectRecords_Model.textField; 
            SetValueStr = SelectRecords_Model.SetValueStr;
            ROW_NUMBER_ORDER = SelectRecords_Model.ROW_NUMBER_ORDER; 
            MustWrite = SelectRecords_Model.MustWrite;
            IsmultiSelect = SelectRecords_Model.IsmultiSelect;
        }

        if (string.IsNullOrEmpty(keyWordValue)) return;


        if (sys != null && !string.IsNullOrEmpty(sys.ShowTitle))
        {
            SetValueResID = sys.ResID;
        }

        string strSql = "SELECT [CD_ID],[CD_RESID] ,[CD_COLNAME] 内部字段名,[CD_DISPNAME] 显示字段名,CS_SHOW_ORDER 排序 FROM [CMS_TABLE_DEFINE] D join  [CMS_TABLE_SHOW] S on D.CD_COLNAME=S.CS_COLNAME and D.CD_RESID=S.CS_RESID where CD_RESID='" + UserDefinedSql + "'"; ;
        if (UserDefinedSql.ToLower().IndexOf("order by")>0) UserDefinedSql.Substring(0, UserDefinedSql.ToLower().IndexOf("order by"));
        if (strSql.ToLower().IndexOf("where") < 0) strSql = strSql + " where 1=2";
        else strSql = strSql + " and 1=2";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt =Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        string gridField = "";
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            QueryKeyField += "," + dt.Columns[i].ColumnName;
            gridField += "{field: '" + dt.Columns[i].ColumnName + "',title:'" + dt.Columns[i].ColumnName + "',width:100, sortable: true ,align:'center'},";
            panelWidth += 100;
        }
        //if (panelWidth > 300) panelWidth = 300;
        SelectfieldValue = "[[" + gridField.Substring(0, gridField.Length - 1) + "]]";
        if (QueryKeyField.Trim() != "") QueryKeyField = QueryKeyField.Substring(1);

        ResID_Key = SetValueResID + "_" + keyWordValue;
        if (ControlWidth == 0) ControlWidth = 150;
    }
}