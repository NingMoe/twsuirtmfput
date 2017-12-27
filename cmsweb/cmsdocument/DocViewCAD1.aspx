<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocViewCAD1.aspx.vb" Inherits="cmsdocument_DocViewCAD1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<script language="javascript">
function DetectActiveX()
{
    try
    {
       var comActiveX= new ActiveXObject("MXDRAWX.MxDrawXCtrl.1");  
      
       
    }
    catch(e)
    { 
       return false;   
    }
     
    return true;
 }
DetectActiveX();
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <object classid="clsid:6EEC44E0-338B-408A-983E-B43E6F22B929" id="MxDrawXCtrl"  codebase="/cmsweb/control/MxDrawX_CAB.CAB#version=1,0,0,1" width=100% height=90%> 
  			<param name="_Version" value="65536">
  			<param name="_ExtentX" value="24262">                                                
  			<param name="_ExtentY" value="16219">
  			<param name="_StockProps" value="0">
			<param name="DwgFilePath" value="<%=cmsurldoc %>">
			<param name="IsRuningAtIE" value="1">
			<param name="EnablePrintCmd" value="1">

			<param name="ShowStatusBar" value="0">
			<param name="ShowToolBars"  value="0">
			<param name="EnableIntelliSelect"  value="0">
			<param name="ShowCommandWindow" value="0">
			<param name="ShowModelBar" value="0">
			<param name="IniFilePath" value="AutoActive=N,ResPath=ViewRes,EnableGripPoint=N,USEARROWCURSOR=N">

           <param name="ToolBarFiles" value="MxDraw-ToolBar.mxt">

  </object>
    </div>
    </form>
</body>
</html>



