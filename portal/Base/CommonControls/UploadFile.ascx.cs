using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Base_PublicPage_UploadFile : System.Web.UI.UserControl
{
    public string ParentResID = "";
    public string ParentRecID = "";
    public string ResID = "";
    public string Savefolder = "";
    public string UserID = "";
    public string RecID = "";
    public string IsNotViewFiles = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        // Savefolder = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadFileUrl"];
    }
}