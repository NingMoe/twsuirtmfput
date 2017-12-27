<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserValidate" CodeFile="UserValidate.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.PageBase" %>
<%@ Import NameSpace="System.Text"%>
<%@ Import NameSpace="NetReusables"%>
<%@ Import NameSpace="Unionsoft.Workflow.Web"%>
<%@ Import NameSpace="Unionsoft.Workflow.Platform"%>
<%@ Import NameSpace="Unionsoft.Implement "%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>UserValidate</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body>
	<%
	    Dim strPassword As String = ""
	    Dim strDecryptPwd As String = ""
	    Dim strUserCode As String = Request.QueryString("user")
	    	  
	    strPassword = Request.QueryString("ucode")
	    Try
	        strDecryptPwd = Encrypt.Decrypt(strPassword)
	    Catch ex As Exception
	        Try
	            If strPassword <> "" Then
	                strPassword = CmsEncrypt.GenerateFromAscii(strPassword)
	            End If
	            Dim inByte As Byte() = System.Convert.FromBase64String(strPassword)
	            strPassword = Encoding.GetEncoding("UTF-16").GetString(inByte)
	            strDecryptPwd = CmsEncrypt.Decrypt(strPassword)
	        Catch ex1 As Exception
	            SLog.Err("UserValidate.aspx执行错误.", ex)
	            Response.Write("用户登录帐号或密码错误，请重新输入!")
	        End Try
	    End Try
	    
	    
    Dim strUrl As String = Request.QueryString("url")
    Dim oEmployee As Employee
    If strUserCode <> "" Then
        Try
	            oEmployee = OrganizationFactory.Implementation.GetEmployee(strUserCode)
	            SLog.Err("Password:" + strPassword)
            If strPassword<>"" Then 
	                oEmployee.Password = strDecryptPwd
	            End If
	            SLog.Err("oEmployee.Password :" + oEmployee.Password)
            Session("User") = oEmployee
            If oEmployee.Code <> "" Then
                If Not strUrl="" Then
					Response.Redirect(strUrl)
                Else
                    Response.Redirect("index.aspx")
                End If
            End If
        Catch ex As Exception
            SLog.Err("UserValidate.aspx执行错误.", ex)
            Response.Write("用户登录帐号或密码错误，请重新输入!")
        End Try
    Else
		Response.Write("参数错误,未传入正确的用户名.")
    End If
	%>
	</body>
</html>

