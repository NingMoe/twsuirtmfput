<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RightsSet" CodeFile="RightsSet.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE 
id=onetidTitle>RightsSet</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
			<script language="javascript">
<!--
var ctrlKeyClicked = false;
document.onkeydown=EvtKeyDown //������̵�����¼����
document.onkeyup=EvtKeyUp //������̵�����¼����
//���̻������¼���Ӧ���
function EvtKeyDown(){
	if (event.keyCode == 17){ //����ctrl��
		ctrlKeyClicked=true;
	}
}
//���̻������¼���Ӧ���
function EvtKeyUp(){
	if (event.keyCode == 17){ //����ctrl��
		ctrlKeyClicked=false;
	}
}

function RowLeftClickPost(src){
	if (ctrlKeyClicked == true){
		//��ѡ��¼
		src.bgColor = "#C4D9F9";
		Form1.RECID.value = Form1.RECID.value + "," + src.RECID; //��Ҫ���û�ѡ����к�POST��������
	}else{
		//�����ύ
		Form1.qxaction.value = "qxrowclicked"; //��Ҫ���û�ѡ����к�POST��������
		Form1.RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
		Form1.submit();
	}
}

//ȫѡ����Ȩ��
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

//�������Ȩ�޵�ѡ��
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
//-->
			</script>
