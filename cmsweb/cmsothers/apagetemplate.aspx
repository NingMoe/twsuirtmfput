<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.APageTemplate" CodeFile="APageTemplate.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <TITLE>ģ�崰��01</TITLE>
	<meta http-equiv="Pragma" content="no-cache" />
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <TABLE class="frame_table" cellSpacing="0" cellPadding="0">
        <tr>
          <td>
            <TABLE class="form_table" cellSpacing="0" cellPadding="0">
              <TR>
                <TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">ģ�崰��01</asp:Label></b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:Label id="Label8" runat="server">Ŀ����Դ</asp:Label></TD>
                <TD style="HEIGHT: 25px" align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:Label id="Label3" runat="server">ģ�����</asp:Label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:Label id="Label9" runat="server">Ԫ������</asp:Label></TD>
                <TD align="left"><asp:TextBox id="txtSqlDbName" runat="server" Width="196px"></asp:TextBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="ȷ��" OnClick="btnConfirm_Click"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="�˳�" OnClick="btnExit_Click"></asp:Button></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
