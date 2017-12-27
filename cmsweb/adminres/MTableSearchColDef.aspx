<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearchColDef" CodeFile="MTableSearchColDef.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>查询设置</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<script language="javascript">
<!--
function OpenMultiTableSearchResultWindow(MTSHostID){
  //宽度720：刚好打印A4纸
	window.open("/cmsweb/adminres/MTableSearchResultSimple.aspx?mtshostid=" + MTSHostID + "&mnuresid=" + getUrlParam("mnuresid") + "&mtslogicand=1", '_blank', "left=10,top=10,height=630,width=720,status=no,toolbar=no,menubar=yes,location=no,resizable=yes,scrollbars=yes");
	return false;
}

function RowLeftClickNoPost(src){
	try{
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			o.children[k].bgColor = "white";
		}
		src.bgColor = "#C4D9F9";
		self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
	}catch(ex){
	}
}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header"><b><asp:label id="lblTitle" runat="server">Label</asp:label></b></TD>
							</TR>
							<TR>
								<TD><FONT face="宋体"><asp:label id="lblSearchTitle" runat="server">设置标题</asp:label></FONT>&nbsp;<asp:textbox id="txtMTSearchTitle" runat="server" Width="260px"></asp:textbox><asp:button id="btnSave" runat="server" Width="100px" Text="保存设置"></asp:button><asp:Button id="btnDelete" runat="server" Width="80px" Text="删除设置"></asp:Button></TD>
							</TR>
							<TR>
								<TD><FONT face="宋体"><asp:linkbutton id="lbtnSelHostRes" runat="server">选择资源</asp:linkbutton></FONT>&nbsp;<asp:dropdownlist id="ddlResList" runat="server" Width="104px"></asp:dropdownlist><asp:dropdownlist id="ddlHostResCol" runat="server" Width="156px"></asp:dropdownlist><asp:label id="Label1" runat="server">的值</asp:label><asp:dropdownlist id="ddlHostConditions" runat="server" Width="72px"></asp:dropdownlist><asp:dropdownlist id="dllDefaultVal" runat="server" Width="100px"></asp:dropdownlist><asp:textbox id="txtColValue" runat="server" Width="90px"></asp:textbox><asp:label id="Label4" runat="server">或</asp:label><asp:dropdownlist id="ddlValCol" runat="server" Width="138px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD vAlign="middle"><asp:button id="btnStartSearch" runat="server" Width="80px" Text="开始查询"></asp:button><asp:button id="btnAddShow" runat="server" Width="80px" Text="添加显示"></asp:button><asp:button id="btnAddCond" runat="server" Width="80px" Text="添加条件"></asp:button><asp:button id="btnAddOrderASC" runat="server" Width="80px" Text="添加升序"></asp:button><asp:button id="btnAddOrderDesc" runat="server" Width="80px" Text="添加倒序"></asp:button><asp:button id="btnAddEmail" runat="server" Width="80px" Text="添加邮箱"></asp:button><asp:button id="btnAddMobile" runat="server" Width="80px" Text="添加手机"></asp:button><asp:button id="btnExit" runat="server" Width="80px" Text="退出"></asp:button><FONT face="宋体"><asp:image id="imgUp" runat="server" ImageUrl="/cmsweb/images/Icons/up.gif"></asp:image></FONT><asp:linkbutton id="lbtnMoveUp" runat="server">向上移动</asp:linkbutton><FONT face="宋体">&nbsp;
										<asp:image id="imgDown" runat="server" ImageUrl="/cmsweb/images/Icons/up.gif"></asp:image></FONT><FONT face="宋体">&nbsp;</FONT><asp:linkbutton id="lbtnMoveDown" runat="server">向下移动</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" vAlign="middle"></TD>
							</TR>
							<TR>
								<TD vAlign="middle"><FONT face="宋体"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
