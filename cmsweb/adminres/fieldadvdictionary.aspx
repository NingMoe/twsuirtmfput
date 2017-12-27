<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvDictionary" validateRequest="false" CodeFile="FieldAdvDictionary.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>高级字典设置</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
function RowLeftClickNoPost(src){
	var o=src.parentNode;
	for (var k=1;k<o.children.length;k++){
		o.children[k].bgColor = "white";
	}
		
	src.bgColor = "#C4D9F9";
	self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="10"><b>高级字典设置</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 102px; HEIGHT: 12px" align="right" width="102" height="12"></TD>
								<TD style="WIDTH: 448px; HEIGHT: 12px" width="448" height="12"></TD>
							</TR>
							<TR height="21">
								<TD style="WIDTH: 102px" align="right" width="102"><asp:label id="Label2" runat="server">当前字段：</asp:label></TD>
								<TD style="WIDTH: 448px" width="448"><asp:label id="lblFieldName" runat="server"></asp:label>&nbsp;(<asp:label id="lblFieldType" runat="server"></asp:label>)</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 102px" align="right" width="102"><asp:label id="Label3" runat="server">字典记录编辑：</asp:label></TD>
								<TD style="WIDTH: 448px" width="448">
									<asp:checkbox id="chkAddDictRes" runat="server" Text="允许添加字典记录"></asp:checkbox>&nbsp;
									<asp:checkbox id="chkEditDictRes" runat="server" Text="允许修改字典记录"></asp:checkbox>&nbsp;
									<asp:button id="btnSave" runat="server" Text="保存编辑设置" Width="104px"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 102px" align="right" width="102"></TD>
								<TD style="WIDTH: 448px" width="448"></TD>
							</TR>
							<TR height="21">
								<TD style="WIDTH: 102px; HEIGHT: 18px" align="right" width="102"></TD>
								<TD style="WIDTH: 448px; HEIGHT: 18px" width="448"></TD>
							</TR>
							<TR height="23">
								<td style="WIDTH: 557px" vAlign="top" colSpan="2" height="22">
									<TABLE style="WIDTH: 508px" cellSpacing="0" cellPadding="0" width="508" border="0">
										<TR>
											<TD style="WIDTH: 252px" vAlign="bottom"><asp:label id="Label1" runat="server">当前资源</asp:label>&nbsp;<asp:label id="lblResName2" runat="server"></asp:label></TD>
											<TD style="WIDTH: 321px" vAlign="bottom"><asp:linkbutton id="lbtnSelDictRes" runat="server">选择字典资源</asp:linkbutton>&nbsp;<asp:label id="lblDictResName" runat="server"></asp:label></TD>
											<TD style="WIDTH: 18px" vAlign="bottom"></TD>
										</TR>
										<tr>
											<td style="WIDTH: 252px; HEIGHT: 250px" vAlign="top"><asp:listbox id="ListBox1" runat="server" Width="240px" Height="245px"></asp:listbox></td>
											<td style="WIDTH: 321px; HEIGHT: 250px" vAlign="top"><asp:listbox id="ListBox2" runat="server" Width="240px" Height="245px"></asp:listbox></td>
											<td style="WIDTH: 18px; HEIGHT: 250px" vAlign="top"></td>
										</tr>
										<TR>
											<TD style="WIDTH: 578px; HEIGHT: 28px" vAlign="top" align="left" colSpan="2"><asp:button id="btnAddDicField" runat="server" Text="添加匹配字段" Width="100px"></asp:button><asp:button id="btnAddDictRefCol" runat="server" Text="添加参考字段" Width="100px"></asp:button><asp:button id="btnAddFilterCol" runat="server" Text="添加过滤字段" Width="100px"></asp:button><asp:button id="btnDictWhere" runat="server" Text="行条件设置" Width="88px"></asp:button><asp:button id="btnExit" runat="server" Text="退出" Width="68px"></asp:button></TD>
											<TD style="WIDTH: 18px; HEIGHT: 28px" vAlign="top"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 578px; HEIGHT: 28px" vAlign="top" align="left" colSpan="2">
												<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">
												<asp:linkbutton id="lbtnMoveup" runat="server">向上移动</asp:linkbutton>
												<asp:LinkButton id="lbtnMoveToFirst" runat="server">移至首位</asp:LinkButton><IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">
												<asp:linkbutton id="lbtnMovedown" runat="server">向下移动</asp:linkbutton>
												<asp:LinkButton id="lbtnMoveToLast" runat="server">移至末位</asp:LinkButton>
											</TD>
											<TD style="WIDTH: 18px; HEIGHT: 28px" vAlign="top"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 455px" vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></TD>
										</TR>
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
