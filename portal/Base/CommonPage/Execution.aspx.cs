using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Common_CommonPage_Execution : UserPagebase
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        RedirectVerify();
        WebServices.Services Resource = new WebServices.Services();
        string UserID = CurrentUser.ID;
        string ucode = CurrentUser.Password;
        //读取web.config文件，得到Post提交地址
        //string BPMPostUrl = System.Configuration.ConfigurationManager.AppSettings["BPMPostUrl"].ToString();

        ////读取web.config文件，得到Process地址
        //string BPMProcessUrl = System.Configuration.ConfigurationManager.AppSettings["BPMProcessUrl"].ToString();
        ////读取web.config文件，得到Read地址
        //string BPMReadUrl = System.Configuration.ConfigurationManager.AppSettings["BPMReadUrl"].ToString();
        ////读取web.config文件，得到易正流程中心地址
        //string BPMCenter = System.Configuration.ConfigurationManager.AppSettings["BPMCenter"].ToString();
        //读取web.config文件，得到CMS中心地址
        //string cmsweb = System.Configuration.ConfigurationManager.AppSettings["cmsweb"].ToString();
        string type = "post";
        string WorkFlowWebPath = "";
        string ParentFlowRowID = "";
        string RowID = "";
        string WorkflowInstId = "";
        string WorklistItemId = "";
        string _ParentValue = "";
        string ParentRowId="";
        string Para = "";

       if (Request["cookiedate"] != null)
       {
           if (!string.IsNullOrEmpty(Request["cookiedate"].ToString()))
           {
               _ParentValue = "&ParentValue=" + Request["cookiedate"].ToString();
           }
       }

        if (Request["Type"] != null)
        {
            type = Request["Type"].ToString().ToLower();
        }

        if (Request["Para"] != null) Para = Request.QueryString["Para"];

        if (Request.QueryString["ParentFlowRowID"] != null)
        {
            ParentFlowRowID = Request.QueryString["ParentFlowRowID"].ToString().ToLower();
        }
         if (Request.QueryString["RowID"] != null)
        {
            RowID = Request.QueryString["RowID"].ToString().ToLower();
        }
         if (Request.QueryString["WorklistItemId"] != null)
        {
            WorklistItemId = Request.QueryString["WorklistItemId"].ToString().ToLower();//483624242700
        }
        if (Request.QueryString["WorkflowInstId"] != null)
        {
            WorkflowInstId = Request.QueryString["WorkflowInstId"].ToString().ToLower();//483624240010
        }
        if (Request["RecID"]!=null)
        {
            ParentRowId = Request["RecID"].ToString().Trim();
        }
        if (type == "create")
        {
            WorkFlowWebPath = "/webflow/process/director.aspx?action=" + type + "&WorkflowId=" + ParentFlowRowID + _ParentValue + "&ParentRowId=" + RowID + "&Para=" + Para;
        }
        else if (type == "transtract")
        {
            WorkFlowWebPath = "/webflow/process/director.aspx?action=" + type + "&WorkflowInstId=" + WorkflowInstId + "&WorklistItemId=" + WorklistItemId;
        }
        else if (type == "xmxxck")
        {
            if (ParentRowId != null || ParentRowId !="")
            {
                string sql = "select top 1 wf.id WorkflowInstId from WF_INSTANCE WF left join ct482433275742 ct ON WF.RecordID=ct.id WHERE ParentRowId='" + ParentRowId + "'";
                string[] changePassWord = Common.getChangePassWord();
                DataTable dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    WorkflowInstId = dt.Rows[0]["WorkflowInstId"].ToString();
                }
            }
            if (WorkflowInstId != null)
            {
                string SQLS = " select top 1 b.id WorklistItemId  from WF_TASK a  left join WF_USERTASK b on a.nodeid=b.nodeid and a.id=b.taskid  where a.WF_INSTANCE_ID='" + WorkflowInstId + "' and a.TASKSTATUS='0' order by b.createtime desc";
                string[] changePassWord = Common.getChangePassWord();
                DataTable TABLES = Resource.SelectData(SQLS, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                if (TABLES.Rows.Count > 0)
                {
                    WorklistItemId = TABLES.Rows[0]["WorklistItemId"].ToString();
                }
            }
            WorkFlowWebPath = "/webflow/process/director.aspx?action=transtract&WorkflowInstId=" + WorkflowInstId + "&WorklistItemId=" + WorklistItemId;
        }
        else if (type.ToLower() == "form")
        {
            string[] changePassWord = Common.getChangePassWord();
            DataTable dt = Resource.SelectData(" select top 1 b.id WorklistItemId  from WF_TASK a  left join WF_USERTASK b on a.nodeid=b.nodeid and a.id=b.taskid  where  wf_instance_id='" + WorkflowInstId + "' order by b.createtime desc", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            if (dt.Rows.Count > 0)
            {
                WorklistItemId = dt.Rows[0]["WorklistItemId"].ToString();
                WorkFlowWebPath = "/webflow/process/director.aspx?action=transtract&WorkflowInstId=" + WorkflowInstId + "&WorklistItemId=" + WorklistItemId + "&ParentRowId=" + RowID;
            }
        }
        else if (type.ToLower() == "wfqdrw")
        {
            WorkFlowWebPath = "/webflow/process/director.aspx?action=view&WorklistItemId=" + WorklistItemId;
        }
        else if (type.ToLower() == "bxsq")
        {
            WorkFlowWebPath = "/webflow/ExtensionForms/报销申请/index.aspx?action=create&WorkflowId=193841883015";

        }
        else if (type.ToLower() == "qksq")
        {
            WorkFlowWebPath = "/webflow/ExtensionForms/请款申请/index.aspx?action=create&WorkflowId=194376055201";
        }
        else
        {
            WorkFlowWebPath = "/webflow/process/director.aspx?action=" + type + "&WorkflowInstId=" + WorkflowInstId + "&WorklistItemId=" + WorklistItemId + "&ParentRowId=" + RowID;
        }

        Response.Redirect("workflow_directservice.aspx?strURL=" + Server.UrlEncode(WorkFlowWebPath));
    }
}
