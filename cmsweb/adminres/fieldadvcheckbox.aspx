<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCheckbox" validateRequest="false" CodeFile="FieldAdvCheckbox.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>��ѡһѡ����</title>
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
                <TD class="header_level2" colSpan="2" height="22"><b>��ѡһѡ��������</b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"><FONT face="����"><BR>
                  </FONT>
                </TD>
                <TD></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 140px" align="right" width="140"><asp:label id="lblResDispName" runat="server">��ǰ��Դ��</asp:label></TD>
                <TD><asp:label id="lblResName" runat="server"></asp:label></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 140px" align="right" width="140"><asp:label id="lblColDispName" runat="server">��ǰ�ֶΣ�</asp:label></TD>
                <TD><asp:label id="lblFieldName" runat="server"></asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" vAlign="top" align="right" width="140"><asp:label id="lblOptionDesc" runat="server">ѡ��������</asp:label></TD>
                <TD><FONT face="����"><asp:textbox id="txtOptionDesc" runat="server" Width="552px" Height="68px" TextMode="MultiLine"></asp:textbox></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"><asp:label id="Label1" runat="server">ѡ��ѡ�еı���ֵ��</asp:label></TD>
                <TD><asp:textbox id="txtYes" runat="server" Width="204px">1</asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"><FONT face="����"><asp:label id="Label2" runat="server">ѡ�ѡ�еı���ֵ��</asp:label></FONT></TD>
                <TD><asp:textbox id="txtNo" runat="server" Width="204px">0</asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right" width="140"></TD>
                <TD><asp:button id="btnConfirm" runat="server" Width="84px" Text="ȷ��"></asp:button><asp:button id="btnCancel" runat="server" Width="84px" Text="ȡ��"></asp:button></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
