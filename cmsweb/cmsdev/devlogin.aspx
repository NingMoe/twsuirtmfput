<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DevLogin" CodeFile="DevLogin.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE id=onetidTitle>ϵͳ���ߣ���¼</TITLE>
		<meta http-equiv="Pragma" content="no-cache" />
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
<script language="javascript">
<!--	
function GotoLogin(){
	window.location = "/cmsweb/Default.htm";
}
//-->
</script>
</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" onKeyDown="13" method="post" runat="server">
			<TABLE style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px"
				width="100%" border="0">
				<TBODY>
					<TR>
						<!--class="toolbar_table"  ϵͳ��װ��ʼ��-->
						<TD vAlign="middle" align="center" width="220" colSpan="2">&nbsp;</TD>
					</TR>
					<TR>
						<TD width="2"></TD>
						<TD align="center" width="99%">
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="500" border="0">
								<TR>
									<TD class="header_level2" align="center" colSpan="2" height="19">
										ϵͳ����</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="19"></TD>
									<TD vAlign="middle" align="left" width="340" height="19"><BR>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="19">ϵͳ���ߺ��룺</TD>
									<TD vAlign="middle" align="left" width="340" height="19">
										<asp:TextBox id="txtCode" runat="server" Width="240px" TextMode="Password"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="55"></TD>
									<TD vAlign="middle" align="left" width="340" height="55"><INPUT id=btnLogin type=submit value=ϵͳ��¼ name=btnLogin><INPUT id=btnToLogin onclick="javascript:window.location='/cmsweb/Default.htm';" type=button value='����ҳ'>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
<script language="javascript">
<!--
try{
	Form1.txtCode.focus();
}catch(exception){
}
-->
</script>
	</BODY>
</HTML>
