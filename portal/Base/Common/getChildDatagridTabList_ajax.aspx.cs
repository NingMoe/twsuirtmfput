using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_Common_getChildDatagridTabList_ajax : UserPagebase
{
    UserInfo oEmployee = null;
    public string titleValue = "";
    public string ResID = "";
    public string keyWordValue = "";
    public string UserID = "";
    public string UserName = "";
    public int PageSize = 100;
    public string SearchType = "";
    public string NodeID = "";
    DataTable TableList = new DataTable();
    public string MasterTableAssociationstr = "";
    public string tabHeight = "400";
    public string tabWidth = "300";
    public string ChildDataGirdTabList = "";
    public string selectRows = "";
     
    public List<MasterTableAssociation> AssociationList = new List<MasterTableAssociation>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        oEmployee = CurrentUser;
        if (oEmployee == null)
        {
            Services Resource = new Services();
            oEmployee = Resource.GetUserInfo(Request["UserID"].ToString());
            Session["Employee"] = oEmployee;
        }

        UserID = oEmployee.ID;
        UserName = oEmployee.Name;
        string typeValue = Request["typeValue"];
        string QueryTZ = Request["QueryTZ"];
        tabHeight = Request["tabHeight"] ==null? "300" : Request["tabHeight"];
        tabWidth = Request["tabWidth"] == null ? "1000" : Request["tabWidth"];
        selectRows = Request["selectRows"] == null ? "[]" : Request["selectRows"];

        List<ChildDataGirdTabList>  ChildTabList = Session["ChildTabList"] == null ? null : Session["ChildTabList"] as List<ChildDataGirdTabList>;

        if (ChildTabList != null && ChildTabList.Count > 0)
            ChildDataGirdTabList = Newtonsoft.Json.JsonConvert.SerializeObject(ChildTabList);

        AssociationList = Session["AssociationList"] == null ? null: Session["AssociationList"] as List<MasterTableAssociation>;
         
        if(AssociationList != null && AssociationList.Count>0)
            MasterTableAssociationstr = Newtonsoft.Json.JsonConvert.SerializeObject(AssociationList);
    }
}