<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RightSetFrm.aspx.vb" Inherits="cmsrights_RightSetFrm" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
	<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" runat="server">
    <div style="margin-top:30px;">
    <asp:Button ID="btnSave" runat="server" Text="保存权限" />&nbsp;<asp:Button ID="btnExit" runat="server" Text="退出" style="display:none;"/>
    <TABLE id="Table2" style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid" cellSpacing="0" cellPadding="0" width="312" border="0">
		<TR>
			<TD height="22"><A id="checkAll" onclick="return CheckAllRights();" href="#">选中所有权限选项</A>&nbsp;&nbsp;<A id="chkUnCheckAll" onclick="return UncheckAllRights();" href="#">清除所有权限选项</A>
			</TD>
		</TR>
		<TR>
			<TD bgColor="#e7ebef" height="22"><STRONG>记录和文档操作权限</STRONG></TD>
		</TR>
		<TR>
			<TD>
				<asp:checkbox id="chkRecView" runat="server" Text="浏览记录/文档"></asp:checkbox>
				<asp:checkbox id="chkRecAdd" runat="server" Text="增加记录/文档"></asp:checkbox>
				<asp:checkbox id="chkRecEdit" runat="server" Text="修改记录/文档"></asp:checkbox><BR>
				<asp:checkbox id="chkRecDel" runat="server" Text="删除记录/文档"></asp:checkbox>
				<asp:checkbox id="chkRecPrint" runat="server" Text="打印记录"></asp:checkbox>
				<asp:checkbox id="chkRecPrintList" runat="server" Text="打印列表"></asp:checkbox><BR>
				<asp:checkbox id="chkResEmailSmsNotify" runat="server" Text="发送邮件短信"></asp:checkbox>
				<asp:checkbox id="chkResBatchUpdateField" runat="server" Text="批量更新字段"></asp:checkbox>
				<asp:checkbox id="chkRecBatchUpdateRecords" runat="server" Text="批量更新记录"></asp:checkbox><BR>
				<asp:checkbox id="chkRecSearchMultitableList" runat="server" Text="列表统计"></asp:checkbox>
			</TD>
		</TR>
		<TR>
			<TD height="12"></TD>
		</TR>
		<TR>
			<TD bgColor="#e7ebef" height="22"><STRONG>文档专用操作权限</STRONG></TD>
		</TR>
		<TR height="22">
			<TD>
				<asp:checkbox id="chkDocCheckin" runat="server" Text="文档签入签出"></asp:checkbox>
				<asp:checkbox id="chkDocCheckoutCancel" runat="server" Text="取消签出状态"></asp:checkbox>
				<asp:checkbox id="chkDocGet" runat="server" Text="提取文档"></asp:checkbox><BR>
				<asp:checkbox id="chkDocViewHistory" runat="server" Text="查阅历史版本"></asp:checkbox>
				<asp:checkbox id="chkDocViewOnline" runat="server" Text="在线浏览文档"></asp:checkbox>
				<asp:checkbox id="chkDocShare" runat="server" Text="共享文档"></asp:checkbox><BR>
				<asp:checkbox id="chkDocPrint" runat="server" Text="在线打印文档（仅Office文档）"></asp:checkbox>
			</TD>
		</TR>
		<TR>
			<TD height="12"></TD>
		</TR>
		<TR>
			<TD bgColor="#e7ebef" height="22"><STRONG>管理类操作权限</STRONG></TD>
		</TR>
		<TR height="22">
			<TD height="33"><asp:checkbox id="chkMgrRightsSet" runat="server" Text="资源权限设置"></asp:checkbox><asp:checkbox id="chkMgrColumnSet" runat="server" Text="资源字段设置"></asp:checkbox><asp:checkbox id="chkMgrColumnShowSet" runat="server" Text="资源显示设置"></asp:checkbox><FONT face="宋体"><BR>
				</FONT>
				<asp:checkbox id="chkMgrRelatedTable" runat="server" Text="关联表单设置"></asp:checkbox><asp:checkbox id="chkMgrRowColor" runat="server" Text="记录颜色设置"></asp:checkbox><asp:checkbox id="chkFormula" runat="server" Text="计算公式设置"></asp:checkbox><FONT face="宋体"><BR>
				</FONT>
				<asp:checkbox id="chkMgrInputFormDesign" runat="server" Text="输入窗体设计"></asp:checkbox><asp:checkbox id="chkMgrPrintFormDesign" runat="server" Text="打印窗体设计"></asp:checkbox><BR>
				<asp:checkbox id="chkResExport" runat="server" Text="导出资源数据"></asp:checkbox><asp:checkbox id="chkResImport" runat="server" Text="导入资源数据"></asp:checkbox><FONT face="宋体"><BR>
					<asp:checkbox id="chkResAdd" runat="server" Text="增加资源"></asp:checkbox><asp:checkbox id="chkResEdit" runat="server" Text="修改资源"></asp:checkbox><asp:checkbox id="chkResDel" runat="server" Text="删除资源"></asp:checkbox></FONT></TD>
		</TR>
		<TR>
			<TD height="12"></TD>
		</TR>
		<TR>
			<TD bgColor="#e7ebef" height="22"><STRONG>其它权限和功能</STRONG></TD>
		</TR>
		<TR height="22">
			<TD>
				<asp:LinkButton id="lbtnColumn" runat="server">列字段权限</asp:LinkButton>&nbsp;&nbsp;
				<asp:LinkButton id="lbtnRow" runat="server">行记录权限</asp:LinkButton>&nbsp;&nbsp;
				<asp:LinkButton id="lbtnRowFilter" runat="server">个人行过滤</asp:LinkButton>&nbsp;&nbsp;
				<asp:LinkButton id="lbtnMenu" runat="server">定制菜单权限</asp:LinkButton>
			</TD>
		</TR>
	</TABLE>
    </div>
    </form>
