using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_CommonPage_TestFiles : UserPagebase 
{
    protected string ResID = "";
    protected string RecID = "";
    protected string UserID = "";
    protected string KeyWord = "";
    protected string SearchType = "";
    protected string gridID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadScript("add");
        if (Request["ResID"] != null) ResID = Request["ResID"];
        if (Request["keyWordValue"] != null) KeyWord = Request["keyWordValue"];
        if (Request["SearchType"] != null) SearchType = Request["SearchType"];
        if (Request["RecID"] != null) RecID = Request["RecID"]; 
        UserID = CurrentUser.ID;
        if (Request["gridID"] != null) gridID = Request["gridID"];


        this.UploadFile1.ResID = ResID;
        this.UploadFile1.UserID = UserID;
     //   this.UploadFile1.SearchType = SearchType;
    }
}