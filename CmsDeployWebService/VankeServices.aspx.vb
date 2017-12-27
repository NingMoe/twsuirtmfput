
Partial Class VankeServices
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim typeValue As String = Request("typeValue")
        Dim ResourceID As String = Request("ResourceID")
        Dim Condition As String = ""
        If Request("Condition") IsNot Nothing Then
            Condition = Server.UrlDecode(Request("Condition"))
            If Condition.TrimStart.Substring(0, 3).ToLower <> "and" Then
                Condition = " and " + Condition
            End If
        End If
        Dim IsGetChildren As Boolean = False
        If Request("IsGetChildren") IsNot Nothing Then IsGetChildren = Convert.ToBoolean(Convert.ToInt32(Request("IsGetChildren")))
        Dim UserID As String = ""
        If Request("UserID") IsNot Nothing Then UserID = Request("UserID")
        Dim SortField As String = "ID"
        If Request("SortField") IsNot Nothing Then SortField = Request("SortField")
        Dim SortBy As String = ""
        If Request("SortBy") IsNot Nothing Then SortBy = Request("SortBy")
        Dim PageSize As Integer = 20
        If Request("PageSize") IsNot Nothing Then PageSize = Convert.ToInt32(Request("PageSize"))
        Dim PageIndex As Integer = 0
        If Request("PageIndex") IsNot Nothing Then PageIndex = Convert.ToInt32(Request("PageIndex"))

        Dim json As String = ""
        Dim timeConverter As New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
        Select Case typeValue
            Case "GetDataListByResourceID" 
                Dim ds As DataSet = Common.GetDataListByResourceID(ResourceID, IsGetChildren, Condition) 
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
            Case "GetDataListByResID"
                Dim ds As DataSet = Common.GetDataListByResID(ResourceID, SortField, Condition, UserID)
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
            Case "GetDataListRecordCount"
                json = Common.GetDataListRecordCount(ResourceID, UserID, Condition).ToString
            Case "GetPageOfDataList"
                Dim p As New PageParameter
                p.PageIndex = PageIndex
                p.PageSize = PageSize
                p.SortBy = SortBy
                p.SortField = SortField
                Dim ds As DataSet = Common.GetDataListPage(ResourceID, UserID, Condition, p)
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)  
        End Select
        Response.Write(json)
    End Sub
End Class
