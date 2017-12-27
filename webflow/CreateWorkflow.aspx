<%@ Reference Page="~/admin/redirectemployeeselect.aspx" %>
<%@ Import NameSpace="Unionsoft.Workflow.Platform"%>
<%@ Import NameSpace="Unionsoft.Workflow.Engine"%>
<%@ Register TagPrefix="ActionBar" NameSpace="Unionsoft.Workflow.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" validateRequest="false" Inherits="Unionsoft.Workflow.Web.CreateWorkflow" CodeFile="CreateWorkFlow.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.RecordEditBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>新建流程</title>
		<LINK href="css/flowstyle.css" type="text/css" rel="stylesheet">
		<script src="script/Valid.js" type="text/javascript"></script>
		<script src="script/jscommon.js" type="text/javascript"></script>
		<script src="script/base.js" type="text/javascript"></script>
		<script src="script/CmsScript.js" type="text/javascript"></script>
		<script src="script/FlowCommonScript.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server"> <!-------- enctype="multipart/form-data"------------->
		
			<ACTIONBAR:NODEACTIONBAR id="NodeActionBar1" runat="server" SetIsExigence="True" CssName="ToolBar"></ACTIONBAR:NODEACTIONBAR>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td height="3"></td>
				</tr>
			</table>
			<asp:Panel id="PanelMemo" runat="server">
				<TABLE class=MemoTable cellSpacing=1 cellPadding=0 width="<%=FormWidth%>" border=0>
					<THEAD>
						<TR>
							<TD>处理意见</TD>
						</TR>
					</THEAD>
					<TR>
						<TD><asp:textbox id="txtMemo" runat="server" TextMode="MultiLine"></asp:textbox></TD>
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
						<asp:panel id="Panel1" BorderWidth="0" runat="server" Height="400" style="LEFT: 12px; POSITION: absolute; TOP: 140px"></asp:panel> <!----- -->
					</td>
				</tr>
			</table>
			<asp:Panel id="PanelAttachment" runat="server">
				<TABLE cellSpacing=0 cellPadding=0 width="<%=FormWidth%>" border=0>
					<TR>
						<TD height="3"></TD>
					</TR>
				</TABLE>
				<TABLE class=AttachmentTable cellSpacing=1 cellPadding=0 width="<%=FormWidth%>" border=0>
					<THEAD>
						<TR>
							<TD>文件名称</TD>
							<TD>文件大小</TD>
							<TD>文件类型</TD>
							<TD>上传日期</TD>
							<TD>&nbsp;</TD>
						</TR>
					</THEAD>
					<asp:Panel id="PanelAttachmentAdd" runat="server">
						<TBODY>
							<TR>
								<TD colSpan="5"><B>附件上传:</B> 
								<INPUT class="ButtonCommon" id="UploadFile" onkeydown="return false;" style="WIDTH: 600px" type="file" name="UploadFile" runat="server">
									<asp:Button id="btnUpload" runat="server" CssClass="ButtonCommon" Text="上传"></asp:Button>
								</TD>
							</TR>
					</asp:Panel>
					</TBODY>
					</TABLE>
			</asp:Panel> 
		</form>
	</body>
</HTML>
 