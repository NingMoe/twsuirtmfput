<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceCreateTable" CodeFile="ResourceCreateTable.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>ResourceCreateTable</title>
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
          <td width="4"></td>
          <td>
            <TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
              <TR>
                <TD height="22" colspan="2" class="header_level2"><b>��������Դ</b></TD>
              </TR>
              <TR>
                <TD height="5" colspan="2"><FONT face="����"><BR>
                  </FONT>
                </TD>
              </TR>
              <TR height="22">
                <TD align="right" width="130" style="WIDTH: 130px; HEIGHT: 9px">
                  <asp:Label id="Label2" runat="server">�����ͣ�</asp:Label></TD>
                <TD style="HEIGHT: 9px">
                  <asp:DropDownList id="ddlResType" runat="server" Height="22px" Width="152px"></asp:DropDownList>
                </TD>
              </TR>
              <TR>
                <TD style="WIDTH: 130px" align="right" width="130">
                  <asp:Label id="lblTableName" runat="server">�Զ�������ƣ�</asp:Label></TD>
                <TD><FONT face="����">
                    <asp:TextBox id="txtTableName" runat="server" Width="152px"></asp:TextBox>
                    <asp:Label id="lblComments" runat="server">���߼�ѡ�ϵͳʵʩ��Աר�ã�</asp:Label>
                  </FONT>
                </TD>
              </TR>
              <TR height="22">
                <td style="WIDTH: 130px"></td>
                <TD><FONT face="����"><BR>
                  </FONT>
                  <asp:Button id="btnCreateTable" runat="server" Text="������" Width="80px"></asp:Button>
                  <asp:Button id="btnCancle" runat="server" Width="56px" Text="ȡ��"></asp:Button>
                </TD>
              </TR>
              <TR>
                <TD height="5" colspan="2"></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
