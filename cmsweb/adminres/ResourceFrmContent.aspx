<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceFrmContent" CodeFile="ResourceFrmContent.aspx.vb" %>

<HTML>
	<HEAD>
		<TITLE id=onetidTitle>资源管理</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<LINK href="../css/cmstree.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../script/CmsScript.js"></SCRIPT>
		<script language="JavaScript" src="../script/CmsTreeview.js"></script>
		<SCRIPT language="JavaScript" src="../script/Valid.js"></SCRIPT>
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server"> 
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td vAlign="top" colSpan="2" height="30">
						<TABLE class="toolbar_table" border="0" width="100%" cellpadding="0" cellspacing="2">
							<TR height="20">
								<TD noWrap align="left" width="105">
									<IMG src="../images/titleicon/add.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnAddResource" runat="server">创建资源</asp:linkbutton>
								</TD>
								<TD noWrap align="left" width="105">
									<IMG src="../images/titleicon/modify2.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnEditResource" runat="server">修改资源</asp:linkbutton>
								</TD>
								<TD noWrap align="left" width="105">
									<IMG src="../images/titleicon/delete.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnDelResource" runat="server">删除资源</asp:linkbutton>
								</TD>
								<TD noWrap align="left" width="105"><IMG src="../images/titleicon/creat.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnCreateHostTable" runat="server">创建表单</asp:linkbutton></TD>
								<TD noWrap align="left" width="105">
									<IMG src="../images/titleicon/field.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnSetHostTable" runat="server">字段设置</asp:linkbutton></TD>
								<TD noWrap align="left" width="105">
									<IMG src="../images/titleicon/table.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnSetHostTableShow" runat="server">显示设置</asp:linkbutton></TD>
								<TD noWrap align="left" width="105">
									<IMG src="../images/titleicon/key1.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnSetRights" runat="server">权限设置</asp:linkbutton>
								</TD>
							</TR>
							<TR height="20">
								<TD noWrap align="left">
									<IMG src="../images/tree/res_twod.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnInputformDesign" runat="server">输入窗体设计</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/print.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnPrintForm" runat="server">打印窗体设计</asp:linkbutton>
								</TD>
								<TD noWrap align="left"><IMG src="../images/titleicon/associated2.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnRelatedTable" runat="server">关联表设置</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/creat.gif" align="absMiddle" border="0" width="16">
									<asp:linkbutton id="lbtnOrderBy" runat="server">排序设置</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/source3.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnFormula" runat="server">计算公式设置</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/email.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnCommunication" runat="server">邮箱手机字段</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/source3.gif" align="absMiddle" width="16">&nbsp;
									<asp:linkbutton id="lbtnFormRouter" runat="server">窗体条件设置</asp:linkbutton>
								</TD>
							</tr>
							<TR height="20">
								<TD noWrap align="left">
									<IMG src="../images/tree/res_workflow.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnChangeToDocFlowResource" runat="server">转换文档流</asp:linkbutton>
								</TD>
								
								<TD noWrap align="left">
									<IMG src="../images/titleicon/modify2.gif" align="absMiddle" border="0" width="16" height="16">
									<asp:linkbutton id="lbtnLogExtension" runat="server">日志扩展</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/associated2.gif" align="absMiddle" border="0" width="16" height="16">
									<asp:linkbutton id="lbtnResSync" runat="server">数据同步</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/modify2.gif" align="absMiddle" border="0" width="16" height="16">
									<asp:linkbutton id="lbtnGeneralRowFilter" runat="server">通用行过滤</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/modify2.gif" align="absMiddle" border="0" width="16" height="16">
									<asp:linkbutton id="lbtnMTableStatistic" runat="server">列表统计</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/source3.gif" align="absMiddle" width="16" height="16">&nbsp;
									<asp:linkbutton id="lbtnRowColor" runat="server">行颜色设置</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/source3.gif" align="absMiddle" width="16" height="16">&nbsp;
									<asp:linkbutton id="lbtnCopyRes" runat="server">复制资源</asp:linkbutton>
								</TD>
							</TR>
							<TR height="20">
								<TD noWrap align="left">
									<IMG src="../images/titleicon/window.gif" align="absMiddle" width="16" height="16">&nbsp;
									<asp:linkbutton id="lbtnImportRes" runat="server">导入资源</asp:linkbutton>
								</TD>
								<TD noWrap align="left">
									<IMG src="../images/titleicon/window.gif" align="absMiddle" width="16" height="16">&nbsp;
									<asp:linkbutton id="lbtnExportRes" runat="server">导出资源</asp:linkbutton>
								</TD>
								<TD noWrap align="left"><IMG src="../images/titleicon/modify2.gif" align="absMiddle" border="0" width="16" height="16">
									<asp:linkbutton id="lbtnViewCondition" runat="server">视图条件设置</asp:linkbutton></TD>
								<TD noWrap align="left"><IMG src="../images/titleicon/modify2.gif" align="absMiddle" border="0" width="16" height="16">
									<asp:linkbutton id="lbtnCreateView" runat="server">创建视图</asp:linkbutton></TD>
								<TD noWrap align="left"></TD>
								<TD noWrap align="left"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR height="100%">
					<TD vAlign="top" width="287">
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="280" align="left" border="0">
							<TR>
								<TD class="header_level3" height="22">资源列表&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:linkbutton id="lbtnMoveRes" runat="server">移动</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveup" runat="server">上移</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnMovedown" runat="server">下移</asp:linkbutton></TD>
							</TR>
							<TR vAlign="top">
								<td>
								<asp:Panel id="panelTree" style="OVERFLOW: auto" Height="577px" runat="server"><%ResourceFrmContent.LoadTreeView(CmsPass, Request, Response, ViewState)%></asp:Panel>
								</td>
							</TR>
						</TABLE>
					</TD>
					<TD align="left" vAlign="top">
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD class="header_level3" colSpan="2" height="22"><B>资源属性设置－通用类&nbsp;</B></TD>
							</TR>
							<TR>
								<TD width="120" height="22">显示资源</TD>
								<TD height="22"><asp:checkbox id="chkShowResource" runat="server" Text="在内容管理的资源树结构中显示本资源"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">关联表显示</TD>
								<TD height="22"><asp:checkbox id="chkShowInRel" runat="server" Text="在内容管理的关联表中显示本资源"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">子关联表显示</TD>
								<TD height="22"><asp:checkbox id="chkNotShowRelTables" runat="server" Text="不显示当前资源的子关联资源"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">显示子资源数据</TD>
								<TD height="22"><asp:checkbox id="chkShowChildResData" runat="server" Text="在本资源数据列表中显示所有继承型子资源的数据"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">显示设置</TD>
								<TD height="22"><asp:checkbox id="chkUseParentShow" runat="server" Text="使用父资源的显示设置(仅适用于继承型子资源)"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">记录创建显示</TD>
								<TD height="22"><asp:checkbox id="chkShowCRT" runat="server" Text="在资源数据表单中显示记录创建人和创建时间"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">记录修改显示</TD>
								<TD height="22"><asp:checkbox id="chkShowEDT" runat="server" Text="在资源数据表单中显示记录最后修改人和修改时间"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">记录显示Checkbox</TD>
								<TD height="22"><asp:checkbox id="chkShowCheckBox" runat="server" Text="在资源数据表单中为每条记录显示CheckBox"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">记录显示条数</TD>
								<TD height="22" title="在资源数据表单中本资源每页显示条数"><asp:TextBox id="txt_RecCount" runat="server" Width="124px" fType="num" errmsg="记录显示条数"></asp:TextBox>
									<asp:Label id="Label2" runat="server">条</asp:Label></TD>
							</TR>
							<TR>
								<TD width="120" height="22">
									<asp:Label id="Label4" runat="server">递归计算关系</asp:Label></TD>
								<TD height="22">
									<asp:CheckBox id="chkRecursiveFormula" runat="server" Text="本资源与至少一个子资源有递归计算关系"></asp:CheckBox></TD>
							</TR>
							<% If CmsConfig.GetInt("SYS_CONFIG", "RECORD_SAVEHISTORY") = 1 then %>
							<TR>
								<TD width="120" height="22">
									<asp:Label id="Label1" runat="server">保存历史记录</asp:Label></TD>
								<TD height="22">
									<asp:CheckBox id="chkSaveHis" runat="server" Text="本资源中所有记录每一次修改都会被保存"></asp:CheckBox></TD>
							</TR>
							<% end if%>
							<TR>
								<TD width="120" height="22">工作流</TD>
								<TD height="22"><asp:checkbox id="chkIsWorkflowRes" runat="server" Text="是工作流资源"></asp:checkbox><FONT face="宋体">&nbsp;
									</FONT>
									<asp:checkbox id="chkFlowShowFiledOnly" runat="server" Text="仅显示归档数据"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">
									<asp:Label id="Label5" runat="server">记录数量</asp:Label></TD>
								<TD height="22">
									<asp:TextBox id="txtRecNum" runat="server" Width="124px"></asp:TextBox>
									<asp:Label id="Label6" runat="server">条</asp:Label></TD>
							</TR>
							<TR>
								<TD width="120" height="22" valign="top">
									<asp:Label id="lblResComments" runat="server">资源说明信息</asp:Label></TD>
								<TD height="22">
									<asp:TextBox id="txtResComments" runat="server" Width="323px" TextMode="MultiLine" Height="36px"></asp:TextBox></TD>
							</TR>
							<tr>
							    <td><asp:Label ID="lblResServerUrl" runat="server">资源附件路径</asp:Label></td>
							    <td><asp:TextBox id="txtResServerUrl" runat="server" Width="323px" ></asp:TextBox></td>
							</tr>
							<tr>
							    <td><asp:Label ID="lblEMPTYRESOURCEURL" runat="server">空资源链接路径</asp:Label></td>
							    <td><asp:TextBox id="txtEMPTYRESOURCEURL" runat="server" Width="323px" ></asp:TextBox></td>
							</tr>
							<tr>
							    <td><asp:Label ID="lblEMPTYRESOURCETARGET" runat="server">空资源链接打开方式</asp:Label></td>
							    <td>
							        <table cellpadding="0" cellspacing="0" border="0" width="100%">
							            <tr>
							                <td> 
							                    
							                    <asp:CheckBoxList ID="chkEMPTYRESOURCETARGET" runat="server" RepeatDirection="horizontal">
							                        <asp:ListItem Value="_blank" onclick="SelectedTarget(this);"></asp:ListItem>
							                        <asp:ListItem Value="_parent" onclick="SelectedTarget(this);"></asp:ListItem>
							                        <asp:ListItem Value="_search" onclick="SelectedTarget(this);"></asp:ListItem>
							                        <asp:ListItem Value="_self" onclick="SelectedTarget(this);"></asp:ListItem>
							                        <asp:ListItem Value="_top" onclick="SelectedTarget(this);"></asp:ListItem>
							                    </asp:CheckBoxList>
							                </td>
							                <td>
							                    <asp:TextBox ID="txtEMPTYRESOURCETARGET" runat="server" Width="50px"></asp:TextBox>
							                </td>
							            </tr>
							        </table> 
							    </td>
							</tr>
							
							<TR>
								<TD width="120" height="25"></TD>
								<TD height="25"><asp:button id="btnSaveResAttr" runat="server" Text="保存设置" Width="80px"></asp:button><asp:Button id="btnDelAllData" runat="server" Text="删除数据" Width="80px"></asp:Button><asp:Button id="btnDelDataKeep10Day" runat="server" Text="删除10天前" Width="80px"></asp:Button>
									<asp:Button id="btnDelDataKeep30Day" runat="server" Text="删除30天前" Width="80px"></asp:Button></TD>
							</TR>
						</TABLE>
						<BR> 
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" align="center">
							<TR>
								<TD class="header_level3" colSpan="2" height="22"><B>资源属性设置－文档管理类</B></TD>
							</TR>
							<TR>
								<TD width="120" height="22">历史版本管理</TD>
								<TD height="22"><asp:checkbox id="chkEnableVerCtrl" runat="server" Text="自动备份文档修改过的每一个版本"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22">全文检索</TD>
								<TD height="22"><asp:checkbox id="chkFTSearchOn" runat="server" Text="支持智能全文检索"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD width="120" height="22"></TD>
								<TD height="22"><asp:button id="btnSaveDocResAttr" runat="server" Text="保存设置" Width="80px"></asp:button></TD>
							</TR>
						</TABLE>
						<br>
						<table class="table_level2" cellSpacing="0" cellPadding="0" width="100%" align="center">
							<tr>
								<TD class="header_level3" align="left" colSpan="2" height="22"><b>资源属性设置－机密等级</b></TD>
							</tr>
							<tr>
								<TD noWrap align="left" width="120" height="25">机密等级</TD>
								<TD noWrap align="left" width="340" height="25"><asp:dropdownlist id="ddlSecurity" runat="server" Width="100px"></asp:dropdownlist></TD>
							</tr>
							<TR>
								<TD noWrap align="left" width="120" height="25">系统安全员密码</TD>
								<TD noWrap align="left" width="340" height="25"><asp:textbox id="txtSecurityPass" runat="server" Width="100px" TextMode="Password"></asp:textbox><asp:label id="Label3" runat="server">（降低机密等级时需要）</asp:label></TD>
							</TR>
							<TR>
								<TD noWrap align="left" width="120" height="22"></TD>
								<TD noWrap align="left" width="340" height="22"><asp:button id="btnSaveSecurity" runat="server" Text="保存设置" Width="80px"></asp:button></TD>
							</TR>
						</table>
					</TD>
					<TD align="left" vAlign="top" width="100%"></TD>
				</TR>
			</table>
		</FORM>
	</BODY>
</HTML>

<script language="javascript">
function SelectedTarget(obj)
{
    var strChecked=obj.checked;
    var inputList=document.getElementsByTagName("input");
    for(var i=0;i<inputList.length;i++)
    {
        if(inputList[i].id.indexOf("chkEMPTYRESOURCETARGET")>=0 && inputList[i].type=="checkbox")
        {
           inputList[i].checked=false; 
        }
    }
    obj.checked=strChecked; 
    
}
</script>