<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DepEmpList" CodeFile="DepEmpList.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">部门人员列表</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
			<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
		 <input type="hidden" name="selectRecId1">
			<TABLE cellSpacing="2" cellPadding="2" height="500" width="700" border="0">
				<TR vAlign="top">
					<TD width="220" height="25">
						<asp:Button id="btnChooseDep" runat="server" Width="80px" Text="选取部门"></asp:Button><asp:Button id="btnChooseEnterprise" runat="server" Width="80px" Text="选取企业"></asp:Button>
					</TD>
					<TD vAlign="top" height="25">
						<asp:Button id="btnChooseEmp" runat="server" Width="80px" Text="选取人员"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button>
						<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" />
					</TD>
				</TR>
				
				<TR vAlign="top">
					<TD width="220" height="500">
						<asp:Panel id="panelTree" style="OVERFLOW: auto" runat="server" Width="100%" Height="100%"
							BorderColor="#C4D9F9" BorderWidth="1px">
							<%Dim strNoDep As String = RStr("nodep")%>
							<%WebTreeDepartment.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/adminsys/DepEmpList.aspx?nodep=" & strNoDep & (iif(Request("type") isnot nothing,"&type=Virtual","")).ToString(), "_self",unionsoft.Platform .AspPage.RStr("depid", Request),CmsPass.Employee.ID,,False)%>
						</asp:Panel>
					</TD>
					<TD width="480" height="500">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<TD align="left">
									<Pager:CmsPager id="Cmspager1" runat="server"></Pager:CmsPager>
								</TD>
							</tr>
							<tr>
								<TD height="2"></TD>
							</tr>
							<tr>
								<TD vAlign="top" height="100%">
									<asp:DataGrid id="DataGrid1" runat="server">
										<PagerStyle Visible="False"></PagerStyle>
									</asp:DataGrid>
								</TD>
							</tr>
						</table>
					</TD>
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
		var chk = document.getElementById("DataGrid1__ctl" + j + "_cbx"); 
		if(chk!=null && chk.style.display!="none")
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
		var chk = document.getElementById("DataGrid1__ctl2_cbx");		
		if(chk!=null)
			chk.checked =false;
	} 
Form1.selectRecId1.value = strRecIds; 
//alert(strRecIds+"----"+Form1.selectRecId1.value);
//RowLeftClickNoPost(obj)
	event.cancelBubble = true;
}


IsShowCheckbox("DataGrid1");
</script>