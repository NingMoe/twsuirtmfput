Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports NetReusables

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Integral
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function Add(ByVal UserID As String, ByVal Action As String, ByVal CreateTime As DateTime) As Boolean

        Dim ResourceID As String = Common.GetResourceIDByDescription("动作日志")

        Dim Score As Integer = GetSocreByUserIDAndAction(UserID, Action)

        Dim Fieldinfo() As FieldInfo
        ReDim Fieldinfo(3)
        Dim fi As FieldInfo

        fi = New FieldInfo
        fi.FieldDescription = "用户ID"
        fi.FieldValue = UserID
        Fieldinfo(0) = fi

        fi = New FieldInfo
        fi.FieldDescription = "动作"
        fi.FieldValue = Action
        Fieldinfo(1) = fi


        fi = New FieldInfo
        fi.FieldDescription = "积分变动"
        fi.FieldValue = Score
        Fieldinfo(2) = fi

        fi = New FieldInfo
        fi.FieldDescription = "时间"
        fi.FieldValue = CreateTime
        Fieldinfo(3) = fi

        If Common.Add(ResourceID, UserID, Fieldinfo) = True Then
            Return UpdateMain(UserID, CreateTime)
        Else
            Return False
        End If





    End Function

    <WebMethod()> _
    Public Function SelectByUserID(ByVal UserID As String) As DataTable
        Dim ResourceID As String = Common.GetResourceIDByDescription("用户积分")

        Dim dt As DataTable = Common.GetDataListByResourceID(ResourceID, False, " and 用户ID='" + UserID + "'").Tables(0)
        Return dt

    End Function
    <WebMethod()> _
    Public Function SelectAll() As DataTable
        Dim ResourceID As String = Common.GetResourceIDByDescription("用户积分")

        Return Common.GetDataListByResourceID(ResourceID, "", False).Tables(0)


    End Function

    Public Function UpdateMain(ByVal UserID As String, ByVal CreateTime As DateTime) As Boolean

        Dim ResourceID As String = Common.GetResourceIDByDescription("用户积分")

        Dim dt As DataTable = Common.GetDataListByResourceID(ResourceID, False, " and 用户ID='" + UserID + "'").Tables(0)
        If dt.Rows.Count > 0 Then

            Dim Fieldinfo() As FieldInfo
            ReDim Fieldinfo(1)
            Dim fi As FieldInfo

            fi = New FieldInfo
            fi.FieldDescription = "总积分"
            
            fi.FieldValue = SumScore(UserID)


            Fieldinfo(0) = fi

            fi = New FieldInfo
            fi.FieldDescription = "更新时间"
            fi.FieldValue = CreateTime
            Fieldinfo(1) = fi

            Return Common.Edit(ResourceID, dt.Rows(0)("id").ToString, "", Fieldinfo)
        Else

            Dim Fieldinfo() As FieldInfo
            ReDim Fieldinfo(2)
            Dim fi As FieldInfo

            fi = New FieldInfo
            fi.FieldDescription = "总积分"
            fi.FieldValue = SumScore(UserID)
            Fieldinfo(0) = fi

            fi = New FieldInfo
            fi.FieldDescription = "更新时间"
            fi.FieldValue = CreateTime
            Fieldinfo(1) = fi

            fi = New FieldInfo
            fi.FieldDescription = "用户ID"
            fi.FieldValue = UserID
            Fieldinfo(2) = fi


            Return Common.Add(ResourceID, "", Fieldinfo)

        End If



    End Function

    Public Function SumScore(ByVal UserID As String) As Integer
        Dim ResourceID As String = Common.GetResourceIDByDescription("动作日志")

        Dim sql As String = Resource.GetSqlByResourceID(ResourceID, " and 用户ID='" + UserID + "'")
        'Common.getsq()

        sql = "select isnull(sum([积分变动]),0) from (" + sql + ") T"


        Return SDbStatement.Query(sql).Tables(0).Rows(0)(0)
    End Function
    <WebMethod()> _
       Public Function Delete(ByVal ID As String) As Boolean

        Dim ResourceID As String = Common.GetResourceIDByDescription("动作日志")
        Dim dt As DataTable = Common.GetDataListByResourceID(ResourceID, False, " and id=" + ID).Tables(0)


        If Common.Delete(ResourceID, ID, "") Then
            Return UpdateMain(dt.Rows(0)("用户ID"), dt.Rows(0)("时间"))
        Else
            Return False
        End If

      
    End Function
    Public Function GetSocreByUserIDAndAction(ByVal UserID As String, ByVal Action As String) As Integer

        Dim ResourceID As String = Common.GetResourceIDByDescription("积分规则")
        Dim TableName As String = ""

        Dim RoleColName As String = ""
        Dim ActionColName As String = ""
        Dim ScoreColName As String = ""
        If ResourceID = "" Then
            Return 0
        Else
            TableName = Common.GetTableNameByDescription("积分规则")
            Dim dt As DataTable = Resource.GetColumnsByResouceID(ResourceID)
            For Each row As DataRow In dt.Rows

                If row("CD_DISPNAME") = "角色" Then
                    RoleColName = row("CD_COLNAME")
                ElseIf row("CD_DISPNAME") = "动作" Then
                    ActionColName = row("CD_COLNAME")
                ElseIf row("CD_DISPNAME") = "积分" Then
                    ScoreColName = row("CD_COLNAME")
                End If
            Next
            If RoleColName <> "" And ActionColName <> "" Then
                Dim sql As String = "select " + ScoreColName + " from " + TableName + " where " + ActionColName + "='" + Action + "' and " + RoleColName + " in (select name from CMS_DEPARTMENT D join CMS_DEP_VIRTUAL V on V.VDEP_DEPID=D.ID where VDEP_EMPID='" + UserID + "')"
                dt = SDbStatement.Query(sql).Tables(0)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If


        End If



    End Function
End Class
