<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FormSetProperty" CodeFile="FormSetProperty.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>窗体属性</title>
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
                <TD class="form_header" colSpan="2"><b><asp:label id="lblTitle" runat="server">窗体属性设置</asp:label></b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:label id="Label8" runat="server">目标资源</asp:label></TD>
                <TD style="HEIGHT: 25px" align="left"><asp:textbox id="txtResName" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 16px" align="right"></TD>
                <TD style="HEIGHT: 16px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label3" runat="server">计算公式</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label9" runat="server">当前窗体是否运算公式</asp:label></TD>
                <TD align="left"><asp:checkbox id="chkRunFormula" runat="server" Text="运算计算公式"></asp:checkbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 20px" align="right"></TD>
                <TD style="HEIGHT: 20px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label1" runat="server">文本替换</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label2" runat="server">源文本</asp:label></TD>
                <TD align="left"><asp:textbox id="txtReplaceSrc" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:label id="Label4" runat="server">目标文本</asp:label></TD>
                <TD align="left">
                  <asp:textbox id="txtReplaceDest" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 20px" align="right"></TD>
                <TD style="HEIGHT: 20px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label5" runat="server">编辑窗体设置</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label6" runat="server">保留记录信息</asp:label></TD>
                <TD align="left"><asp:checkbox id="chkKeepPrevRecord" runat="server" Text="新建记录时界面上保留之前正编辑记录的信息"></asp:checkbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 20px" align="right"></TD>
                <TD style="HEIGHT: 20px" align="left"></TD>
              </TR>
              <TR class="module_header">
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:label id="Label7" runat="server">窗体字段条件设置</asp:label></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:label id="Label10" runat="server">条件字段</asp:label></TD>
                <TD align="left">
                  <asp:DropDownList id="ddlCondCol" runat="server" Width="196px"></asp:DropDownList></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:label id="Label11" runat="server">条件值</asp:label></TD>
                <TD align="left">
                  <asp:textbox id="txtCondVal" runat="server" Width="196px"></asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"></TD>
                <TD align="left">
                  <asp:CheckBox id="chkNoCond" runat="server" Text="不受其它窗体字段条件的影响"></asp:CheckBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 15px" align="right"></TD>
                <TD style="HEIGHT: 15px" align="left"></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:button id="btnConfirm" runat="server" Width="80px" Text="保存"></asp:button><INPUT style="WIDTH: 80px; HEIGHT: 24px" type="button" onclick="window.close();" value="退出"></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
