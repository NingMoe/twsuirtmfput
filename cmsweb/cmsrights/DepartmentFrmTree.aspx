<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DepartmentFrmTree.aspx.vb" Inherits="cmsrights_DepartmentFrmTree" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
	<LINK href="../css/cmsstyle.css" type="text/css" rel="stylesheet" /> 
    <link type="text/css" rel="stylesheet" href="../css/sys-menu2.css" /> 
    <script src="../js/MzTreeView10.js"></script> 
    <script src="../js/menu4.js" type="text/javascript"></script>
    <script src="../js/poslib.js" type="text/javascript"></script>
    <script src="../js/scrollbutton.js" type="text/javascript"></script>
    
    <LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
</head>
<body>


  <form id="form1" runat="server">
  <TABLE cellSpacing="0" cellPadding="0" width="236" height="100%" border="0" style="PADDING-LEFT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px">
				<tr>
					<td width="4" valign="top"></td>
					<td valign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" class="table_level2">
							<TR>
								<TD height="25" class="header_level2">&nbsp;权限管理</TD>
							</TR>
							<TR>
								<TD height="100%" valign="top"><asp:Panel id="panelDepTree" style="OVERFLOW: auto" Width="220" Height="100%" runat="server"><%cmsrights_DepartmentFrmTree.LoadTreeView(CmsPass, Request, Response)%></asp:Panel>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			
    <div id="treeviewarea" style="display:none;"></div>
    </form>
    
<%--    <script language="javascript">
    <!--
    var tree = new MzTreeView("tree");
    tree.icons["enterprise"] = "enterprise.gif";
    tree.icons["Dept"] = "dep_real.gif";
    tree.icons["factory"] = "Factory.gif";
    tree.icons["virtual"]="dep_virtual.gif";
    tree.setIconPath("../images/tree1/");
    
    <%=strTree %>
    
    document.getElementById('treeviewarea').innerHTML = tree.toString();
    
    function expand2lChieldren()
    {
        try{
            var root = tree.node["1"];
            if(root.hasChild)
            {
                for (var k=0; k<root.childNodes.length; k++) tree.expand(root.childNodes[k].id,true);
            }
        }
        catch(e){}
    }
    //setTimeout("expand2lChieldren();",1000);
    //-->
    </script>--%>
    
</body>
</html>
