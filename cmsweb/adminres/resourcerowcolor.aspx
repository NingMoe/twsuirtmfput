<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceRowColor" CodeFile="ResourceRowColor.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>行颜色设置</title>
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
                <TD class="header_level2" style="WIDTH: 457px" height="22"><b>行颜色设置</b></TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 4px" align="left"></TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 26px" align="left">资源名称：<asp:label id="lblResName" runat="server"></asp:label>
                </TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 4px" align="left"></TD>
              </TR>
              <TR height="23">
                <td style="WIDTH: 457px">
                  <TABLE style="WIDTH: 700px" cellSpacing="0" cellPadding="0" width="700" border="0">
                    <TR>
                      <TD vAlign="top"><FONT face="宋体">字段<asp:dropdownlist id="ddlFields11" runat="server" Width="160px"></asp:dropdownlist>的值
                          <asp:dropdownlist id="ddlCondition11" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal11" runat="server" Width="220px"></asp:textbox><BR>
                          <FONT face="宋体"><FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields12" runat="server" Width="160px"></asp:dropdownlist>的值
                              <asp:dropdownlist id="ddlCondition12" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal12" runat="server" Width="220px"></asp:textbox></FONT><BR>
                            <FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields13" runat="server" Width="160px"></asp:dropdownlist>的值
                              <asp:dropdownlist id="ddlCondition13" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal13" runat="server" Width="220px"></asp:textbox><BR>
                              <FONT face="宋体">同时满足以上条件的记录的颜色设置为：<asp:dropdownlist id="ddlColor1" runat="server" Width="120px"></asp:dropdownlist></FONT></FONT></FONT></FONT></TD>
                    </TR>
                    <TR>
                      <TD style="HEIGHT: 29px" vAlign="top"></TD>
                    </TR>
                    <TR>
                      <TD vAlign="top"><FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields21" runat="server" Width="160px"></asp:dropdownlist>的值
                          <asp:dropdownlist id="ddlCondition21" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal21" runat="server" Width="220px"></asp:textbox><BR>
                          <FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields22" runat="server" Width="160px"></asp:dropdownlist>的值
                            <asp:dropdownlist id="ddlCondition22" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal22" runat="server" Width="220px"></asp:textbox><BR>
                            <FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields23" runat="server" Width="160px"></asp:dropdownlist>的值
                              <asp:dropdownlist id="ddlCondition23" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal23" runat="server" Width="220px"></asp:textbox></FONT><BR>
                          </FONT><FONT face="宋体"><FONT face="宋体">同时满足以上条件的记录的颜色设置为：</FONT><asp:dropdownlist id="ddlColor2" runat="server" Width="120px"></asp:dropdownlist></FONT></FONT></TD>
                    </TR>
                    <TR>
                      <TD style="HEIGHT: 30px" vAlign="top"></TD>
                    </TR>
                    <TR>
                      <TD vAlign="top"><FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields31" runat="server" Width="160px"></asp:dropdownlist>的值
                          <asp:dropdownlist id="ddlCondition31" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal31" runat="server" Width="220px"></asp:textbox><BR>
                          <FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields32" runat="server" Width="160px"></asp:dropdownlist>的值
                            <asp:dropdownlist id="ddlCondition32" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal32" runat="server" Width="220px"></asp:textbox><BR>
                            <FONT face="宋体"><FONT face="宋体">字段</FONT><asp:dropdownlist id="ddlFields33" runat="server" Width="160px"></asp:dropdownlist>的值
                              <asp:dropdownlist id="ddlCondition33" runat="server" Width="100px"></asp:dropdownlist>指定常量值<asp:textbox id="txtCondVal33" runat="server" Width="220px"></asp:textbox></FONT></FONT><FONT face="宋体"><BR>
                            <FONT face="宋体">同时满足以上条件的记录的颜色设置为：</FONT><asp:dropdownlist id="ddlColor3" runat="server" Width="120px"></asp:dropdownlist></FONT></FONT></TD>
                    </TR>
                  </TABLE>
                </td>
              </TR>
              <TR>
                <TD><FONT face="宋体"><BR>
                  </FONT>
                  <asp:button id="btnConfirm" runat="server" Width="80px" Text="保存设置"></asp:button><asp:Button id="btnClearSettings" runat="server" Text="清除设置"></asp:Button><asp:button id="btnCancel" runat="server" Width="64px" Text="退出"></asp:button></TD>
              </TR>
              <TR>
                <TD style="HEIGHT: 4px"></TD>
              </TR>
              <TR>
                <TD><FONT face="宋体">
                    <P>帮助信息1：上述多个条件从上到下的次序即是判断条件时的优先级。<BR>
                      <BR>
                      帮助信息2：当条件值为数字时，则无论该条件中的字段类型是文本还是数字，都以数字进行比较。<BR>
                      <BR>
                      帮助信息3：若要删除某个行颜色设置，只需将该行任意一个下拉框选空值，然后单击“确认”按钮。
                      <BR>
                      <BR>
                      帮助信息4：支持以下常量值的宏定义：<BR>
                      &nbsp;&nbsp;&nbsp;&nbsp;[TODAY] ：表示服务器当天日期<BR>
                      &nbsp;&nbsp;&nbsp; [TODAY]+3 ：表示服务器当天日期日期加3天。例：若当天是2004-11-20，则结果日期是2004-11-23<BR>
                      &nbsp;&nbsp;&nbsp; [TODAY]-5 ：表示服务器当天日期减5天。<BR>
                  </FONT></P></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
