<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewDwg.aspx.vb" Inherits="cmsdocument_ViewDwg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <OBJECT   id=DwgViewX  name=DwgViewX   codebase="/cmsweb/control/dwgviewx.CAB"   width="100%"   height="100%"     
              classid=clsid:AC53EFE4-94A7-47E6-BBFC-E9B9CF322299 VIEWASTEXT width="650" height="400">
              <PARAM name="_Version" value="65536">
              <PARAM name="_ExtentX" value="17198">
              <PARAM name="_ExtentY" value="10266">
              <PARAM name="_StockProps" value="0">  
              <param name="DrawingFile" value="<%=cmsurldoc %>">  
          </OBJECT>
    </div>
    </form>
</body>
</html>
