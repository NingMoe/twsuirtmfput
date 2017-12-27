<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceRelationEdit" CodeFile="ResourceRelationEdit.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>关联表设置</title>
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
            <TABLE class="table_level2" style="WIDTH: 532px" cellSpacing="0" cellPadding="0" width="532"
              border="0">
              <TR>
                <TD class="header_level2" style="WIDTH: 557px" colSpan="2" height="10"><b>关联表设置</b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 102px" align="right" width="102" height="12">
                </TD>
                <TD style="WIDTH: 448px" width="448" height="12"></TD>
              </TR>
              <TR height="23">
                <td style="WIDTH: 557px" colSpan="2" height="22" valign="top">
                  <TABLE style="WIDTH: 512px" cellSpacing="0" cellPadding="0" width="512" border="0">
                    <TR height="25">
                      <TD style="WIDTH: 220px" vAlign="bottom"><FONT face="宋体"><asp:label id="Label1" runat="server">本资源：</asp:label>
                          <asp:label id="lblResName" runat="server"></asp:label></FONT></TD>
                      <TD style="WIDTH: 220px" vAlign="bottom"><FONT face="宋体">
                          <asp:LinkButton id="lbtnSelectRelRes" runat="server">选择关联资源</asp:LinkButton></FONT></TD>
                    </TR>
                    <tr>
                      <td style="WIDTH: 220px" vAlign="top"><FONT face="宋体"><asp:listbox id="ListBox1" runat="server" Width="248px" Height="245px"></asp:listbox></FONT></td>
                      <td style="WIDTH: 220px" vAlign="top"><FONT face="宋体"><asp:listbox id="ListBox2" runat="server" Width="248px" Height="245px"></asp:listbox></FONT></td>
                    </tr>
                    <TR>
                      <TD colspan="2" vAlign="top" align="left" height="28"><asp:button id="btnAddMainRelatedCol" runat="server" Width="96px" Text="添加主关联"></asp:button>
                        <asp:button id="btnAddInputRelatedCol" runat="server" Width="96px" Text="添加输入关联"></asp:button>
                        <asp:Button id="btnAddShowRelation" runat="server" Text="添加显示关联" Width="96px"></asp:Button><asp:Button id="btnAddCalcRelation" runat="server" Width="96px" Text="添加计算关联"></asp:Button><FONT face="宋体">&nbsp;</FONT>
                        <asp:button id="btnExit" runat="server" Width="72px" Text="完成"></asp:button></TD>
                    </TR>
                    <TR>
                      <TD style="WIDTH: 732px" vAlign="top" colspan="2"><FONT face="宋体"><asp:datagrid id="DataGrid1" runat="server" Width="390px"></asp:datagrid>
                        </FONT>
                      </TD>
                    </TR>
                  </TABLE>
                </td>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
