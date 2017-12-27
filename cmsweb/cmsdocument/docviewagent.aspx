<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocViewAgent" CodeFile="DocViewAgent.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ÎÄµµÔÚÏßä¯ÀÀ</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
<script language="javascript">
<!--
function EvtMouseDown(){
	window.event.returnValue=false;
}
document.oncontextmenu=EvtMouseDown;
-->
</script>


<script type="text/javascript"> 
 <!-- 
 var omitformtags=["input", "textarea", "select"] 
 omitformtags=omitformtags.join("|") 
 function disableselect(e){ 
 if (omitformtags.indexOf(e.target.tagName.toLowerCase())==-1) 
 return false 
 } 
function reEnable(){ 
return true 
} 
if (typeof document.onselectstart!="undefined") 
document.onselectstart=new Function ("return false") 
else{ 
document.onmousedown=disableselect 
document.onmouseup=reEnable 
} 
--> 
</script> 

	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		<table cellpadding="0" cellspacing="0" border="0" style="padding:10px;">
		    <tr>
		        <td>
		            <asp:Image ID="img1" runat="server" Visible="false" />
			        <asp:Label ID="lblTxt" runat="server" Visible="false"></asp:Label>
		        </td>
		    </tr>
		</table>
			
		</form>
	</body>
</HTML>
