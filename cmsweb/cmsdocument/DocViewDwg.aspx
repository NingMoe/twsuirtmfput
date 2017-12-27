<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocViewDwg" CodeFile="DocViewDwg.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>ÔÚÏßä¯ÀÀÎÄµµ</title>
	<meta http-equiv="Pragma" content="no-cache">
	<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
<script language="javascript">
<!--
function EvtMouseDown(){
	window.event.returnValue=false;
}
document.oncontextmenu=EvtMouseDown;
-->
</script>
  </head>
  <body>
	<OBJECT id=BravaACXViewCtl1 style="WIDTH: 100%; HEIGHT: 100%" classid=CLSID:EFC35212-E8FC-4DED-8D98-BD626B9CFDCA VIEWASTEXT codeBase="/cmsweb/control/BravaActive.CAB#version=1,0,0,0">
	</OBJECT>
	<SCRIPT language="vbscript">
		BravaACXViewCtl1.filename ="<%=cmsurldoc %>"
	</SCRIPT>
</body>
</html>
