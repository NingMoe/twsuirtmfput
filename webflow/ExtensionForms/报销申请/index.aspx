<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="NetReusables" %>
<%@ Import Namespace="Unionsoft.Workflow.Items" %>
<%@ Import Namespace="Unionsoft.Workflow.Platform" %>
<%@ Import Namespace="Unionsoft.Workflow.Engine" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase"
    ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>报销申请</title>
    <!--验证页面文本框必填及类型 js文件----------->
    <script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <!--#include file="../includes/WorkflowUtilies.aspx"-->
      
    

    <script language="vb" runat="server">
        Private ActionKey As String
        Private WorkflowId As String
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim bxkmDt As DataTable = SDbStatement.Query("select KeyValue from DataDictionary WHERE ParentId='545845512082' ORDER BY KeySort ASC").Tables(0)
            BXKMRep.DataSource = bxkmDt
            BXKMRep.DataBind()
            
            ''获取剩余年假天数
            'Dim dt As DataTable = SDbStatement.Query("select 姓名,年假天数,isnull(已用年假天数,0) 已用年假天数,剩余年假=年假天数-isnull(已用年假天数,0) from(select EmpName as 姓名 ,isnull(Annual,0) 年假天数 from HR_Employee ) a left join (select EmpName,isnull(SUM(LeaveDays),0)已用年假天数 from HR_Leave WHERE Leavetype='年假' GROUP BY EmpName ) b on a.姓名=b.EmpName where 姓名='" + CurrentUser.Name + "'").Tables(0)
          
            
            'If dt.Rows.Count > 0 Then
            '    DayNum.Value = dt.Rows(0)("剩余年假").ToString
            'Else
            '    DayNum.Value = "0"
            'End If
            
            If ViewState.Item("_Id") Is Nothing Then ViewState("_Id") = TimeId.CurrentMilliseconds(30)
            If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
            If Request("WorkflowId") <> "" Then WorkflowId = Request("WorkflowId")
            If Not Me.IsPostBack Then Return
            If ActionKey = "" Then Return
         
            Dim FLOWCODE As String 
            Dim C3_284057533109 As String
            Dim MaxSql As String = "select Count(1) from CT184270067595 where C3_284057533109 like '" + System.DateTime.Now.ToString("yyMMdd") + "%'"
            Dim MaxDt As DataTable = SDbStatement.Query(MaxSql).Tables(0)
            Dim maxHXBH As Integer = Convert.ToInt32(MaxDt.Rows(0)(0))
            C3_284057533109 = System.DateTime.Now.ToString("yyMMdd") + maxHXBH.ToString("00")
            Dim MaxFLOWCODEDt As DataTable = SDbStatement.Query("select max(FLOWCODE)+1 from CT184270067595 ").Tables(0)
            FLOWCODE = Convert.ToInt32(MaxFLOWCODEDt.Rows(0)(0)).ToString()
            
            Dim hst As New Hashtable
            hst.Add("ID", ViewState("_Id"))
            hst.Add("RESID", 184270067595)
            hst.Add("RELID", 0)
            hst.Add("CRTID", CurrentUser.Code)
            hst.Add("CRTTIME", DateTime.Now)
            hst.Add("EDTID", CurrentUser.Code)
            hst.Add("EDTTIME", DateTime.Now)
            hst.Add("FLOWCODE",FLOWCODE)
            hst.Add("C3_284057533109", C3_284057533109)
            hst.Add("C1", Request.Form("C1")) '日期   
            hst.Add("C2", Request.Form("C2")) '报销人 
            hst.Add("C3", Request.Form("C3")) '报销内容 
            hst.Add("C6", Request.Form("C6")) '报销说明 
            hst.Add("C4", Request.Form("C4")) '报销金额 
            hst.Add("C5", Request.Form("C5")) '凭证张数 
            hst.Add("C3_381946879924", Request.Form("C3_381946879924")) '报销科目 
            hst.Add("C3_284723762234", Request.Form("C3_284723762234")) '是否已有请款   
            hst.Add("C7", Request.Form("C7")) '备注
            hst.Add("C3_386163718156", Request.Form("C3_386163718156")) '外包项目编号 
            '财务状态
            hst.Add("C3_284723586906", Request.Form("C3_284723586906")) '是否已汇款 
            hst.Add("C3_284723656796", Request.Form("C3_284723656796")) '凭证是否已到财务 
            SDbStatement.InsertRow(hst, "CT184270067595")
            Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId), Convert.ToInt64(ViewState("_Id")), ActionKey, hst, "")
            hst.Add("WorkflowInstId", oWorklistItem.ActivityInstance.WorkflowInstance.Key)
            StartWorkflowInstance(oWorklistItem)
		    
        End Sub
    </script>
    <script src="../OverTime/scripts/FormValidate.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"> 
   //验证
    function SaveBXSQ() {
        alert(!CheckValue(Form1));
        if (!CheckValue(Form1)) {
             alert("提交失败！")
            return false;
        } else {
            alert("提交成功！");
            return true;
        }
       
    }
    </script>
    
    <form id="Form1" method="post" runat="server">
        <%GenerateCommand(Convert.ToInt64(WorkflowId), "return SaveBXSQ();")%>
        <h1 align="center">
            报销申请</h1>
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center" style="height:30px;">
                    日期</td>
                <td width="250" align="left" valign="middle">
                    <input name="C1" type="text" class="noborder" id="C1" style="width: 70px"
                        value="<%=datetime.now.ToString("yyyy-MM-dd")%>" /></td>
                <td width="100" class="F_center">
                    报销人</td>
                <td width="250" align="left" valign="middle">
                    <input name="C2" type="text" class="noborder" id="C2" style="width: 100px"
                        value="<%=CurrentUser.Name%>"  /></td>
            </tr>
            <tr>
                <td class="F_center" >报销内容</td>
                <td align="left" colspan="3" ><input name="C3" type="text"   FieldTitle="报销内容" class="noborder" id="C3" style="width: 98%" /></td>
            </tr>
            <tr>
                <td class="F_center">
                    报销说明</td>
                <td align="left" colspan="3" align="left" valign="top">
                    <textarea name="C6"  type="text" class="noborder" id="C6" mInput="true" FieldTitle="报销说明" style="width: 98%;height:60px;" ></textarea></td>
            </tr>
            <tr>
                <td class="F_center" style="height:30px;">报销金额</td>  
                <td >
                    <input name="C4" class="noborder" id="C4" style="width: 98%;" mInput="true" FieldTitle="报销金额" />
                </td>   
                <td class="F_center">凭证张数</td>  
                <td >
                    <input name="C5" class="noborder" id="C5" style="width: 98%;" mInput="true" FieldTitle="凭证张数" />
                </td>
            </tr>
              <tr  style="height:40px;">
                <td class="F_center"  style="height:30px;">报销科目</td>  
                <td >
                    <select style="width: 98%;height:30px;" id="C3_381946879924" name="C3_381946879924" >
                        <asp:Repeater ID="BXKMRep" runat="server">
                        <ItemTemplate>
                            <option value="<%#Eval("KeyValue") %>"><%#Eval("KeyValue") %></option>
                        </ItemTemplate>
                        </asp:Repeater>
                    </select>
                </td>   
                <td class="F_center">是否已有请款</td>  
                <td >
                    <select style="width: 98%;height:30px;" id="C3_284723762234" name="C3_284723762234" ><option value=""></option>
                    <option value="是">是</option><option value="否">否</option></select>
                </td>
            </tr>
            <tr>
                <td class="F_center">
                    备注</td>
                <td align="left" colspan="3" align="left" valign="top">
                    <textarea name="C7"  type="text" class="noborder" id="C7" style="width: 98%;height:60px;" ></textarea></td>
            </tr>  
            <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/gray/easyui.css" />
            <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/icon.css"/> 
            <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.min.js"></script>
            <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
            <script type="text/javascript" src="../easyUI/easyui-lang-zh_CN.js" ></script>
            <script type="text/javascript">
                $(function () {
                    $('#C3_386163718156').combogrid({
                        panelWidth: 700,
                        height: 30,
                        width: 580,
                        checkbox: true,
                        rownumbers: true,
                        selectOnCheck: true,
                        checkOnSelect: true,
                        singleSelect: true,
                        multiple: true,
                        showFooter: true,
                        fitColumns: true,
                        pagination: true,
                        idField: '外包项目编号',
                        textField: '外包项目编号',
                        url: '../common/AjaxRequest.aspx?typeValue=GetWBXMBH',
                        columns: [[
                            { field: 'ck', checkbox: true },
					        { field: '项目编号', title: '项目编号', width: 80 ,align : 'center'},
					        { field: '外包项目编号', title: '外包项目编号', width: 80, align: 'center' },
					        { field: '代理名称', title: '代理名称', width: 60, align: 'center' },
                            { field: '填表日期', title: '填表日期', width: 60, align: 'center' },
                            { field: '样本单价', title: '样本单价', width: 30, align: 'center' },
                            { field: '总费用', title: '总费用', width: 30, align: 'center' },
                            { field: '督导', title: '督导', width: 30, align: 'center' }
				        ]]
                    });
                });
            </script>   
            <tr style="height: 40px;">
                <td class="F_center" style="height: 30px;">外包项目编号</td>
                <td align="left" colspan="3" valign="center" >
                     <input name="C3_386163718156" id="C3_386163718156" class="easyui-combogrid" />
                </td>
            </tr> 
             <tr style="height: 40px;">
                <td class="F_center" colspan="4"  style="height: 30px;">财务状态</td>
            </tr> 
            <tr style="height: 40px;">
                <td class="F_center"  style="height: 30px;">是否已汇款</td>
                <td ><select id="C3_284723586906" name="C3_284723586906" style="width:95%;height: 30px;"><option></option><option value="是">是</option><option value="否">否</option></select></td>
                <td class="F_center"  style="height: 30px;">凭证是否已到财务</td>
                <td><select id="C3_284723656796" name="C3_284723656796" style="width:95%;height: 30px;"><option></option><option value="是">是</option><option value="否">否</option></select></td>
            </tr> 
        </table>
    </form>
</body>
</html>

