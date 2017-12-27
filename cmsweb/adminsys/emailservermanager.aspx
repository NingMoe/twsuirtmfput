<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmailServerManager" CodeFile="EmailServerManager.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <TITLE id="onetidTitle">邮件服务器管理</TITLE>
        <META content="MSHTML 6.00.3790.0" name="GENERATOR">
        <META http-equiv="expires" content="0">
        <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
    </HEAD>
    <BODY>
        <FORM id="Form1" name="Form1" method="post" runat="server">
            <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td width="4"></td>
                    <td>
                        <TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="750" border="0">
                            <TR>
                                <TD colspan="2" class="header_level2" align="center" height="19">邮件服务器基本信息</TD>
                            </TR>
                            <TR>
                                <TD vAlign="middle" align="right" width="230" height="19"><asp:Label id="Label5" runat="server">发送方电子邮件：</asp:Label></TD>
                                <TD vAlign="middle" align="left" height="19"><asp:TextBox id="txtEmpSender" runat="server" Width="240px"></asp:TextBox>
                                    <asp:Label id="Label9" runat="server">格式:&nbsp;&nbsp;&nbsp;Jane&lt;zhangsan@test.com&gt;</asp:Label></TD>
                            </TR>
                            <TR>
                                <TD align="right" valign="middle" width="230" height="19">
                                    <asp:Label id="Label1" runat="server">系统发送邮件用SMTP：</asp:Label>
                                </TD>
                                <TD align="left" valign="middle" height="19">
                                    <asp:TextBox id="txtSmtpServer" runat="server" Width="240px"></asp:TextBox>
                                    <asp:Label id="Label10" runat="server">格式:&nbsp;&nbsp;&nbsp;smtp.test.com</asp:Label></TD>
                            </TR>
                            <TR>
                                <TD vAlign="middle" align="right" width="230" height="19"><asp:Label id="Label2" runat="server">SMTP校验帐号：</asp:Label></TD>
                                <TD vAlign="middle" align="left" height="19"><asp:TextBox id="txtSmtpUser" runat="server" Width="240px"></asp:TextBox></TD>
                            </TR>
                            <TR>
                                <TD vAlign="middle" align="right" width="230" height="19"><asp:Label id="Label3" runat="server">SMTP校验密码：</asp:Label></TD>
                                <TD vAlign="middle" align="left" height="19"><asp:TextBox id="txtSmtpPass" runat="server" Width="240px"></asp:TextBox></TD>
                            </TR>
                            <TR>
                                <TD vAlign="middle" align="right" width="230" height="23"></TD>
                                <TD vAlign="middle" align="left" height="23">
                                </TD>
                            </TR>
                            <TR>
                                <TD vAlign="middle" align="right" width="230" height="19"></TD>
                                <TD vAlign="middle" align="left" height="19">
                                    <asp:Button id="btnConfirm" runat="server" Width="96px" Text="确认"></asp:Button></TD>
                            </TR>
                        </TABLE>
                    </td>
                </tr>
            </TABLE>
        </FORM>
    </BODY>
</HTML>
