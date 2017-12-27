<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.BatchSendWorking" CodeFile="BatchSendWorking.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>群发线程</title>
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
    </HEAD>
    <body>
        <form id="Form1" method="post" runat="server">
            <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td width="4"></td>
                    <td>
                        <TABLE class="table_level2" style="WIDTH: 712px" cellSpacing="0" cellPadding="0" width="712"
                            border="0">
                            <TR>
                                <TD class="header_level2" colSpan="2" height="22"><b><asp:label id="lblTitle" runat="server">Label</asp:label></b></TD>
                            </TR>
                            <TR>
                                <TD style="WIDTH: 168px; HEIGHT: 18px" align="right" width="168"></TD>
                                <TD style="HEIGHT: 18px"></TD>
                            </TR>
                            <TR height="25">
                                <TD style="WIDTH: 168px" align="right" width="168"><asp:label id="Label3" runat="server">群发标题：</asp:label></TD>
                                <TD><asp:textbox id="txtTitle" runat="server" ReadOnly="True" Width="216px"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD style="WIDTH: 168px" align="right" width="168"><asp:label id="Label1" runat="server">当前状态：</asp:label></TD>
                                <TD><asp:textbox id="txtStatus" runat="server" ReadOnly="True" Width="216px" ForeColor="Red"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD style="WIDTH: 168px" align="right" width="168"><asp:label id="Label4" runat="server">待群发总数量(最近一次)：</asp:label></TD>
                                <TD><asp:textbox id="txtTotalNum" runat="server" ReadOnly="True" Width="216px"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD style="WIDTH: 168px" align="right" width="168"><asp:label id="Label5" runat="server">群发成功计数(最近一次)：</asp:label></TD>
                                <TD><asp:textbox id="txtCounter" runat="server" ReadOnly="True" Width="216px"></asp:textbox><FONT face="宋体">&nbsp;</FONT><asp:linkbutton id="lbtnRefresh" runat="server">更新计数</asp:linkbutton></TD>
                            </TR>
                            <TR>
                                <TD style="WIDTH: 168px" align="right" width="168"><asp:label id="Label6" runat="server">群发操作总次数：</asp:label></TD>
                                <TD><asp:textbox id="txtBSendTimes" runat="server" ReadOnly="True" Width="216px"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD style="WIDTH: 168px" align="right" width="168"></TD>
                                <TD></TD>
                            </TR>
                            <TR height="25">
                                <TD style="WIDTH: 168px"></TD>
                                <TD><asp:button id="btnStart" runat="server" Width="80px" Text="开始群发"></asp:button><asp:button id="btnPause" runat="server" Width="80px" Text="暂停"></asp:button><asp:button id="btnResume" runat="server" Width="80px" Text="恢复"></asp:button><asp:button id="btnStop" runat="server" Width="80px" Text="中止"></asp:button><FONT face="宋体">&nbsp;
                                    </FONT>
                                    <asp:button id="btnExit" runat="server" Width="80px" Text="退出"></asp:button></TD>
                            </TR>
                        </TABLE>
                    </td>
                </tr>
            </TABLE>
        </form>
    </body>
</HTML>
