using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebServices;

public partial class Base_SystemRights_RowRights :UserPagebase
{
    Services oServices = new Services();
    Int64  MTS_ID = 0;
    string ObjectID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string MenuResID = Request["MenuResID"];
        ObjectID = Request["ObjectID"];
        if (CurrentUser.DepartmentName != "系统管理员")
        {
            Response.Redirect("../../login.aspx", true);
            return;
        }
        if (IsPostBack) return;
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = oServices.SelectData("select * from cms_resource where id=" + MenuResID, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        if (dt.Rows.Count > 0)
        {
            txtResourceName.Text = dt.Rows[0]["Name"].ToString();
            
            if (dt.Rows[0]["Res_Comments"].ToString().Trim() != "")
            {
                SysSettings sys = Common.GetSysSettingsByENKey(dt.Rows[0]["Res_Comments"].ToString().Trim());
                string ResourceID=sys.ResID ;
                LoadColName(ResourceID);
                LoadColValue();
                BindData(ResourceID,ObjectID);
                txtResourceID.Text = ResourceID;
            }
        }
    }

    protected void LoadColName(string ResourceID)
    { 
        Field[] f = oServices.GetFieldListAll(ResourceID);
        drpColName.DataValueField = "Name";
        drpColName.DataTextField = "Description";
        drpColName.DataSource = f;
        drpColName.DataBind();
    }

    protected void BindData(string ResID,string ObjectID)
    { 
        DataSet ds = oServices.GetMTSearch(ResID, ObjectID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            MTS_ID = Convert.ToInt64(ds.Tables[0].Rows[0]["MTS_ID"]);
        }
        txtMTS_ID.Text = MTS_ID.ToString();
       repList.DataSource = ds.Tables[1];
       repList.DataBind();
    }

    protected void LoadColValue()
    {
        FieldInfo[] fi = oServices.GetMTSCol_ColCond();
        drpColValue.DataTextField = "FieldDescription";
        drpColValue.DataValueField = "FieldName";
        drpColValue.DataSource = fi;
        drpColValue.DataBind();

        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string ColName = drpColName.Items[drpColName.SelectedIndex].Value;
        string ColDispName = drpColName.Items[drpColName.SelectedIndex].Text;
        string ColCond = drpOperator.Items[drpOperator.SelectedIndex].Text;
        string ColCond_EN = drpOperator.Items[drpOperator.SelectedIndex].Value;
        string ColValue = (txtSearch.Text.Trim() == "" ? this.drpColValue.Items[drpColValue.SelectedIndex].Text : txtSearch.Text.Trim());
        string ColValue_EN = (txtSearch.Text.Trim() == "" ? this.drpColValue.Items[drpColValue.SelectedIndex].Value : txtSearch.Text.Trim());
        string Loglc = drpLogic.Items[drpLogic.SelectedIndex].Value;
       // string str = MTS_ID + ",'" + txtResourceID.Text.Trim() + "','" + ObjectID + "','" + ColName + "','" + ColDispName + "','" + ColCond + "','" + ColCond_EN + "','" + ColValue + "','" + ColValue_EN + "','" + Loglc + "','" + CurrentUser.ID + "','";
        if (oServices.UpdateMTSearch(Convert.ToInt64( txtMTS_ID.Text.Trim()), txtResourceID.Text.Trim(), ObjectID, ColName, ColDispName, ColCond, ColCond_EN, ColValue, ColValue_EN, Loglc))
        {
            BindData(txtResourceID.Text.Trim(), ObjectID);
        }
        else
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('操作失败！');</script>");
    } 
    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        Int32 index = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        HtmlGenericControl htmC = (HtmlGenericControl)repList.Items[index].FindControl("lblMts_ID");
        string MTSCOL_ID = htmC.InnerText; //((Label)repList.Items[index].FindControl("lblID")).Text.Trim();
        if (oServices.DeleteByTableName("CMS_MTSEARCH_COLDEF", " and MTSCOL_ID=" + MTSCOL_ID, CurrentUser.ID))
            BindData(txtResourceID.Text.Trim(), ObjectID);
        else
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('操作失败！');</script>");
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (txtMTS_ID.Text.Trim() != "0")
        {
            if (oServices.DeleteMTSearch(txtMTS_ID.Text))
                BindData(txtResourceID.Text.Trim(), ObjectID);
            else
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('操作失败！');</script>");
        }
    }
}