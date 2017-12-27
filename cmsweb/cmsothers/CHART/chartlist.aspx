<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ChartList" CodeFile="ChartList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ChartList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<script language="javascript">
		function OpenCenterWindow(url,width,height)
		{
			var left = (screen.availWidth-width)/2;
			var top = (screen.availHeight-height)/2-20;
			
			var openWin = window.open(url,"","top="+top+",left="+left+",height="+height+",width="+width+",scrollbars=yes,resizable=no,status=yes");
			try
			{
				openWin.focus();
			}
			catch(e)
			{
				alert(e);
			}
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header"><b><asp:label id="lblTitle" runat="server">图表统计</asp:label></b></TD>
							</TR>
							<TR>
								<TD vAlign="middle"><asp:button id="btnAddCond" runat="server" Text="添加统计" Width="80px"></asp:button><FONT face="宋体"></FONT><FONT face="宋体">&nbsp;&nbsp;</FONT><FONT face="宋体">&nbsp;</FONT></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" vAlign="middle"></TD>
							</TR>
							<TR>
								<TD vAlign="middle"><asp:datagrid id="DataGrid1" runat="server">
										<Columns>
											<asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
											<asp:BoundColumn DataField="CHARTNAME" HeaderText="统计名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="CHARTKIND" HeaderText="图表类型"></asp:BoundColumn>
											<asp:BoundColumn DataField="ConditionColumnNAME1" HeaderText="条件字段1"></asp:BoundColumn>
											<asp:BoundColumn DataField="ConditionColumnNAME2" HeaderText="条件字段2"></asp:BoundColumn>
											<asp:BoundColumn DataField="VALUECOLUMNNAME" HeaderText="统计字段"></asp:BoundColumn>
											<asp:ButtonColumn Text="编辑" CommandName="Select"></asp:ButtonColumn>
											<asp:TemplateColumn><ItemTemplate><a onclick="javascript:OpenCenterWindow('Chartview.aspx?chartid=<%#DataBinder.Eval(Container.DataItem,"Id")%>',900,700); return false;" style="CURSOR: hand; TEXT-DECORATION: underline">查看</a></ItemTemplate></asp:TemplateColumn>
											<asp:ButtonColumn Text="&lt;font onclick = &quot;javascript:return confirm('您确定删除吗？')&quot;&gt;删除&lt;/font&gt;"
												CommandName="Delete"></asp:ButtonColumn>
											
										</Columns>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
