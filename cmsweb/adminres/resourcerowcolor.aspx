<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceRowColor" CodeFile="ResourceRowColor.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>����ɫ����</title>
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
            <TABLE class="table_level2" style="WIDTH: 756px" cellSpacing="0" cellPadding="0" width="756"
              border="0">
              <TR>
                <TD class="header_level2" style="WIDTH: 457px" height="22"><b>����ɫ����</b></TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 4px" align="left"></TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 26px" align="left">��Դ���ƣ�<asp:label id="lblResName" runat="server"></asp:label>
                </TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 4px" align="left"></TD>
              </TR>
              <TR height="23">
                <td style="WIDTH: 457px">
                  <TABLE style="WIDTH: 700px" cellSpacing="0" cellPadding="0" width="700" border="0">
                    <TR>
                      <TD vAlign="top"><FONT face="����">�ֶ�<asp:dropdownlist id="ddlFields11" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                          <asp:dropdownlist id="ddlCondition11" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal11" runat="server" Width="220px"></asp:textbox><BR>
                          <FONT face="����"><FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields12" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                              <asp:dropdownlist id="ddlCondition12" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal12" runat="server" Width="220px"></asp:textbox></FONT><BR>
                            <FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields13" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                              <asp:dropdownlist id="ddlCondition13" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal13" runat="server" Width="220px"></asp:textbox><BR>
                              <FONT face="����">ͬʱ�������������ļ�¼����ɫ����Ϊ��<asp:dropdownlist id="ddlColor1" runat="server" Width="120px"></asp:dropdownlist></FONT></FONT></FONT></FONT></TD>
                    </TR>
                    <TR>
                      <TD style="HEIGHT: 29px" vAlign="top"></TD>
                    </TR>
                    <TR>
                      <TD vAlign="top"><FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields21" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                          <asp:dropdownlist id="ddlCondition21" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal21" runat="server" Width="220px"></asp:textbox><BR>
                          <FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields22" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                            <asp:dropdownlist id="ddlCondition22" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal22" runat="server" Width="220px"></asp:textbox><BR>
                            <FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields23" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                              <asp:dropdownlist id="ddlCondition23" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal23" runat="server" Width="220px"></asp:textbox></FONT><BR>
                          </FONT><FONT face="����"><FONT face="����">ͬʱ�������������ļ�¼����ɫ����Ϊ��</FONT><asp:dropdownlist id="ddlColor2" runat="server" Width="120px"></asp:dropdownlist></FONT></FONT></TD>
                    </TR>
                    <TR>
                      <TD style="HEIGHT: 30px" vAlign="top"></TD>
                    </TR>
                    <TR>
                      <TD vAlign="top"><FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields31" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                          <asp:dropdownlist id="ddlCondition31" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal31" runat="server" Width="220px"></asp:textbox><BR>
                          <FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields32" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                            <asp:dropdownlist id="ddlCondition32" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal32" runat="server" Width="220px"></asp:textbox><BR>
                            <FONT face="����"><FONT face="����">�ֶ�</FONT><asp:dropdownlist id="ddlFields33" runat="server" Width="160px"></asp:dropdownlist>��ֵ
                              <asp:dropdownlist id="ddlCondition33" runat="server" Width="100px"></asp:dropdownlist>ָ������ֵ<asp:textbox id="txtCondVal33" runat="server" Width="220px"></asp:textbox></FONT></FONT><FONT face="����"><BR>
                            <FONT face="����">ͬʱ�������������ļ�¼����ɫ����Ϊ��</FONT><asp:dropdownlist id="ddlColor3" runat="server" Width="120px"></asp:dropdownlist></FONT></FONT></TD>
                    </TR>
                  </TABLE>
                </td>
              </TR>
              <TR>
                <TD><FONT face="����"><BR>
                  </FONT>
                  <asp:button id="btnConfirm" runat="server" Width="80px" Text="��������"></asp:button><asp:Button id="btnClearSettings" runat="server" Text="�������"></asp:Button><asp:button id="btnCancel" runat="server" Width="64px" Text="�˳�"></asp:button></TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 4px"></TD>
              </TR>
              <TR>
                <TD><FONT face="����">
                    <P>������Ϣ1����������������ϵ��µĴ������ж�����ʱ�����ȼ���<BR>
                      <BR>
                      ������Ϣ2��������ֵΪ����ʱ�������۸������е��ֶ��������ı��������֣��������ֽ��бȽϡ�<BR>
                      <BR>
                      ������Ϣ3����Ҫɾ��ĳ������ɫ���ã�ֻ�轫��������һ��������ѡ��ֵ��Ȼ�󵥻���ȷ�ϡ���ť��
                      <BR>
                      <BR>
                      ������Ϣ4��֧�����³���ֵ�ĺ궨�壺<BR>
                      &nbsp;&nbsp;&nbsp;&nbsp;[TODAY] ����ʾ��������������<BR>
                      &nbsp;&nbsp;&nbsp; [TODAY]+3 ����ʾ�����������������ڼ�3�졣������������2004-11-20������������2004-11-23<BR>
                      &nbsp;&nbsp;&nbsp; [TODAY]-5 ����ʾ�������������ڼ�5�졣<BR>
                  </FONT></P></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
