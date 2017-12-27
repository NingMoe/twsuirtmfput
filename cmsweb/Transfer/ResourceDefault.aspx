<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResourceDefault.aspx.vb" Inherits="Transfer_ResourceDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<frameset cols="240,*" frameborder="yes" bordercolor="#4A65A5">
	<frame src='/cmsweb/Transfer/ResourceTree.aspx?resid=<%=Request("mnuresid") %>&recid=<%=Request("mnurecid") %>' name="tree" noresize=true frameborder="no">
	<frame src="" name="List" noresize=true frameborder="no">
</frameset>
</html>
