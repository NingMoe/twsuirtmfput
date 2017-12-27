<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CmsFrmBody" CodeFile="CmsFrmBody.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD><TITLE>Unionsoft 企业管理应用平台</TITLE>
	<meta http-equiv="Pragma" content="no-cache" />
  </HEAD>
	<frameset cols="200,*" frameborder="yes">
		<frame src="CmsFrmLeftTree.aspx?noderesid=<%=AspPage.RStr("noderesid", Request)%>&depid=<%=AspPage.RStr("depid", Request)%>&timeid=<%=TimeId.CurrentMilliseconds(1)%>" name="tree" frameborder="no">
		<frame src="CmsFrmBridge.aspx?noderesid=<%=AspPage.RStr("noderesid", Request)%>&schdocid=<%=AspPage.RStr("schdocid", Request)%>&depid=<%=AspPage.RStr("depid", Request)%>&timeid=<%=TimeId.CurrentMilliseconds(1)%>&type=<%=AspPage.RStr("type", Request)%>&RecId=<%=AspPage.RStr("RecId", Request)%>" name="content" frameborder="no">
		
		
		 
	</frameset>
</HTML>
