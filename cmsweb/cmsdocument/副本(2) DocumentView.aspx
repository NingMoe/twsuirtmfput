<%@ Page Language="VB" AutoEventWireup="false" CodeFile="副本(2) DocumentView.aspx.vb" Inherits="DocumentView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>文档在线浏览</title>
			<link href="../css/cmsstyle.css" type="text/css" rel="stylesheet"/>
			
			<style type="text/css">
<!--
.bt {
	font-size: 12px;
	color: #000000;
	font-weight: bold;
	line-height: 31px;
}
.tq {
	font-size: 12px;
	color: #354f77;
}
.box {
	font-family: Arial, Helvetica, sans-serif;
	color: #999999;
	text-decoration: none;
	height: 22px;
	border: 1px solid #b9c8e7;
	width: 140px;
}
-->
</style>
<script  type="text/javascript">
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
		<table width="800" height="94" border="0" cellpadding="0" cellspacing="1" bgcolor="#d7d7d7">
 
  <tr>
    <td height="63" align="center" bgcolor="#FFFFFF">
   
		<table cellpadding="0" cellspacing="0" border="0" style="padding-left:10px;" width="600">
		    <tr>
		        <td>
		            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="../Control/swflash.cab#version=7,0,19,0" width="800" id="obj1">
    <param name="movie" value='<%=FlashFile %>' />
    <param name="quality" value="high" />
    <param name="wmode" value="transparent" />
    <embed src="flash/tbpreview.swf" width="800" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" wmode="transparent"></embed>
  </object>
		        </td>
		        
  </tr>
</table>
		        </td>
		    </tr>
		</table>
			
		</form>
	</body>
</html>

<script language="javascript">
    document.getElementById("obj1").style.height =document.documentElement.offsetHeight-10;
</script>