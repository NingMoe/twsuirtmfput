
Imports System.Collections.Generic

Partial Class _Default
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim oRecordInfo As New RecordInfo
        'Dim fi As New FieldInfo
        'fi.FieldValue = "0"
        'fi.FieldDescription = "类别名称"


        'Dim fi1 As New FieldInfo
        'fi1.FieldValue = "00000000"
        'fi1.FieldDescription = "项目分类编号"


        'Dim oFieldInfo As FieldInfo() = {fi1, fi}

        'Common.Add("521904372663", "523538351359", "521904566120", "test", oFieldInfo)


        'oRecordInfo.RecordID = 0
        'oRecordInfo.ResourceID = "502818638816"
        'oRecordInfo.FieldInfoList = oFieldInfo

        'fi = New FieldInfo
        'fi.FieldName = "DOC2_NAME"
        'fi.FieldValue = "Text"

        'Dim RecInfo As New RecordInfo
        'RecInfo.RecordID = 0
        'RecInfo.ResourceID = "513336958379"
        'RecInfo.FieldInfoList = {fi}

        'Dim oRecInfoList As RecordInfo() = {RecInfo}

        'Dim oRecordInfoList As RecordInfo()() = {oRecInfoList}

        'Transaction.AddofDoc("test", oRecordInfo, oRecordInfoList)


        ' Transaction.AddofDoc("test", oRecInfoList)


        ' Resource.GetRelationTable("505925446167")
        '  Common.DeleteDOC("503593152654", "506639332746", "test")

        'Common.GetNextPortalTreeRootByResourceIDAndUserID("tq", "496767152706")
        'Common.GetNextAllPortalTreeByResourceIDAndUserID("tq", "496767164608")

        '    Dim TableName As String = Common.GetTableNameByResourceid("513336958379")
        'Common.GetAllPortalOperationByResourceIDAndUserID("vkbsadmission", "510507959971")

        'Dim p As New PageParameter()
        'p.PageIndex = 0
        'p.PageSize = 10
        'p.SortField = "id"
        'p.SortBy = ""

        'Dim ds As DataSet = Common.GetDataListPage(513368633407, "test", " and 城市='上海'", p)

        ' Dim dt As DataTable = Common.GetDataListByResourceID("513368620217", False, " and ID=514031229509").Tables(0)

        'Dim dt As DataTable = Common.GetAllPortalOperationByResourceIDAndUserID("xjl", "496767205388", True)

        ' Dim oList As List(Of ResourceInfo) = Common.GetNextPortalTreeRootByResourceIDAndUserID("xjl", "496767152706")r

        ' Dim f() As Field = Common.GetFieldListAll(1300)
        '  Dim dt As List(Of ResourceInfo) = Common.GetNextPortalTreeRootByResourceIDAndUserID("tq", "496767152706")
        ' Dim dt As DataSet = Resource.GetDictionaryReturnRelatedColumn("505925446167", "备注")
        'Dim oResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID("1300")
        ' Dim o As UserInfo = Users.GetUserInfoByID("test



        ' Dim ds As DataSet = Common.GetDataListPage(521561033054, True, "cs", "", p)

        ' MTSearch.UpdateMTSearch(0, "521561033054", "cs", "CustomerCode", "客户编号", "包含", "like", "3", "3", "AND", "test")
        ' Common.GetNextPortalTreeRootByResourceIDAndUserID("zhangj", "506537627075")

        ' Common.Delete("521907386229", "523037729287", "")

        '   Dim dt As DataTable = Common.GetDataListByTableName("AutoEncoded", " and 业务类别='项目编号' and 编号类型='System' and 其他类别='' and 年度='2016' and 月份='' and 位数=4").Tables(0)
        '  Common.Delete("521908299061", "524072738769")

        '  Dim i As Int64 = Common.GetDataListRecordCount(529354502609, "", " and 编号 = '529359351665'")

        '  Dim i As Int64 = Common.GetDataListRecordCount(526151314066, "", " and 所属分类ID='527176977861' and 档案所属类型编号='2016'")


        '  dtRelation.PrimaryKey = New DataColumn() {dtRelation.Columns("ChildResourceID"), dtRelation.Columns("ChildColName")}

        ' Dim dt As DataTable = Common.GetAllPortalChildResourceList("EmployeeInfo")

        ' Dim l As List(Of ResourceInfo) = Common.GetNextPortalTreeRootByResourceIDAndUserID("yzz0012", "521559492788")

        '  Dim dt As DataTable = Common.GetNextAllDirectoryTreeByResourceID("496767164608")


        Dim l As DataTable = Common.GetNextAllDirectoryTreeByResourceID("", False, True )

    End Sub
End Class
