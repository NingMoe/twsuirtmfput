<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DomainLogin.aspx.vb" Inherits="DomainLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>正在登陆到域</title>
<style type="text/css">
<!--
.use_text {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #000000;
}
.use_text_line {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #000000;
	text-decoration: underline;
}
-->
</style>
<script>
function verification()
{
     if (document.getElementById("txtUsername").value=="")
     {
     
         alert('用户名不能为空');
         document.getElementById("txtUsername").focus();
         return false;
     }
     if (document.getElementById("txtPassword").value=="")
     {
     
         alert('密码不能为空');
         document.getElementById("txtPassword").focus();
         return false;
     }
    return true;
}
function CancelClick()
{
     document.getElementById("txtUsername").value="";
     document.getElementById("txtPassword").value="";
     document.getElementById("txtUsername").focus();
     return false;
}

</script>
</head>

<body>

<form id="form1"  runat=server>
<table width="320" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:150px">
  <tr>
    <td height="57"><img src="images/Domainkey.gif" width="320" height="60" /></td>
  </tr>
  <tr>
    <td height="201" align="center" valign="middle" bgcolor="#d4d0c8">
    <table width="288" height="130" border="0" cellpadding="0" cellspacing="0">
      
      <tr>
        <td align="left" class="use_text">用户名(<span class="use_text_line">U</span>):</td>
        <td align="left" class="use_text"><input runat=server style="width:175px" type="text"  id="txtUsername"/></td>
      </tr>
      <tr>
        <td align="left" class="use_text">密码(<span class="use_text_line">P</span>):</td>
        <td align="left" class="use_text"><input runat=server style="width:175px" type="password" id="txtPassword"/></td>
      </tr>
     
      <tr>
        <td align="left" class="use_text" style="height: 54px">&nbsp;</td>
        <td align="right" valign="bottom" class="use_text" style="height: 54px">
          <label>
            <asp:button  runat=server id="BtnLogin" style="width:80px" text="确定"  OnClientClick="return verification();"/>
            <asp:button  runat=server id="BtnCancel" style="width:80px" text="取消" OnClientClick="return CancelClick();"/>
            </label>
        </td>
      </tr>
    </table></td>
  </tr>
</table>
 </form>
</body>
</html>

