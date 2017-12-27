<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="NetReusables"%>
<%@ Page Language="vb" AutoEventWireup="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>登录中转站</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
<%
	Session("CMS_PASSPORT") = Nothing
    Dim pst As CmsPassport = Nothing

    '验证用户名和密码，开始登录
    Dim strUserName As String = Request.QueryString("user")
    If strUserName <> "" Then
        Try
            Dim strUserEncryptPass As String = Request.QueryString("ucode")  '加密后的密码
            If strUserEncryptPass <> "" Then
                Dim strDecryptPass As String = ""
                Try
                    strDecryptPass = NetReusables.Encrypt.Decrypt(strUserEncryptPass)
                    
                Catch
                    Try
                        strDecryptPass = CmsEncrypt.DecryptPassword(CmsEncrypt.GenerateFromAscii(strUserEncryptPass))
                    Catch ex As Exception

                    End Try
                End Try
                pst = CmsPassport.GenerateCmsPassport(strUserName, strDecryptPass, Request.UserHostAddress)
                
            Else
                pst = CmsPassport.GenerateCmsPassport(strUserName, "", Request.UserHostAddress)
            End If
            Session("CMS_PASSPORT") = pst
        Catch ex As Exception
            Response.Write(ex.Message)
            pst = Nothing
            Response.End
        End Try
    End If

    If pst Is Nothing Then
        Response.Write("参数错误!")
        Response.End()
    Else
        '登录成功
        Dim url As String = ""
        If Request.QueryString("targetpage") IsNot Nothing Then
            url = Request.QueryString("targetpage").Replace("@*", "&")
        End If
        If url <> "" Then
            '切换入指定页面
            Response.Redirect(url)
        Else
            Dim strNodeResId As String = ""
            If Request.QueryString("noderesid") IsNot Nothing Then strNodeResId = Request.QueryString("noderesid")
            If strNodeResId <> "" Then
                Response.Write("cmshost/cmsframe.aspx?noderesid=" & strNodeResId & "&timeid=" & TimeId.CurrentMilliseconds(1))
            Else
                Dim strMnuRecId As String = Request.QueryString("mnurecid")
                If strMnuRecId <> "" Then
                    Dim strMnuinmode As String = Request.QueryString("mnuinmode")
                    Dim strMnuresid As String = Request.QueryString("mnuresid")
                    Response.Redirect("cmshost/RecordEdit.aspx?mnuinmode=" & strMnuinmode & "&mnurecid=" & strMnuRecId & "&mnuresid=" & strMnuresid & "&mnuhostresid=" & strMnuresid & "&mnuformresid=" & strMnuresid)
                End If
            End If
        End If
        End If
%>
	</body>
</HTML>
