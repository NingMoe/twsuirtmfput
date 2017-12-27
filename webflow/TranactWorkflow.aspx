<%@ Reference Page="~/admin/redirectemployeeselect.aspx" %>
<%@ Reference Control="~/controls/ctlflowhistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CtlFlowHistory" Src="controls/CtlFlowHistory.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" validateRequest="false" Inherits="Unionsoft.Workflow.Web.TranactWorkFlow" CodeFile="TranactWorkflow.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.RecordEditBase" %>
<%@ Register TagPrefix="ActionBar" NameSpace="Unionsoft.Workflow.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>TranactWorkFlow</title>
		<LINK href="css/flowstyle.css" type="text/css" rel="stylesheet">
		<script src="script/Valid.js" type="text/javascript"></script>
		<script src="script/jscommon.js" type="text/javascript"></script>
		<script src="script/base.js" type="text/javascript"></script>
		<script src="script/CmsScript.js" type="text/javascript"></script>
		<script language="javascript" src="script/FlowCommonScript.js"></script>
		<script language="javascript" src="script/loading.js"></script>
		<script language="javascript">
		function openfile(resourceId,documentId,filename,action)
		{
			try{
				<%If Not WebOfficeExceptant Then%>
				if (action=='view')			 
					window.open("Document/FileWebAdapter.aspx?ResourceID=" + resourceId + "&DocumentID=" + documentId + "&IsCheckOut=3",'','height=700,width=1000,resizable=yes');
				else
				{
				    //alert("FileWebAdapter.aspx?ResourceID=" + resourceId + "&DocumentID=" + documentId);
					window.open("Document/FileWebAdapter.aspx?ResourceID=" + resourceId + "&DocumentID=" + documentId,'','height=700,width=1000,resizable=yes');
				}
				return;
				<%End If%>
				
				if (action=='view'){window.open("Document/FileWebAdapter.aspx?ResourceID=" + resourceId + "&DocumentID=" + documentId + "&IsCheckOut=3",'','height=700,width=1000,resizable=yes');return;}
				
				var host = location.href.substring(0,location.href.lastIndexOf("/")); 
				var url = host + "/AttachmentService/down.aspx?ResourceID=" + resourceId + "&DocumentID=" + documentId;
				if (WebAttachment.FileExists(documentId,filename)==false) WebAttachment.OpenFile(url,documentId,filename);
				else WebAttachment.ShellFile(documentId,filename);
			}
			catch(ex)
			{
				alert(ex.toString());
			}
		}

		function updatefile(resourceId,documentId,filename)
		{
			<%If Not WebOfficeExceptant Then%>
			return;
			<%End If%>
			try{
				var host = location.href.substring(0,location.href.lastIndexOf("/")); 
				var url = host + "/AttachmentService/FileWebSave.aspx?ResourceID=" + resourceId + "&DocumentID=" + documentId;
				if (WebAttachment.FileExists(documentId,filename)) WebAttachment.UpdateFile(url,documentId,filename);
			}
			catch(ex)
			{
				alert(ex.toString());
			}
		}
		
		function deletefile(resourceId,documentId)
		{
			if (window.confirm('确实要删除吗?'))
			{
				var value = Unionsoft.Workflow.Web.TranactWorkFlow.DeleteAttachment(resourceId,documentId).value;
				if (value.toString()=="1")
				{
					for (var i=0;i<tblAttachments.rows.length;i++)
					{
						if (tblAttachments.rows[i].documentId==documentId) tblAttachments.deleteRow(i);
					}
					
				}
				else
				{
					alert("删除附件失败!");
				}
				
			}
		}
		
		function updatefileprocess()
		{
			<%If Not WebOfficeExceptant=False Then%>
			for (var i=0;i<tblAttachments.rows.length;i++)
			{
				updatefile(tblAttachments.rows[i].resourceId,tblAttachments.rows[i].documentId,tblAttachments.rows[i].filename);
			}
			<%End If%>
		}
		</script>
</HEAD>
	<body>
	<%If Not WebOfficeExceptant=False Then%>
	<OBJECT id=WebAttachment height=20 width=176 classid=CLSID:7649C363-E9F5-4D35-AFE1-363762170CE6 name=WebAttachment  style="display:none" CODEBASE="UnionsoftControls.CAB#version=1,0,0,0" VIEWASTEXT>
	<PARAM NAME="_ExtentX" VALUE="4657">
	<PARAM NAME="_ExtentY" VALUE="529">
	</OBJECT>
	<%End If%>
	
		<form id="Form1" method="post" runat="server"> 
<ACTIONBAR:NODEACTIONBAR id="NodeActionBar1" runat="server" CssName="ToolBar" SubmitScript="updatefileprocess();"></ACTIONBAR:NODEACTIONBAR>
<table cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td height="3"></td>
	</tr>
