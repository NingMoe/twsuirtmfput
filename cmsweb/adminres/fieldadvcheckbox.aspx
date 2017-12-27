<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCheckbox" validateRequest="false" CodeFile="FieldAdvCheckbox.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>多选一选择项</title>
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
            <TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="700" border="0">
              <TR>
                <TD class="header_level2" colSpan="2" height="22"><b>多选一选择项设置</b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"><FONT face="宋体"><BR>
                  </FONT>
                </TD>
                <TD></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 140px" align="right" width="140"><asp:label id="lblResDispName" runat="server">当前资源：</asp:label></TD>
                <TD><asp:label id="lblResName" runat="server"></asp:label></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 140px" align="right" width="140"><asp:label id="lblColDispName" runat="server">当前字段：</asp:label></TD>
                <TD><asp:label id="lblFieldName" runat="server"></asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" vAlign="top" align="right" width="140"><asp:label id="lblOptionDesc" runat="server">选项描述：</asp:label></TD>
                <TD><FONT face="宋体"><asp:textbox id="txtOptionDesc" runat="server" Width="552px" Height="68px" TextMode="MultiLine"></asp:textbox></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"><asp:label id="Label1" runat="server">选项选中的保存值：</asp:label></TD>
                <TD><asp:textbox id="txtYes" runat="server" Width="204px">1</asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"><FONT face="宋体"><asp:label id="Label2" runat="server">选项不选中的保存值：</asp:label></FONT></TD>
                <TD><asp:textbox id="txtNo" runat="server" Width="204px">0</asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"></TD>
                <TD><asp:button id="btnConfirm" runat="server" Width="84px" Text="确定"></asp:button><asp:button id="btnCancel" runat="server" Width="84px" Text="取消"></asp:button></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
