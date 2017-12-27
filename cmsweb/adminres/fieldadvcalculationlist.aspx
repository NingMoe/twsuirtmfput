<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCalculationList" validateRequest="false" CodeFile="FieldAdvCalculationList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>计算公式列表</title>
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
	self.document.forms(0).fmlaiid.value = src.fmlaiid; //需要将用户选择的行号POST给服务器
	self.document.forms(0).fmlcolname.value = src.fmlcolname; //需要将用户选择的行号POST给服务器
	self.document.forms(0).fmlresid.value = src.fmlresid; //需要将用户选择的行号POST给服务器
	self.document.forms(0).fmltype.value = src.fmltype; //需要将用户选择的行号POST给服务器
}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="fmltype"> <input type="hidden" name="fmlcolname"> <input type="hidden" name="fmlresid">
			<input type="hidden" name="fmlaiid">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="99%" border="0">
							<TR>
								<TD class="header_level2" height="22"><b>计算公式设置&nbsp;（资源：<asp:label id="lblResDispName" runat="server"></asp:label>）</b>
								</TD>
							</TR>
							<tr>
								<td height="4"></td>
							</tr>
							<TR>
								<TD height="5"><asp:button id="btnSetFormula" runat="server" Text="修改公式"></asp:button><asp:button id="btnDelFormula" runat="server" Text="删除公式"></asp:button>
									<asp:Button id="btnFmlOption" runat="server" Text="选项设置"></asp:Button><asp:Button id="btnSchedule" runat="server" Text="定时计算"></asp:Button><asp:button id="btnAddVerifyFormula" runat="server" Text="添加校验公式"></asp:button><asp:button id="btnExit" runat="server" Text="退出" Width="72px"></asp:button>
									&nbsp;&nbsp;<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveup" runat="server">向上移动</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveToFirst" runat="server">移至首位</asp:linkbutton>&nbsp;&nbsp;&nbsp;<IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;&nbsp;<asp:linkbutton id="lbtnMovedown" runat="server">向下移动</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveToLast" runat="server">移至末位</asp:linkbutton></TD>
							</TR>
							<tr>
								<td height="4"></td>
							</tr>
							<TR>
								<TD height="22"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False"></asp:datagrid></TD>
							</TR>
							<TR>
								<TD height="12"></TD>
							</TR>
							<TR>
								<TD height="5"><FONT face="宋体">以上计算公式的排列次序即是各个公式的运算次序。</FONT>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
