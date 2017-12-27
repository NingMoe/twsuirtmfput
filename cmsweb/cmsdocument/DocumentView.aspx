<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentView.aspx.vb" Inherits="DocumentView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>文档在线浏览</title>
			<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet"/>
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

	</head>
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

