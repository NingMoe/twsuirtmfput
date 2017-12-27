<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase"%>
<%@ import namespace="NetReusables"%>
<%@ import namespace="System.IO"%>

<script language="vb" runat="server">
Private _action As String
Private _Id As String = "0"
Private _RecId As String = "0"

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
	_action = Request("action")
	_Id = Request("id")
	_RecId = Request("RecId")
	Log("_RecId:" & _RecId)
    Try
		If _action = "upload" Then
			doFormUploadDisk1()
		Else
			doFormUploadDisk()
		End If
    Catch ex As Exception
        Log("OFFICE CONTROL 在线保存文件 发生错误!")
        Log(ex.ToString())
    End Try
End Sub

Public Sub doFormUploadDisk()
    Dim uploadpath As String
        uploadpath = Request.PhysicalApplicationPath
        
    Dim uploadFiles As System.Web.HttpFileCollection
    Dim theFile As System.Web.HttpPostedFile
    uploadFiles = Request.Files
    Dim i As Integer
    For i = 0 To uploadFiles.Count - 1
        theFile = uploadFiles(i)
        If uploadFiles.GetKey(i).ToUpper() = "EDITFILE" Then
			Dim filename As String = theFile.FileName.Substring(theFile.FileName.LastIndexOf("\") + 1)
			Log(uploadpath & "\temp\" & filename)
			If File.Exists(uploadpath & "\temp\" & filename) Then File.Delete(uploadpath & "\temp\" & filename)
			theFile.SaveAs(uploadpath & "\temp\" & filename)
			
			'将文件存入数据库
                Dim buffer(Convert.ToInt32(theFile.InputStream.Length)) As Byte
			Dim br As BinaryReader = New BinaryReader(theFile.InputStream)
			br.Read(buffer, 0, buffer.Length)
			br.Close()
			
                SaveAttachment(Convert.ToInt64(_Id), filename, buffer, Convert.ToInt64(_RecId))
        End If
    Next
End Sub

Public Sub doFormUploadDisk1()
	Dim Id As Long = TimeId.CurrentMilliseconds(30)
    Dim uploadpath As String
    uploadpath = Request.PhysicalApplicationPath
    Dim uploadFiles As System.Web.HttpFileCollection
    Dim theFile As System.Web.HttpPostedFile
    uploadFiles = Request.Files
    Dim i As Integer
    For i = 0 To uploadFiles.Count - 1
        theFile = uploadFiles(i)
		Dim filename As String = theFile.FileName.Substring(theFile.FileName.LastIndexOf("\") + 1)
		Log(filename)
		Log(uploadpath & "\temp\" & filename)
		If File.Exists(uploadpath & "\temp\" & filename) Then File.Delete(uploadpath & "\temp\" & filename)
		theFile.SaveAs(uploadpath & "\temp\" & filename)
		
		'将文件存入数据库
            Dim buffer(Convert.ToInt32(theFile.InputStream.Length)) As Byte
		Dim br As BinaryReader = New BinaryReader(theFile.InputStream)
		br.Read(buffer, 0, buffer.Length)
		br.Close()
		
            SaveAttachment(Id, filename, buffer, Convert.ToInt64(_RecId))
		Response.Write(Id)
    Next
End Sub

Private Sub Log(str As String)
	SDbStatement.Execute("Insert into log (content) values ('" & str & "')")
End Sub

Private Sub SaveAttachment(Id As Long,FileName As String, buffer() As Byte,RecId  As Long)
	Dim strSql As String 
	Dim hst As New Hashtable()
	hst.Add("Id",Id)
	hst.Add("FileName",FileName)
	hst.Add("FileImage",buffer)
	hst.Add("RecId",RecId)
	hst.Add("State",0)
	hst.Add("CreatorCode",CurrentUser.Code)
	hst.Add("CreatorName",CurrentUser.Name)
	hst.Add("CreateDate",DateTime.Now)

	strSql = "SELECT ID FROM WORKFLOW_FORM_ATTACHMENTS WHERE ID=" & Id
	
	If SDbStatement.Query(strSql).Tables(0).Rows.Count = 0 Then
		SDbStatement.InsertRow(hst,"WORKFLOW_FORM_ATTACHMENTS")
	Else
		SDbStatement.UpdateRows(hst,"WORKFLOW_FORM_ATTACHMENTS","ID=" & Id)
	End If

End Sub
</script>