<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>办公平台登录</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <%-- <link href="css/cmsstyle.css" type="text/css" rel="stylesheet"/>--%>
    <style type="text/css">
        BODY {
            MARGIN: 0px;
            background-image: url(images/loginBG1.gif);
            background-size:100%;
            background-repeat: no-repeat;
        }

        .copyright {
            FONT-SIZE: 12px;
            COLOR: #4d4d4d;
            LINE-HEIGHT: 20px;
            padding-top: 2px;
            FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
        }

        .STYLE1 {
            FONT-SIZE: 12px;
            color: #4d4d4d;
        }

        .input {
            height: 20px;
            width: 124px;
            border: 1px solid #C2DBF8;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function loginCheck() {
            var context = document.getElementById("txtUserCode").value;
            context = context.replace(/^\s+|\s+$/g, "");
            if (context == "" || context == null) {
                alert("请输入用户名！");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form runat="server" id="Form1">

        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="29%" height="186">&nbsp;</td>
                <td width="38%">&nbsp;</td>
                <td width="33%">&nbsp;</td>
            </tr>
            <tr>
                <td height="154">&nbsp;</td>
                <td align="center" valign="top">
                    <table width="513" border="0" cellspacing="0" cellpadding="0">

                        <tr align="right" valign="middle">
                            <td width="513" height="258" align="center" background="images/loginbg1.png" style="background-position:center;">
                                <table width="300" height="143" border="0" cellpadding="0" cellspacing="2">
                                    <tr>
                                        <td style="width:100px;" height="47" align="right" class="words">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td height="13" align="right" class="words STYLE1" style="color:white">用户名:</td>
                                        <td style="text-align:center;">
                                            <asp:TextBox ID="txtUserCode" runat="server" CssClass="input" Style="height: 20px; width: 150px;"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="words STYLE1" style="color:white">密&nbsp;&nbsp;&nbsp;&nbsp;码:</td>
                                        <td style="text-align:center">
                                            <asp:TextBox ID="txtPwd" runat="server" CssClass="input" TextMode="password" Style="height: 20px; width: 150px;"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                        <td>
                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/images/tj.png" BorderStyle="none"  OnClick="btnSubmit_Click" Height="32px" Width="92px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
