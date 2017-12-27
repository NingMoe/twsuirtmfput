using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_SolidWaste_AddOrEditOutsource : UserPagebase
{
    public string ResID = "564794299148";//入库信息
    public string RecID = "";  //记录ID
    public string UserID = "";//当前用户账号
    public string UserName = "";//当前登录用户姓名
    public string keyWordValue = "";
    public string SearchType = "";
    public string FormNumber = "";
    public string WasteCode = "";
    public string WasteName = "";
    public string ProduceUnit = "";
    public string TransportUnit = "";
    public string LicensePlate = "";
    public string WasteShape = "";
    public string Pack = "";
    public string Piece = "";
    public string Treatment = "";
    public string Date = "";
    public string Quantity = "";
    public string Operator = "";
    DataTable TableList = new DataTable();
    public string Time = DateTime.Now.ToString("yyyy.MM.dd");
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        UserName = CurrentUser.Name;

        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"].ToString();
        }
        if (Request["keyWordValue"] != null)
        {
            keyWordValue = Request["keyWordValue"].ToString();
        }
        if (Request["SearchType"] != null)
        {
            SearchType = Request["SearchType"].ToString();
        }
    }
}