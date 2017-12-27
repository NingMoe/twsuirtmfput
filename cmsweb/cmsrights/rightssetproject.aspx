<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RightsSetProject" CodeFile="RightsSetProject.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>项目权限设置</title>
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
        <SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
        <SCRIPT language="JavaScript" src="/cmsweb/script/base.js"></SCRIPT>
        <SCRIPT language="JavaScript" src="/cmsweb/script/Valid.js"></SCRIPT>
        <SCRIPT language="JavaScript" src="/cmsweb/script/CmsScript.js"></SCRIPT>
        <script language="javascript">
<!--
//调整输入窗体中各个Panel的位置和显示模式
function AdjustPanel(){
	panelForm.style.left = parseInt(4);
	panelForm.style.top = parseInt(30);
	panelForm.style.display = "";
	return false;
}
-->
        </script>
    </HEAD>
    <body>
        <form id="Form1" method="post" runat="server">
            <TABLE style="PADDING-LEFT: 4px; WIDTH: 774px; PADDING-TOP: 1px; HEIGHT: 16px" cellSpacing="0"
                cellPadding="0" width="774" border="0">
                <!--主表数据-->
                <tr>
                    <td>
                        <TABLE class="toolbar_table" cellSpacing="0" border="0">
                            <TR>
                                <TD width="8"></TD>
                                <TD noWrap align="left" width="60"><IMG src="/cmsweb/images/imagebuttons/save.gif" align="absMiddle" width="16" height="16">
                                    <asp:linkbutton id="lnkSave" Runat="server">保存</asp:linkbutton></TD>
                                <TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
                                </TD>
                                <TD noWrap align="left" width="60"><IMG src="/cmsweb/images/imagebuttons/exit.gif" align="absMiddle" width="16" height="16">
                                    <asp:linkbutton id="lnkExit" Runat="server">退出</asp:linkbutton></TD>
                                <TD>&nbsp;</TD>
                            </TR>
                        </TABLE>
                    </td>
                </tr>
            </TABLE>
            <asp:panel id="panelForm" style="Z-INDEX: 102; LEFT: 12px; POSITION: absolute; TOP: 84px" runat="server"
                Width="368px" Height="184px" BorderColor="Aqua" BorderWidth="1px"></asp:panel>
            <script language="JavaScript">AdjustPanel();</script>
        </form>
    </body>
</HTML>
