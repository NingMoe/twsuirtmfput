<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysConfigUpdate" CodeFile="SysConfigUpdate.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE 
id=onetidTitle>系统配置升级</TITLE>
		
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
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="696" border="0" height=134>
							<TR>
								<TD class="header_level2" width="320" colSpan="2" height="22"><b>系统配置升级</b></TD>
							</TR>
							<TR height="5">
								<td align="right" width="122" height=22></td>
								<TD width="200" height=22>
								</TD>
							</TR>
							<TR>
								<TD align="right" width="122" height="3"></TD>
								<TD height="3"><asp:Label id=Label3 runat="server">请在本软件系统厂商或服务商的指导下使用此功能</asp:Label></TD>
							</TR>
        <TR>
          <TD align=right width=122 height=22><asp:Label id=Label1 runat="server">上传配置文件：</asp:Label></TD>
          <TD height=22><INPUT 
            style="WIDTH: 432px; HEIGHT: 21px" type=file size=52 id=fileConfig runat="server"><asp:Button id=btnUpload runat="server" Text="开始上传" Width="116px" Height="21px"></asp:Button></TD></TR>
        <TR>
          <TD align=right width=122 height=22><asp:Label id=Label2 runat="server">配置文件类型：</asp:Label></TD>
          <TD height=22><asp:DropDownList id=ddlFileType runat="server" Width="140px"></asp:DropDownList>&nbsp;<asp:Label id=Label4 runat="server">或指定目录：</asp:Label><asp:TextBox id=txtFolder runat="server" Width="220px"></asp:TextBox><asp:Label id=Label5 runat="server">(格式如：\data\db)</asp:Label></TD></TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
