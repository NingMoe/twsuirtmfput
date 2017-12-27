<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FormSetElementScript" CodeFile="FormSetElementScript.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>属性设置</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" rel="stylesheet" type="text/css">
		<script language="JavaScript">
<!--
//打开选择的窗体
function ConfirmProperty(){
	window.returnValue = document.all.item("txtValue").value;
	window.close();
}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="564" border="0" class="table_level2" style="WIDTH: 564px; HEIGHT: 313px">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>元素脚本设置</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 101px; HEIGHT: 19px" align="right" width="101"><FONT face="宋体"> </FONT>
								</TD>
								<TD style="HEIGHT: 19px"></TD>
							</TR>
							<TR height="25">
								<TD width="101" align="right" style="WIDTH: 101px" vAlign="top"><FONT face="宋体"><FONT face="宋体">脚本内容：</FONT></FONT></TD>
								<TD align="left" vAlign="top"><asp:TextBox id="txtValue" runat="server" Width="444px" Height="304px" TextMode="MultiLine"></asp:TextBox></TD>
							</TR>
							<TR height="25">
								<TD width="101" align="right" style="WIDTH: 101px"></TD>
								<TD vAlign="top"><INPUT id="btnConfirm" onclick="javascript:ConfirmProperty()" style="WIDTH: 72px; HEIGHT: 24px"
										type="button" value="确认"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
