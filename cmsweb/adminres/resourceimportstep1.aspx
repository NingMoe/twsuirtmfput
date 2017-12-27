<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceImportStep1" CodeFile="ResourceImportStep1.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>导入外部数据表单－步骤1</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body>
	<form id="Form1" method="post" encType="multipart/form-data" runat="server">
	<TABLE class="frame_table" cellSpacing=0 cellPadding=0>
		<tr>
			<td>
				<TABLE class="form_table" cellSpacing="0" cellPadding="0">
					<TR>
						<TD colspan="2" class="form_header"><b>导入外部数据表单</b></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 136px; HEIGHT: 24px" align=right><asp:Label id=Label8 runat="server">目标资源</asp:Label></TD>
						<TD style="HEIGHT: 24px" align=left><FONT face=宋体>
																	<asp:Label id="lblResName" runat="server"></asp:Label></FONT></TD></TR>
					<TR class="module_header">
						<TD style="WIDTH: 136px" align=right></TD>
						<TD align=left><asp:Label id=Label1 runat="server" >导入MS Access或MS Excel表单数据</asp:Label></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right><asp:Label id=Label2 runat="server">文件全路径</asp:Label></TD>
						<TD align=left>
                            <asp:FileUpload ID="File1" runat="server" />
						</TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right></TD>
						<TD align=left><asp:button id=btnConfirm runat="server" Text="导入表单" Width="104px"></asp:button><asp:Button id=btnExit runat="server" Text="退出" Width="56px"></asp:Button></TD></TR>
					<TR>
						<TD style="WIDTH: 136px; HEIGHT: 12px" align=right></TD>
						<TD style="HEIGHT: 12px" align=left></TD></TR>
					<TR class="module_header">
						<TD style="WIDTH: 136px" align=right></TD>
						<TD align=left><asp:Label id=Label3 runat="server" >导入SQL表单数据</asp:Label></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right><asp:Label id=Label4 runat="server">SQL数据库地址</asp:Label></TD>
						<TD align=left><asp:TextBox id=txtSqlIP runat="server" Width="168px"></asp:TextBox></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right><asp:Label id=Label5 runat="server">数据库端口</asp:Label></TD>
						<TD align=left><FONT face=宋体><asp:TextBox id=txtSqlPort runat="server" Width="168px">1433</asp:TextBox></FONT></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right><asp:Label id=Label9 runat="server">数据库名称</asp:Label></TD>
						<TD align=left><asp:TextBox id=txtSqlDbName runat="server" Width="168px"></asp:TextBox></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right><asp:Label id=Label6 runat="server">登录用户名</asp:Label></TD>
						<TD align=left><asp:TextBox id=txtSqlUser runat="server" Width="168px"></asp:TextBox></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right><asp:Label id=Label7 runat="server">登录用户密码</asp:Label></TD>
						<TD align=left><asp:TextBox id=txtSqlUserPass runat="server" Width="168px" TextMode="Password"></asp:TextBox></TD></TR>
					<TR>
						<TD style="WIDTH: 136px" align=right></TD>
						<TD align=left><asp:Button id=btnImpSQL runat="server" Width="104px" Text="导入SQL表单"></asp:Button><asp:Button id=btnExit2 runat="server" Width="56px" Text="退出"></asp:Button></TD></TR>
				</TABLE>
			</td>
		</tr>
	</TABLE>
	</form>
	</body>
</HTML>
