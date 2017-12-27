<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceExport" CodeFile="ResourceExport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>资源导出</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
var btnClicked = false; //用来判断是否已经点击的“确认”按钮
function ConfirmButtonClicked(){
	if (btnClicked == false){
		btnClicked = true;
		return true;
	}else{
		alert("正在导出，不能重复点击！");
	}
}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD colspan="2" class="form_header">
									<P><b>资源数据</b><B>导出</B></P>
								</TD>
							</TR>
							<TR>
								<TD colspan="2" style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:label id="Label3" runat="server">导出文件类型：</asp:label></TD>
								<TD align="left"><asp:dropdownlist id="ddlExportFileType" runat="server" Width="208px"></asp:dropdownlist>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right">&nbsp;</TD>
								<TD style="HEIGHT: 12px" align="left"><asp:checkbox id="chkExpChild" runat="server" Text="导出所有子资源"></asp:checkbox><FONT face="宋体"><BR>
									</FONT><FONT face="宋体">
										<asp:checkbox id="chkContinueOnError" runat="server" Text="导出出错时继续下一条记录的导出"></asp:checkbox></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right">&nbsp;</TD>
								<TD align="left">
									<asp:button id="btnConfirm" runat="server" Width="104px" Text="开始导出"></asp:button>
									<asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
