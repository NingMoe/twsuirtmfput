using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExtensionForms_common_GetOrganizationTree : PageBase
{
    public string SaveType = "";
    public string NoSelectDeptID = "";
    public string DeptID = "";
    public string IsMultiSelect = "";
    public string IsLoadUser = "";
    public string type = "";
    public string IsSelectCompany = "";
    public string IsGetParentNode = "";
      
    protected void Page_Load(object sender, EventArgs e)
    {
        IsMultiSelect = Request["IsMultiSelect"] == null ? "" : Request["IsMultiSelect"].ToString();
        IsLoadUser = Request["IsLoadUser"] == null ? "" : Request["IsLoadUser"].ToString();
        type = Request["type"] == null ? "" : Request["type"].ToString();
        IsSelectCompany = Request["IsSelectCompany"] == null ? "" : Request["IsSelectCompany"].ToString();
        IsGetParentNode = Request["IsGetParentNode"] == null ? "" : Request["IsGetParentNode"].ToString();
    }
}