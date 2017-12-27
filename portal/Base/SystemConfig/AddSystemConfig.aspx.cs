using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetReusables;

public partial class Base_AddSystemConfig : UserPagebase
{
    public string ResID = "";
    public string RecID = "0";
    protected string NodeID = "";
    public string UserID = "";
    public string UserName = "";
    protected string SearchCol = "";
    
    DataTable TableList = new DataTable();
    WebServices.Services Resource = new WebServices.Services();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("add");
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (CurrentUser.DepartmentName != "系统管理员" && CurrentUser.ID != "sysuser")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (Request["ResID"] != null)
        {
            ResID = Request["ResID"];
        }
        if (Request["NodeID"] != null)
        {
            NodeID = Request["NodeID"];
        }
        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"].ToString();
        }

        this.SelectRecords_Sql1.SearchType = "";
        this.SelectRecords_Sql1.ColumnName = "默认排序";
        this.SelectRecords_Sql1.SetValueResID = ResID; 
        this.SelectRecords_Sql1.keyWordValue = "Col";
        this.SelectRecords_Sql1.ControlWidth = 205;
        this.SelectRecords_Sql1.MustWrite = "" ;
        this.SelectRecords_Sql1.SetValueStr = "默认排序=显示字段名"; 
        this.SelectRecords_Sql1.idField = "显示字段名";
        this.SelectRecords_Sql1.textField = "显示字段名";
        this.SelectRecords_Sql1.UserDefinedSql = NodeID;
        this.SelectRecords_Sql1.IsmultiSelect = true;
        this.SelectRecords_Sql1.ROW_NUMBER_ORDER = " Order BY 排序";

        BindData();
    }

    void BindData()
    {
        long ResID = Resource.GetTopParentID(NodeID);
        string strSql =  "select T.*,(Case when IsNull(Col.ID,0)=0 then CD_DISPNAME else Col.ShowColumnName end) CD_ShowName,IsNull(Col.OrderNum,0) OrderNum,(Case when IsNull(Col.ID,0)=0 then 0 else 1 end) IsBe  from(SELECT [CD_ID],[CD_RESID] ,[CD_COLNAME],[CD_DISPNAME],(case CD_Type when 1 then 'nvarchar' when 2 then 'float' when 3 then 'int' when 4 then 'datetime'  when 5 then 'ntext'  when 6 then 'image'  when 7 then 'money' when 8 then 'datetime' when 9 then 'bit' when 10 then 'ntext'  end) DataType,CS_SHOW_ORDER FROM [CMS_TABLE_DEFINE] D join  [CMS_TABLE_SHOW] S on D.CD_COLNAME=S.CS_COLNAME and D.CD_RESID=S.CS_RESID where CD_RESID  in (" + ResID + "))T left join (select R.ID,R.ColumnName,R.ShowColumnName,R.OrderNum from SysSettings S join ResourceColumn R on S.ENKeyWord=R.KeyWord where  S.id=" + RecID + ")Col on Col.ColumnName=T.CD_DISPNAME    order by IsBe desc, OrderNum , CS_SHOW_ORDER;" +
            " select * from (select KeyWord, SearchCol=stuff((select ','+SearchCol from Sys_CXZJLB t where KeyWord=t.KeyWord and KeyWord in (select  ENKeyWord from dbo.SysSettings where id=" + RecID + ") for xml path('')), 1, 1, '') from Sys_CXZJLB  group by KeyWord) tab ";
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]);
        DataTable dt = ds.Tables[0];
        dlCol.DataSource = dt;
        dlCol.DataBind();
        if (ds.Tables[1].Rows.Count > 0)
        {
            DataRow dr=ds.Tables[1].Rows[0];
            SearchCol = DbField.GetStr(ref dr, "SearchCol");
        }
        
    } 
}