</body>
</html>
<script language="javascript">

//全选所有权限
function CheckAllRights(){
	Form1.chkRecView.checked = true;
	Form1.chkRecAdd.checked = true;
	Form1.chkRecEdit.checked = true;
	Form1.chkRecDel.checked = true;
	Form1.chkRecPrint.checked = true;
	Form1.chkRecPrintList.checked = true;
	<%If CmsFunc.IsEnable("FUNC_TABLETYPE_DOC") Then%>
		Form1.chkDocCheckin.checked = true;
		Form1.chkDocCheckoutCancel.checked = true;
		Form1.chkDocGet.checked = true;
		<%If CmsFunc.IsEnable("FUNC_ONLINEVIEW") Then%>
			Form1.chkDocViewOnline.checked = true;
		<%End If%>
		<%If CmsFunc.IsEnable("FUNC_ONLINEPRINT") Then%>
			Form1.chkDocPrint.checked = true;
		<%End If%>
		<%If CmsFunc.IsEnable("FUNC_DOCHISTORY") Then%>
			Form1.chkDocViewHistory.checked = true;
		<%End If%>
		Form1.chkDocShare.checked = true;
	<%End If%>
	Form1.chkMgrRightsSet.checked = true;
	<%If CmsFunc.IsEnable("FUNC_COLUMN_SET") Then%>
		Form1.chkMgrColumnSet.checked = true;
	<%End If%>
	<%If CmsFunc.IsEnable("FUNC_COLUMNSHOW_SET") Then%>
		Form1.chkMgrColumnShowSet.checked = true;
	<%End If%>
	Form1.chkMgrInputFormDesign.checked = true;
	Form1.chkMgrPrintFormDesign.checked = true;
	<%If CmsFunc.IsEnable("FUNC_RELATION_TABLE") Then%>
		Form1.chkMgrRelatedTable.checked = true;
	<%End If%>
	<%If CmsFunc.IsEnable("FUNC_RES_ROWCOLOR") Then%>
		Form1.chkMgrRowColor.checked = true;
	<%End If%>
	<%If CmsFunc.IsEnable("FUNC_FORMULA") Then%>
		Form1.chkFormula.checked = true;
	<%End If%>
	<%If CmsFunc.IsEnable("FUNC_RES_IMP_OTHERTABLE") Then%>
		Form1.chkResImport.checked = true;
	<%End If%>
	<%If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") Then%>
		Form1.chkResExport.checked = true;
	<%End If%>
	<%If CmsFunc.IsEnable("FUNC_COMM_EMAILPHONE") Then%>
		Form1.chkResEmailSmsNotify.checked = true;
	<%End If%>
	Form1.chkResBatchUpdateField.checked = true;
	Form1.chkRecBatchUpdateRecords.checked = true;
	
	<%If CmsFunc.IsEnable("FUNC_RESEDIT_RIGHTS") Then%>
	  Form1.chkResAdd.checked = true;
	  Form1.chkResEdit.checked = true;
	  Form1.chkResDel.checked = true;
	<%End If%>
	Form1.chkRecSearchMultitableList.checked=true;
	return false;
}

//清除所有权限的选项
function UncheckAllRights(){
	Form1.chkRecView.checked = false;
	Form1.chkRecAdd.checked = false;
	Form1.chkRecEdit.checked = false;
	Form1.chkRecDel.checked = false;
	Form1.chkRecPrint.checked = false;
	Form1.chkRecPrintList.checked = false;
	Form1.chkDocCheckin.checked = false;
	Form1.chkDocCheckoutCancel.checked = false;
	Form1.chkDocGet.checked = false;
	Form1.chkDocViewOnline.checked = false;
	Form1.chkDocPrint.checked = false;
	Form1.chkDocViewHistory.checked = false;
	Form1.chkDocShare.checked = false;
	
	Form1.chkMgrRightsSet.checked = false;
	Form1.chkMgrColumnSet.checked = false;
	Form1.chkMgrColumnShowSet.checked = false;
	Form1.chkMgrInputFormDesign.checked = false;
	Form1.chkMgrPrintFormDesign.checked = false;
	Form1.chkMgrRelatedTable.checked = false;
	Form1.chkMgrRowColor.checked = false;
	Form1.chkFormula.checked = false;
	Form1.chkResExport.checked = false;
	Form1.chkResImport.checked = false;
	Form1.chkResEmailSmsNotify.checked = false;
	Form1.chkResBatchUpdateField.checked = false;
	Form1.chkRecBatchUpdateRecords.checked = false;
	
	Form1.chkResAdd.checked = false;
	Form1.chkResEdit.checked = false;
	Form1.chkResDel.checked = false;
	Form1.chkRecSearchMultitableList.checked=false;
	return false;
}
</script>