using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class PrintBXGL : UserPagebase
{
    protected string ResID = "184270067595";
    protected string RecID = "";
    protected string UserID = "";
    protected string BXDBH = "";//报销单编号
    protected string RQ = "";   //日期
    protected string BXR = "";//报销人
    protected string BXNR = "";//报销内容
    protected string BXSM = "";//报销说明
    protected string BXJE = "";//报销金额
    protected string PZZS = "";//凭证张数
    protected string BXKMDM = ""; //报销科目代码
    protected string BXKMMC = ""; //报销科目名称
    protected string WBXMBH = ""; //外包项目编号
    protected string BZ = ""; //备注
    protected string SFYHK = "";//是否已汇款
    protected string PZSFYDCW = "";//凭证是否已到财务
    protected string SFYYQK = "";//是否已有请款
    protected string QKBH = "";//请款编号
    protected string QKJE = "";//请款金额
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        WebServices.Services Resource = new WebServices.Services();
        try
        {
            if (Request["RecID"] != null && Request["RecID"].ToString() != "")
            {
                RecID = Request["RecID"].ToString();
                DataTable dt = Resource.GetDataListByResID(ResID, "", " and ID=" + RecID, UserID).Tables[0];
                if (dt!=null&&dt.Rows.Count>0)
                {
                    BXDBH = dt.Rows[0]["报销单编号"].ToString();//报销单编号
                    RQ = dt.Rows[0]["日期"].ToString();//日期
                    BXR = dt.Rows[0]["报销人"].ToString();//报销人
                    BXNR = dt.Rows[0]["报销内容"].ToString();//报销内容
                    BXSM = dt.Rows[0]["报销说明"].ToString();//报销说明
                    BXJE = dt.Rows[0]["报销金额"].ToString();//报销金额
                    PZZS = dt.Rows[0]["凭证张数"].ToString();//凭证张数
                    BXKMDM = dt.Rows[0]["报销科目代码"].ToString();//报销科目代码
                    BXKMMC = dt.Rows[0]["报销科目名称"].ToString();//报销科目名称
                    WBXMBH = dt.Rows[0]["外包项目编号"].ToString();//外包项目编号
                    BZ = dt.Rows[0]["备注"].ToString();//备注
                    SFYHK = dt.Rows[0]["是否已汇款"].ToString();//是否已汇款
                    PZSFYDCW = dt.Rows[0]["凭证是否已到财务"].ToString();//凭证是否已到财务
                    SFYYQK = dt.Rows[0]["是否已有请款"].ToString();//是否已有请款
                    QKBH = dt.Rows[0]["请款编号"].ToString();//请款编号
                    QKJE = dt.Rows[0]["请款金额"].ToString();//请款金额
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}