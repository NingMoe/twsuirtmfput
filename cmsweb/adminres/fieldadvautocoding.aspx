<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvAutoCoding" validateRequest="false" CodeFile="FieldAdvAutoCoding.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FieldAdvAutoCoding</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
function RowLeftClickPost(src){
	var o=src.parentNode;
	for (var k=1;k<o.children.length;k++){
		o.children[k].bgColor = "white";
	}
	
	self.document.forms(0).acodeaction.value = "rowclick"; //需要将用户选择的行号POST给服务器
	self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
	self.document.forms(0).submit();
}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="acodeaction"> <input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="504" border="0" style="WIDTH: 504px">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="490" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b>自动编码设置 （资源：[
										<asp:label id="lblResName" runat="server"></asp:label>]&nbsp; 字段：[
										<asp:label id="lblFieldName" runat="server"></asp:label>]）</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><asp:datagrid id="DataGrid1" runat="server" Width="456px"></asp:datagrid></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><FONT face="宋体"><BR>
										<BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5">
									<table class="table_level3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="header_level3" colSpan="2" height="20">常量设置</td>
										</tr>
										<tr>
											<td height="20"><asp:textbox id="txtSNumConstant" runat="server"></asp:textbox><asp:button id="btnSNumAddConstant" runat="server" Text="添加常量"></asp:button><asp:button id="btnSNumEditConstant" runat="server" Text="修改常量"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><FONT face="宋体"><BR>
										<BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5">
									<table class="table_level3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="header_level3" colSpan="2" height="20">日期设置</td>
										</tr>
										<tr>
											<td height="20"><asp:radiobutton id="rdoSNumYear1" runat="server" Text="YYYY(年)" GroupName="GrpSNumDate"></asp:radiobutton><asp:radiobutton id="rdoSNumYear2" runat="server" Text="YY(年)" GroupName="GrpSNumDate"></asp:radiobutton><asp:radiobutton id="rdoSNumMonth" runat="server" Text="MM(月)" GroupName="GrpSNumDate"></asp:radiobutton><asp:radiobutton id="rdoSNumDate" runat="server" Text="DD(日)" GroupName="GrpSNumDate"></asp:radiobutton><asp:button id="btnSNumAddDate" runat="server" Text="添加日期"></asp:button><asp:button id="btnSNumEditDate" runat="server" Text="修改日期"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><FONT face="宋体"><BR>
										<BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5">
									<!--流水号设置开始-->
									<table class="table_level3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="header_level3" colSpan="2" height="20">流水号设置</td>
										</tr>
										<tr>
											<td height="20">重置时间：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumNoReset" runat="server" Text="不重置" GroupName="GrpSNumResetTime"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumYearReset" runat="server" Text="每年" GroupName="GrpSNumResetTime"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumMonthReset" runat="server" Text="每月" GroupName="GrpSNumResetTime"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumDateReset" runat="server" Text="每日" GroupName="GrpSNumResetTime"></asp:radiobutton></td>
										</tr>
										<tr>
											<td height="20">流水号位数：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:textbox id="txtSNumDigitNum" runat="server" Width="48px" MaxLength="2"></asp:textbox><asp:checkbox id="chkSNumPreZero" runat="server" Text="位数不够的前面用0补齐"></asp:checkbox></td>
										</tr>
										<tr>
											<td height="20">跳过不吉利数字：
												<asp:textbox id="txtNumToSkip" runat="server" Width="56px"></asp:textbox>(格式：4 
												或 4,7)
											</td>
										</tr>
										<TR>
											<TD height="20"><FONT face="宋体"><FONT face="宋体">当前流水号值：&nbsp;&nbsp;
														<asp:textbox id="txtSNumValue" runat="server" Width="64px"></asp:textbox></FONT></FONT></TD>
										</TR>
										<tr>
											<td height="20"><asp:button id="btnSNumAddSNum" runat="server" Text="添加流水号设置"></asp:button><asp:button id="btnSNumEditSNum" runat="server" Text="修改流水号设置"></asp:button></td>
										</tr>
									</table> <!--流水号设置结束--></TD>
							</TR>
							<TR height="22">
								<TD style="WIDTH: 480px"><FONT face="宋体"><BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px"><FONT face="宋体"><asp:button id="btnExit" runat="server" Width="104px" Text="退出"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
