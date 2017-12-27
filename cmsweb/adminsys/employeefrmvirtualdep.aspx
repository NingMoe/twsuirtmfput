<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmployeeFrmVirtualDep" CodeFile="EmployeeFrmVirtualDep.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE id="onetidTitle">虚拟部门的人员管理</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<style>
            .GOGG02-FixedHeader   
              {   
              position:relative;   
              top:expression(this.offsetParent.scrollTop-2);            
              }
        </style>
    
			<script language="javascript">
<!--
function RowLeftClickNoPost(src){
	try{
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			o.children[k].bgColor = "white";
		}
		src.bgColor = "#C4D9F9";
		self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
		var h=document.documentElement.offsetHeight-340;
		document.getElementById("frmEmpDep").src="EmployeeVirtualDep.aspx?uid="+src.USERID+"&h="+h;
	}catch(ex){
	}
} 
-->
			</script>
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; PADDING-TOP: 1px"
				cellSpacing="0" cellPadding="0" width="98%" border="0">
				<tr>
					<td colspan="2">
						<TABLE class="toolbar_table" cellSpacing="0" border="0">
							<TR>
								<TD width="8"></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/man.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lnkAdd" runat="server">添加人员</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/man_del.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lnkDelete" runat="server" OnClientClick="return window.confirm('您确定要删除？');">删除人员</asp:linkbutton></TD>
								<td width="100%">&nbsp;</td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" style="width:450px;">	
					    <TABLE class="toolbar_table" cellSpacing="0" border="0" style="width:450px;">
		                    <TR>
			                    <TD width="5"></TD>
			                    <TD noWrap align="left" style="font-weight:bold;">
			                         帐号/姓名：<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>	<asp:Button ID="btnSearch" runat="server" Text="搜索" />
			                    </TD>  
		                    </TR>
	                    </TABLE>	
					   		   
				        <asp:DataGrid id="DataGrid1" runat="server">
				            <Headerstyle forecolor="Black" cssclass="GOGG02-FixedHeader"></Headerstyle>   
				        </asp:DataGrid>	
					</TD>
					<td vAlign="top"><iframe src="" runat="server" id="frmEmpDep" width="100%" frameborder=0 height="200"></iframe></td>
				</TR> 
			</TABLE>
		</FORM>
	</BODY>
</HTML>

<script language="javascript">

//document.getElementById("frmEmpDep").style.height = document.documentElement.offsetHeight-340;
</script>
