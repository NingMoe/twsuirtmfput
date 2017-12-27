Imports System.Runtime.InteropServices
Imports System.Security.Principal
Public Class Domain

    Public Const LOGON32_LOGON_INTERACTIVE As Integer = 2
    Public Const LOGON32_PROVIDER_DEFAULT As Integer = 0

    Private impersonationContext As System.Security.Principal.WindowsImpersonationContext

    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function LogonUser(ByVal lpszUserName As String, ByVal lpszDomain As String, ByVal lpszPassword As String, ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, ByRef phToken As IntPtr) As Integer
    End Function
    <DllImport("advapi32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Auto, SetLastError:=True)> _
    Public Shared Function DuplicateToken(ByVal hToken As IntPtr, ByVal impersonationLevel As Integer, ByRef hNewToken As IntPtr) As Integer
    End Function
    ''' <summary>
    ''' �����û��������롢��¼���ж��Ƿ�ɹ�
    ''' </summary>
    ''' <example>
    ''' if (impersonateValidUser(UserName, Domain, Password)){}
    ''' </example>
    ''' <param name="userName">�˻����ƣ��磺string UserName = UserNameTextBox.Text;</param>
    ''' <param name="domain">Ҫ��¼�����磺string Domain   = DomainTextBox.Text;</param>
    ''' <param name="password">�˻�����, �磺string Password = PasswordTextBox.Text;</param>
    ''' <returns>�ɹ�����true,���򷵻�false</returns>
    Public Function ValidUser(ByVal userName As [String], ByVal domain As [String], ByVal password As [String]) As Boolean
        Dim tempWindowsIdentity As System.Security.Principal.WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero

        If LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
            If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                impersonationContext = tempWindowsIdentity.Impersonate()
                If impersonationContext IsNot Nothing Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
End Class
