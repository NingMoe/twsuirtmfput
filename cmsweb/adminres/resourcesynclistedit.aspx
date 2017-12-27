<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceSyncListEdit" CodeFile="ResourceSyncListEdit.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>数据同步</title>
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
                <TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">数据同步</asp:Label></b></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
                <TD style="HEIGHT: 12px" align="left"></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right"><asp:Label id="lblResName" runat="server">主资源名称</asp:Label></TD>
                <TD align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:Label id="Label1" runat="server">同步资源名称</asp:Label></TD>
                <TD align="left"><FONT face="宋体">
                    <asp:TextBox id="txtSyncResName" runat="server" Width="196px"></asp:TextBox>
                    <asp:LinkButton id="lbtnSelectRes" runat="server">选择资源</asp:LinkButton></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">
                  <asp:Label id="lblSyncAction" runat="server">同步动作</asp:Label></TD>
                <TD align="left">
                  <asp:RadioButton id="rdoActionAdd" runat="server" Text="添加记录" GroupName="SYNC_ACTION" Checked="True"></asp:RadioButton><FONT face="宋体">&nbsp;
                    <asp:RadioButton id="rdoActionEdit" runat="server" Text="修改记录" GroupName="SYNC_ACTION"></asp:RadioButton>&nbsp;
                    <asp:RadioButton id="rdoActionDel" runat="server" Text="删除记录" GroupName="SYNC_ACTION"></asp:RadioButton></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 12px" align="right" valign="top">
                  <asp:Label id="lblSyncType" runat="server">同步类型</asp:Label></TD>
                <TD style="HEIGHT: 12px" align="left"><FONT face="宋体">
                    <asp:RadioButton id="rdoAddAndEdit" runat="server" Text="根据同步关联字段的值判断，同步资源记录已经存在则修改记录，不存在则添加记录。"
                      GroupName="SYNC_TYPE" Checked="True"></asp:RadioButton><BR>
                    <asp:RadioButton id="rdoAddOnly" runat="server" Text="在同步资源中总是添加记录" GroupName="SYNC_TYPE"></asp:RadioButton></FONT></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
                <TD style="HEIGHT: 12px" align="left"></TD>
              </TR>
              <TR>
                <TD style="WIDTH: 140px" align="right">&nbsp;</TD>
                <TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="确认"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button></TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
