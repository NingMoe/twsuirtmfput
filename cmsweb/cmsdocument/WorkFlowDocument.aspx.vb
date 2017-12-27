Imports NetReusables
Imports Unionsoft.Platform

Partial Class cmsdocument_WorkFlowDocument
    Inherits System.Web.UI.Page

    Protected RecID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("mnurecid") IsNot Nothing Then RecID = Request("mnurecid")

        DownloadDoc()
    End Sub


    Public Sub DownloadDoc()
        Try
            Dim strSql As String = "select * from WORKFLOW_FORM_ATTACHMENTS where ID='" + RecID.Trim + "'"
            Dim dt As DataTable = NetReusables.SDbStatement.Query(strSql).Tables(0)
            If dt.Rows.Count > 0 Then
                Dim strFileName As String = DbField.GetStr(dt.Rows(0), "FileName")
                Dim docExt As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1)
                'strFileName = strFileName.Substring(0, strFileName.LastIndexOf("."))
                'strFileName = strFileName.Replace(" ", "e8n9") '空格会被Encode成+，所以先转换，Encode后在转换回来
                'strFileName = System.Web.HttpUtility.UrlEncode(strFileName)
                'strFileName = strFileName.Replace(".", "%2E") '空格会被Encode成+，所以先转换，Encode后在转换回来
                'strFileName = strFileName.Replace("e8n9", "%20") '正确转换空格
                'strFileName &= "." & docExt
                'strFileName = strFileName.Replace("+", "%20")

                'inline : 表示以在线方式打开,attachment : 表示以下载方式打开文件.
                ' Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                Dim file As Byte() = CType(DbField.GetObj(dt.Rows(0), "FileImage"), Byte())
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName)
                Response.ClearContent()
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Dim strMime As String = CmsMime.ConvertExt2MimeForIE(docExt)
                Response.ContentType = "application/octet-stream"

                '开始向客户端写文件流
                Response.BinaryWrite(file)
                Response.Flush()
                file = Nothing
                'datDoc.bytDOC2_FILE_BIN = Nothing '手动清除文档所占内存
                'datDoc = Nothing
                Response.End()
            End If

        Catch ex As Exception
            '线程正被中止，不做任何操作
        End Try
    End Sub
End Class
