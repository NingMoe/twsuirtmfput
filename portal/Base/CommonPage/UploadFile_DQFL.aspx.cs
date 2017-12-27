using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_AJSJ_UploadFile_DQFL : UserPagebase
{
    protected string ResID = "";
    protected string RecID = "";
    protected string UserID = "";
    protected string KeyWord = "";
    protected string SearchType = "";
    protected string NodeID = "";
    protected string City = "";
    protected string OptionModelStr = "";
    protected string OpenDiv = "";
    protected string gridID = "";
    protected string ChildgridID = "";
    protected string argYear = "";
    protected string argMonth = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        argYear = (Request["argYear"] == null ? "" : Request["argYear"]);
        argMonth = (Request["argMonth"] == null ? "" : Request["argMonth"]);
        KeyWord = (Request["key"] == null ? "" : Request["key"]);
        NodeID = (Request["NodeID"] == null ? "" : Request["NodeID"]);
        ResID = (Request["ResID"] == null ? "" : Request["ResID"]);
        RecID = (Request["RecID"] == null ? "" : Request["RecID"]);
        OpenDiv = (Request["OpenDiv"] == null ? "" : Request["OpenDiv"]);
        gridID = (Request["gridID"] == null ? "" : Request["gridID"]);
        ChildgridID = (Request["ChildgridID"] == null ? "" : Request["ChildgridID"]);
        this.UploadFile1.ResID = ResID;
        this.UploadFile1.RecID = RecID;
        this.UploadFile1.UserID = UserID;
        this.UploadFile1.Savefolder = "../upload/";
        GetExcelHelperOptionModel OptionModel = new GetExcelHelperOptionModel();
        OptionModelStr = Newtonsoft.Json.JsonConvert.SerializeObject(OptionModel);
    }
}