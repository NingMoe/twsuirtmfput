Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement
Imports System.Collections.Generic
Imports System.EnterpriseServices

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.None)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Services
    Inherits System.Web.Services.WebService
    ''' <summary>
    ''' 返回登陆成功与否
    ''' </summary>
    ''' <param name="UserID">用户ID</param>
    ''' <param name="Password">用户密码</param>
    ''' <returns>是否</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function Login(ByVal UserID As String, ByVal Password As String) As Boolean

        Try
            If UserID.IndexOf("\") > 0 Then
                Dim DomainName As String = UserID.Split("\")(0)
                Dim UserName As String = UserID.Split("\")(1)

                If UserID.Trim.Equals("") Then
                    Return False
                End If

                If UserID.Trim.ToLower.Contains("insert") Or UserID.Trim.ToLower.Contains("delete") Or UserID.Trim.ToLower.Contains("select") Or UserID.Trim.ToLower.Contains("drop") Or UserID.Trim.ToLower.Contains("union") Or UserID.Trim.ToLower.Contains("or") Or UserID.Trim.ToLower.Contains("from") Or UserID.Trim.ToLower.Contains("and") Or UserID.Trim.ToLower.Contains("exec") Or UserID.Trim.ToLower.Contains("count") Or UserID.Trim.ToLower.Contains("master") Or UserID.Trim.ToLower.Contains("sysobjects") Then
                    Return False
                End If

                Dim Domaincls As Domain = New Domain
                If Domaincls.ValidUser(UserName, DomainName, Password) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                If UserID.Trim.Equals("") Then
                    Return False
                End If

                If UserID.Trim.ToLower.Contains("insert") Or UserID.Trim.ToLower.Contains("delete") Or UserID.Trim.ToLower.Contains("select") Or UserID.Trim.ToLower.Contains("drop") Or UserID.Trim.ToLower.Contains("union") Or UserID.Trim.ToLower.Contains("or") Or UserID.Trim.ToLower.Contains("from") Or UserID.Trim.ToLower.Contains("and") Or UserID.Trim.ToLower.Contains("exec") Or UserID.Trim.ToLower.Contains("count") Or UserID.Trim.ToLower.Contains("master") Or UserID.Trim.ToLower.Contains("sysobjects") Then
                    Return False
                End If

                Dim pst As CmsPassport = CmsPassport.GenerateCmsPassport(UserID, Password)
                If pst Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            SLog.Err("登录出错=" + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 修改登录密码
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="NewPassword"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function ChangePassword(ByVal UserID As String, ByVal NewPassword As String) As Boolean
        If UserID.Trim.Equals("") Then
            Return False
        End If

        If UserID.Trim.ToLower.Contains("insert") Or UserID.Trim.ToLower.Contains("delete") Or UserID.Trim.ToLower.Contains("select") Or UserID.Trim.ToLower.Contains("drop") Or UserID.Trim.ToLower.Contains("union") Or UserID.Trim.ToLower.Contains("or") Or UserID.Trim.ToLower.Contains("from") Or UserID.Trim.ToLower.Contains("and") Or UserID.Trim.ToLower.Contains("exec") Or UserID.Trim.ToLower.Contains("count") Or UserID.Trim.ToLower.Contains("master") Or UserID.Trim.ToLower.Contains("sysobjects") Then
            Return False
        End If

        Dim hst As Hashtable = New Hashtable
        hst.Add("EMP_PASS", NetReusables.Encrypt.Encrypt(NewPassword))
        Try
            SDbStatement.UpdateRows(hst, "CMS_EMPLOYEE", "EMP_ID='" + UserID + "'")
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try

    End Function
    


    ''' <summary>
    ''' 通过用户ID返回用户通行证
    ''' </summary>
    ''' <param name="UserID">用户ID</param>
    ''' <returns>返回用户通行证</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
      Public Function GetUserInfo(ByVal UserID As String) As UserInfo
        Return Users.GetUserInfoByID(UserID)
        ' Return CmsPassport.GenerateCmsPassport(UserID)
    End Function
 <WebMethod()> _
    Public Function AddReturnID(ByVal ResourceID As String, ByVal UserID As String, ByVal FieldInfoList As FieldInfo()) As Long
        Return Common.AddReturnID(ResourceID, UserID, FieldInfoList)
    End Function
    ''' <summary>
    ''' 根据表名查询数据
    ''' </summary>
    ''' <param name="TableName"></param> 
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDataListByTableName(ByVal TableName As String, ByVal Condition As String) As DataSet
        Return Common.GetDataListByTableName(TableName, Condition)
    End Function

    <WebMethod()> _
    Public Function GetUserInfoIDMax(ByVal ResourceName As String, ByVal IsCheckTableName As Boolean) As String
        Return Common.GetUserInfoIDMax(ResourceName, IsCheckTableName)
    End Function


    ''' <summary>
    ''' 返回用户列表
    ''' </summary>
    ''' <returns>用户集合类</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
     Public Function GetAllUsers() As List(Of UserInfo)
        Return Users.GetAllUsers()
    End Function

    <WebMethod()> _
    Public Function GetAllParentDepartmentIDByUserID(ByVal UserID As String) As String
        Return Users.GetAllParentDepartmentIDByUserID(UserID)
    End Function

    ''' <summary>
    ''' 返回该资源描述下的数据集
    ''' </summary>
    ''' <param name="Description">资源说明</param>
    ''' <param name="IsGetChildren">是否获取字表数据</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Function GetDataList(ByVal Description As String, ByVal IsGetChildren As Boolean) As DataSet
        Return Common.GetDataList(Description, IsGetChildren)
    End Function


    ''' <summary>
    ''' 返回该资源ID的数据集
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="IsGetChildren"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
 Public Function GetDataListByResourceID(ByVal ResourceID As String, ByVal IsGetChildren As Boolean, ByVal Condition As String) As DataSet
        Return Common.GetDataListByResourceID(ResourceID, IsGetChildren, Condition)
    End Function
    <WebMethod()> _
    Public Function GetDataListByResourceIDOfView(ByVal ResourceID As String, ByVal Condition As String, ByVal UserID As String) As DataSet
        Return Common.GetDataListByResourceID(ResourceID, False, Condition, UserID)
    End Function

    ''' <summary>
    ''' 通过ResID查询数据，支持视图，支持排序
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()>
    Public Function GetDataListByResID(ByVal ResourceID As String, ByVal OrderBy As String, ByVal Condition As String, ByVal UserID As String) As DataSet
        Return Common.GetDataListByResID(ResourceID, OrderBy, Condition, UserID)
    End Function

    ''' <summary>
    ''' 通过ResID查询数据，支持自定义列，支持排序
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()>
    Public Function GetDataColumnListByResID(ByVal ResourceID As String, ByVal ColumnStr As String, ByVal OrderBy As String, ByVal Condition As String, ByVal UserID As String) As DataSet
        Return Common.GetDataColumnListByResID(ResourceID, ColumnStr, OrderBy, Condition, UserID)
    End Function

    ''' <summary>
    ''' 返回符合条件的记录数
    ''' </summary>
    ''' <param name="Description">资源说明</param>
    ''' <param name="Condition">检索条件</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetDataListRecordCount1")> _
   Public Function GetDataListRecordCount(ByVal Description As String, ByVal Condition As String) As Int64

        Return Common.GetDataListRecordCount(Description, Condition)
    End Function
    <WebMethod(EnableSession:=True, MessageName:="GetDataListRecordCount2")> _
 Public Function GetDataListRecordCount(ByVal Description As String, ByVal UserID As String, ByVal Condition As String) As Int64
        Return Common.GetDataListRecordCount(Description, UserID, Condition)
    End Function
    <WebMethod(EnableSession:=True, MessageName:="GetDataListRecordCount3")> _
   Public Function GetDataListRecordCount(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String) As Int64
        Return Common.GetDataListRecordCount(ResourceID, UserID, Condition)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="GetDataListRecordCount4")> _
 Public Function GetDataListRecordCount(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal IsRowRights As Boolean) As Int64
        Return Common.GetDataListRecordCount(ResourceID, UserID, Condition, IsRowRights)
    End Function
    <WebMethod(EnableSession:=True, MessageName:="GetDataListRecordCount5")> _
    Public Function GetDataListRecordCount(ByVal Sql As String) As Int64
        Return Common.GetDataListRecordCount(Sql)
    End Function
    ''' <summary>
    ''' 返回相应的子表
    ''' </summary>
    ''' <param name="ID">主键</param>
    ''' <param name="ResourceID">资源ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function ShowChildTablesByID(ByVal ID As String, ByVal ResourceID As String) As DataSet
        Return Resource.ShowChildTablesByID(ID, ResourceID)
    End Function
    ''' <summary>
    ''' 通过ResourceID返回该资源的所有属性
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetResourceInfoByID(ByVal ResourceID As String) As ResourceInfo
        Return Resource.GetResourceInfoByResourceID(ResourceID)
    End Function
    <WebMethod()> _
    Public Function GetAllChildResourceID(ByVal ResourceID As String) As String
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        Return Resource.GetAllChildResourceID(ResourceID, dtResource)
    End Function

    Public Shared Function GetAllResourceInfoByCondition(ByVal Condition As String) As List(Of ResourceInfo)
        Return Resource.GetAllResourceInfoByCondition(Condition)
    End Function




    ''' <summary>
    ''' 通过资源ID返回资源的目录路径
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <returns>目录路径以\分开</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetPathByResourceID(ByVal ResourceID As String) As String
        Return Resource.GetResuorsePath(ResourceID)
    End Function

    ''' <summary>
    ''' 返回检索条件的数据
    ''' </summary>
    ''' <param name="Description">资源说明</param>
    ''' <param name="IsGetChildren">是否显示字表信息</param>
    ''' <param name="Condition">检索条件</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDataListByCondition(ByVal Description As String, ByVal IsGetChildren As Boolean, ByVal Condition As String) As DataSet
        Return Common.GetDataList(Description, IsGetChildren, Condition)
    End Function
    ''' <summary>
    ''' 通过传递分页类返回某页的数据
    ''' </summary>
    ''' <param name="Description">资源说明</param>
    ''' <param name="Condition">检索条件</param>
    ''' <param name="PageParameter">分页类</param>
    ''' <returns>数据集</returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList1")> _
    Public Function GetPageOfDataList(ByVal Description As String, ByVal Condition As String, ByVal PageParameter As PageParameter) As DataSet
        Return Common.GetDataListPage(Description, Condition, PageParameter)
    End Function


    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList2")> _
    Public Function GetPageOfDataList(ByVal Description As String, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal UserID As String) As DataSet
        Return Common.GetDataListPage(Description, UserID, Condition, PageParameter)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList3")> _
    Public Function GetPageOfDataList(ByVal ResourceID As Int64, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal UserID As String) As DataSet
        Return Common.GetDataListPage(ResourceID, UserID, Condition, PageParameter)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList4")> _
    Public Function GetPageOfDataList(ByVal ResourceID As Int64, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal UserID As String, ByVal IsShowChild As Boolean) As DataSet
        Return Common.GetDataListPage(ResourceID, UserID, Condition, PageParameter, IsShowChild)
    End Function
    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList5")> _
    Public Function GetPageOfDataList(ByVal ResourceID As Int64, ByVal IsRowRights As Boolean, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal UserID As String) As DataSet
        Return Common.GetDataListPage(ResourceID, IsRowRights, UserID, Condition, PageParameter)
    End Function
    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList6")> _
    Public Function GetPageOfDataList(ByVal strSql As String, ByVal PageParameter As PageParameter) As DataSet
        Return Common.GetDataListPage(strSql, PageParameter)
    End Function

    ''' <summary>
    ''' 返回此资源描述下的所有目录结构（包括子目录）
    ''' </summary>
    ''' <param name="Description">资源描述</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetAllDirectoryList(ByVal Description As String) As DataTable
        Return Common.GetAllDirectoryTreeByResourceName(Description)
    End Function
    ''' <summary>
    ''' 返回此资源下的第一层目录结构
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetNextDirectoryList1")> _
    Public Function GetNextDirectoryList(ByVal ResourceID As String) As List(Of ResourceInfo)
        Return Common.GetNextDirectoryTreeByResourceID(ResourceID)
    End Function

    ''' <summary>
    ''' 返回此用户在资源下的可访问的第一层目录结构
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetNextDirectoryList2")> _
    Public Function GetNextDirectoryList(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
        Return Common.GetNextDirectoryTreeByResourceIDAndUserID(UserID, ResourceID)
    End Function

    <WebMethod()> _
    Public Function GetNextAllDirectoryList(ByVal ResourceID As String) As DataTable
        Return Common.GetNextAllDirectoryTreeByResourceID(ResourceID, True )
    End Function

    <WebMethod(EnableSession:=True, MessageName:="GetNextDirectoryList3")> _
    Public Function GetNextDirectoryList(ByVal ResourceID As String, ByVal IsEnable As Boolean, ByVal IsShowDefault As Boolean) As DataTable
        Return Common.GetNextAllDirectoryTreeByResourceID(ResourceID, IsEnable, IsShowDefault)
    End Function


    ''' <summary>
    ''' 获取下一级子资源 （该权限在Portal中设置）
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetNextPortalTreeRootByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
        Return Common.GetNextPortalTreeRootByResourceIDAndUserID(UserID, ResourceID)
    End Function


    <WebMethod()> _
    Public Function GetNextPortalTreeRoot(ByVal UserID As String, ByVal ResourceID As String) As DataTable
        Return Common.GetNextPortalAllTreeByResourceIDAndUserID(UserID, ResourceID, False)
    End Function

    ' ''' <summary>
    ' ''' 获取所有子资源的菜单（流量）权限（该权限在Portal中设置）
    ' ''' </summary>
    ' ''' <param name="UserID"></param>
    ' ''' <param name="ResourceID"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    '<WebMethod()> _
    'Public Function GetNextAllPortalTreeByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
    '    Return Common.GetNextAllPortalTreeByResourceIDAndUserID(UserID, ResourceID)
    'End Function

    <WebMethod()> _
    Public Function GetNextPortalTreeByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
        Return Common.GetNextPortalTreeByResourceIDAndUserID(UserID, ResourceID)
    End Function

    <WebMethod()> _
    Public Function GetNextPortalAllTreeByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As DataTable
        Return Common.GetNextPortalAllTreeByResourceIDAndUserID(UserID, ResourceID)
    End Function



    ''' <summary>
    ''' 获取所有子资源的操作权限（该权限在Portal中设置）
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="ResourceID"></param>
    ''' <param name="IsGetChildResource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetAllPortalOperationByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String, ByVal IsGetChildResource As Boolean) As DataTable
        Return Common.GetAllPortalOperationByResourceIDAndUserID(UserID, ResourceID, IsGetChildResource)
    End Function


    ''' <summary>
    ''' 获取所有子资源（该权限在Portal中设置）
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetPortalChildResourceList(ByVal UserID As String, ByVal KeyWord As String) As DataTable
        Return Common.GetPortalChildResourceList(UserID, KeyWord)
    End Function

    ''' <summary>
    ''' 获取所有子资源
    ''' </summary> 
    ''' <param name="KeyWord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetAllPortalChildResourceList(ByVal KeyWord As String) As DataTable
        Return Common.GetAllPortalChildResourceList(KeyWord)
    End Function


    ''' <summary>
    ''' 获取此条目下ID的所有文档
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetFileListByID(ByVal id As String) As DocmentInfo
        Return Document.GetDocumentByID(id)
    End Function
    ''' <summary>
    ''' 获取继承资源和视图资源的继承父资源
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetTopParentID(ByVal ResourceID As String) As Long
        Dim dtResource As DataTable = NetReusables.SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        Return Resource.GetTopParentID(ResourceID, dtResource)
    End Function

    <WebMethod()> _
    Public Function GetDocuments(ByVal ResourceID As Long, ByVal ID As Long) As List(Of DocmentInfo)
        Return Document.GetDocuments(ResourceID, ID)
    End Function
    <WebMethod()> _
    Public Function GetDocument(ByVal DocResourceID As Long, ByVal ID As Long) As DataSet
        Return Document.GetDocument(DocResourceID, ID)
    End Function

    <WebMethod()> _
    Public Function GetResourceIDByDescription(ByVal Description As String) As String
        Return Common.GetResourceIDByDescription(Description)
    End Function

    <WebMethod()> _
    Public Function GetResourceIDByTableName(ByVal TableName As String) As String
        Return Common.GetResourceidByTableName(TableName)
    End Function

    <WebMethod()> _
    Public Function GetTableNameByResourceid(ByVal ResourceID As String) As String
        Return Resource.GetTableNameByResourceID(ResourceID)
    End Function

    <WebMethod()> _
    Public Function GetFieldListAll(ByVal ResourceID As String) As Field()
        Return Common.GetFieldListAll(ResourceID)
    End Function
    ''' <summary>
    ''' 获取资源下的所有列属性
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetFieldList1")> _
    Public Function GetFieldList(ByVal ResourceID As String) As Field()
        Return Common.GetFieldList(ResourceID)
    End Function
    <WebMethod(EnableSession:=True, MessageName:="GetFieldList2")> _
    Public Function GetFieldList(ByVal ResourceID As String, ByVal UserID As String) As Field()
        Dim FieldList As Field() = New Field() {}
        Dim filter As String = Right.GetColumns(UserID, ResourceID)
        For Each fd As Field In Common.GetFieldList(ResourceID)
            Dim isShow As Boolean = True
            For Each str As String In filter.Split(",")
                If fd.Name = str Then
                    isShow = False
                    Exit For
                End If
            Next
            If isShow Then
                ReDim Preserve FieldList(FieldList.Length)
                FieldList(FieldList.Length - 1) = fd
            End If
        Next
        Return FieldList
    End Function
    ''' <summary>
    ''' 添加数据到某资源下
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="FieldInfoList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="Add1")> _
    Public Function Add(ByVal ResourceID As String, ByVal UserID As String, ByVal FieldInfoList As FieldInfo()) As Boolean
        Return Common.Add(ResourceID, UserID, FieldInfoList)
    End Function


    <WebMethod(EnableSession:=True, MessageName:="Add2")> _
    Public Function Add(ByVal ParentResourceID As String, ByVal ParentRecordID As String, ByVal ResourceID As String, ByVal UserID As String, ByVal FieldInfoList As FieldInfo()) As Boolean
        Return Common.Add(ParentResourceID, ParentRecordID, ResourceID, UserID, FieldInfoList)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="Add3")> _
    Public Function Add(ByVal UserID As String, ByVal ParentRecordInfo As RecordInfo, ByVal ParamArray ChildRecordInfoList As RecordInfo()()) As Long
        Return Transaction.Add(UserID, ParentRecordInfo, ChildRecordInfoList)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="Add4")> _
    Public Function Add(ByVal UserID As String, ByVal RecordInfo() As RecordInfo) As RecordInfo()
        Return Transaction.Add(UserID, RecordInfo)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="")> _
    Public Function AddofDoc(ByVal UserID As String, ByVal ParentRecordInfo As RecordInfo, ByVal ParamArray ChildRecordInfoList As RecordInfo()()) As Long
        Return Transaction.AddofDoc(UserID, ParentRecordInfo, ChildRecordInfoList)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="")> _
    Public Function AddOfDocument(ByVal UserID As String, ByVal DocumentRecordInfo() As RecordInfo) As RecordInfo()
        Return Transaction.AddofDoc(UserID, DocumentRecordInfo)
    End Function

    ''' <summary>
    ''' 删除某资源ID下某条数据(包括doc资源数据)
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ID"></param>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function Delete(ByVal ResourceID As String, ByVal ID As String, ByVal UserID As String) As Boolean
        Return Common.Delete(ResourceID, ID, UserID)
    End Function

    <WebMethod()> _
    Public Function DeleteOfChild(ByVal ResourceID As String, ByVal RecID As String, ByVal UserID As String, ByVal IsDeleteChildData As Boolean) As Boolean
        Return Common.Delete(ResourceID, RecID, UserID, IsDeleteChildData)
    End Function

    <WebMethod()> _
    Public Function DeleteByTableName(ByVal TableName As String, ByVal Condition As String, ByVal UserID As String) As Boolean
        Return Common.DeleteByTableName(TableName, Condition, UserID)
    End Function
    ''' <summary>
    ''' 编辑数据某资源ID下某条数据
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="FieldInfoList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function Edit(ByVal ResourceID As String, ByVal ID As String, ByVal UserID As String, ByVal FieldInfoList As FieldInfo()) As Boolean
        Return Common.Edit(ResourceID, ID, UserID, FieldInfoList)
    End Function
    ''' <summary>
    ''' 返回特殊SQL语句下的数据集
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks>select <%表名.字段1%>,<%表名.字段2%> from [表名] join [table2] on <%表名.t%>=<%table2.t%></remarks>
    <WebMethod()> _
    Public Function SelectData(ByVal sql As String, ByVal timeTicks As String, ByVal randomNum As String, ByVal keyPass As String) As DataSet
        If Common.IsTrue(timeTicks, randomNum, keyPass) Then
            Return SDbStatement.Query(Common.translation(sql))
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    '''  执行特殊SQL语句
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks>update  [表名] set <%表名.字段1%>='value' where id=123456</remarks>
    <WebMethod()> _
    Public Function ExecuteSql(ByVal sql As String, ByVal timeTicks As String, ByVal randomNum As String, ByVal keyPass As String) As Integer
        If Common.IsTrue(timeTicks, randomNum, keyPass) Then
            Return SDbStatement.Execute(Common.translation(sql))
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' 返回资源ID下的某列的默认下拉选择列表
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="ColumnDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetColumnOptionList(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Return Resource.GetColumnOptionList(ResourceID, ColumnDescription)
    End Function

    ''' <summary>
    ''' 返回资源ID下的某列的单选列表
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="ColumnDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetColumnRadioList(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Return Resource.GetColumnRadioList(ResourceID, ColumnDescription)
    End Function

    <WebMethod()> _
    Public Function GetRecord(ByVal ResourceID As String, ByVal ID As String) As DataSet
        Return Common.GetDataListByResourceID(ResourceID, False, " and id=" + ID)
    End Function

    <WebMethod()> _
    Public Function GetDataListByParentRecID(ByVal ParentResID As String, ByVal ResID As String, ByVal ParentRecID As String) As DataTable
        Return Common.GetDataListByParentRecID(ParentResID, ResID, ParentRecID)
    End Function



    <WebMethod()> _
    Public Function GetAllRoles() As List(Of RoleInfo)
        Return Common.GetAllRoles()
    End Function

    <WebMethod()> _
    Public Function GetRolesByUserID(ByVal UserID As String) As List(Of RoleInfo)
        Return Common.GetRolesByUserID(UserID)
    End Function

    ''' <summary>
    ''' 上传文件
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="UserID">用户ID</param>
    ''' <param name="FileName">文件名</param>
    ''' <param name="BinFile">二进制</param>
    ''' <param name="FieldInfoList">该文档其他字段与值的集合</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function UploadFile(ByVal ResourceID As String, ByVal UserID As String, ByVal FileName As String, ByVal BinFile As Byte(), ByVal FieldInfoList As FieldInfo()) As Boolean
        Return Common.UploadFile(ResourceID, UserID, FileName, BinFile, FieldInfoList)
    End Function

    <WebMethod()> _
    Public Function UploadFileByXml(ByVal ResourceID As String, ByVal UserID As String, ByVal FileName As String, ByVal BinFile As Byte(), ByVal xmlFieldAndValueList As String) As String
        Return Common.UploadFile(ResourceID, UserID, FileName, BinFile, xmlFieldAndValueList)
    End Function


    ''' <summary>
    ''' 该用户对该资源的权限列表
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetRights(ByVal UserID As String, ByVal ResourceID As String) As RightInfo()
        Return Right.GetRights(UserID, ResourceID)
    End Function
    ''' <summary>
    ''' 返回字典数据列表
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="ColumnDescription">列描述</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDictionaryList(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Return Resource.GetDictionaryList(ResourceID, ColumnDescription)
    End Function
    ''' <summary>
    ''' 返回字典数据对应的列名
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="ColumnDescription">列描述</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDictionaryReturnColumnName(ByVal ResourceID As String, ByVal ColumnDescription As String) As String
        Return Resource.GetDictionaryReturnColumnName(ResourceID, ColumnDescription)
    End Function

    ''' <summary>
    ''' 返回字典列表字段
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ColumnDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDictionaryReturnColumn(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataTable
        Return Resource.GetDictionaryReturnColumn(ResourceID, ColumnDescription)
    End Function

    ''' <summary>
    ''' 返回资源与字典的关联字典
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ColumnDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDictionaryReturnRelatedColumn(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Return Resource.GetDictionaryReturnRelatedColumn(ResourceID, ColumnDescription)
    End Function

    ''' <summary>
    ''' 返回字典的资源ID
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ColumnDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDictionaryReturnResourceID(ByVal ResourceID As String, ByVal ColumnDescription As String) As String
        Return Resource.GetDictionaryReturnResourceID(ResourceID, ColumnDescription)
    End Function

    <WebMethod()> _
    Public Function SendMail(ByVal DisplayName As String, ByVal FromEmail As String, ByVal ToMail As String, ByVal Subject As String, ByVal Body As String) As Boolean

        Return SimpleMail.Send(DisplayName, FromEmail, ToMail, Subject, Body)
    End Function
    <WebMethod()> _
    Public Function GetRelationTable(ByVal ResourceID As String) As DataSet
        Return Resource.GetRelationTable(ResourceID).DataSet
    End Function
    <WebMethod()> _
    Public Function GetRelationByResourceID(ByVal ResourceID As String, ByVal IsGetInputRelated As Boolean) As DataSet
        Return Resource.GetRelation(ResourceID, IsGetInputRelated)
    End Function

    <WebMethod()> _
    Public Function GetDocumentToBinary(ByVal FullFileName As String) As Byte()
        Return Document.GetDocumentToBinary(FullFileName)
    End Function

    <WebMethod()> _
    Public Function GetPageOfDocuments(ByVal ResourceDescription As String, ByVal DocmentName As String, ByVal PageParameter As PageParameter) As DataSet
        Return Document.GetPageOfDocuments(ResourceDescription, DocmentName, PageParameter, "", "")
    End Function
    <WebMethod()> _
    Public Function Decrypt(ByVal password As String) As String
        Return Encrypt.GetDecryptKey(password)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="ColumnDescription">列中文描述</param>
    ''' <param name="IsUpdate">是否更新自动编码值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetAutoCode1")> _
    Public Function GetAutoCode(ByVal ResourceID As Long, ByVal ColumnDescription As String, ByVal IsUpdate As Boolean) As String
        Return AutoCode.GetAutoCode(ResourceID, ColumnDescription, IsUpdate)
    End Function
    ''' <summary>
    ''' 获取某个资源列的自动编码
    ''' </summary>
    ''' <param name="ResourceDescription">资源描述</param>
    ''' <param name="ColumnDescription">列名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True, MessageName:="GetAutoCode2")> _
    Public Function GetAutoCode(ByVal ResourceDescription As String, ByVal ColumnDescription As String) As String
        Return AutoCode.GetAutoCode(ResourceDescription, ColumnDescription)
    End Function
    ''' <summary>
    ''' 返回该资源下所有下拉框
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetColumnOptionListByResourceID(ByVal ResourceID As String) As DataSet
        Return Resource.GetColumnOptionListByResourceID(ResourceID)
    End Function

    ''' <summary>
    ''' 返回该文档的二进制数组
    ''' </summary>
    ''' <param name="DocumentID">文档ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetBytesByDocumentID(ByVal DocumentID As String) As Byte()
        Return Document.GetBytesByDocumentID(DocumentID)
    End Function

    <WebMethod()> _
    Public Function PortalUploadFile(ByVal Cms_DocumentCenterId As String, ByVal KeyWords As String, ByVal ResourceID As String, ByVal strFilePath As String, ByVal UserID As String, ByVal FileName As String, ByVal Size As Long, ByVal FieldValueList As FieldInfo()) As Boolean
        Return Common.UploadFile(Cms_DocumentCenterId, KeyWords, ResourceID, strFilePath, UserID, FileName, Size, FieldValueList)
    End Function

    <WebMethod()> _
    Public Function EditByCondition(ByVal ResourceID As String, ByVal Condition As String, ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Boolean
        Return Common.EditByCondition(ResourceID, Condition, UserID, FieldValueList)
    End Function
    ''' <summary>
    ''' GetSqlByResourceID-zwb
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetSqlByResourceID(ByVal ResourceID As String, ByVal Condition As String, ByVal UserID As String) As String
        Return Resource.GetSqlByResourceID(ResourceID, UserID, Condition, True)
    End Function

    ''' <summary>
    '''  Query-zwb
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function Query(ByVal sql As String, ByVal timeTicks As String, ByVal randomNum As String, ByVal keyPass As String) As DataSet
        If Common.IsTrue(timeTicks, randomNum, keyPass) Then
            Return SDbStatement.Query(sql)
        Else
            Return Nothing
        End If
    End Function

    <WebMethod()> _
    Public Function DecryptPassword(ByVal password As String) As String
        Return Encrypt.Decrypt(password)
    End Function
    <WebMethod()> _
    Public Function EncryptPassword(ByVal password As String) As String
        Return Encrypt.Encrypt(password)
    End Function

    <WebMethod()> _
    Public Function CommitUpdate(ByVal selectSql As String, ByVal ds As DataSet) As Boolean
        Return Transaction.CommitUpdate(selectSql, ds)
    End Function

    <WebMethod()> _
    Public Function GetMTSearch(ByVal ResourceID As String, ByVal GainerObjectID As String) As DataSet
        Return MTSearch.GetMTSearch(ResourceID, GainerObjectID)
    End Function

    <WebMethod()> _
    Public Function GetConnectString() As String
        Return Transaction.GetConnectString()
    End Function

    <WebMethod()> _
    Public Function DeleteMTSearch(ByVal MTS_ID As String) As Boolean
        Return MTSearch.DeleteMTSearch(MTS_ID)
    End Function

    <WebMethod()> _
    Public Function GetMTSCol_ColCond() As List(Of FieldInfo)
        Return MTSearch.GetMTSCol_ColCond()
    End Function

    <WebMethod()> _
    Public Function UpdateMTSearch(ByVal Mts_ID As Long, ByVal ResourceID As String, ByVal Mts_EmpID As String, ByVal ColName As String, ByVal ColDispName As String, ByVal ColCond As String, ByVal ColCond_EN As String, ByVal ColValue As String, ByVal ColValue_EN As String, ByVal Loglc As String) As Boolean
        Return MTSearch.UpdateMTSearch(Mts_ID, ResourceID, Mts_EmpID, ColName, ColDispName, ColCond, ColCond_EN, ColValue, ColValue_EN, Loglc, "admin")
    End Function

    <WebMethod()> _
    Public Function GetFieldNameSum(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal IsRowRights As Boolean, ByVal fieldName As String) As String
        Return Common.GetFieldNameSum(ResourceID, UserID, Condition, IsRowRights, fieldName)
    End Function


    <WebMethod()> _
    Public Function GetFieldNameSumList(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal IsRowRights As Boolean, ByVal fieldName As String) As DataTable
        Return Common.GetFieldNameSumList(ResourceID, UserID, Condition, IsRowRights, fieldName)
    End Function

    <WebMethod()> _
    Public Function GetCurrentMilliseconds() As String
        Return Common.GetCurrentMilliseconds()
    End Function
End Class
