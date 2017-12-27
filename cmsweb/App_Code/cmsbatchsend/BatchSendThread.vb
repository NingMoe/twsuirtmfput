Option Strict On
Option Explicit On 

Imports System.Threading

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Enum BatchSendType As Integer
    Unknown = 0
    Email = 1
    SMS = 2
End Enum

Public Enum ThreadStatus As Integer
    Idle = 0
    Running = 1
    Pause = 2
End Enum

Public Class BatchSendThread
    Private Shared g_intStatus As ThreadStatus = ThreadStatus.Idle '�߳�״̬

    Private Shared g_pst As CmsPassport = Nothing 'CmsPassport
    Private Shared g_lngMtsHostID As Long = 0 '���ͳ���б������¼ID
    Private Shared g_intBSendType As BatchSendType = BatchSendType.Unknown 'Ⱥ�����ͣ��ʼ� �� ����

    Private Shared g_strMsg As String = "" 'Ⱥ�������еĴ������ʾ��Ϣ

    Private Shared g_intTotalNum As Integer = 0 '������ֻ����벻Ϊ�յ��ܼ�¼��
    'Private Shared g_intRecordNum As Integer = 0 '��Ч������ֻ�������ܼ�¼��
    Private Shared g_intSentNum As Integer = 0 'ʵ�ʷ��ͳɹ���������ֻ�������ܼ�¼��

    Public Shared Sub StopThread()
        g_intStatus = ThreadStatus.Idle
    End Sub

    Public Shared Sub PauseThread()
        g_intStatus = ThreadStatus.Pause
    End Sub

    Public Shared Sub ResumeThread()
        g_intStatus = ThreadStatus.Running
    End Sub

    Public Shared Function GetStatus() As ThreadStatus
        Return g_intStatus
    End Function

    Public Shared Property MtsHostID() As Long
        Get
            Return g_lngMtsHostID
        End Get
        Set(ByVal Value As Long)
            g_lngMtsHostID = Value
        End Set
    End Property

    Public Shared Property BSendType() As BatchSendType
        Get
            Return g_intBSendType
        End Get
        Set(ByVal Value As BatchSendType)
            g_intBSendType = Value
        End Set
    End Property

    Public Shared Property CmsPass() As CmsPassport
        Get
            Return g_pst
        End Get
        Set(ByVal Value As CmsPassport)
            g_pst = Value
        End Set
    End Property

    Public Shared Property ThreadMessage() As String
        Get
            Return g_strMsg
        End Get
        Set(ByVal Value As String)
            g_strMsg = Value
        End Set
    End Property

    Public Shared Property TotalNum() As Integer
        Get
            Return g_intTotalNum
        End Get
        Set(ByVal Value As Integer)
            g_intTotalNum = Value
        End Set
    End Property

    'Public Shared Property RecordNum() As Integer
    '    Get
    '        Return g_intRecordNum
    '    End Get
    '    Set(ByVal Value As Integer)
    '        g_intRecordNum = Value
    '    End Set
    'End Property

    Public Shared Property SentNum() As Integer
        Get
            Return g_intSentNum
        End Get
        Set(ByVal Value As Integer)
            g_intSentNum = Value
        End Set
    End Property

    '--------------------------------------------------------------------
    '�߳����
    '--------------------------------------------------------------------
    Public Shared Sub Run()
        Try
            SLog.Fatal("ϵͳȺ����̨�̳߳ɹ�������")

            '--------------------------------------------------------------------
            '��ʼ��Ⱥ��������״̬
            g_intStatus = ThreadStatus.Running
            g_strMsg = ""
            g_intTotalNum = 0
            g_intSentNum = 0
            '--------------------------------------------------------------------

            '--------------------------------------------------------------------
            '��ȡ�ʼ��ֶλ�����ֶ�
            Dim lngResID As Long = CmsDbBase.GetFieldLng(g_pst, CmsTables.MTableHost, "MTS_RESID", "MTS_ID=" & g_lngMtsHostID)
            Dim strResTable As String = g_pst.GetDataRes(lngResID).ResTable
            Dim strSendColumn As String
            If g_intBSendType = BatchSendType.Email Then
                strSendColumn = CmsDbBase.GetFieldStr(g_pst, CmsTables.MTableColDef, "MTSCOL_COLNAME", "MTSCOL_HOSTID=" & g_lngMtsHostID & " AND MTSCOL_TYPE=" & MTSearchColumnType.Email)
            ElseIf g_intBSendType = BatchSendType.SMS Then
                strSendColumn = CmsDbBase.GetFieldStr(g_pst, CmsTables.MTableColDef, "MTSCOL_COLNAME", "MTSCOL_HOSTID=" & g_lngMtsHostID & " AND MTSCOL_TYPE=" & MTSearchColumnType.MobilePhone)
            End If
            Dim strWhere As String = MultiTableSearchColumn.GetWhereOfColCondition(g_pst, g_lngMtsHostID)
            SLog.Info("test strSendColumn=" & strSendColumn)
            SLog.Info("test strWhere=" & strWhere)

            '��ȡ�ʼ������Ż�����Ϣ
            Dim datParam As DataParameter = Nothing
            Dim strEmailTitle As String = ""
            Dim strEmailContent As String = ""
            Dim strSmsContent As String = ""
            Dim intEmailFormat As Mail.MailFormat
            Dim intSmsPort As Integer = 1
            Dim hashFieldVal As Hashtable = CmsDbBase.GetRecordByWhere(CmsPass, CmsTables.MTableBatchSend, "BSEND_HOSTID=" & g_lngMtsHostID)
            If g_intBSendType = BatchSendType.Email Then
                datParam = DbParameter.GetParameter(SDbConnectionPool.GetDbConfig(), DbParameter.BSEND_SMTP)
                If datParam Is Nothing OrElse datParam.strPARM_STR1 = "" OrElse datParam.strPARM_STR2 = "" OrElse datParam.strPARM_STR4 = "" OrElse datParam.strPARM_STR4.StartsWith("<") = True OrElse datParam.strPARM_STR4.IndexOf("<") < 0 OrElse datParam.strPARM_STR4.IndexOf(">") < 0 OrElse datParam.strPARM_STR4.IndexOf("@") < 0 OrElse datParam.strPARM_STR4.IndexOf(".") < 0 Then
                    g_strMsg &= "Ⱥ���ʼ�ǰ���������ʼ���������Ϣ��" & Environment.NewLine
                    g_intStatus = ThreadStatus.Idle
                    Return
                End If
                strEmailTitle = HashField.GetStr(hashFieldVal, "BSEND_EMAIL_TITLE")
                strEmailContent = HashField.GetStr(hashFieldVal, "BSEND_EMAIL_CONTENT")
                intEmailFormat = CType(HashField.GetInt(hashFieldVal, "BSEND_EMAIL_TYPE"), Mail.MailFormat)
            ElseIf g_intBSendType = BatchSendType.SMS Then
                strSmsContent = HashField.GetStr(hashFieldVal, "BSEND_SMS_CONTENT")
                intSmsPort = HashField.GetInt(hashFieldVal, "BSEND_SMS_PORT")
            End If
            'SLog.Info("strEmailTitle=" & strEmailTitle)
            'SLog.Info("strEmailContent=" & strEmailContent)
            'SLog.Info("strSmsContent=" & strSmsContent)
            'SLog.Info("intSmsPort=" & intSmsPort)
            '--------------------------------------------------------------------

            '--------------------------------------------------------------------
            '��ȡ�ʼ��б�
            Dim dv As DataView = GetColumnList(lngResID, strResTable, strSendColumn, strWhere)
            If dv Is Nothing Then
                g_intStatus = ThreadStatus.Idle
                Return
            End If
            g_intTotalNum = dv.Count
            SLog.Info("Ⱥ��������=" & g_intTotalNum)
            '--------------------------------------------------------------------

            '--------------------------------------------------------------------
            '��ʼ�̵߳�Ⱥ������
            Dim drv As DataRowView
            For Each drv In dv
                Dim strColValue As String = DbField.GetStr(drv, strSendColumn)
                Try
                    If g_intBSendType = BatchSendType.Email Then
                        If IsValideEmail(strColValue) Then '��ʼ�����ʼ�
                            SimpleEmail.SendEmail(datParam.strPARM_STR4, strColValue, "", strEmailTitle, strEmailContent, datParam.strPARM_STR1, datParam.strPARM_STR2, datParam.strPARM_STR3, , , intEmailFormat)
                            g_intSentNum += 1
                            SLog.Info("����һ���ʼ��ɹ������䣺" & strColValue)
                        Else
                            SLog.Err("����һ���ʼ�ʧ�ܣ�Ŀ�꣺" & strColValue & "������Ⱥ������")
                        End If
                    ElseIf g_intBSendType = BatchSendType.SMS Then
                        If IsValideHandset(strColValue) Then '��ʼ���Ͷ���
                            SimpleSms.Send(strSmsContent, strColValue, intSmsPort)
                            g_intSentNum += 1
                            SLog.Info("����һ�����ųɹ����ֻ���" & strColValue)
                        Else
                            SLog.Err("����һ������ʧ�ܣ�Ŀ�꣺" & strColValue & "������Ⱥ������")
                        End If
                    End If
                Catch ex As Exception
                    SLog.Err("����һ���ʼ������ʧ�ܣ�Ŀ�꣺" & strColValue & "������Ⱥ������", ex)
                End Try
                'g_intRecordNum += 1

                '--------------------------------------------------------------------
                'У��״̬
                If g_intStatus = ThreadStatus.Pause Then '��ǰ��Pause״̬
                    While True
                        If g_intStatus <> ThreadStatus.Pause Then Exit While
                        Thread.Sleep(500)
                    End While
                End If

                If g_intStatus = ThreadStatus.Idle Then
                    Exit For
                ElseIf g_intStatus = ThreadStatus.Running Then
                    Thread.Sleep(100) 'ÿ��100����Ⱥ��һ���ʼ�����ţ�Ϊ�˲�����ռ��ϵͳCPU
                End If
                '--------------------------------------------------------------------
            Next
            '--------------------------------------------------------------------

            Dim hashTemp As New Hashtable
            hashTemp.Add("BSEND_HOSTID", g_lngMtsHostID)
            If g_intBSendType = BatchSendType.Email Then
                hashTemp.Add("BSEND_EMAIL_TIMES", HashField.GetInt(hashFieldVal, "BSEND_EMAIL_TIMES") + 1)
                hashTemp.Add("BSEND_EMAIL_TOTALNUM", g_intTotalNum)
                hashTemp.Add("BSEND_EMAIL_SENTNUM", g_intSentNum)
            ElseIf g_intBSendType = BatchSendType.SMS Then
                hashTemp.Add("BSEND_SMS_TIMES", HashField.GetInt(hashFieldVal, "BSEND_SMS_TIMES") + 1)
                hashTemp.Add("BSEND_SMS_TOTALNUM", g_intTotalNum)
                hashTemp.Add("BSEND_SMS_SENTNUM", g_intSentNum)
            End If
            CmsDbBase.AddOrEditRecordByWhere(CmsPass, CmsTables.MTableBatchSend, hashTemp, "BSEND_HOSTID=" & g_lngMtsHostID, "BSEND_ID", "BSEND_SHOWORDER")

            g_intStatus = ThreadStatus.Idle
            SLog.Fatal("�ʼ������̳߳ɹ���ɣ�Ⱥ��������" & g_intSentNum)
        Catch ex As Exception
            SLog.Err("�߳������쳣��������Ⱥ�������Ƿ���ȷ��", ex)
            g_strMsg &= "�߳������쳣��������Ⱥ�������Ƿ���ȷ��" & Environment.NewLine
        End Try
    End Sub

    '--------------------------------------------------------------------
    '��ȡ�ʼ����ֻ�������б�
    '--------------------------------------------------------------------
    Private Shared Function GetColumnList(ByVal lngResID As Long, ByVal strResTable As String, ByVal strSendColumn As String, ByVal strWhere As String) As DataView
        Try
            strWhere = CmsWhere.AppendAnd(strWhere, "RESID=" & lngResID & " AND " & strSendColumn & "<>'' AND NOT (" & strSendColumn & " IS NULL)")
            Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "SELECT " & strSendColumn & " FROM " & strResTable & " WHERE " & strWhere)
            Return ds.Tables(0).DefaultView
        Catch ex As Exception
            SLog.Err("��ȡ�ʼ�/���ű����ֶ���Ϣ�쳣ʧ�ܣ�����Ⱥ�������Ƿ���ȷ��")
            g_strMsg &= "��ȡ�ʼ�/���ű����ֶ���Ϣ�쳣ʧ�ܣ�����Ⱥ�������Ƿ���ȷ��" & Environment.NewLine
            g_intStatus = ThreadStatus.Idle
            Return Nothing
        End Try
    End Function

    '--------------------------------------------------------------------
    '�ж������Ƿ���Ч
    '--------------------------------------------------------------------
    Public Shared Function IsValideEmail(ByVal strEmail As String) As Boolean
        If strEmail.IndexOf("@") <= 0 OrElse strEmail.IndexOf(".") <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    '--------------------------------------------------------------------
    '�ж��ֻ������Ƿ���Ч
    '--------------------------------------------------------------------
    Public Shared Function IsValideHandset(ByVal strHandset As String) As Boolean
        If IsNumeric(strHandset) = False Then Return False
        If strHandset.Length < 11 Then Return False
        Return True
    End Function
End Class

End Namespace
