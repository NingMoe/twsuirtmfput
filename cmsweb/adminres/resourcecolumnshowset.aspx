<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceColumnShowSet" CodeFile="ResourceColumnShowSet.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>显示设置</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
    <script language="javascript">
		<!--
		    var RECID="";
			function RowLeftClickNoPost(src){
				var o=src.parentNode;
				for (var k=1;k<o.children.length;k++){
					o.children[k].bgColor = "white";
				}
				src.bgColor = "#C4D9F9";
				self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
 
				RECID=src.RECID;
			}
			
			function SetColumnUrl(obj)
			{ 
			    var objParent=obj.parentElement.parentElement; 
			    window.location.href='ResourceColumnUrlSet.aspx?colname='+objParent.RECID+'&resid=<%=VLng("PAGE_RESID") %>'
			}
		-->
    </script>
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <input type="hidden" name="RECID">
      <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
          <td width="4"></td>
          <td>
            <TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="99%" border="0">
              <TR>
                <TD class="header_level2" height="22"><b>字段显示设置&nbsp;（资源：<asp:label id="lblResDispName" runat="server"></asp:label>）</b>
                </TD>
              </TR>
              <tr>
                <td height="5"></td>
              </tr>
              <TR>
                <TD height="5"><FONT face="宋体"><asp:Button id="btnShowAll" runat="server" Text="全部显示"></asp:Button><asp:Button id="btnShowNone" runat="server" Text="全不显示"></asp:Button><asp:Button id="btnReset" runat="server" Text="清空设置"></asp:Button>&nbsp;<asp:button id="btnExit" runat="server" Text="退出" Width="72px"></asp:button>&nbsp;&nbsp;<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">
                    <asp:linkbutton id="lbtnMoveup" runat="server">向上移动</asp:linkbutton>&nbsp;
                    <asp:LinkButton id="lbtnMoveUp5Step" runat="server">上移5位</asp:LinkButton>&nbsp;
                    <asp:LinkButton id="lbtnMoveToFirst" runat="server">移至首位</asp:LinkButton>&nbsp;<IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">
                    <asp:linkbutton id="lbtnMovedown" runat="server">向下移动</asp:linkbutton>&nbsp;
                    <asp:LinkButton id="lbtnMoveDown5Step" runat="server">下移5位</asp:LinkButton>&nbsp;
                    <asp:LinkButton id="lbtnMoveToLast" runat="server">移至末位</asp:LinkButton>
                  </FONT>
                </TD>
              </TR>
              <TR>
                <TD height="5"><FONT face="宋体"> </FONT>
                </TD>
              </TR>
              <TR>
                <TD height="22"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False"></asp:datagrid></TD>
              </TR>
              <TR>
                <TD height="5"><FONT face="宋体"> </FONT>
                </TD>
              </TR>
              <TR>
                <TD height="5"><FONT face="宋体">
                    <asp:Button id="btnColSet" runat="server" Text="字段设置"></asp:Button><asp:Button id="btnInputFormSet" runat="server" Text="输入窗体"></asp:Button><asp:Button id="btnRightsSet" runat="server" Text="权限设置"></asp:Button></FONT>
                </TD>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
