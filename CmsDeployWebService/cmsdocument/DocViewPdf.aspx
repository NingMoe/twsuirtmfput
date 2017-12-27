<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocViewPdf" CodeFile="DocViewPdf.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>在线浏览文档</title>
	<meta http-equiv="Pragma" content="no-cache"/>
	<link href="../css/cmsstyle.css" type="text/css" rel="stylesheet"/>
	<SCRIPT language="JavaScript" src="../script/jscommon.js"></SCRIPT>
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
	<OBJECT id="ViewPdf1" style="WIDTH: 100%; HEIGHT: 100%" classid="CLSID:F3F230F8-DE5B-4266-A993-EB3D4DC88F63" VIEWASTEXT codeBase="../control/DFWebDoc.CAB#version=1,0,0,0">
	</OBJECT>
  	<SCRIPT language="vbscript">
		Call ViewPdf1.LoadUrlFile("<%=cmsurldoc %>", false)
	</SCRIPT>
  </body>
</html>
