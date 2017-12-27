<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.BatchUpdateField" CodeFile="BatchUpdateField.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>批处理设置字段值</title>
		<meta http-equiv="Pragma" content="no-cache">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--		
function ConfirmUpdate(msg1, msg2){
	if (confirm(msg1) == true){
		if (confirm(msg2) == true){
			return true;
		}else{
			return false;
		}
	}else{
		return false;
	}
}

function displayConditions(obj)
{
if(obj.checked)
{
document.all("tr1").style.display="none";
document.all("tr2").style.display="none";
document.all("tr3").style.display="none";
}
else
{
document.all("tr1").style.display="block";
document.all("tr2").style.display="block";
document.all("tr3").style.display="block";
}
}
-->
		</script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD colspan="2" class="form_header"><b>批量更新字段</b></TD>
							</TR>
							<TR height="28">
								<TD style="WIDTH: 136px" align="right" width="136"><FONT face="宋体">资源名称</FONT></TD>
								<TD>
									<asp:Label id="lblResName" runat="server"></asp:Label></TD>
							</TR>
							<% If datres.IsShowCheckBox = 1 and RStr("selectedrecid")<>"" then %>
							<TR>
								<td width="136" align="right" style="WIDTH: 136px"></td>
								<TD>
									<asp:CheckBox id="CheckBox1" runat="server" Text="更新已选择的记录" onclick="displayConditions(this)"></asp:CheckBox>
								</TD>
							</TR>
							<% end if%>
							<TR id="tr1" style="display:<%if CheckBox1.Checked then 
										response.write("none") 
										else 
										response.write("block") 
										end if%>">
								<td width="136" align="right" style="WIDTH: 136px">条件1</td>
								<TD>
									<asp:dropdownlist id="ddlColumns1" runat="server" Width="140px"></asp:dropdownlist><FONT face="宋体">中</FONT>
									<asp:dropdownlist id="ddlConditions1" runat="server" Width="88px"></asp:dropdownlist>
									<asp:textbox id="txtSearchValue1" runat="server" Width="160px" ToolTip="请输入查询内容"></asp:textbox>
								</TD>
							</TR>
							<TR id="tr2"  style="display:<%if CheckBox1.Checked then 
										response.write("none") 
										else 
										response.write("block") 
										end if%>">
								<TD style="WIDTH: 136px" align="right" width="136"><FONT face="宋体">条件2</FONT></TD>
								<TD>
									<asp:dropdownlist id="ddlColumns2" runat="server" Width="140px"></asp:dropdownlist><FONT face="宋体">中</FONT>
									<asp:dropdownlist id="ddlConditions2" runat="server" Width="88px"></asp:dropdownlist>
									<asp:textbox id="txtSearchValue2" runat="server" Width="160px" ToolTip="请输入查询内容"></asp:textbox></TD>
							</TR>
							<TR id="tr3"  style="display:<%if CheckBox1.Checked then 
										response.write("none") 
										else 
										response.write("block") 
										end if%>">
								<TD style="WIDTH: 136px" align="right" width="136"><FONT face="宋体">条件3</FONT></TD>
								<TD>
									<asp:dropdownlist id="ddlColumns3" runat="server" Width="140px"></asp:dropdownlist><FONT face="宋体">中</FONT>
									<asp:dropdownlist id="ddlConditions3" runat="server" Width="88px"></asp:dropdownlist>
									<asp:textbox id="txtSearchValue3" runat="server" Width="160px" ToolTip="请输入查询内容"></asp:textbox></TD>
							</TR>
							<TR>
								<td width="136" align="right" valign="bottom" style="WIDTH: 136px"><FONT face="宋体">满足条件的字段赋值</FONT></td>
								<TD valign="bottom">
									<asp:dropdownlist id="ddlDestColumn" runat="server" Width="140px"></asp:dropdownlist><FONT face="宋体">的值修改为
									</FONT>
									<asp:TextBox id="txtColValue" runat="server" Width="208px"></asp:TextBox>
								</TD>
							</TR>
							<TR height="12">
								<td width="136" style="WIDTH: 136px"></td>
								<TD></TD>
							</TR>
							<TR>
								<td style="WIDTH: 136px"></td>
								<TD>
									<asp:Button id="btnConfirm" runat="server" Text="确认" Width="100px"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Text="退出" Width="68px"></asp:Button>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</BODY>
</HTML>
