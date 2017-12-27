Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class FileWebEditor
    Inherits UserPageBase

    Protected Url As String                 '文件的Url
    Protected lngIsCheckOut As Long = 0
    Protected strCurrentName As String

    Protected strDocumentExtName As String = ""

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lngResourceID As Long = CLng(Request.QueryString("ResourceID"))
        Dim lngDocumentID As Long = CLng(Request.QueryString("DocumentID"))
        strCurrentName = Me.CurrentUser.Name
        lngIsCheckOut = CLng(Request.QueryString("IsCheckOut"))
        Dim datDoc As DataDocument = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
        'Dim datDoc As DataDocument = CmsDocFlow.GetOneAttachment((New CmsResource(MyBase.CurrentUser.Code, MyBase.CurrentUser.Password)).GetPst, lngResourceID, lngDocumentID, True)
        If Not datDoc Is Nothing Then
            Me.txtDocumentInfo.Text = lngResourceID & "_" & lngDocumentID
            strDocumentExtName = datDoc.strDOC2_EXT.ToLower()
        End If


        Url = "DownloadFile.aspx?ResourceID=" & lngResourceID & "&DocumentID=" & lngDocumentID

        Me.btnUpdateFile.Attributes.Add("onClick", "TANGER_OCX_SaveEditToServerDisk();return false;")

        '从缓存中获取该文档的最近访问时间,如果访问时间在10分钟内,那么就不让其他人再更改这个文件
        Me.btnUpdateFile.Enabled = True
        If lngIsCheckOut <> 3 Then
            If Configuration.DocumentAccessCache.Contains(CStr(lngDocumentID)) Then
                If CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode <> Me.CurrentUser.Code Then
                    If DateDiff(DateInterval.Minute, CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).AccessTime, Now) <= 10 Then
                        Me.btnUpdateFile.Enabled = False
                        Me.btnUpdateFile.Text = "此文件正在被[" & OrganizationFactory.Implementation.GetEmployee(CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode).Name & "]编辑"
                    Else
                        Configuration.DocumentAccessCache(CStr(lngDocumentID)) = New DocumentAccessLog(MyBase.CurrentUser.Code)
                    End If
                End If
            Else
                Configuration.DocumentAccessCache.Add(CStr(lngDocumentID), New DocumentAccessLog(MyBase.CurrentUser.Code))
            End If
        Else
            Me.btnUpdateFile.Enabled = False
        End If
    End Sub

End Class

End Namespace
