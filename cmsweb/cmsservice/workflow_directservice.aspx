<%@ Import namespace="System.Data"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import namespace="NetReusables"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Platform.AspPage"%>

<script language="vb" runat="server">
'---------------------------------------------------------------
'���ݱ���¼��ID��ȡ��Ӧ����ʵ����ID
'CHENYU 2010-5-5 CREATE
'---------------------------------------------------------------
    Private Function GetWorkflowInstId(ByVal RecId As String) As Long
        Dim strSql As String = "select * from WF_INSTANCE where RecordID=" & RecId
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        If dt.Rows.Count = 1 Then
            Return DbField.GetLng(dt.Rows(0), "Id")
        Else
            Return 0
        End If
    End Function
</script>
<%

'---------------------------------------------------------------
'ʵ������ģ��ĵ�½ת��
'CHENYU 2010-4-1 CREATE
'---------------------------------------------------------------
If Not Session("CMS_PASSPORT") Is Nothing Then
	Dim WorkflowInstId As Long = GetWorkflowInstId(Request.QueryString("mnurecid"))
	If WorkflowInstId = 0 Then
		Response.Write("������������Ϣ!")
		Response.End
	End If
	Dim action As String = Request.QueryString("action")
	Dim url As String = Request.QueryString("url")
	Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"),CmsPassport)
	If action = "viewworkflow" Then
		url = "ViewFlowHistroy.aspx?WorkflowId=" & WorkflowInstId
		Response.Redirect("/webflow/UserValidate.aspx?user=" & pst.Employee.ID & "&ucode=" & pst.Employee.Password & "&url=" & Server.UrlEncode(url))
	Else
		Response.Redirect("/webflow/UserValidate.aspx?user=" & pst.Employee.ID & "&ucode=" & pst.Employee.Password & "&url=" & Server.UrlEncode(url))
	End If
Else
	Response.Write("��¼��ʱ,�����µ�¼.")
End If
%>