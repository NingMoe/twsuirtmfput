using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;
using System.Data;

public partial class Base_PrintInfo : UserPagebase
{
    public string RecID = "";
    public string UserID = "";
    public string FormNumber = "";
    public long Piece = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Services Resource = new Services();
          
            //开票记录进来打印的
            if (Request["RecID"] != null&&Request["RecID"].ToString() != "")
            {
                DataTable dt= Resource.GetDataListByResID("564794299148", "", " and  ID='" + Request["RecID"].ToString() + "'", CurrentUser.ID).Tables[0];
                FormNumber = dt.Rows[0]["联单编号"].ToString();
                Piece =Convert.ToInt64(dt.Rows[0]["入库件数"].ToString());
            }
        }
    }
}