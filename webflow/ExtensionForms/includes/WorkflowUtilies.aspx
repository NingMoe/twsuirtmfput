
<script type="text/javascript" language="JavaScript" src="../scripts/Wo_Modal.js"></script>
<style>
@media print {.noprint {display:none;}}
@media screen {.notprint {display:block;cursor:hand;}}
</style>
<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="Unionsoft.Workflow.Platform"%>
<%@ Import Namespace="Unionsoft.Workflow.Items"%>
<%@ Import Namespace="Unionsoft.Workflow.Engine"%>
<%@ Import Namespace="NetReusables"%>
<Script language="vb" runat="server">
'-----------------------------------------------------------------------
'生成命令栏
    '-----------------------------------------------------------------------
    
    Sub GenerateCommand(ByVal WorkflowId As Long, ByVal CommandScript As String)
        
        If Request("action") = "view" Then Response.Redirect("process.aspx?WorkflowInstId=" & Request("WorkflowInstId") & "&WorklistItemId=" & Request("WorklistItemId"))
	
        Dim oWorkflowInstance As WorkflowInstance
        oWorkflowInstance = WorkflowFactory.CreateInstance(WorkflowId.ToString, CurrentUser)   '当前的工作流对象
        Dim oActions As ActionCollection = oWorkflowInstance.WorkflowTemplate.StartNode.Actions

        Response.Write("<fieldset style=""background:menu;"">")
        Response.Write("<table cellspacing=""0"" cellpadding=""0"" height=""22"" width=""100%"" align=""center"">" & vbCrLf)
        Response.Write("<tr>" & vbCrLf)
        For Each oActionItem As ActionItem In oActions
            Response.Write("<td width=90>" & vbCrLf)
            Response.Write("<input type=""submit"" name=""" & oActionItem.Key & """ value=""" & oActionItem.Name & """ style=""width:85px;"" ")
            If CommandScript <> "" Then
                Response.Write("onclick="" document.getElementById('action_value').value=this.name;" & CommandScript & """")
            Else
                Response.Write("onclick=""document.getElementById('action_value').value=this.name;""")
            End If
            Response.Write(">" & vbCrLf)
            Response.Write("</td>" & vbCrLf)
        Next
	
        Response.Write("<td><input type=""hidden"" value="""" name=""action_value"" id=""action_value"">&nbsp;</td>" & vbCrLf)
        Response.Write("</tr>" & vbCrLf)
        Response.Write("</table>" & vbCrLf)
        Response.Write("</fieldset>" & vbCrLf)
    End Sub

'-----------------------------------------------------------------------
'生成命令栏
'-----------------------------------------------------------------------
Sub GenerateActInstCommand(WorklistItemId As Long,CommandScript As String)
	Dim actionAutoProcess As String  = ""
	Dim WorkflowInstanceId As String = "0"
        Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(WorklistItemId.ToString)
	Dim oActions As ActionCollection '= CType(oWorklistItem.ActivityInstance.NodeTemplate,NodeItem).Actions
	Select Case oWorklistItem.ActivityInstance.NodeTemplate.Type
		Case NodeTypeConstants.StartNode : oActions = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions
        Case NodeTypeConstants.MiddleNode : oActions = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem).Actions
	End Select
	WorkflowInstanceId = oWorklistItem.ActivityInstance.WorkflowInstance.Key
	
	actionAutoProcess = Request.QueryString("AutoProcess")
	Response.Write("<fieldset style=""background: menu;"" class=""noprint"">" &vbCrlf)
	Response.Write("<table cellspacing=""0"" cellpadding=""0"" height=""22"" border=0 width=""100%"" align=""center"">" &vbCrlf)
	Response.Write("<tr>" &vbCrlf)
	If actionAutoProcess<>"" Then
		Dim hstFormValues As New Hashtable
		oWorklistItem.FormFieldValues = FillEmployeeAttr(oWorklistItem.ActivityInstance.WorkflowInstance.Creator.Code,hstFormValues)
		oWorklistItem.Action = oActions(CInt(actionAutoProcess)-1)
		oWorklistItem.Memo = ""
		Worklist.TransactWorklistItem(oWorklistItem,AddressOf RedirectEmployeeSelect)
	Else
            If oWorklistItem.Status = 1 Or oWorklistItem.Status = 2 Then 'And CurrentUser.Code=oWorklistItem.Transactor.Code
                For Each oActionItem As ActionItem In oActions
                    Response.Write("<td width=80>")
                    Response.Write("<input type=""submit"" name=""" & oActionItem.Key & """ value=""" & oActionItem.Name & """  style=""width:85px;"" ")
                    If CommandScript <> "" Then
                        Response.Write("onclick="" document.getElementById('action_value').value=this.name;" & CommandScript & """")
                    Else
                        Response.Write("onclick=""document.getElementById('action_value').value=this.name;""")
                    End If
                    Response.Write(">" & vbCrLf)
                    Response.Write("</td>" & vbCrLf)
                Next
            End If
	End If
	
	Response.Write("<td align=right>" &vbCrlf)

	'Response.Write("<input type=""button"" value=""  打印  "" name=""bPrint"" id=""bPrint"" onclick=""window.print();"">" &vbCrlf)

	'If CurrentUser.Code=oWorklistItem.Transactor.Code Then
	'Response.Write("<input type=""button"" value=""  转发  "" name=""bCollection"" id=""bCollection"" onclick=""Wo_Modal.Open('../opinioncollection/opinionCollection.aspx?opinionWorkflowInstanceId=" & WorkflowInstanceId & "&opinionWorklistItemId=" & WorklistItemId & "',700,250)"">" &vbCrlf)
	'End If
	Response.Write("<input type=""hidden"" value="""" name=""action_value"" id=""action_value"">" &vbCrlf)
	Response.Write("<input type=""button"" value=""查看流程"" name=""displayWorkflowDiagram"" id=""displayWorkflowDiagram"" onclick=""showWorkflowDiagram('" & oWorklistItem.ActivityInstance.WorkflowInstance.Key & "')"">" &vbCrlf)
	Response.Write("</td>" &vbCrlf)
	
	Response.Write("</tr>" &vbCrlf)
	Response.Write("</table>" &vbCrlf)
	Response.Write("</fieldset>" &vbCrlf)
End Sub

'-----------------------------------------------------------------------
'创建一个流程实例
'-----------------------------------------------------------------------
Private Function CreateWorkflowInstance(WorkflowId As Long,RecordId As Long,ActionKey As String,hstFormValues As Hashtable,Memo As String) As WorklistItem
	Dim oWorkflowInstance As WorkflowInstance
	Dim oWorklistItem As WorklistItem
	Dim oAction As ActionItem
	
        oWorkflowInstance = WorkflowFactory.CreateInstance(WorkflowId.ToString, CurrentUser)
	oAction = oWorkflowInstance.WorkflowTemplate.StartNode.Actions(ActionKey)
	oWorkflowInstance.RecordID = RecordId
	oWorklistItem = oWorkflowInstance.Create(hstFormValues)
	oWorklistItem.FormFieldValues = FillEmployeeAttr(CurrentUser.Code,hstFormValues)
	oWorklistItem.Action = oAction
	oWorklistItem.Memo = Memo
	Return oWorklistItem
End Function


'-----------------------------------------------------------------------
'启动流程任务
'-----------------------------------------------------------------------
Private Sub StartWorkflowInstance(oWorklistItem As WorklistItem)
	Session("TASK_MEMO") = oWorklistItem.Memo
	Worklist.StartWorkflowInstance(oWorklistItem,AddressOf RedirectEmployeeSelect)
	Response.Redirect("/webflow/2009/message.aspx")
	Response.End()
End Sub

'-----------------------------------------------------------------------
'处理任务
'-----------------------------------------------------------------------
Private Sub ProcessWorklistItem(ByVal oWorklistItem As WorklistItem, ActionKey As String,hstFormValues As Hashtable,Memo As String)	
	Dim node As NodeBase = oWorklistItem.ActivityInstance.NodeTemplate
	Session("TASK_MEMO") = Memo
	oWorklistItem.FormFieldValues = FillEmployeeAttr(oWorklistItem.ActivityInstance.WorkflowInstance.Creator.Code,hstFormValues)
	Select Case node.Type
		Case NodeTypeConstants.StartNode : oWorklistItem.Action = CType(node, NodeStart).Actions(ActionKey)
        Case NodeTypeConstants.MiddleNode : oWorklistItem.Action = CType(node, NodeItem).Actions(ActionKey)
	End Select
	'oWorklistItem.Action = oWorklistItem.ActivityInstance.NodeTemplate.Actions(ActionKey)
	oWorklistItem.Memo = Memo
	Worklist.TransactWorklistItem(oWorklistItem,AddressOf RedirectEmployeeSelect)
	Response.Redirect("/webflow/2009/message.aspx")
	Response.End()
End Sub

'-----------------------------------------------------------------------
'选择任务处理人
'-----------------------------------------------------------------------
Private Sub RedirectEmployeeSelect(ByVal oWorklistItem As WorklistItem, ByVal link As LinkItem)
    Response.Redirect("/webflow/EmployeeSelect.aspx?ActionId=" + oWorklistItem.Action.Key + "&WorklistItemId=" & oWorklistItem.Key & "&link=" & link.Key & "&url=2009/message.aspx", True)
    Response.End()
End Sub

'-----------------------------------------------------------------------
'获取表单数据
'-----------------------------------------------------------------------
Function GetFormValues(RecordId As Long,table As String) As Hashtable
	Dim hstFormValues As New Hashtable()
	Dim strSql As String = "SELECT * FROM " & table & " where Id=" & RecordId
	Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
	If dt.Rows.Count=1 Then
		For i As Integer = 0 To dt.Columns.Count-1
			hstFormValues.Add(dt.Columns(i).ColumnName,DbField.GetStr(dt.Rows(0),dt.Columns(i).ColumnName))
		Next
	End If
	Return hstFormValues
End Function

'记录日志
Private Sub Log(str As String)
	SDBStatement.Execute("insert into log (content) values ('" & str & "')")
End Sub

'加载附件到文件夹
Private Sub LoadAttachment(Id As Long)
	Dim path As String
	Dim dt As DataTable
	Dim strSql As String
	path = Request.PhysicalApplicationPath & "\temp\" & Id & ".doc"
	If File.Exists(path) Then File.Delete(path)
	Dim fs As FileStream = New FileStream(path, FileMode.Create)
    Dim br As BinaryWriter = New BinaryWriter(fs)
	strSql = "SELECT FileImage FROM WORKFLOW_FORM_ATTACHMENTS WHERE ID=" & Id
	dt =  SDbStatement.Query(strSql).Tables(0)
	If dt.Rows.Count > 0 Then
            br.Write(CType(DbField.GetObj(dt.Rows(0), "FileImage"), Byte))
	End If
	br.Close()
    fs.Close()
End Sub

'获取人员所在的部门
Function GetDepartment(EmployeeCode As String) As String
	Dim strSql As String = "SELECT * FROM CMS_DEPARTMENT WHERE ID=(SELECT HOST_ID FROM CMS_EMPLOYEE WHERE EMP_ID='" & EmployeeCode & "')"
	Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
	If dt.Rows.Count=1 Then
		Return DbField.GetStr(dt.Rows(0),"Name")
	Else
		Return ""
	End If
End Function

'-----------------------------------------------------------------------
'
'-----------------------------------------------------------------------
Function GetDuties(EmployeeCode As String) As String
	Return GetEmployeeAttr(EmployeeCode,"C3_337790108500")
End Function

'-----------------------------------------------------------------------
'GetEmployeeAttr("","C3_338226335937")
'-----------------------------------------------------------------------
Function GetEmployeeAttr(EmployeeCode As String,AttrName As String) As String
	Dim strSql As String = "SELECT * FROM CMS_EMPLOYEE WHERE EMP_ID='" & EmployeeCode & "'"
	Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
	If dt.Rows.Count=1 Then
		Return DbField.GetStr(dt.Rows(0),AttrName)
	Else
		Return ""
	End If
End Function

'-----------------------------------------------------------------------
'显示附件
'-----------------------------------------------------------------------
Sub DisplayAttachment(RecordId As Long)
	Dim displaybutton As Boolean = False
	Dim action As String = Request.QueryString("action")  'transtract/create
	If action = "transtract" Or action = "create" Then displaybutton = True

	Dim strSql As String = "SELECT * FROM WORKFLOW_FORM_ATTACHMENTS WHERE RecId=" & RecordId
	Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)

	Response.Write("<table width='100%' border=0 cellspacing=0 cellpadding=0 Id='tbl_attachment'>" & vbcrlf)
	Response.Write("<tr>" & vbcrlf)
        Response.Write("	<td align='left' valign='top' style='border:none;font-size:12px;" & IIf(displaybutton = False, "display:none;", "").ToString & "'>附件:" & "<span id=""spanButtonPlaceHolder""></span>" & "</td>" & vbCrLf)
	Response.Write("</tr>" & vbcrlf)
	For i As Integer = 0 To dt.Rows.Count-1
	Response.Write("<tr Id='tr_" & DbField.GetStr(dt.Rows(i),"Id") & "'>" & vbcrlf)
	Response.Write("	<td style='border:none;padding-left:5px;font-size:12px;'>")
	Response.Write("<a href='../common/default.aspx?action=view&id=" & DbField.GetStr(dt.Rows(i),"Id") & "' target=_blank>" & DbField.GetStr(dt.Rows(i),"FileName") & "</a>")
	Response.Write("&nbsp;&nbsp;(" & DbField.GetStr(dt.Rows(i),"CreatorName") & "/" & DbField.GetStr(dt.Rows(i),"CreateDate") & ") &nbsp;&nbsp;")
	If displaybutton Then
	Response.Write("<a href='../common/default.aspx?action=edit&id=" & DbField.GetStr(dt.Rows(i),"Id") & "' target=_blank>编辑</a>")
	Response.Write("&nbsp;&nbsp;")
	Response.Write("<a href=""javascript:void(0);"" onclick=""javascript:$.post('../common/FileDelete.aspx?id=" &  DbField.GetStr(dt.Rows(i),"Id") & "',null,null);document.all('tr_" & DbField.GetStr(dt.Rows(i),"Id") & "').style.display='none';"">删除</a>")
	End If
	Response.Write("</td>" & vbcrlf)
	Response.Write("</tr>" & vbcrlf)
	Next
	Response.Write("</table>" & vbcrlf)
	
	If displaybutton = True Then
	Response.Write("<div class=""fieldset flash"" id=""fsUploadProgress""></div>" & vbcrlf)
	Response.Write("<div>" & vbcrlf)
	Response.Write("	<span id=""spanButtonPlaceHolder1""></span>" & vbcrlf)
	Response.Write("	<input id=""btnCancel"" type=""hidden"" value=""Cancel All Uploads"" style=""margin-left: 2px; font-size: 8pt; height: 29px;"" />" & vbcrlf)
	Response.Write("</div>" & vbcrlf)
	End If

End Sub

Sub DisplayAuditInfo(WorklistItemId As Long)
	Dim strBgColor As String,strMemoAttr As String
	Dim oWorkflowInstance As WorkflowInstance
        Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(WorklistItemId.ToString)
	oWorkflowInstance = oWorklistItem.ActivityInstance.WorkflowInstance

	Response.Write("<table width='700' border=1 cellspacing=0 cellpadding=0 class='bold_box' bordercolorlight='#67b2ec' bordercolordark='#FFFFFF'>" & vbcrlf)
	For i As Integer = 0 To oWorkflowInstance.Activities.Count - 1
		If UCase(oWorkflowInstance.Activities(i).NodeTemplate.Code)<>"NODISPLAY" Then
			If oWorkflowInstance.Activities(i).NodeTemplate.Type >0 And oWorkflowInstance.Activities(i).NodeTemplate.Type<>2 Then
				strBgColor = ""
				If oWorkflowInstance.Activities(i).Status =TaskStatusConstants.Actived Then strBgColor = " class='left'"
				Response.Write("<tr" & strBgColor & ">" & vbcrlf)
				Response.Write("	<th height='20' align='left' colspan='4' valign='middle'>" & oWorkflowInstance.Activities(i).Name & ": </th>" & vbcrlf)
				Response.Write("</tr>" & vbcrlf)
				For j As Integer = 0 To oWorkflowInstance.Activities(i).WorklistItems.Count - 1
					If oWorkflowInstance.Activities(i).WorklistItems(j).IsCc = False Then
						strMemoAttr = " readonly class='box2' style='width:99%'"
						strBgColor = ""
						If oWorkflowInstance.Activities(i).WorklistItems(j).Status = TaskStatusConstants.Actived Then
							strBgColor = " class='left'"
                                If oWorkflowInstance.Activities(i).WorklistItems(j).Key = WorklistItemId.ToString Then 'And CurrentUser.Code=oWorklistItem.Transactor.Code
                                    strMemoAttr = "name='memo' style='background-color:yellow;width:99%;' "
                                    strBgColor = "class='focus'"
                                End If
						End If
						Response.Write("<tr " & strBgColor & ">" & vbcrlf)
						Response.Write("	<th height='65' align='center' colspan='4' valign='middle'><textarea  " & strMemoAttr & " rows=5>" & oWorkflowInstance.Activities(i).WorklistItems(j).Memo & "</textarea></th>" & vbcrlf)
						Response.Write("</tr>" & vbcrlf)
						Response.Write("<tr valign='middle' " & strBgColor & ">" & vbcrlf)
                            'Response.Write("	<td align='left' width='350'>&nbsp;" & DisplayOpinionInfo(oWorkflowInstance.Activities(i).WorklistItems(j).Key) & "</td>" & vbcrlf)
						Response.Write("	<td align='left' width='120'>办理结果:" & oWorkflowInstance.Activities(i).WorklistItems(j).Action.Name & "</td>" & vbcrlf)
						Response.Write("	<td align='left' width='100'>办理人:" & oWorkflowInstance.Activities(i).WorklistItems(j).Transactor.Name & "</td>" & vbcrlf)
                            Response.Write("	<td align='left' width='130'>办理日期:" & Convert.ToString(IIf(oWorkflowInstance.Activities(i).WorklistItems(j).DealTime.ToString("yyyy-MM-dd") <> "0001-01-01", oWorkflowInstance.Activities(i).WorklistItems(j).DealTime.ToString("yyyy-MM-dd"), "")) & "</td>" & vbCrLf)
						Response.Write("</tr>" & vbcrlf)
					End If
				Next
			End If
		End If
	Next
	Response.Write("</table>")
End Sub

'显示意见收集的信息
    Function DisplayOpinionInfo(ByVal WorklistItemId As Long) As String
        Dim strSql As String = "SELECT * FROM WORKFLOW_FORM_OPINIONCOLLECTION WHERE SrcWorklistItemId=" & WorklistItemId & " AND CreatorCode='" & CurrentUser.Code & "'"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return "<font color='blue'>→<a href=""javascript:void(0)"" onclick=""Wo_Modal.Open('../opinioncollection/opinionDetail.aspx?WorklistItemId=" & WorklistItemId & "',700,320)"">查看意见收集详细</a>.</font>"
        Else
            Return ""
        End If
    End Function

Function FillEmployeeAttr(EmployeeCode As String,hstFormValues As Hashtable) As Hashtable
	Dim strSql As String = "SELECT CD_COLNAME,CD_DISPNAME FROM dbo.CMS_TABLE_DEFINE WHERE CD_RESID=1300"
	Dim dtDefine As DataTable = SDbStatement.Query(strSql).Tables(0)
	strSql="SELECT * FROM CMS_EMPLOYEE WHERE EMP_ID='" & EmployeeCode & "'"
	Dim dtEmployee as DataTable= SDbStatement.Query(strSql).Tables(0)
	If dtEmployee.Rows.Count>0 Then 
		For i As Integer =0 to dtDefine.Rows.Count-1
			Dim CD_DISPNAME as string = DbField.GetStr(dtDefine.Rows(i),"CD_DISPNAME")
			Dim CD_COLNAME as string = DbField.GetStr(dtDefine.Rows(i),"CD_COLNAME")
			If Not hstFormValues.Contains(CD_DISPNAME) Then
				hstFormValues.add(CD_DISPNAME,DbField.GetStr(dtEmployee.Rows(0),CD_COLNAME))
			End If 
		Next
	End If
	Return hstFormValues
End Function

    Public Sub CodeUpdate(ByVal tableName As String, ByVal ID As Long)
        Try
            Dim strSql As String = "select IsNull(max(CodeIndex),0)+1 as CodeIndex from " & tableName
            Dim dtCode As DataTable = SDbStatement.Query(strSql).Tables(0)
            Dim Code As String = CLng(dtCode.Rows(0)("CodeIndex")).ToString("000000")
            Dim Updsql As String = String.Format("update {0} set Code='{1}',CodeIndex={2} where ID={3}", tableName, Code, Convert.ToInt32(Code), ID)
            SDbStatement.Execute(Updsql)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

Public Function CodeGenerate(tableName As String) As String
	Try
		Dim strSql As String="select IsNull(max(CodeIndex),0)+1 as CodeIndex from " & tableName
		Dim dtCode As DataTable = SDbStatement.Query(strSql).Tables(0)
		Return CLng(dtCode.Rows(0)("CodeIndex")).ToString("000000")
	Catch ex As Exception
		Return "000001"
	End Try
End Function
</Script>

<script>
function showWorkflowDiagram(WorkflowInstId)
{
	var url = "/webflow/ViewFlowHistroy.aspx?WorkflowId=" + WorkflowInstId;
	var value=window.showModalDialog(url,'','dialogWidth:705px; dialogHeight:575px;status:no; directories:yes;scrollbars:no;Resizable=no;');
}
</script>




