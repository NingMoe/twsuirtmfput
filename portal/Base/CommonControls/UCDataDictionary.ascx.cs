using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Base_CommonControls_UCDataDictionary : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //WebServices.Services oServices = new WebServices.Services();
        //DataTable dt = oServices.GetDictionaryList(ResourceID, ResourceColumn).Tables[0];
      //  imgDataDictionary.Attributes.Add("onclick", "OpenDictionary(this,'" + ResourceTableName.Trim() + "','" + ResourceColumn.Trim() + "')");
    }

    private string _ResourceID = "";
    public string ResourceID
    {
        get { return _ResourceID; }
        set { _ResourceID = value; }
    }

    //private string _ResourceTableName = "";
    //public string ResourceTableName
    //{
    //    get { return _ResourceTableName; }
    //    set { _ResourceTableName = value; }
    //}
    private string _ResourceColumn = "";
    public string ResourceColumn
    {
        get { return _ResourceColumn; }
        set { _ResourceColumn = value; }
    }

    private int _ChildIndex = -1;
    public int ChildIndex
    {
        get { return _ChildIndex; }
        set { _ChildIndex = value; }
    }

    private Int32 _IsMultiselect = 0;
    public int IsMultiselect
    {
        get { return _IsMultiselect; }
        set { _IsMultiselect = value; }
    }

    private Int32 _IsAppend = 0;
    public int IsAppend
    {
        get { return _IsAppend; }
        set { _IsAppend = value; }
    }

    

    private int _DicWidth = 0;
    public int DicWidth
    {
        get { return _DicWidth; }
        set { _DicWidth = value; }
    }

    private int _DicHeight = 0;
    public int DicHeight
    {
        get { return _DicHeight; }
        set { _DicHeight = value; }
    }
}