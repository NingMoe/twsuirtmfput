<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearchResult2ColumnII" CodeFile="MTableSearchResult2ColumnII.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>统计表</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<style type="text/css">
		
			TABLE { TABLE-LAYOUT: fixed }
			TH STRONG { DISPLAY: block; WIDTH: 100% }
			TR STRONG { OVERFLOW: hidden; WHITE-SPACE: nowrap }
			TR TD { OVERFLOW: hidden; WHITE-SPACE: nowrap }
			
			@media Print { 
				.notprint { DISPLAY: none }
			}
			@media Screen {
				.notprint { DISPLAY: inline; CURSOR: hand }
			}
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD style="HEIGHT: 30px" align="center" colSpan="4"><asp:label id="lblHeader" runat="server" Font-Bold="True" Font-Size="24px" Font-Names="宋体"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="center" colSpan="4"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF1" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><FONT face="宋体"><asp:label id="lblF1Val" runat="server"></asp:label></FONT></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><FONT face="宋体"><asp:label id="lblF2" runat="server"></asp:label></FONT></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><FONT face="宋体"><asp:label id="lblF2Val" runat="server"></asp:label></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF3" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF3Val" runat="server"></asp:label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF4" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF4Val" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF5" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF5Val" runat="server"></asp:label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF6" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF6Val" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF7" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF7Val" runat="server"></asp:label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF8" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF8Val" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="left" colSpan="4"></TD>
							</TR>
							<TR>
								<TD align="left" colSpan="4"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="left" colSpan="4"><FONT face="宋体"></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF9" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF9Val" runat="server"></asp:label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:label id="lblF10" runat="server"></asp:label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><asp:label id="lblF10Val" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="left" colSpan="4"><FONT face="宋体"></FONT></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 2px" align="left" colSpan="4"><asp:label id="lblTail" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<OBJECT id="WebBrowser" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" VIEWASTEXT>
			</OBJECT>
			<div style="display:none">
			<asp:label class="notprint" id="Label4" runat="server">行 高:  </asp:label><asp:textbox class="notprint" Width="50" id="txtRowHeight" runat="server">20</asp:textbox>
			<asp:label class="notprint" id="Label1" runat="server">行 数:  </asp:label><asp:textbox class="notprint" Width="50" id="txtPageSize"  runat="server">10</asp:textbox>
			<asp:label class="notprint" id="Label3" runat="server">总页数: </asp:label><asp:textbox class="notprint" Width="50" id="txtPageCount" runat="server">0</asp:textbox>
			<asp:label class="notprint" id="Label2" runat="server">当前页: </asp:label><asp:textbox class="notprint" Width="50" id="txtPageIndex" runat="server">0</asp:textbox>
			<asp:button class="notprint" id="btnSetting" runat="server" Text="参数设定"></asp:button><br>
			</div>
			<asp:button class="notprint" id="btnFirstPage" runat="server" Text="第一页"></asp:button>
			<asp:button class="notprint" id="btnUpPage" runat="server" Text="上一页"></asp:button>
			<asp:button class="notprint" id="btnNextPage" runat="server" Text="下一页"></asp:button>
			<asp:button class="notprint" id="btnLastPage" runat="server" Text="最一页"></asp:button>
			<input type="button" id="btn_PreView"   title="预览"             value="预览"             class='notprint' onclick="WebBrowser.ExecWB(7,1);">
			<input type="button" id="btn_Print"     title="打印"             value="打印"             class='notprint' onclick="WebBrowser.ExecWB(6,1);">
			<input type="button" id="btn_PrintNext" title="打印 并 转下一页" value="打印 并 转下一页" class="notprint" onclick="WebBrowser.ExecWB(6,1);document.getElementById('btnNextPage').click();">
			<asp:label class="notprint" id="labPageInfo" runat="server"></asp:label>
			</form>
	</body>
</HTML>