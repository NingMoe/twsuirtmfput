<%@ Page Language="vb" %>
<%@ Import namespace="NetReusables"%>
<%@ import namespace="Unionsoft.Platform"%>
<%@ import namespace="Cms.Web"%>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>SvcLogin</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="vb" runat="server">
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       Dim strUserName As String = Request("user").ToString()
	   dim pst As CmsPassport
        If strUserName <> "" Then
            Try
                    Dim strUserEncryptPass As String = Request("ucode").ToString() '加密后的密码
				
                Dim strUserPass As String = ""
                If strUserPass <> "" Then
                    pst = CmsPassport.GenerateCmsPassport(strUserName, strUserPass, Request.UserHostAddress)
                ElseIf strUserEncryptPass <> "" Then
                    'pst = CmsPassport.GenerateCmsPassportWithEncryptPass(strUserName, strUserEncryptPass, Request.UserHostAddress)
                    pst = CmsPassport.GenerateCmsPassport(strUserName, OrgFactory.EmpDriver.DecryptPass(strUserEncryptPass), Request.UserHostAddress)
                Else
                    pst = CmsPassport.GenerateCmsPassport(strUserName, "", Request.UserHostAddress)
                End If
                Session("CMS_PASSPORT") = pst
            Catch ex As Exception
                Response.Write(ex.Message)
                pst = Nothing
            End Try
        End If
        If not pst is nothing Then
            Try
               
                    Dim strMnuRecId As String = ""
                    if not Request("mnurecid") is nothing then
                    strMnuRecId =Request("mnurecid").ToString()
                    end if
                    
                    If strMnuRecId <> "" Then
                        Dim strMnuinmode As String = Request("mnuinmode").ToString()
                        Dim strMnuresid As String = Request("mnuresid").ToString()
                        Response.Redirect("cmshost/RecordEdit.aspx?mnuinmode=" & strMnuinmode & "&mnurecid=" & strMnuRecId & "&mnuresid=" & strMnuresid & "&mnuhostresid=" & strMnuresid & "&mnuformresid=" & strMnuresid)
                    else
						Dim strFlowEntryUrl As String = CmsConfig.GetString("SYS_CONFIG", "LOGIONPAGE")						
						If strFlowEntryUrl <> "" Then							
							If strFlowEntryUrl.ToLower() = "pageweb" Then
								Response.Redirect("/pageweb/Index.aspx?timeid=" & TimeId.CurrentMilliseconds(1) )
							Else
								Response.Redirect(strFlowEntryUrl & "?timeid=" & TimeId.CurrentMilliseconds(1))
							End If							
						Else
							Response.Redirect("cmshost/cmsframe.aspx?timeid=" & TimeId.CurrentMilliseconds(1))
						End If
                    End If
               
            Catch ex As Exception
                SLog.Err("ProjectLogin.aspx执行用于转换时发生错误.", ex)
               throw ex
            End Try
        End If

    End Sub
    </script>
  </head>
  <body MS_POSITIONING="GridLayout">

    <form id="Form1" method="post" runat="server">

    </form>

  </body>
</html>
