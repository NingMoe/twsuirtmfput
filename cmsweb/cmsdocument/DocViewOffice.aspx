<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocViewOffice" CodeFile="DocViewOffice.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>‘⁄œﬂ‰Ø¿¿Œƒµµ</title>
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
	<OBJECT id=ViewOffice1 style="WIDTH: 100%; HEIGHT: 100%" classid=CLSID:97A17659-93B1-4F74-BF03-7A1CDC07BEE5 VIEWASTEXT codeBase="/cmsweb/control/DFWebDoc.CAB#version=1,0,0,0">
		<PARAM NAME="_ExtentX" VALUE="23363"><PARAM NAME="_ExtentY" VALUE="9684">
	</OBJECT>
	<SCRIPT language="vbscript">
		Call ViewOffice1.LoadUrlFile("http://" & window.location.host & "/" & "<%=AspPage.RStr("cmsurldoc", Request)%>" , false, false, <%=AspPage.RStr("cmsdocreadonly", Request)%>, <%=AspPage.RStr("cmsprint", Request)%>, false)
	</SCRIPT>
  </body>
</html>
