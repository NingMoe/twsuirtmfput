using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetReusables;
using BPM;
using BPM.Client;
using System.Web.Security;
using System.Data;
public partial class UpdatePWD : PageBase 
{
    protected string pwd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        pwd = NetReusables.Encrypt.Decrypt("44CAFFC085295F38C0598F5ECCAE7715");
        //MessageBox(Request.ApplicationPath); 
        btnSubmit_Click(null, null);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //DataTable dt =
        WebServices.Services services = new WebServices.Services();
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = services.SelectData("select * from CMS_EMPLOYEE order by Emp_Name", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string pass = "";
            try
            {
                pass = NetReusables.CmsEncrypt.DecryptPassword(dt.Rows[i]["EMP_PASS"].ToString());
            }
            catch (Exception)
            {
                pass = NetReusables.Encrypt.Decrypt(dt.Rows[i]["EMP_PASS"].ToString()) + "(已更新)";
            }
            dt.Rows[i]["EMP_PASS"] = pass;
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
   
    }
}