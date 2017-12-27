<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmployeeFrmContent" CodeFile="EmployeeFrmContent.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE>人员管理</TITLE>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
		 <input type="hidden" name="selectRecId1">
			<TABLE style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px"
				cellSpacing="0" cellPadding="0" width="98%" border="0">
				<tr>
					<td>
						<TABLE class="toolbar_table" cellSpacing="0" border="0">
							<TR>
								<TD width="8"></TD>

								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/man.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lnkAdd" runat="server">增加人员</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/man2.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lnkEdit" runat="server">修改人员</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/man_del.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lnkDelete" runat="server">删除人员</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/man_move.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnChangeDep" runat="server">更换部门</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/lock.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lnkSetPassword" runat="server">设置密码</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/key4.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnClearPassword" runat="server">清空密码</asp:linkbutton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/field.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:LinkButton id="lbtnColumnSet" runat="server">字段设置</asp:LinkButton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/table.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:LinkButton id="lbtnColshowSet" runat="server">显示设置</asp:LinkButton></TD>
								<TD noWrap align="left" width="80"><IMG src="/cmsweb/images/titleicon/window.gif" align="absMiddle" width="18" height="18">&nbsp;<asp:LinkButton id="lbtnInputformDesign" runat="server">窗体设计</asp:LinkButton></TD>

								<td width="100%">&nbsp;</td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td align="left">
						<asp:dropdownlist id="ddlColumns" runat="server" Width="150px"></asp:dropdownlist><asp:label id="Label2" runat="server">中</asp:label><asp:dropdownlist id="ddlConditions" runat="server" Width="80px"></asp:dropdownlist><asp:textbox id="txtSearchValue" runat="server" Width="120px"></asp:textbox>
						&nbsp;&nbsp;<asp:linkbutton id="lbtnSearch" runat="server">开始查询</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnCancelSearch" runat="server">取消查询</asp:linkbutton>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<Pager:CmsPager id="Cmspager1" runat="server"></Pager:CmsPager></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<asp:DataGrid id="dgEmployee" runat="server" DataKeyField="emp_id">
							<PagerStyle Visible="False"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>


<script language="javascript">

function checkAllCheckBox(obj)
{ 
	var o=obj.parentNode.parentNode.parentNode;
	var selectRecIds = "";
	var strPre = "";	 
	for (var k=1;k<o.children.length;k++){
		var j = k+2;
		var chk = document.getElementById("dgEmployee__ctl" + j + "_cbx"); 
		if(chk!=null)
		{
			chk.checked = obj.checked;
			
			if(obj.checked)
			{
				selectRecIds =  selectRecIds + o.children[k].RECID + ",";
			}
		}
	}
	Form1.selectRecId1.value = selectRecIds; 
}

function showControls(obj)
{
    if(obj.USERCODE=="admin" || obj.USERCODE=="security" || obj.USERCODE=="sysuser")
    {  
        document.getElementById("lnkSetPassword").disabled=true;
        document.getElementById("lnkEdit").disabled=true;
        document.getElementById("lnkDelete").disabled=true;
        document.getElementById("lbtnChangeDep").disabled=true;
        document.getElementById("lbtnClearPassword").disabled=true;
        
    }
    else
    { 
        document.getElementById("lnkSetPassword").disabled=false;
        document.getElementById("lnkEdit").disabled=false;
        document.getElementById("lnkDelete").disabled=false;
        document.getElementById("lbtnChangeDep").disabled=false;
        document.getElementById("lbtnClearPassword").disabled=false;
    }
}




function clickCheckBox(obj)
{
	var strRecIds = "";
	var strPre = "";
	var o=obj.parentNode.parentNode;
	var checkRecId = "" 
        strRecIds = Form1.selectRecId1.value;
		checkRecId =  o.RECID;
	
	if(obj.checked)
	{		
		strRecIds =  strRecIds + checkRecId + ",";
	}
	else
	{
		strRecIds = strRecIds.replace(checkRecId + ",","");
		var chk = document.getElementById("dgEmployee__ctl2_cbx");		
		if(chk!=null)
			chk.checked =false;
	} 
Form1.selectRecId1.value = strRecIds; 

//RowLeftClickNoPost(obj)
	//event.cancelBubble = true;
}
IsShowCheckbox("dgEmployee");
</script>