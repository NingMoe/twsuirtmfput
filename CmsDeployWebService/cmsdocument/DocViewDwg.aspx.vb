Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
 


Namespace Unionsoft.Cms.Web


Partial Class DocViewDwg
        Inherits Page

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

        Public cmsurldoc As String = ""

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim DocID As String = Request("DOCID")

            Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(DocID)



            cmsurldoc = Common.GetServerPath + datDoc.UpFilePath

          
        End Sub  

End Class

End Namespace
