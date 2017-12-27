<%@ Reference Control="~/controls/ctlflowhistory.ascx" %>
<%@ Register TagPrefix="ActionBar" NameSpace="Unionsoft.Workflow.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.ViewWorkFlow" CodeFile="ViewWorkFlow.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.RecordEditBase" %>
<%@ Register TagPrefix="uc1" TagName="CtlFlowHistory" Src="controls/CtlFlowHistory.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ViewWorkFlow</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="css/flowstyle.css" type="text/css" rel="stylesheet">
		<script src="script/Valid.js" type="text/javascript"></script>
		<script src="script/jscommon.js" type="text/javascript"></script>
		<script src="script/base.js" type="text/javascript"></script>
		<script src="script/CmsScript.js" type="text/javascript"></script>
		<script language="javascript" src="script/FlowCommonScript.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<ACTIONBAR:NODEACTIONBAR id="NodeActionBar1" runat="server" CssName="ToolBar"></ACTIONBAR:NODEACTIONBAR>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td height="3"></td>
				</tr>
			</table>
			<TABLE class="MemoTable" cellSpacing="0" cellPadding="0" border="0">
				<THEAD>
					<TR>
						<TD>表单信息</TD>
					</TR>
				</THEAD>
			</table>
			<table class="UserForm1" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td vAlign="top" height="400">
						<asp:panel id="Panel1" style="LEFT: 12px; POSITION: absolute; TOP: 43px" runat="server" Height="400" BorderWidth="0"></asp:panel>
					</td>
				</tr>
			</table>
			<asp:Panel id="PanelAttachment" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="<%=FormWidth%>" border="0">
					<tr>
						<td height="3"></td>
					</tr>
				</table>
				<TABLE cellSpacing="1" cellPadding="0" width="<%=FormWidth%>" border="0" class="AttachmentTable">
					<thead>
					<TR height="23" class="ToolBar">
						<TD>文件名称</TD>
						<TD>文件大小</TD>
						<TD>文件类型</TD>
						<TD>上传时间</TD>
						<TD width="40"></TD>
						<TD width="40"></TD>
					</TR>
					</thead>
					<asp:Repeater id="AttachmentList" Runat="server">
						<ItemTemplate>
							<tr height="23">
								<td>&nbsp;<%#DataBinder.Eval(Container.DataItem,"Name")%></td>
								<td>&nbsp;<%#DataBinder.Eval(Container.DataItem,"Size")%></td>
								<td>&nbsp;<%#DataBinder.Eval(Container.DataItem,"Ext")%></td>
								<td>&nbsp;<%#DataBinder.Eval(Container.DataItem,"EditTime")%></td>
								<td align="center"><asp:HyperLink ID="lnkViewDocument" NavigateUrl="#" Runat="server">查看</asp:HyperLink></td>
								<td align="center"><asp:HyperLink ID="lnkDownloadFile" NavigateUrl="#" Runat="server">下载</asp:HyperLink></td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</TABLE>
			</asp:Panel>
			<TABLE cellSpacing="0" cellPadding="0" width="<%=FormWidth%>" border="0" class="MemoTable">
				<THEAD>
					<TR>
						<TD>流程历史信息</TD>
					</TR>
				</THEAD>
				<TR>
					<TD>
						<uc1:CtlFlowHistory id="CtlFlowHistory1" runat="server"></uc1:CtlFlowHistory>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

