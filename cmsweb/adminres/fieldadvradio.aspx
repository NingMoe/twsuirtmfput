<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvRadio" validateRequest="false" CodeFile="FieldAdvRadio.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>��ѡһѡ����</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
    <script language="javascript">
<!--
//�������봰���и���Panel��λ�ú���ʾģʽ
function ListboxOptionClicked(){
  var selIndex = document.all.item("lboxOptValues").selectedIndex;
  var txtSelected = document.all.item("lboxOptValues").options(selIndex).text;
  document.all.item("txtOptValue").value = txtSelected;
}
//-->
    </script>
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
          <td width="4"></td>
          <td>
            <TABLE class="table_level2" style="WIDTH: 700px; HEIGHT: 310px" cellSpacing="0" cellPadding="0"
              width="700" border="0">
              <TR>
                <TD class="header_level2" colSpan="2" height="22"><b>��ѡһѡ��������</b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" height="12" width="118"><FONT face="����"> </FONT>
                </TD>
                <TD height="12"></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="lblResDispName" runat="server">��ǰ��Դ��</asp:label></TD>
                <TD>
                  <asp:TextBox id="txtResName" runat="server" Width="240px"></asp:TextBox></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="lblColDispName" runat="server">��ǰ�ֶΣ�</asp:label></TD>
                <TD>
                  <asp:TextBox id="txtFieldName" runat="server" Width="240px"></asp:TextBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" vAlign="top" align="right" width="118"><asp:label id="lblOptionDesc" runat="server">ѡ��������</asp:label></TD>
                <TD><FONT face="����"><asp:textbox id="txtOptionDesc" runat="server" TextMode="MultiLine" Height="40px" Width="552px"></asp:textbox></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="Label1" runat="server">ѡ��������������</asp:label></TD>
                <TD><asp:textbox id="txtDescRowNum" runat="server" Width="72px" MaxLength="2">1</asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="Label3" runat="server">ѡ�����з�ʽ��</asp:label></TD>
                <TD><asp:radiobutton id="rdoRow" runat="server" Text="��������" GroupName="rdoOptionDispStyle" Checked="True"></asp:radiobutton><FONT face="����">&nbsp;&nbsp;&nbsp;
                  </FONT>
                  <asp:radiobutton id="rdoCol" runat="server" Text="��������" GroupName="rdoOptionDispStyle"></asp:radiobutton></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118">
                  <asp:label id="Label2" runat="server">ѡ���ֵ��</asp:label></TD>
                <TD>
                  <asp:radiobutton id="rdoSaveOptionValue" runat="server" Checked="True" GroupName="OPTION_SAVE" Text="���������ѡ��ֵ"></asp:radiobutton><FONT face="����">&nbsp;</FONT>
                  <asp:radiobutton id="rdoSaveNumber" runat="server" GroupName="OPTION_SAVE" Text="�����1��ʼ��ѡ��ֵ����"></asp:radiobutton><FONT face="����">&nbsp;</FONT>
                  <asp:radiobutton id="rdoSaveABC" runat="server" GroupName="OPTION_SAVE" Text="�����A��ʼ��ѡ��ֵ����"></asp:radiobutton></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="lblOptionValues" runat="server">ѡ��ֵ��</asp:label></TD>
                <TD><asp:textbox id="txtOptValue" runat="server" Width="472px"></asp:textbox></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 118px" vAlign="top" align="right" width="118"></TD>
                <TD vAlign="top">
                  <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
                      <td style="WIDTH: 476px" vAlign="top" width="476"><asp:listbox id="lboxOptValues" runat="server" Height="324px" Width="472px"></asp:listbox></td>
                      <td vAlign="top"><asp:button id="btnAdd" runat="server" Width="84px" Text="���"></asp:button><FONT face="����"><BR>
                        </FONT>
                        <asp:button id="btnEdit" runat="server" Width="84px" Text="�޸�"></asp:button><FONT face="����"><BR>
                        </FONT>
                        <asp:button id="btnInsert" runat="server" Width="84px" Text="����"></asp:button><br>
                        <asp:button id="btnDel" runat="server" Width="84px" Text="ɾ��"></asp:button><FONT face="����"><BR>
                        </FONT><FONT face="����">
                          <BR>
                          <asp:button id="btnUp" runat="server" Width="84px" Text="�����ƶ�"></asp:button><BR>
                          <asp:button id="btnDown" runat="server" Width="84px" Text="�����ƶ�"></asp:button><BR>
                          <BR>
                          <BR>
                          <asp:button id="btnConfirm" runat="server" Width="84px" Text="ȷ��"></asp:button><BR>
                        </FONT><FONT face="����">
                          <asp:button id="btnCancel" runat="server" Width="84px" Text="ȡ��"></asp:button></FONT>
                      </td>
                    </tr>
                  </table>
                </TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
