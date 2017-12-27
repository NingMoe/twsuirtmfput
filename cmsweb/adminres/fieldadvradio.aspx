<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvRadio" validateRequest="false" CodeFile="FieldAdvRadio.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>多选一选择项</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
    <script language="javascript">
<!--
//调整输入窗体中各个Panel的位置和显示模式
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
                <TD class="header_level2" colSpan="2" height="22"><b>多选一选择项设置</b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" height="12" width="118"><FONT face="宋体"> </FONT>
                </TD>
                <TD height="12"></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="lblResDispName" runat="server">当前资源：</asp:label></TD>
                <TD>
                  <asp:TextBox id="txtResName" runat="server" Width="240px"></asp:TextBox></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="lblColDispName" runat="server">当前字段：</asp:label></TD>
                <TD>
                  <asp:TextBox id="txtFieldName" runat="server" Width="240px"></asp:TextBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" vAlign="top" align="right" width="118"><asp:label id="lblOptionDesc" runat="server">选项描述：</asp:label></TD>
                <TD><FONT face="宋体"><asp:textbox id="txtOptionDesc" runat="server" TextMode="MultiLine" Height="40px" Width="552px"></asp:textbox></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="Label1" runat="server">选项描述的行数：</asp:label></TD>
                <TD><asp:textbox id="txtDescRowNum" runat="server" Width="72px" MaxLength="2">1</asp:textbox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="Label3" runat="server">选项排列方式：</asp:label></TD>
                <TD><asp:radiobutton id="rdoRow" runat="server" Text="横向排列" GroupName="rdoOptionDispStyle" Checked="True"></asp:radiobutton><FONT face="宋体">&nbsp;&nbsp;&nbsp;
                  </FONT>
                  <asp:radiobutton id="rdoCol" runat="server" Text="竖向排列" GroupName="rdoOptionDispStyle"></asp:radiobutton></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118">
                  <asp:label id="Label2" runat="server">选项保存值：</asp:label></TD>
                <TD>
                  <asp:radiobutton id="rdoSaveOptionValue" runat="server" Checked="True" GroupName="OPTION_SAVE" Text="保存输入的选项值"></asp:radiobutton><FONT face="宋体">&nbsp;</FONT>
                  <asp:radiobutton id="rdoSaveNumber" runat="server" GroupName="OPTION_SAVE" Text="保存从1开始的选项值索引"></asp:radiobutton><FONT face="宋体">&nbsp;</FONT>
                  <asp:radiobutton id="rdoSaveABC" runat="server" GroupName="OPTION_SAVE" Text="保存从A开始的选项值索引"></asp:radiobutton></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 118px" align="right" width="118"><asp:label id="lblOptionValues" runat="server">选项值：</asp:label></TD>
                <TD><asp:textbox id="txtOptValue" runat="server" Width="472px"></asp:textbox></TD>
              </TR>
              <TR height="25">
                <TD style="WIDTH: 118px" vAlign="top" align="right" width="118"></TD>
                <TD vAlign="top">
                  <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
                      <td style="WIDTH: 476px" vAlign="top" width="476"><asp:listbox id="lboxOptValues" runat="server" Height="324px" Width="472px"></asp:listbox></td>
                      <td vAlign="top"><asp:button id="btnAdd" runat="server" Width="84px" Text="添加"></asp:button><FONT face="宋体"><BR>
                        </FONT>
                        <asp:button id="btnEdit" runat="server" Width="84px" Text="修改"></asp:button><FONT face="宋体"><BR>
                        </FONT>
                        <asp:button id="btnInsert" runat="server" Width="84px" Text="插入"></asp:button><br>
                        <asp:button id="btnDel" runat="server" Width="84px" Text="删除"></asp:button><FONT face="宋体"><BR>
                        </FONT><FONT face="宋体">
                          <BR>
                          <asp:button id="btnUp" runat="server" Width="84px" Text="向上移动"></asp:button><BR>
                          <asp:button id="btnDown" runat="server" Width="84px" Text="向下移动"></asp:button><BR>
                          <BR>
                          <BR>
                          <asp:button id="btnConfirm" runat="server" Width="84px" Text="确定"></asp:button><BR>
                        </FONT><FONT face="宋体">
                          <asp:button id="btnCancel" runat="server" Width="84px" Text="取消"></asp:button></FONT>
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
