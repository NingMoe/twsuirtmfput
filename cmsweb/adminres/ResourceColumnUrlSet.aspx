<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResourceColumnUrlSet.aspx.vb" Inherits="adminres_ResourceColumnUrlSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
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
 
				RECID=src.RECID;
				document.getElementById("frm").src="MTableSearchColDef.aspx?mnuresid=<%=ResID %>&colurlid="+RECID+"&mtstype=9&mnuempid=admin"
			}
			
			function SetColumnUrl(obj)
			{ 
			    var objParent=obj.parentElement.parentElement; 
			    window.location.href='ResourceColumnUrlSet.aspx?colname='+objParent.RECID+'&resid=<%=VLng("PAGE_RESID") %>'
			}
		-->
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="header_level2">资源名称：<asp:Label ID="lblResName" runat="server"></asp:Label> &nbsp;字段名称：<asp:Label ID="lblColName" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style=" padding:10PX;">
                <asp:Button ID="btnSubmit" runat="server" Text="添加" />
                <asp:DataGrid ID="dgColumnUrl" runat="server" AutoGenerateColumns="False" style="margin-top:5px;" DataKeyField="CU_ID">
                    <Columns>
                        <asp:BoundColumn DataField="CU_ID" Visible=false></asp:BoundColumn>
                        <asp:EditCommandColumn CancelText="取消" EditText="编辑" UpdateText="保存" HeaderStyle-Width="60px" ItemStyle-Width="60px"></asp:EditCommandColumn>                        
                        <asp:TemplateColumn HeaderText="链接地址" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                            <ItemTemplate><%# Eval("CU_URL") %></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtUrl" runat="server" Text='<%# Eval("CU_URL") %>'></asp:TextBox></EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="链接方式" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                            <ItemTemplate><%# Eval("CU_TARGET") %></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtTarget" runat="server" Text='<%# Eval("CU_TARGET") %>'></asp:TextBox></EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="链接参数" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                            <ItemTemplate><%# Eval("CU_PARAM") %></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtParam" runat="server" Text='<%# Eval("CU_PARAM") %>'></asp:TextBox></EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="是否启用" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                            <ItemTemplate><asp:CheckBox ID="chk" runat="server" Checked='<%# Eval("ISENABLED") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chk" runat="server" Checked='<%# Eval("ISENABLED") %>' /></EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <iframe id="frm" src="" width="100%" height="300px"></iframe>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
