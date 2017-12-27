<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysGetLog" CodeFile="SysGetLog.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE id=onetidTitle>提取系统技术日志</TITLE>
		
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
</HEAD>
	<BODY>
		<SCRIPT>
		</SCRIPT>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="400" border="0" style="WIDTH: 400px">
							<TR>
								<TD class="header_level2" width="320" colSpan="2" height="22"><b>提取系统技术日志</b></TD>
							</TR>
							<TR height="5">
								<td align="right" width="10" height=22></td>
								<TD width="200" height=22>
								</TD>
							</TR>
							<TR>
								<TD align="right" width="10" height="3"></TD>
								<TD height="3">
									请提取系统运行时产生的技术日志文件，并以电子邮件方式发送给软件厂商，以便解决系统问题。</TD>
							</TR>
        <TR>
          <TD align=right width=10 height=22></TD>
          <TD height=22></TD></TR>
							<TR height="22">
								<td width="10"></td>
								<TD>
									<asp:LinkButton id="lbtnGetThisMonth" runat="server">[提取技术日志文件]</asp:LinkButton></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
