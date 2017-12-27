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
    <title>��������</title>
    <!--��֤ҳ���ı��������� js�ļ�----------->
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
            
            ''��ȡʣ���������
            'Dim dt As DataTable = SDbStatement.Query("select ����,�������,isnull(�����������,0) �����������,ʣ�����=�������-isnull(�����������,0) from(select EmpName as ���� ,isnull(Annual,0) ������� from HR_Employee ) a left join (select EmpName,isnull(SUM(LeaveDays),0)����������� from HR_Leave WHERE Leavetype='���' GROUP BY EmpName ) b on a.����=b.EmpName where ����='" + CurrentUser.Name + "'").Tables(0)
          
            
            'If dt.Rows.Count > 0 Then
            '    DayNum.Value = dt.Rows(0)("ʣ�����").ToString
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
            hst.Add("C1", Request.Form("C1")) '����   
            hst.Add("C2", Request.Form("C2")) '������ 
            hst.Add("C3", Request.Form("C3")) '�������� 
            hst.Add("C6", Request.Form("C6")) '����˵�� 
            hst.Add("C4", Request.Form("C4")) '������� 
            hst.Add("C5", Request.Form("C5")) 'ƾ֤���� 
            hst.Add("C3_381946879924", Request.Form("C3_381946879924")) '������Ŀ 
            hst.Add("C3_284723762234", Request.Form("C3_284723762234")) '�Ƿ��������   
            hst.Add("C7", Request.Form("C7")) '��ע
            hst.Add("C3_386163718156", Request.Form("C3_386163718156")) '�����Ŀ��� 
            '����״̬
            hst.Add("C3_284723586906", Request.Form("C3_284723586906")) '�Ƿ��ѻ�� 
            hst.Add("C3_284723656796", Request.Form("C3_284723656796")) 'ƾ֤�Ƿ��ѵ����� 
            SDbStatement.InsertRow(hst, "CT184270067595")
            Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId), Convert.ToInt64(ViewState("_Id")), ActionKey, hst, "")
            hst.Add("WorkflowInstId", oWorklistItem.ActivityInstance.WorkflowInstance.Key)
            StartWorkflowInstance(oWorklistItem)
		    
        End Sub
    </script>
    <script src="../OverTime/scripts/FormValidate.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"> 
   //��֤
    function SaveBXSQ() {
        alert(!CheckValue(Form1));
        if (!CheckValue(Form1)) {
             alert("�ύʧ�ܣ�")
            return false;
        } else {
            alert("�ύ�ɹ���");
            return true;
        }
       
    }
    </script>
    
    <form id="Form1" method="post" runat="server">
        <%GenerateCommand(Convert.ToInt64(WorkflowId), "return SaveBXSQ();")%>
        <h1 align="center">
            ��������</h1>
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center" style="height:30px;">
                    ����</td>
                <td width="250" align="left" valign="middle">
                    <input name="C1" type="text" class="noborder" id="C1" style="width: 70px"
                        value="<%=datetime.now.ToString("yyyy-MM-dd")%>" /></td>
                <td width="100" class="F_center">
                    ������</td>
                <td width="250" align="left" valign="middle">
                    <input name="C2" type="text" class="noborder" id="C2" style="width: 100px"
                        value="<%=CurrentUser.Name%>"  /></td>
            </tr>
            <tr>
                <td class="F_center" >��������</td>
                <td align="left" colspan="3" ><input name="C3" type="text"   FieldTitle="��������" class="noborder" id="C3" style="width: 98%" /></td>
            </tr>
            <tr>
                <td class="F_center">
                    ����˵��</td>
                <td align="left" colspan="3" align="left" valign="top">
                    <textarea name="C6"  type="text" class="noborder" id="C6" mInput="true" FieldTitle="����˵��" style="width: 98%;height:60px;" ></textarea></td>
            </tr>
            <tr>
                <td class="F_center" style="height:30px;">�������</td>  
                <td >
                    <input name="C4" class="noborder" id="C4" style="width: 98%;" mInput="true" FieldTitle="�������" />
                </td>   
                <td class="F_center">ƾ֤����</td>  
                <td >
                    <input name="C5" class="noborder" id="C5" style="width: 98%;" mInput="true" FieldTitle="ƾ֤����" />
                </td>
            </tr>
              <tr  style="height:40px;">
                <td class="F_center"  style="height:30px;">������Ŀ</td>  
                <td >
                    <select style="width: 98%;height:30px;" id="C3_381946879924" name="C3_381946879924" >
                        <asp:Repeater ID="BXKMRep" runat="server">
                        <ItemTemplate>
                            <option value="<%#Eval("KeyValue") %>"><%#Eval("KeyValue") %></option>
                        </ItemTemplate>
                        </asp:Repeater>
                    </select>
                </td>   
                <td class="F_center">�Ƿ��������</td>  
                <td >
                    <select style="width: 98%;height:30px;" id="C3_284723762234" name="C3_284723762234" ><option value=""></option>
                    <option value="��">��</option><option value="��">��</option></select>
                </td>
            </tr>
            <tr>
                <td class="F_center">
                    ��ע</td>
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
                        idField: '�����Ŀ���',
                        textField: '�����Ŀ���',
                        url: '../common/AjaxRequest.aspx?typeValue=GetWBXMBH',
                        columns: [[
                            { field: 'ck', checkbox: true },
					        { field: '��Ŀ���', title: '��Ŀ���', width: 80 ,align : 'center'},
					        { field: '�����Ŀ���', title: '�����Ŀ���', width: 80, align: 'center' },
					        { field: '��������', title: '��������', width: 60, align: 'center' },
                            { field: '�������', title: '�������', width: 60, align: 'center' },
                            { field: '��������', title: '��������', width: 30, align: 'center' },
                            { field: '�ܷ���', title: '�ܷ���', width: 30, align: 'center' },
                            { field: '����', title: '����', width: 30, align: 'center' }
				        ]]
                    });
                });
            </script>   
            <tr style="height: 40px;">
                <td class="F_center" style="height: 30px;">�����Ŀ���</td>
                <td align="left" colspan="3" valign="center" >
                     <input name="C3_386163718156" id="C3_386163718156" class="easyui-combogrid" />
                </td>
            </tr> 
             <tr style="height: 40px;">
                <td class="F_center" colspan="4"  style="height: 30px;">����״̬</td>
            </tr> 
            <tr style="height: 40px;">
                <td class="F_center"  style="height: 30px;">�Ƿ��ѻ��</td>
                <td ><select id="C3_284723586906" name="C3_284723586906" style="width:95%;height: 30px;"><option></option><option value="��">��</option><option value="��">��</option></select></td>
                <td class="F_center"  style="height: 30px;">ƾ֤�Ƿ��ѵ�����</td>
                <td><select id="C3_284723656796" name="C3_284723656796" style="width:95%;height: 30px;"><option></option><option value="��">��</option><option value="��">��</option></select></td>
            </tr> 
        </table>
    </form>
</body>
</html>

