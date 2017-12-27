using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_CommonPage_SeniorSearchForm : PageBase 
{

    //public string ALLCommonSearchField = "";
    //public string ALLCommonSearchOP = "";
    //public string CommonSearchField = "";
    static Services Resource = new Services();
    public  List<ReadDataColumnSet> CommonSearchField = new List<ReadDataColumnSet>();
    public string BaseKeyWordValue = "";
    public string BaseResid = "";
    public string ReportKey = "";
    public string SearchTitle = "";
    public string IsDynamicallyCreatTableHead = "";
    public string HideQueryBox = "";
    protected void Page_Load(object sender, EventArgs e)
    { 
        BaseKeyWordValue = Request["BaseKeyWordValue"] == null ? "" : Request["BaseKeyWordValue"];
        HideQueryBox = Request["HideQueryBox"] == null ? "" : Request["HideQueryBox"];
        ReportKey = Request["argReportKey"] == null ? "" : Request["argReportKey"];
        BaseResid = Request["BaseResid"] == null ? "" : Request["BaseResid"];
        SearchTitle = Request["SearchTitle"] == null ? "" : Request["SearchTitle"];
        IsDynamicallyCreatTableHead = Request["IsDynamicallyCreatTableHead"] == null ? "" : Request["IsDynamicallyCreatTableHead"];

        string vCommonSearchField = CommonGetInfo.GetALLCommonSearchField(BaseResid, ReportKey,"", IsDynamicallyCreatTableHead, true,false,true).Split( new string[] { "[#]" },StringSplitOptions.RemoveEmptyEntries)[0];

        if (!string.IsNullOrWhiteSpace(vCommonSearchField))
            CommonSearchField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReadDataColumnSet>>(vCommonSearchField);
    }
 
}
