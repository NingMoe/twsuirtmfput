<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceAdd" CodeFile="ResourceAdd.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
  <HEAD>
    <TITLE id="onetidTitle">������Դ</TITLE>
    <META content="MSHTML 6.00.3790.0" name="GENERATOR">
    <META http-equiv="expires" content="0">
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
  <BODY>
    <SCRIPT>
    </SCRIPT>
    <FORM id="Form1" name="Form1" action="" method="post" runat="server">
      <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
          <td width="4"></td>
          <td>
            <TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
              <TR>
                <TD height="22" colspan="2" class="header_level2"><b>������Դ����</b></TD>
              </TR>
              <TR>
                <TD align="right" width="104"><BR>
                </TD>
                <TD></TD>
              </TR>
              <TR height="22">
                <td width="104" align="right">
                  <asp:Label id="Label1" runat="server">��Դ���ƣ�</asp:Label></td>
                <TD>
                  <asp:TextBox id="txtResName" runat="server"></asp:TextBox>
                </TD>
              </TR>
              <TR>
                <TD align="right" width="104">
                  <asp:Label id="lblTemplate" runat="server">������ģ�壺</asp:Label></TD>
                <TD>
                  <asp:DropDownList id="ddlTemplate" runat="server" Width="152px" Height="22px"></asp:DropDownList></TD>
              </TR>
              <TR>
                <td width="104"></td>
                <TD height="22"><asp:CheckBox id="chkInherit" runat="server" Text="�̳и���Դ���븸��Դ������ͬ�ı��ṹ��" Width="328px" ForeColor="Red"
                    Font-Bold="True"></asp:CheckBox></TD>
              </TR>
              <TR height="22">
                <td width="104"></td>
                <TD><BR>
                  <asp:Button id="btnSaveResource" runat="server" Text="ȷ��" Width="80px"></asp:Button>
                  <asp:Button id="btnCancle" runat="server" Text="ȡ��" Width="80px"></asp:Button>
                </TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </FORM>
  </BODY>
</HTML>
