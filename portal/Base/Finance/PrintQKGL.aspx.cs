using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class PrintQKGL : UserPagebase
{
    protected string ResID = "282994819609";
    protected string RecID = "";
    protected string UserID = "";
    protected string SQRQ = "";//申请日期
    protected string QKJE = "";   //请款金额
    protected string CGJEHJ = "";//采购金额合计
    protected string QKSY = "";//请款事由
    protected string PZSFYDCW = "";//凭证是否已到财务
    protected string SFYHK = "";//是否已汇款
    protected string SFYZWBX = "";//是否已为报销
    protected string GLBXDBH = "";//关联报销单编号
    protected string LCBH = "";//流程编号
    protected string SQR = "";//申请人
    protected string TD = "";//退单
    protected string BXKMDM = "";//报销科目代码
    protected string BXKMMC = "";//报销科目名称
    protected string WBXMBH = "";//外包项目编号
         
    protected void Page_Load(object sender, EventArgs e)
    {
        WebServices.Services Resource = new WebServices.Services();
        UserID = CurrentUser.ID;
        if (Request["RecID"]!=null&&Request["RecID"].ToString()!="")
        {
            RecID = Request["RecID"].ToString();
            DataTable dt = Resource.GetDataListByResID(ResID, "", " and ID=" + RecID, UserID).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                LCBH = dt.Rows[0]["流程编号"].ToString();//流程编号
                SQRQ = dt.Rows[0]["申请日期"].ToString();//申请日期
                SQR = dt.Rows[0]["申请人"].ToString();//申请人
                CGJEHJ = dt.Rows[0]["采购金额合计"].ToString();//采购金额合计
                QKSY = dt.Rows[0]["请款事由"].ToString();//请款事由
                QKJE = dt.Rows[0]["请款金额"].ToString();//请款金额
                TD = dt.Rows[0]["退单"].ToString();//退单
                SFYHK = dt.Rows[0]["是否已汇款"].ToString();//是否已汇款
                SFYZWBX = dt.Rows[0]["是否已转为报销"].ToString();//是否已转为报销
                GLBXDBH = dt.Rows[0]["关联报销单编号"].ToString();//关联报销单编号
                BXKMDM = dt.Rows[0]["报销科目代码"].ToString();//报销科目代码
                BXKMMC = dt.Rows[0]["报销科目名称"].ToString();//报销科目名称
                WBXMBH = dt.Rows[0]["外包项目编号"].ToString();//外包项目编号

            }
        }
    }
}