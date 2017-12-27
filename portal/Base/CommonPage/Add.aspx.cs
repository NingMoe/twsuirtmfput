using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_Add : UserPagebase
{
    protected string ResID = ""; 
    protected string RecID = "";
    public string UserID = "";
    public string UserName = "";
    protected string KeyWord = "";
    protected string ParentKey = "";
    protected string SearchType = "";
    protected string gridID = "";
    string NodeID = "";
    //protected string NodeCode = "0";
    //protected string NodeName = "旭辉档案分类";
    protected string ParentResID = "";
    protected string ParentRecID = "";
    //DataTable TableList = new DataTable();
    protected string strTilte = "";
    public string Time = DateTime.Now.ToString("yyyy-MM-dd");
    //protected string  MasterCol = "";
    protected string strUrl = "";
    WebServices.Services Resource = new WebServices.Services();
    protected void Page_Load(object sender, EventArgs e)
    {
       // LoadScript("add");
        
        if (Request["ResID"] != null) ResID = Request["ResID"];
        if (Request["ParentResID"] != null) ParentResID = Request["ParentResID"];
        if (Request["ParentRecID"] != null) ParentRecID = Request["ParentRecID"];
        if (Request["keyWordValue"] != null) KeyWord = Request["keyWordValue"];
        if (Request["ParentKey"] != null) ParentKey = Request["ParentKey"];
        if (Request["SearchType"] != null) SearchType = Request["SearchType"];
        if (Request["RecID"] != null) RecID = Request["RecID"];
        if (Request["NodeID"] != null) NodeID = Request["NodeID"];
        if (Request["gridID"] != null) gridID = Request["gridID"];
        else gridID = "CenterGrid"+KeyWord;
        string UserControlsUrl = "";
        if (Request["UCurl"] != null) UserControlsUrl = Request["UCurl"];
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;
        if (IsPostBack) return;
        SysSettings sys = Common.GetSysSettingsByENKey(KeyWord);
        ResID = sys.ResID;
        txtResourceID.Text = ResID;

        PlaceHolder1.Controls.Clear();

        Control oControl = LoadControl("../UserControls/" + UserControlsUrl.Trim());
        if (oControl.FindControl("txtDivTitle") != null) strTilte = ((TextBox)oControl.FindControl("txtDivTitle")).Text;
        else strTilte = "——";
        PlaceHolder1.Controls.Add(oControl);


        strUrl = "../Common/CommonAjax_Request.aspx?typeValue=SaveInfo&ResID=" + ResID + "&RecID=" + RecID;
        if (ParentKey.Trim() != "" && ParentRecID.Trim() != "")
            strUrl = "../Common/CommonAjax_Request.aspx?typeValue=SaveChildInfo&ResID=" + ResID + "&ParentResID=" + ParentResID + "&ParentRecID=" + ParentRecID;


        //if (ParentKey.Trim() != "" && ParentRecID.Trim() != "")
        //{
        //    DataTable dt = Resource.SelectData("select LedgerChildKey  from MasterTableAssociation where MasterKeyWord='" + ParentKey + "'   and ChildKeyWord='" + KeyWord + "'").Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        string[] ch = { "," };
        //        MasterCol = dt.Rows[0]["LedgerChildKey"].ToString().Trim(); 
        //    }
        //}
    } 
}