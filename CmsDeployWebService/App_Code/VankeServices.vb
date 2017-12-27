Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement
Imports System.Collections.Generic
Imports System.EnterpriseServices
Imports System.Runtime.Serialization.Json

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.None)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class VankeServices
    Inherits System.Web.Services.WebService
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="IsGetChildren"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDataListByResourceID(ByVal ResourceID As String, ByVal IsGetChildren As Boolean, ByVal Condition As String) As String
        Dim ds As DataSet = Common.GetDataListByResourceID(ResourceID, IsGetChildren, Condition)  
        Dim timeConverter As New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
        Return Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
    End Function
 
    ''' <summary>
    ''' 通过ResID查询数据，支持视图，支持排序
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function GetDataListByResID(ByVal ResourceID As String, ByVal OrderBy As String, ByVal Condition As String, ByVal UserID As String) As String
        Dim ds As DataSet = Common.GetDataListByResID(ResourceID, OrderBy, Condition, UserID)
        Dim timeConverter As New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
        Return Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
    End Function
 
    <WebMethod(EnableSession:=True, MessageName:="GetDataListRecordCount3")> _
    Public Function GetDataListRecordCount(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String) As Int64
        Return Common.GetDataListRecordCount(ResourceID, UserID, Condition)
    End Function
     
    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList3")> _
    Public Function GetPageOfDataList(ByVal ResourceID As Int64, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal UserID As String) As String
        Dim ds As DataSet = Common.GetDataListPage(ResourceID, UserID, Condition, PageParameter)
        Dim timeConverter As New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
        Return Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
    End Function

    <WebMethod(EnableSession:=True, MessageName:="GetPageOfDataList4")> _
    Public Function GetPageOfDataList(ByVal ResourceID As Int64, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal UserID As String, ByVal IsShowChild As Boolean) As String
        Dim ds As DataSet = Common.GetDataListPage(ResourceID, UserID, Condition, PageParameter, IsShowChild)
        Dim timeConverter As New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
        Return Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
    End Function
 
     
    ''' <summary>
    ''' 返回特殊SQL语句下的数据集
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks>select <%表名.字段1%>,<%表名.字段2%> from [表名] join [table2] on <%表名.t%>=<%table2.t%></remarks>
    <WebMethod()> _
    Public Function SelectData(ByVal sql As String) As String
        Dim ds As DataSet = SDbStatement.Query(Common.translation(sql))
        Dim timeConverter As New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
        Return Newtonsoft.Json.JsonConvert.SerializeObject(ds, timeConverter)
    End Function
   
End Class
