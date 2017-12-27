<%@ Import Namespace="NetReusables"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CmsFrmBridge" CodeFile="CmsFrmBridge.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CmsFrmBridge</title>
		<meta http-equiv="Pragma" content="no-cache">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
	</HEAD>
	<body>
		<SCRIPT LANGUAGE="JavaScript">
<!--
//本页面仅为了向内容管理主页面传递页面Size
var RecId ="<%=AspPage.RStr("RecId", Request)%>";
if(RecId!="")
self.location="/cmsweb/cmshost/RecordEdit.aspx?mnuinmode=12&MenuSection=&MenuKey=&MNURESLOCATE=1&cmsaction=&mnuformresid=" + getUrlParam("noderesid") + "&mnuhostresid=" + getUrlParam("noderesid") + "&mnuhostrecid=" + RecId + "&mnuresid=" + getUrlParam("noderesid") + "&mnuformname=&mnurecid=" + RecId + "&content_width=" + self.document.body.clientWidth + "&content_height=" + self.document.body.clientHeight + "&backpage=/cmsweb/cmshost/CmsFrmContent.aspx";
else
{

self.location="/cmsweb/cmshost/CmsFrmContent.aspx?noderesid=" + getUrlParam("noderesid") + "&type="+getUrlParam("type")+"&schdocid=" + getUrlParam("schdocid") + "&depid=" + getUrlParam("depid") + "&content_width=" + self.document.body.clientWidth + "&content_height=" + self.document.body.clientHeight + "&timeid=" + Math.round(Math.random() * 10000000000);

}
//-->
		</SCRIPT>
		
	</body>
</HTML>
