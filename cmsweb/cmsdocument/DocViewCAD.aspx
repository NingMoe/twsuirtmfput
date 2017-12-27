<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocViewCAD.aspx.vb" Inherits="cmsdocument_DocViewCAD" %>
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
//DetectActiveX();
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
      <OBJECT id=DWGViewX classid="clsid:AC53EFE4-94A7-47E6-BBFC-E9B9CF322299" codebase="/cmsweb/control/dwgviewx.cab" width="700" height="520">
       <param name="_Version" value="65536">
       <param name="_ExtentX" value="18521">
       <param name="_ExtentY" value="13758">
       <param name="_StockProps" value="0">
       <param name="DrawingFile" value="<%=cmsurldoc %>">
       <param name="ShowToobar" value="-1">
       <param name="ShowLayoutBar" value="1">  
      </OBJECT>
    </div>
    </form>
</body>
</html>