<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RightSetFrmContent.aspx.vb" Inherits="cmsrights_RightSetFrmContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
  <head>
    <title>Unionsoft 企业管理应用平台</title>
    <LINK  href="/cmsweb/css/cmsstyle.css" rel="stylesheet" type="text/css">
  </head>
  
	<frameset cols="240,*" frameborder="yes" bordercolor="#E7EBEF">
		<frame src="ResourceTree.aspx?gainerid=<%=Request.QueryString("depid") %>&type=<%=unionsoft.Platform.RightsGainerType.IsDepartment %>" name="left" noresize=true frameborder="no">
		<frame src="Blank.aspx" name="List" noresize=true frameborder="no">
	</frameset>
</html>