</table>
<asp:Panel id="PanelMemo" runat="server">
<TABLE class=MemoTable cellSpacing=1 cellPadding=0 width="<%=FormWidth%>" border=0>
  <TR>
    <TD>处理意见</TD></TR>
  <TR>
    <TD><asp:textbox id=txtMemo runat="server" TextMode="MultiLine" CssClass="MemoInput"></asp:textbox></TD>
  </TR>
</TABLE>
</asp:Panel>
<TABLE class="MemoTable" cellSpacing="0" cellPadding="0" border="0" width="790">
	<THEAD>
		<TR>
			<TD>表单信息</TD>
		</TR>
	</THEAD>
</TABLE>
<table class="UserForm1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td vAlign="top" height="400">
			<asp:panel id="Panel1" BorderWidth="0" runat="server" style="LEFT: 12px; POSITION: absolute; TOP: 140px"></asp:panel>
		</td>
	</tr>
</table>

<!--
附件处理部分
-->
<asp:Panel id="PanelAttachment" runat="server">
	<TABLE cellSpacing=0 cellPadding=0 width="<%=FormWidth%>" border=0>
	<TR>
		<TD height=3></TD>
	</TR>
	</TABLE>
	
	<TABLE cellSpacing=0 cellPadding=0 width="<%=FormWidth%>" border=0 id="tblAttachments" class="AttachmentTable">
	<thead>
	<tr height="23" class="ToolBar">
		<td>文件名</td>
		<td width="80"></td>
		<td width="80"></td>
		<td width="80"></td>
	</tr>
	</thead>
	<asp:Repeater ID="rptAttachmentList" Runat="server">
	<ItemTemplate>
	<TR height="23" resourceId="<%=ResourceId%>" documentId="<%#DataBinder.Eval(Container.DataItem,"Id")%>" filename="<%#DataBinder.Eval(Container.DataItem,"name")%>.<%#DataBinder.Eval(Container.DataItem,"ext")%>">
		<td align="left">
		<a href="javascript:openfile('<%=ResourceId%>','<%#DataBinder.Eval(Container.DataItem,"Id")%>','<%#DataBinder.Eval(Container.DataItem,"name")%>.<%#DataBinder.Eval(Container.DataItem,"ext")%>','view')"><%#DataBinder.Eval(Container.DataItem,"name")%>.<%#DataBinder.Eval(Container.DataItem,"ext")%></a>&nbsp;&nbsp;&nbsp;&nbsp;
		&nbsp;&nbsp;(创建人:<%#DataBinder.Eval(Container.DataItem,"EDTID")%>)
		</td>
		<td align="center"><%If _AttachmentEdit=True Then%><a href="javascript:openfile('<%=ResourceId%>','<%#DataBinder.Eval(Container.DataItem,"Id")%>','<%#DataBinder.Eval(Container.DataItem,"name")%>.<%#DataBinder.Eval(Container.DataItem,"ext")%>','edit')"">编辑</a>&nbsp;<%End If%></td>
		<td align="center"><a href="#" onclick="window.open('DownloadFile.aspx?ResourceID=<%=ResourceId%>&DocumentID=<%#DataBinder.Eval(Container.DataItem,"Id")%>','newwindow','height=700,width=1000,resizable=yes');">下载</a>&nbsp;</td>
		<td align="center"><%If _AttachmentDelete=True Then%><a href="javascript:deletefile('<%=ResourceId%>','<%#DataBinder.Eval(Container.DataItem,"Id")%>')">删除</a><%End If%></td>
	</TR>
	</ItemTemplate>
	</asp:Repeater>
	</TABLE>
	
	<asp:Panel id=PanelAttachmentAdd runat="server">
	<TABLE cellSpacing=0 cellPadding=0 width="<%=FormWidth%>" border=0>
	<TR>
		<TD colSpan=5>
			<INPUT class=ButtonCommon id=UploadFile style="WIDTH: 430px" type=file name=UploadFile runat="server"> 
			<asp:Button id=btnUpload runat="server" CssClass="ButtonCommon" Text="上传"></asp:Button>
		</TD>
	</TR>
	</TABLE>
	</asp:Panel>
</asp:Panel>

<!--
流程审批信息显示
-->
<TABLE cellSpacing="0" cellPadding="0" width="<%=FormWidth%>" border="0" class="MemoTable">
	<TR>
		<TD>流程历史信息</TD>
	</TR>
	<TR>
		<TD>
			<uc1:CtlFlowHistory id="CtlFlowHistory1" runat="server"></uc1:CtlFlowHistory>
		</TD>
	</TR>
</TABLE> 
		</form>
	</body>
</HTML>
 