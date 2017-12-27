<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocShare" CodeFile="DocShare.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="NetReusables" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>文档共享</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
		<style type="text/css">.table { BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid }
	.table TR TD { BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-SIZE: 12px; LINE-HEIGHT: 25px; FONT-FAMILY: 宋体 }
		</style>
		<script language="javascript">
		function CancelShare(DOCID,ColumnName,ShareRecordID)
		{
			if(window.confirm('您确定要取消共享？'))
			{
				document.getElementById("<%=txtDocID.ClientID%>").value=DOCID;
				document.getElementById("<%=txtColumnName.ClientID%>").value=ColumnName;
				document.getElementById("<%=txtShareRecordID.ClientID%>").value=ShareRecordID;
				//alert(document.getElementById("<%=txtColumnName.ClientID%>").value);
				document.getElementById("<%=btnCancelShare.ClientID%>").click();
			}
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td vAlign="top">
						<TABLE class="table_level2" style="WIDTH: 604px" cellSpacing="0" cellPadding="0" width="604"
							border="0">
							<TR>
								<TD class="header_level2" style="WIDTH: 457px" height="22"><b>文档共享</b></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 4px" vAlign="bottom" align="left"></TD>
							</TR>
							<TR height="23">
								<td style="WIDTH: 457px">
									<TABLE style="WIDTH: 592px" cellSpacing="0" cellPadding="0" width="592" border="0">
										<TR>
											<TD style="WIDTH: 295px" vAlign="top"></TD>
											<TD vAlign="top"><FONT face="宋体"><asp:button id="btnChooseRes" runat="server" Width="140px" Text="选取共享目标资源"></asp:button><asp:button id="btnCancel" runat="server" Width="80px" Text="取消"></asp:button></FONT></TD>
										</TR>
										<tr>
											<td style="WIDTH: 295px" vAlign="top"><asp:panel id="panelDepTree" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; OVERFLOW: auto; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
													runat="server" Width="284" Height="420"><%WebTreeDepartment.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/cmsdocument/DocShare.aspx", "_self")%></asp:panel></td>
											<td vAlign="top"><asp:panel id="panelResTree" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; OVERFLOW: auto; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
													runat="server" Width="284" Height="420"><%WebTreeResource.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/cmsdocument/DocShare.aspx", "_self", "", "", , , , True)%></asp:panel></td>
										</tr>
										<tr>
											<td colSpan="2"><br>
												<table class="table" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr bgColor="#e7ebef">
														<td width="140"><strong>文档名称</strong>
														</td>
														<td width="140"><strong>所属资源</strong></td>
														<td width="140"><strong>共享资源</strong></td>
														<td width="60">&nbsp;</td>
													</tr>
													<%For i As Integer = 0 To dtDocumentCenter.Rows.Count - 1%>
													<tr>
														<td><%=DbField.GetStr(dtDocumentCenter.Rows(i), "ResName") %></td>
														<td><%=DbField.GetStr(dtDocumentCenter.Rows(i), "DocName") %></td>
														<td><%=DbField.GetStr(dtDocumentCenter.Rows(i), "ShareRecordName") %></td>
														<td width="60px" align="center"><a href="#" onclick="CancelShare('<%=DbField.GetStr(dtDocumentCenter.Rows(i), "DOCID") %>','<%=DbField.GetStr(dtDocumentCenter.Rows(i), "ColumnName") %>',<%=DbField.GetStr(dtDocumentCenter.Rows(i), "ShareRecordID") %>);">取消共享</a></td>
													</tr>
													<%Next%>
												</table>
												<br>
												<asp:Button ID="btnCancelShare" Runat="server" Text="取消共享" style="DISPLAY:none"></asp:Button>
												<asp:TextBox ID="txtDocID" Runat="server" style="DISPLAY:none"></asp:TextBox>
												<asp:TextBox ID="txtColumnName" Runat="server" style="DISPLAY:none"></asp:TextBox>
												<asp:TextBox ID="txtShareRecordID" Runat="server" style="DISPLAY:none"></asp:TextBox>
											</td>
										</tr>
									</TABLE>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
