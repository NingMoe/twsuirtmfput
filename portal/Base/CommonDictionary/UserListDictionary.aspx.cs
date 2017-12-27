using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_CommonDictionary_UserListDictionary : UserPagebase
{
    protected string strParame = "";
    protected string Account = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["parame"] != null) strParame = Server.UrlDecode(Request["parame"]);
        if (Request["account"] != null) Account = Request["account"];
        if (IsPostBack) return; 
        BindData("");
    }

    protected void BindData(string Condition)
    { 
        Services oServices=new Services ();
        repList.DataSource = oServices.GetDataListByResID("521910878147", "", Condition, CurrentUser.ID);
        repList.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Condition = (Account.Trim() != "" ? "and (员工编号 in ('" + Account.Replace(",", "','") + "') OR " : " and (");
        Condition += drpSearch.SelectedItem.Text.Trim() + " like '%" + txtSearch.Text.Trim() + "%')";
        BindData(Condition);
    }
}