<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>登录</title>
    <style type="text/css">
    <!--
    body{ background-color:#EFEFEF;overflow:hidden;}
    div{ 
	    position: absolute; 
	    width:534px;   
	    height:220px;   
	    left:50%;   
	    top:50%;   
	    margin-left:-267px;   
	    margin-top:-141px; 
    }
    
    
      
    .login_bg{
	    color: #000000;
	    font-size: 12px;
	    background: url(images/login_bg.jpg) no-repeat;
	 
    }
    
    .login_bg td{
	    color: #000000;
	    font-size: 12px;
	    
    }
    
    .input{
	    font-family: Arial, Helvetica, sans-serif;
	    color: blue;
	    text-decoration: none;
	    background: url(images/input_bg2.gif) repeat-x left top;
	    border: 1px solid #2f8ad0;
	    height:18px;
    }
    .bar{
	    font-family: Arial, Helvetica, sans-serif;
	    font-size: 10px;
	    line-height: 18px;
	    color: #000000;
	    text-decoration: none;
	    background: url(images/input_bg.gif) repeat-x;
	    border: 1px solid #6bacce;
	    height:18px;
    }
    -->
    </style>
   
</head>
<body> <form id="form1" runat="server">
<div style="left: 50%; top: 50%">
    <table width="534" border="0" cellspacing="0" cellpadding="0" style="height:52%">
    
   
       <tr>
        <td style="height:200" align="center" valign="bottom" class="login_bg">
            <table width="401" border="0" cellspacing="4" cellpadding="4">
            <tr>
    <td  colspan="3"  style="color:Red; font-size:large; margin-top:5px">认证中心</td>
    </tr>
                <tr>
                    <td width="106" height="20" align="right">用户名</td>
                    <td width="146" align="left"><asp:TextBox ID="txtUserID" runat="server" CssClass="input"></asp:TextBox></td>
                    <td align="left">
                        &nbsp;
                        </td>
                </tr>
                <tr>
                    <td height="20" align="right">密&nbsp;&nbsp;&nbsp;&nbsp;码</td>
                    <td align="left"><asp:TextBox ID="txtPassword" runat="server" TextMode="password" CssClass="input"></asp:TextBox></td>
                    <td width="133" align="left" valign="bottom">&nbsp;</td>
                </tr>
                <tr>
                    <td height="20" align="right">域&nbsp;&nbsp;&nbsp;&nbsp;名</td>
                    <td align="left"><asp:DropDownList ID="DropDownListDomain" runat="server" Width="152px"><asp:ListItem></asp:ListItem><asp:ListItem Text="shcrland" Value="LDAP://DC=SHCRLAND,DC=COM"></asp:ListItem></asp:DropDownList></td>
                    <td width="133" align="left" valign="bottom">&nbsp;</td>
                </tr>
                <tr>
                    <td height="60"></td>
                    <td align="left" valign="top" colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="bar"  Text=" 登 录 " /></td>
                </tr>
            </table></td>
      </tr>
   
    </table>
</div>    
<script type="text/javascript">
    document.getElementById('txtUserID').focus();
</script> </form>
</body>
</html>