</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="/cmsweb/cmsrights/RightsSet.aspx" method="post"
			runat="server">
			<input type="hidden" name="qxaction"> <input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td width="4"></td>
						<td>
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD class="header_level2" width="437" height="22"><b>
											<asp:label id="lblResDispName" runat="server"></asp:label></b></TD>
									<TD class="header_level2" height="22"></TD>
								</TR>
								<TR height="22">
									<td width="437" height="5"></td>
									<TD height="5"></TD>
								</TR>
								<TR height="22">
									<td vAlign="top" width="437">
										<table style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid"
											cellSpacing="0" cellPadding="0" width="432" border="0">
											<TR>
												<TD vAlign="top" width="100%"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></TD>
											</TR>
											<TR>
												<TD vAlign="top" width="100%" height="12"><FONT face="����"></FONT></TD>
											</TR>
											<TR>
												<TD vAlign="top" width="100%"><FONT face="����"><asp:button id="btnDeleteAllRights" runat="server" Text="ɾ������Ȩ��" Width="96px"></asp:button><asp:button id="btnDeleteSubResRights" runat="server" Text="ɾ������Դ����Ȩ��" Width="128px"></asp:button><asp:button id="btnDelUserRights" runat="server" Text="ɾ���û�����Ȩ��" Width="120px"></asp:button></FONT></TD>
											</TR>
										</table>
									</td>
									<TD vAlign="top">
										<TABLE id="Table1" style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid"
											cellSpacing="0" cellPadding="0" width="320" border="0">
											<TR>
												<TD vAlign="top" height="16"><asp:button id="btnAddRights" runat="server" Text="���Ȩ��" Width="80px"></asp:button><asp:button id="btnSaveRights" runat="server" Text="����Ȩ��" Width="80px" DESIGNTIMEDRAGDROP="34"></asp:button><asp:button id="btnExit" runat="server" Text="�˳�" Width="80px" DESIGNTIMEDRAGDROP="35"></asp:button></TD>
											</TR>
											<TR>
												<TD vAlign="top" height="16"></TD>
											</TR>
											<TR height="22">
												<TD vAlign="top">
													<TABLE id="Table2" style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid"
														cellSpacing="0" cellPadding="0" width="312" border="0">
														<TR>
															<TD height="22"><A id="checkAll" onclick="return CheckAllRights();" href="#">ѡ������Ȩ��ѡ��</A>&nbsp;&nbsp;<A id="chkUnCheckAll" onclick="return UncheckAllRights();" href="#">�������Ȩ��ѡ��</A>
															</TD>
														</TR>
														<TR>
															<TD bgColor="#e7ebef" height="22"><STRONG>��¼���ĵ�����Ȩ��</STRONG></TD>
														</TR>
														<TR>
															<TD>
																<asp:checkbox id="chkRecView" runat="server" Text="�����¼/�ĵ�"></asp:checkbox>
																<asp:checkbox id="chkRecAdd" runat="server" Text="���Ӽ�¼/�ĵ�"></asp:checkbox>
																<asp:checkbox id="chkRecEdit" runat="server" Text="�޸ļ�¼/�ĵ�"></asp:checkbox><BR>
																<asp:checkbox id="chkRecDel" runat="server" Text="ɾ����¼/�ĵ�"></asp:checkbox>
																<asp:checkbox id="chkRecPrint" runat="server" Text="��ӡ��¼"></asp:checkbox>
																<asp:checkbox id="chkRecPrintList" runat="server" Text="��ӡ�б�"></asp:checkbox><BR>
																<asp:checkbox id="chkResEmailSmsNotify" runat="server" Text="�����ʼ�����"></asp:checkbox>
																<asp:checkbox id="chkResBatchUpdateField" runat="server" Text="���������ֶ�"></asp:checkbox>
																<asp:checkbox id="chkRecBatchUpdateRecords" runat="server" Text="�������¼�¼"></asp:checkbox><BR>
																<asp:checkbox id="chkRecSearchMultitableList" runat="server" Text="�б�ͳ��"></asp:checkbox>
															</TD>
														</TR>
														<TR>
															<TD height="12"></TD>
														</TR>
														<TR>
															<TD bgColor="#e7ebef" height="22"><STRONG>�ĵ�ר�ò���Ȩ��</STRONG></TD>
														</TR>
														<TR height="22">
															<TD>
																<asp:checkbox id="chkDocCheckin" runat="server" Text="�ĵ�ǩ��ǩ��"></asp:checkbox>
																<asp:checkbox id="chkDocCheckoutCancel" runat="server" Text="ȡ��ǩ��״̬"></asp:checkbox>
																<asp:checkbox id="chkDocGet" runat="server" Text="��ȡ�ĵ�"></asp:checkbox><BR>
																<asp:checkbox id="chkDocViewHistory" runat="server" Text="������ʷ�汾"></asp:checkbox>
																<asp:checkbox id="chkDocViewOnline" runat="server" Text="��������ĵ�"></asp:checkbox>
																<asp:checkbox id="chkDocShare" runat="server" Text="�����ĵ�"></asp:checkbox><BR>
																<asp:checkbox id="chkDocPrint" runat="server" Text="���ߴ�ӡ�ĵ�����Office�ĵ���"></asp:checkbox>
															</TD>
														</TR>
														<TR>
															<TD height="12"></TD>
														</TR>
														<TR>
															<TD bgColor="#e7ebef" height="22"><STRONG>���������Ȩ��</STRONG></TD>
														</TR>
														<TR height="22">
															<TD height="33"><asp:checkbox id="chkMgrRightsSet" runat="server" Text="��ԴȨ������"></asp:checkbox><asp:checkbox id="chkMgrColumnSet" runat="server" Text="��Դ�ֶ�����"></asp:checkbox><asp:checkbox id="chkMgrColumnShowSet" runat="server" Text="��Դ��ʾ����"></asp:checkbox><FONT face="����"><BR>
																</FONT>
																<asp:checkbox id="chkMgrRelatedTable" runat="server" Text="����������"></asp:checkbox><asp:checkbox id="chkMgrRowColor" runat="server" Text="��¼��ɫ����"></asp:checkbox><asp:checkbox id="chkFormula" runat="server" Text="���㹫ʽ����"></asp:checkbox><FONT face="����"><BR>
																</FONT>
																<asp:checkbox id="chkMgrInputFormDesign" runat="server" Text="���봰�����"></asp:checkbox><asp:checkbox id="chkMgrPrintFormDesign" runat="server" Text="��ӡ�������"></asp:checkbox><BR>
																<asp:checkbox id="chkResExport" runat="server" Text="������Դ����"></asp:checkbox><asp:checkbox id="chkResImport" runat="server" Text="������Դ����"></asp:checkbox><FONT face="����"><BR>
																	<asp:checkbox id="chkResAdd" runat="server" Text="������Դ"></asp:checkbox><asp:checkbox id="chkResEdit" runat="server" Text="�޸���Դ"></asp:checkbox><asp:checkbox id="chkResDel" runat="server" Text="ɾ����Դ"></asp:checkbox></FONT></TD>
														</TR>
														<TR>
															<TD height="12"></TD>
														</TR>
														<TR>
															<TD bgColor="#e7ebef" height="22"><STRONG>����Ȩ�޺͹���</STRONG></TD>
														</TR>
														<TR height="22">
															<TD>
																<asp:LinkButton id="lbtnColumn" runat="server">���ֶ�Ȩ��</asp:LinkButton>&nbsp;&nbsp;
																<asp:LinkButton id="lbtnRow" runat="server">�м�¼Ȩ��</asp:LinkButton>&nbsp;&nbsp;
																<asp:LinkButton id="lbtnRowFilter" runat="server">�����й���</asp:LinkButton>&nbsp;&nbsp;
																<asp:LinkButton id="lbtnMenu" runat="server">���Ʋ˵�Ȩ��</asp:LinkButton>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
		</FORM></TD></TR></TBODY></TABLE>
	</BODY>
</HTML>
