<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceSyncListEdit" CodeFile="ResourceSyncListEdit.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>����ͬ��</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <TABLE class="frame_table" cellSpacing="0" cellPadding="0">
        <tr>
          <td>
            <TABLE class="form_table" cellSpacing="0" cellPadding="0">
              <TR>
                <TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">����ͬ��</asp:Label></b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
                <TD style="HEIGHT: 12px" align="left"></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:Label id="lblResName" runat="server">����Դ����</asp:Label></TD>
                <TD align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:Label id="Label1" runat="server">ͬ����Դ����</asp:Label></TD>
                <TD align="left"><FONT face="����">
                    <asp:TextBox id="txtSyncResName" runat="server" Width="196px"></asp:TextBox>
                    <asp:LinkButton id="lbtnSelectRes" runat="server">ѡ����Դ</asp:LinkButton></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:Label id="lblSyncAction" runat="server">ͬ������</asp:Label></TD>
                <TD align="left">
                  <asp:RadioButton id="rdoActionAdd" runat="server" Text="��Ӽ�¼" GroupName="SYNC_ACTION" Checked="True"></asp:RadioButton><FONT face="����">&nbsp;
                    <asp:RadioButton id="rdoActionEdit" runat="server" Text="�޸ļ�¼" GroupName="SYNC_ACTION"></asp:RadioButton>&nbsp;
                    <asp:RadioButton id="rdoActionDel" runat="server" Text="ɾ����¼" GroupName="SYNC_ACTION"></asp:RadioButton></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 12px" align="right" valign="top">
                  <asp:Label id="lblSyncType" runat="server">ͬ������</asp:Label></TD>
                <TD style="HEIGHT: 12px" align="left"><FONT face="����">
                    <asp:RadioButton id="rdoAddAndEdit" runat="server" Text="����ͬ�������ֶε�ֵ�жϣ�ͬ����Դ��¼�Ѿ��������޸ļ�¼������������Ӽ�¼��"
                      GroupName="SYNC_TYPE" Checked="True"></asp:RadioButton><BR>
                    <asp:RadioButton id="rdoAddOnly" runat="server" Text="��ͬ����Դ��������Ӽ�¼" GroupName="SYNC_TYPE"></asp:RadioButton></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
                <TD style="HEIGHT: 12px" align="left"></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="ȷ��"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="�˳�"></asp:Button></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
