<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FormSetProperty" CodeFile="FormSetProperty.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>��������</title>
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
                <TD class="form_header" colSpan="2"><b><asp:label id="lblTitle" runat="server">������������</asp:label></b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:label id="Label8" runat="server">Ŀ����Դ</asp:label></TD>
                <TD style="HEIGHT: 25px" align="left"><asp:textbox id="txtResName" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 16px" align="right"></TD>
                <TD style="HEIGHT: 16px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label3" runat="server">���㹫ʽ</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label9" runat="server">��ǰ�����Ƿ����㹫ʽ</asp:label></TD>
                <TD align="left"><asp:checkbox id="chkRunFormula" runat="server" Text="������㹫ʽ"></asp:checkbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 20px" align="right"></TD>
                <TD style="HEIGHT: 20px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label1" runat="server">�ı��滻</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label2" runat="server">Դ�ı�</asp:label></TD>
                <TD align="left"><asp:textbox id="txtReplaceSrc" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:label id="Label4" runat="server">Ŀ���ı�</asp:label></TD>
                <TD align="left">
                  <asp:textbox id="txtReplaceDest" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 20px" align="right"></TD>
                <TD style="HEIGHT: 20px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label5" runat="server">�༭��������</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label6" runat="server">������¼��Ϣ</asp:label></TD>
                <TD align="left"><asp:checkbox id="chkKeepPrevRecord" runat="server" Text="�½���¼ʱ�����ϱ���֮ǰ���༭��¼����Ϣ"></asp:checkbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 20px" align="right"></TD>
                <TD style="HEIGHT: 20px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label7" runat="server">�����ֶ���������</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label10" runat="server">�����ֶ�</asp:label></TD>
                <TD align="left">
                  <asp:DropDownList id="ddlCondCol" runat="server" Width="196px"></asp:DropDownList></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:label id="Label11" runat="server">����ֵ</asp:label></TD>
                <TD align="left">
                  <asp:textbox id="txtCondVal" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"></TD>
                <TD align="left">
                  <asp:CheckBox id="chkNoCond" runat="server" Text="�������������ֶ�������Ӱ��"></asp:CheckBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 15px" align="right"></TD>
                <TD style="HEIGHT: 15px" align="left"></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:button id="btnConfirm" runat="server" Width="80px" Text="����"></asp:button><INPUT style="WIDTH: 80px; HEIGHT: 24px" type="button" onclick="window.close();" value="�˳�"></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
